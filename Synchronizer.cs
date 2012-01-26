using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

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
                    DoSync(syncStream);
                    syncStream.Close();
                }
            }
        }

        private static void DoSync(NetworkStream syncStream)
        {
            byte[] inputBuffer = new byte[150];

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
            aesProvider.Padding = PaddingMode.Zeros;
            aesProvider.Key = key;

            //read the IV from the network link and create the encryptor and decryptor with it (needs work)
            int readSize = syncStream.Read(inputBuffer, 0, 150);
            byte[] cipherIV = new byte[16];
            Array.ConstrainedCopy(inputBuffer, 0, cipherIV, 0, 16);
            aesProvider.IV = cipherIV;
            ICryptoTransform decryptorTransform = aesProvider.CreateDecryptor();
            ICryptoTransform encryptorTransform = aesProvider.CreateEncryptor();

            //recieve and decrypt the first data block with the device time
            byte[] decryptedData = decryptorTransform.TransformFinalBlock(inputBuffer, 21, readSize - 21);
            string securityString = Encoding.ASCII.GetString(decryptedData, 0, readSize - 21);


            DateTime securityTimestamp;
            if (!DateTime.TryParse(securityString, out securityTimestamp))
                return;

            TimeSpan timeGap = DateTime.Now - securityTimestamp;
            if (timeGap.TotalSeconds > 30)
                return;
        }
    }
}
