using StatifyAPI.Models;

namespace StatifyAPI.Interfacse
{
    public interface ITokenService
    {
        public Task<Token> GetToken();
    }
}
