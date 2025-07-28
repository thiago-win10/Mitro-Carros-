namespace BusinessInfo.Application.Common.Interfaces
{
    public interface IRedisCaching
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan expiration);

    }
}
