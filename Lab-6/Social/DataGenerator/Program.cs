namespace DataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Social.Models;

    public class Program
    {
        private static string pathDirectory = string.Empty;
        private static string pathUsers = string.Empty;
        private static string pathFriends = string.Empty;
        private static string pathMessages = string.Empty;

        private static List<User> users;
        private static List<Friend> friends;
        private static List<Message> messages;

        private static void Main(string[] args)
        {
            users = CreateUsersData.CreateData();

            friends = CreateFriendsData.CreateData(users);

            messages = CreateMessagesData.CreateData(users);

            pathDirectory = AppContext.BaseDirectory;

            pathUsers = pathDirectory + @"\users.json";
            pathFriends = pathDirectory + @"\friends.json";
            pathMessages = pathDirectory + @"\messages.json";

            // serializing data
            try
            {
                var dataSerializer = new DataSerialize(pathUsers, pathFriends, pathMessages, users, friends, messages);
                Console.WriteLine("Serializing completed successfully!");
            }
            catch (Exception)
            {
                Console.WriteLine("Serializing wasn't completed successfully");
            }
        }
    }
}
