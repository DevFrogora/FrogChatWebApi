namespace ClientStorage
{
    public interface IClientStorage
    {
        Task<string> GetItemAsStringAsync(string key);
        Task SetItemAsStringAsync(string key, string value);
        Task<T> GetValue<T>(string key);
        Task RemoveItemAsync(string key);
    }
}