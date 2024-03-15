using System.Threading.Tasks;

public interface IStorageService
{
    public Task SaveAsync(string key, object data);
    public Task<T> LoadAsync<T>(string key);
}
