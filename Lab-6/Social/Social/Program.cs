namespace Social
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Social.Extensions;
    using Social.Models;

    internal class Program
    {
        private static string pathDirectory = string.Empty;
        private static string pathUsers = string.Empty;
        private static string pathFriends = string.Empty;
        private static string pathMessages = string.Empty;

        private static void Main(string[] args)
        {
            #if DEBUG
            pathDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Data");
            #else
            pathDirectory = Path.Combine(AppContext.BaseDirectory, "Data");
            #endif

            pathDirectory = Path.GetFullPath(pathDirectory);

            pathUsers = pathDirectory + @"\users.json";
            pathFriends = pathDirectory + @"\friends.json";
            pathMessages = pathDirectory + @"\messages.json";

            Console.WriteLine("Добрый день! Авторизуйтесь, пожалуйста: ");

            string name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
               Console.WriteLine("Имя должно содержать хоть один символ!");
               return;
            }

            var socialDataSource = new SocialDataSource(pathUsers, pathFriends, pathMessages);

            var userContext = socialDataSource.GetUserContext(name);

            var now = DateTime.Today;

            var user = userContext.User;

            int age = now.Year - user.DateOfBirth.Year;

            if (user.DateOfBirth > now.AddYears(-age))
            {
               age--;
            }

            static void PrintNews(List<UserInformation> friends, List<UserInformation> friendshipOffers, List<UserInformation> subcribers, List<News> news)
            {
               Console.WriteLine("\n\n\n\t\t\t\tНовости\n");

               if (news.Count == 0)
               {
                   Console.WriteLine("\t\t\tВ настоящее время новостей нет");
               }

               news.ForEach(message => Console.WriteLine(
                   "\t{0} оставил(а) новость, оценок {1}: [{2}] |" + " Содержимое: {3}\n", message.AuthorName, message.Likes.Count, message.Likes.ListToString(), message.Text));
            }

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("\n\tЗдравствуйте, {0}! На данный момент вам {1}.\n\n", user.Name, age);

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("\tДрузья: Всего {0} | ", userContext.Friends.Count);

            userContext.Friends.ForEach(friend => Console.Write(friend.Name + ", " + "id: " + friend.UserId + " | "));

            Console.WriteLine();

            Console.Write("\n\tДрузья, которые онлайн: Всего {0} | ", userContext.OnlineFriends.Count);

            userContext.OnlineFriends.ForEach(friend => Console.Write(friend.Name + ", " + "id: " + friend.UserId + " | "));

            Console.WriteLine();

            Console.Write("\n\tЗаявки в друзья: Всего {0} | ", userContext.FriendshipOffers.Count);

            userContext.FriendshipOffers.ForEach(friend => Console.Write(friend.Name + ", " + "id: " + friend.UserId + " | "));

            Console.WriteLine();

            Console.Write("\n\tПодписчики: Всего {0} | ", userContext.Subscribers.Count);

            userContext.Subscribers.ForEach(friend => Console.Write(friend.Name + ", " + "id: " + friend.UserId + " | "));

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;

            PrintNews(userContext.Friends, userContext.FriendshipOffers, userContext.Subscribers, userContext.News);

            Console.ResetColor();
        }
    }
}
