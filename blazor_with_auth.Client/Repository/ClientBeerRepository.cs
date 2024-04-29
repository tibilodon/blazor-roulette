using blazor_with_auth.Shared.Interfaces;
using blazor_with_auth.Shared.Models;
using System.Net.Http.Json;

namespace blazor_with_auth.Client.Repository
{
    public class ClientBeerRepository : IBeerRepository
    {
        private readonly HttpClient _httpClient;

        public ClientBeerRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Beer> AddBeer(Beer beer)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/beer", beer);
            return await result.Content.ReadFromJsonAsync<Beer>();
        }

        public async Task<bool> DeleteBeer(int id)
        {
            var result = await _httpClient.DeleteAsync($"/api/beer/{id}");
            return await result.Content.ReadFromJsonAsync<bool>();
        }

        public Task<List<Beer>> GetAllBeers()
        {
            throw new NotImplementedException();
        }

        public Task<List<Beer>> GetAllBeersForRegisteredAppUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Beer>> GetAllBeersForUnRegisteredUser()
        {
            throw new NotImplementedException();
        }

        public async Task<Beer> GetBeerById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Beer>($"/api/beer/{id}");
            return result;
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public async Task<Beer> SubtractFromAmount(int id)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/beer/{id}", id);
            return await result.Content.ReadFromJsonAsync<Beer>();
        }

        public async Task<Beer> UpdateBeer(Beer beer)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/beer", beer);
            return await result.Content.ReadFromJsonAsync<Beer>();
        }


    }
}
