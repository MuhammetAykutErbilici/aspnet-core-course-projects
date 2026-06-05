using System.Data.Common;

namespace MeetingApp.Models
{
    public static class Repository
    {
        private static List<UserInfo> _users = new();

        static Repository()
        {
            _users.Add(new UserInfo() {Id = 1, Name = "Ali", Phone = "1234567890", Email = "abc@example.com" , WillAttend = true });
            _users.Add(new UserInfo() {Id = 2, Name = "Ahmet", Phone = "145678987", Email = "abcd@example.com" , WillAttend = false });
            _users.Add(new UserInfo() {Id = 3, Name = "Canan", Phone = "0987654345", Email = "abcde@example.com" , WillAttend = true });
            
        }

        public static  List<UserInfo> Users
        {
            get { return _users; }
        }
        public static void CreateUser(UserInfo user)
        {
            user.Id = Users.Count + 1;
            _users.Add(user);
        }
         
        public static UserInfo? GetUserById(int id)
        {
            return _users.FirstOrDefault(user => user.Id == id);
        }

    }
}