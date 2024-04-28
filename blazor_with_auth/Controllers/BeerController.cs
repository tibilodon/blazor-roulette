using blazor_with_auth.Shared.Interfaces;
using blazor_with_auth.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace blazor_with_auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IBeerRepository _beerRepository;

        public BeerController(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }
        [HttpPost]
        public async Task<ActionResult> AddBeers(Beer beer)
        {
            var addBeer = await _beerRepository.AddBeer(beer);
            if (addBeer != null)
            {
                return Ok(beer);
            }
            return BadRequest("Could not add beer!");
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBeerById(int id)
        {
            var beer = await _beerRepository.GetBeerById(id);
            return Ok(beer);
        }
        [HttpGet]
        public async Task<ActionResult<Beer>> GetAllBeersForUnRegisteredAppUser()
        {
            var beers = await _beerRepository.GetAllBeersForUnRegisteredUser();
            return Ok(beers);
        }

        [HttpPut]
        public async Task<ActionResult<Beer>> EditBeer(Beer beer)
        {
            var updateBeer = await _beerRepository.UpdateBeer(beer);
            return Ok(updateBeer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Beer>> Randomized(int id)
        {
            var subtractFromAmount = await _beerRepository.SubtractFromAmount(id);
            return Ok(subtractFromAmount);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Beer>> DeleteBeer(int id)
        {
            var result = await _beerRepository.DeleteBeer(id);
            return Ok(result);
        }
    }
}
