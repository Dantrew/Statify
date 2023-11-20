using StatifyModels;

namespace StatifyServices.Interfaces
{
    public interface ITokenService
    {
        public Task<Token> GetToken();
    }
}
