namespace BusinessInfo.Application.Common.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string email, string role);
    }
}
