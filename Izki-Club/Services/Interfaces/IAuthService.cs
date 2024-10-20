namespace Izki_Club.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string username);
    }
}
