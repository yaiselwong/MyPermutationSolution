namespace MyPermutationSolution.Client.Interfaces
{
    public interface ICustomHttpClient
    {
        Task<T> DeleteAsync<T>(string url);
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object requestObject);
    }
}
