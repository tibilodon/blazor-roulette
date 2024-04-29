using blazor_with_auth.Data;
using blazor_with_auth.Shared.Interfaces;
using blazor_with_auth.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace blazor_with_auth.Repository
{
    public class BeerRepository : IBeerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICookieService _cookie;

        public BeerRepository(ApplicationDbContext context, ICookieService cookie)
        {
            _context = context;
            _cookie = cookie;
        }
        public async Task<Beer> AddBeer(Beer beer)
        {
            //  use jsInterop to get (unregistered) user from cookies
            var curUser = await _cookie.GetUnRegisteredAppUser();
            //  create new instance of the object
            if (curUser != null)
            {
                beer.UnRegisteredAppUserId = curUser.Id;
                _context.Beers.Add(beer);
                await Save();

                return beer;
            }
            //  registered user
            else if (beer.AppUserId != null)
            {
                _context.Beers.Add(beer);
                await Save();
                return beer;
            }
            //  in case of an error
            return null;

        }

        public async Task<bool> DeleteBeer(int id)
        {
            //var result = await GetBeerByIdForUnRegisteredAppUser(id);
            var result = await _context.Beers.FindAsync(id);
            if (result != null)
            {
                _context.Remove(result);
                return await Save();
            }
            return false;
        }

        public async Task<List<Beer>> GetAllBeers()
        {
            return await _context.Beers.ToListAsync();
        }

        public async Task<List<Beer>> GetAllBeersForRegisteredAppUser(string userId)
        {
            var result = _context.Beers.Where(b => b.AppUserId == userId).ToList();
            return result;
        }

        public async Task<List<Beer>> GetAllBeersForUnRegisteredUser()
        {
            var unregisteredUser = await _cookie.GetUnRegisteredAppUser();
            if (unregisteredUser != null)
            {
                var result = _context.Beers.Where(b => b.UnRegisteredAppUserId == unregisteredUser.Id).ToList();
                return result;
            }
            return null;
        }

        public async Task<Beer> GetBeerById(int id)
        {
            return await _context.Beers.FindAsync(id);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            //  return boolean instead of int
            return saved > 0 ? true : false;
        }

        public async Task<Beer> SubtractFromAmount(int id)
        {
            var result = await GetBeerById(id);
            var curAmount = result.Amount;
            if (curAmount > 0)
            {
                result.Amount--;
                await Save();
            }
            return result;
        }

        public async Task<Beer> UpdateBeer(Beer beer)
        {
            var editBeer = await GetBeerById(beer.Id);
            if (editBeer != null)
            {
                editBeer.Name = beer.Name;
                editBeer.Amount = beer.Amount;
                await Save();
                return editBeer;
            }
            throw new Exception("Beer not found");
        }
    }
}
