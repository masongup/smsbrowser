using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;

namespace SMSBrowser
{
    
    
    static class MessageDatabase
    {
        [Serializable()]
        private class TextMessage
        {
            public string Text;
            public DateTime Time;
            public Boolean IsOutgoing;
            public int ConversationID;
            public string ContactName;
            public string PhoneNumber;
        }

        private class Contact
        {
            public string ContactName;
            public string PhoneNumber;
            public List<TextMessage> MessageList;
        }

        static List<TextMessage> MasterMessageList;
        static List<Contact> MasterContactList;

        public static void ReadFromTextFile(string fileName)
        {
            List<TextMessage> fileList = new List<TextMessage>();
            StreamReader fileStream = new StreamReader(fileName);
            string fileLine = fileStream.ReadLine();
            DateTime importTime = DateTime.MinValue;

            if (MasterMessageList != null)
                importTime = MasterMessageList[MasterMessageList.Count - 1].Time;

            while (!String.IsNullOrEmpty(fileLine))
            {
                TextMessage thisLineMessage = new TextMessage();
                string[] lineParts = fileLine.Split('\t');
                fileLine = fileStream.ReadLine();
                if (lineParts.Length != 6)
                    throw new Exception("I don't like the format of this file!");

                thisLineMessage.Time = DateTime.Parse(lineParts[0] + ' ' + lineParts[1]);
                if (thisLineMessage.Time < importTime)
                    continue;

                thisLineMessage.IsOutgoing = lineParts[2].Equals("out");
                thisLineMessage.PhoneNumber = lineParts[3];
                thisLineMessage.ContactName = lineParts[4];
                thisLineMessage.Text = lineParts[5];

                fileList.Add(thisLineMessage);
                fileLine = fileStream.ReadLine();
            }

            fileStream.Close();

            if (MasterMessageList == null)
            {
                MasterMessageList = fileList;
                return;
            }

            //code to combine the current message list with the new one
            MasterMessageList.AddRange(fileList);
            MasterMessageList.Sort(delegate(TextMessage a, TextMessage b)
            {
                return DateTime.Compare(a.Time, b.Time);
            });

            List<TextMessage> messagesToDelete = new List<TextMessage>();

            for (int currentMessage = 1; currentMessage < MasterMessageList.Count; currentMessage++)
            {
                if (MasterMessageList[currentMessage].Time == MasterMessageList[currentMessage - 1].Time &&
                    MasterMessageList[currentMessage].PhoneNumber == MasterMessageList[currentMessage - 1].PhoneNumber &&
                    MasterMessageList[currentMessage - 1].Text.Contains(MasterMessageList[currentMessage].Text))
                    messagesToDelete.Add(MasterMessageList[currentMessage]);
            }

            foreach (TextMessage messageToDelete in messagesToDelete)
                MasterMessageList.Remove(messageToDelete);
        }

        public static void PopulateContactsList(DataGridViewRowCollection windowList)
        {
            MasterContactList = new List<Contact>();
            
            foreach (TextMessage thisMessage in MasterMessageList)
            {
                Contact contactForThisMessage = MasterContactList.Find(delegate(Contact c) { return c.ContactName.Equals(thisMessage.ContactName); });
                if (contactForThisMessage == null)
                {
                    contactForThisMessage = new Contact();
                    contactForThisMessage.ContactName = thisMessage.ContactName;
                    contactForThisMessage.PhoneNumber = thisMessage.PhoneNumber;
                    contactForThisMessage.MessageList = new List<TextMessage>();
                    MasterContactList.Add(contactForThisMessage);
                }
                contactForThisMessage.MessageList.Add(thisMessage);
            }

            MasterContactList.Sort(delegate(Contact a, Contact b)
            {
                return DateTime.Compare(b.MessageList[b.MessageList.Count - 1].Time,
                    a.MessageList[a.MessageList.Count - 1].Time);
            });

            windowList.Clear();

            foreach (Contact thisContact in MasterContactList)
            {
                int rowNumber = windowList.Add();
                windowList[rowNumber].Tag = thisContact;
                windowList[rowNumber].Cells[0].Value = thisContact.ContactName;
                windowList[rowNumber].Cells[1].Value = thisContact.MessageList.Count; //.ToString();
                windowList[rowNumber].Cells[2].Value = thisContact.MessageList[thisContact.MessageList.Count - 1].Time; //.ToShortDateString();
            }
            
            FindConversationsAndMultiMessages();
        }

        public static void PopulateMessageList(object contactObject, DataGridViewRowCollection targetGridRows)
        {
            if (contactObject == null || contactObject.GetType() != typeof(Contact))
                return;

            Contact selectedContact = (Contact)contactObject;
            DataGridViewCellStyle incomingCell = null;
            DataGridViewCellStyle outgoingCell = null;

            targetGridRows.Clear();
            int currentConversation = -1;

            foreach (TextMessage thisMessage in selectedContact.MessageList)
            {
                if (currentConversation != thisMessage.ConversationID)
                {
                    currentConversation = thisMessage.ConversationID;
                    int seperatorRowNumber = targetGridRows.Add();
                    targetGridRows[seperatorRowNumber].Cells[1].Value = thisMessage.Time.ToLongDateString();
                }

                if (incomingCell == null)
                {
                    incomingCell = new DataGridViewCellStyle(targetGridRows[0].DefaultCellStyle);
                    outgoingCell = new DataGridViewCellStyle(targetGridRows[0].DefaultCellStyle);
                    outgoingCell.Alignment = DataGridViewContentAlignment.MiddleRight;
                    incomingCell.BackColor = Color.GreenYellow;
                    outgoingCell.BackColor = Color.Khaki;
                }

                int rowNumber = targetGridRows.Add();
                targetGridRows[rowNumber].Cells[0].Value = thisMessage.Time.ToShortTimeString();
                targetGridRows[rowNumber].Cells[1].Value = thisMessage.Text;

                if (thisMessage.IsOutgoing)
                    targetGridRows[rowNumber].Cells[1].Style = outgoingCell;
                else
                    targetGridRows[rowNumber].Cells[1].Style = incomingCell;
            }
        }

        private static void FindConversationsAndMultiMessages()
        {
            foreach (Contact thisContact in MasterContactList)
            {
                int conversationNumber = 0;
                thisContact.MessageList[0].ConversationID = conversationNumber;
                List<TextMessage> messagesToDelete = new List<TextMessage>();

                for (int currentMessage = 1; currentMessage < thisContact.MessageList.Count; currentMessage++)
                {
                    TimeSpan timeSincePrevMessage = thisContact.MessageList[currentMessage].Time - thisContact.MessageList[currentMessage - 1].Time;

                    if (timeSincePrevMessage.TotalHours > 4.0)
                        conversationNumber++;
                    else if ((timeSincePrevMessage.TotalMinutes <= 2) && 
                        (thisContact.MessageList[currentMessage].IsOutgoing == thisContact.MessageList[currentMessage - 1].IsOutgoing) &&
                        (thisContact.MessageList[currentMessage - 1].Text.Length > 120))
                    {
                        thisContact.MessageList[currentMessage - 1].Text += thisContact.MessageList[currentMessage].Text;
                        messagesToDelete.Add(thisContact.MessageList[currentMessage]);
                    }

                    thisContact.MessageList[currentMessage].ConversationID = conversationNumber;
                }

                foreach (TextMessage messageToDelete in messagesToDelete)
                {
                    thisContact.MessageList.Remove(messageToDelete);
                    MasterMessageList.Remove(messageToDelete);
                }
            }
        }

        public static void SaveData()
        {
            Stream writeStream = new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TextData.bin"), FileMode.Create);
            DeflateStream compressStream = new DeflateStream(writeStream, CompressionMode.Compress);
            BinaryFormatter writeFormatter = new BinaryFormatter();
            writeFormatter.Serialize(compressStream, MasterMessageList);
            compressStream.Close();
            writeStream.Close();
        }

        public static Boolean ReadData()
        {
            if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TextData.bin")))
                return false;

            Stream readStream = new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TextData.bin"), FileMode.Open);
            DeflateStream decompressStream = new DeflateStream(readStream, CompressionMode.Decompress);
            BinaryFormatter readFormatter = new BinaryFormatter();
            MasterMessageList = (List<TextMessage>)readFormatter.Deserialize(decompressStream);
            decompressStream.Close();
            readStream.Close();
            return true;
        }
    }
}
