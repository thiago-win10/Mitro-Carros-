namespace MitroVehicle.Application.Common.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string email, string role);
    }
}
