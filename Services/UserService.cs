using MiddlewareTurtorial.Interfaces;
using MiddlewareTurtorial.Models;

namespace MiddlewareTurtorial.Services
{
    public class UserService : IUserService
    {
        public bool Exists(Login login)
        {
            List<Login> users = new List<Login>()
            {
                new Login{Username = "ABC", Password = "123"},
                new Login{Username = "XYZ", Password="987"}
            };
            bool exists = users.Select(user => user.Username.Equals(login.Username) && user.Password.Equals(login.Password)).FirstOrDefault();
            return exists;
        }

        public Dictionary<string, string> GetCookies(Login login)
        {
            Dictionary<string, string> cookies = new Dictionary<string, string>();
            cookies.Add("GuestName", login.Username);
            cookies.Add("SecretCode", login.Password);
            return cookies;

        }
    }
}
