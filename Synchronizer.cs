using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Diagnostics;

namespace SMSBrowser
{
    class Synchronizer
    {
        private static Thread SyncThread;
        private static Boolean syncRunning;
        
        
        private Synchronizer()
        {
        }

        public static void StartSync()
        {
            syncRunning = true;
            SyncThread = new Thread(Sync);
            SyncThread.Start();
        }

        public static void EndSync()
        {
            syncRunning = false;
        }

        public static void Sync()
        {
            TcpListener syncListener = new TcpListener(IPAddress.Any, 1234);
            syncListener.Start();

            while (syncRunning)
            {
                if (!syncListener.Pending())
                {
                    Thread.Sleep(200);
                    continue;
                }

                TcpClient syncClient = syncListener.AcceptTcpClient();
                using (syncClient)
                {
                    NetworkStream syncStream = syncClient.GetStream();
                    syncStream.ReadTimeout = 1500;
                    DoSync(syncStream);
                    syncStream.Close();
                }
            }
        }

        private static void DoSync(NetworkStream syncStream)
        {
            byte[] inputBuffer = new byte[50];
            int readSize;
            try
            {
                //compute the key (need to get the password from somewhere better)
                byte[] salt = { (byte)0x3b, (byte)0x58, (byte)0x3a, (byte)0x8c, (byte)0x49, (byte)0xd3, (byte)0x21, (byte)0x88 };
                MD5 MD5Hasher = MD5.Create();
                byte[] basePassword = Encoding.ASCII.GetBytes("testingpassword");
                byte[] finalPassword = new byte[basePassword.Length + salt.Length];
                Array.ConstrainedCopy(basePassword, 0, finalPassword, 0, basePassword.Length);
                Array.ConstrainedCopy(salt, 0, finalPassword, basePassword.Length, salt.Length);
                byte[] key = MD5Hasher.ComputeHash(finalPassword);

                //create the algorithm definition (missing the IV until it is recieved from the other end)
                RijndaelManaged aesProvider = new RijndaelManaged();
                aesProvider.FeedbackSize = 8;
                aesProvider.Mode = CipherMode.CFB;
                aesProvider.Padding = PaddingMode.None;
                aesProvider.Key = key;

                //read the IV from the network link and create the encryptor and decryptor with it (needs work)
                byte[] cipherIV = new byte[16];
                readSize = syncStream.Read(cipherIV, 0, 16);
                if (readSize < 16)  //initially, I just throw out the whole transaction if the whole IV isn't recieved. Might want
                {                     //to do some sort of serial reciever, though.
                    Debug.WriteLine("Only recieved " + readSize + " bytes on the IV exchange");
                    return;
                }
                aesProvider.IV = cipherIV;
                ICryptoTransform decryptorTransform = aesProvider.CreateDecryptor();
                ICryptoTransform encryptorTransform = aesProvider.CreateEncryptor();

                //recieve and decrypt the first data block with the device time
                //nothing will be sent out or processed until the timestamp is verified, which proves that I am talking to
                //the SMSSync app and that it has the correct password
                readSize = syncStream.Read(inputBuffer, 0, 50);
                if (readSize < 18)
                {
                    Debug.WriteLine("Only recieved " + readSize + " bytes on timestamp exchange");
                    return;
                }
                byte[] decryptedData = decryptorTransform.TransformFinalBlock(inputBuffer, 0, readSize);
                string securityString = Encoding.ASCII.GetString(decryptedData, 0, readSize);

                DateTime securityTimestamp;
                if (!DateTime.TryParse(securityString, out securityTimestamp))
                    return;

                TimeSpan timeGap = DateTime.Now - securityTimestamp;
                if (Math.Abs(timeGap.TotalSeconds) > 120)    //gotta cut this time check down some before running on the net
                    return;

                //the sender has now been verified as the smssync app with the proper password and no interference, so it is time
                //to transmit the last sms timestamp back to the system.
                //get the timestamp, convert to string, encrypt it, and transmit it
                DateTime returnTimestamp = MessageDatabase.GetMostRecentMessageTime();
                byte[] returnTimeCleartext = Encoding.ASCII.GetBytes(returnTimestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffK"));
                byte[] returnTimeCipherText = encryptorTransform.TransformFinalBlock(returnTimeCleartext, 0, returnTimeCleartext.Length);
                syncStream.Write(returnTimeCipherText, 0, returnTimeCipherText.Length);

                //now we recieve and decode the texts
                byte[] smsDataCiphertext = new byte[5000];
                readSize = syncStream.Read(smsDataCiphertext, 0, 5000);
                byte[] smsDataClearBytes = decryptorTransform.TransformFinalBlock(smsDataCiphertext, 0, readSize);
                string smsDataClearString = Encoding.ASCII.GetString(smsDataClearBytes, 0, readSize);

                UInt16 messagesLoaded = MessageDatabase.ReadFromNetworkInput(smsDataClearString);

                //time to generate the return message
                byte[] loadedReturnMessage = BitConverter.GetBytes(messagesLoaded);
                byte[] returnMessageCipherBytes = encryptorTransform.TransformFinalBlock(loadedReturnMessage, 0, 2);
                syncStream.Write(returnMessageCipherBytes, 0, 2);
            }
            catch (Exception ex) 
            {
                if (ex is IOException || //caused by network timeouts
                    ex is ArgumentException //caused by invalid ascii points in decoded strings
                    )
                    return;
                else
                    throw;
            }
        }
    }
}
