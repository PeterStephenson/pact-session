using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Provider.Data;

namespace Provider.Controllers.CreateDvd
{
    [ApiController]
    public class DvdCollectionController : ControllerBase
    {
        private readonly IDvdRepository _dvdRepository;

        public DvdCollectionController(IDvdRepository dvdRepository)
        {
            _dvdRepository = dvdRepository;
        }

        [HttpPost("/dvds")]
        public async Task<IActionResult> CreateDvd([FromBody]CreateDvdRequest request)
        {
            try
            {
                var dvd = new DvdResponse(Guid.NewGuid(), request.Name, request.Director);
                await _dvdRepository.Create(dvd);
                return Created($"/dvds/{dvd.Id}", dvd);
            }
            catch (ConflictException)
            {
                return Conflict();
            }
        }
    }
}