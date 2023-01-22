using Blazored.LocalStorage;

namespace ClientStorage
{
    public class BlazorLocalStorage : IClientStorage
    {
        private readonly ILocalStorageService localStorageService;

        public BlazorLocalStorage(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }
        public async Task<T> GetValue<T>(string key)
        {
            //return await localStorageService.GetItemAsStringAsync(key);
            return await localStorageService.GetItemAsync<T>(key);
        }

        public async Task<string> GetItemAsStringAsync(string key)
        {
            return await localStorageService.GetItemAsStringAsync(key);
        }

        public async Task SetItemAsStringAsync(string key, string value)
        {
            await localStorageService.SetItemAsStringAsync(key, value);
        }

        public async Task RemoveItemAsync(string key)
        {
            await localStorageService.RemoveItemAsync(key);
        }
    }
}