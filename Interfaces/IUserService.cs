using MiddlewareTurtorial.Models;

namespace MiddlewareTurtorial.Interfaces
{
    public interface IUserService
    {
        bool Exists(Login login);
        Dictionary<string, string> GetCookies(Login login);
    }
}
