using blazor_with_auth.Shared.Interfaces;
using blazor_with_auth.Shared.Models;

namespace blazor_with_auth.Shared.Services
{
    public class RandomBeerService : IRandomBeerService
    {
        public Task<Beer> SelectRandom(List<Beer> beers)
        {
            //null and list check
            if (beers == null || beers.Count == 0)
            {

                throw new ArgumentException("The beer list cannot be empty.");
            }

            //  create new empty Beer list
            var newList = new List<Beer>();
            //  iterate through list from argument
            foreach (var beer in beers)
            {
                //  only include beers with positive amount
                if (beer.Amount > 0)
                {
                    newList.Add(beer);

                }
            }
            //  initiate Random class
            Random random = new Random();
            //  generates a random index from 0 to Count - 1
            if (newList.Count > 0)
            {
                int index = random.Next(newList.Count);
                //  pick an item
                Beer selectedBeer = newList[index];
                return Task.FromResult(selectedBeer);
            }

            return null;

        }
    }
}
