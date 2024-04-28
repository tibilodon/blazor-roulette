using blazor_with_auth.Shared.Models;

namespace blazor_with_auth.Shared.Interfaces
{
    public interface IBeerRepository
    {
        Task<Beer> AddBeer(Beer beer);
        //  for further features
        Task<List<Beer>> GetAllBeers();
        //  un-registered user specific
        Task<List<Beer>> GetAllBeersForUnRegisteredUser();
        Task<Beer> GetBeerById(int id);
        //  TODO: add identity user specific
        Task<Beer> UpdateBeer(Beer beer);
        //  subtract one from amount
        Task<Beer> SubtractFromAmount(int id);

        Task<bool> DeleteBeer(int id);
        Task<bool> Save();
    }
}
