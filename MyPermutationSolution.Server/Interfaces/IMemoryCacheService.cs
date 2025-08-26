namespace MyPermutationSolution.Server.Interfaces
{
    public interface IMemoryCacheService
    {
        T Get<T>(string cacheKey);
        void Set<T>(string cacheKey, T value, TimeSpan expiration);
        void Remove(string cacheKey);
        bool Exists(string cacheKey);
    }
}
