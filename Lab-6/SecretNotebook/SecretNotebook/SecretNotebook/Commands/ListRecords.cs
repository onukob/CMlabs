namespace SecretNotebook
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using SecretNotebook.EncryptDecrypt;
    using SecretNotebook.Exceptions;

    public class ListRecords
    {
        private static List<string> splittedText;

        public ListRecords(string text, string hash)
        {
            splittedText = text.Split('|').ToList();

            splittedText = splittedText.Select(item => DecryptData.Decrypt(item, hash)).ToList();
        }

        public void List()
        {
            Console.WriteLine("\nListing records...\n");

            int count = 0;

            Console.ForegroundColor = ConsoleColor.Yellow;

            for (int i = 2; i < splittedText.Count; i += 4)
            {
                if (i + 2 < splittedText.Count)
                {
                    count++;

                    string recordName = splittedText[i];
                    string recordText = splittedText[i + 1];
                    string recordDate = splittedText[i + 2];

                    string recordData = $"{count}. {recordName}\n" +
                                       $"   {recordText}\n" +
                                       $"   {recordDate}\n";

                    if (!string.IsNullOrEmpty(recordName) &&
                        !string.IsNullOrEmpty(recordText) &&
                        !string.IsNullOrEmpty(recordDate))
                    {
                        Console.WriteLine(recordData);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\nListing completed :)");
        }
    }
}
