using blazor_with_auth.Shared.Models;

namespace blazor_with_auth.Shared.Interfaces
{
    public interface IRandomBeerService
    {
        //  take in a list of beers (available to the user) and return a single Beer object
        Task<Beer> SelectRandom(List<Beer> beers);
    }
}
