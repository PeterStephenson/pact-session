using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Provider.Data;

namespace Provider.Controllers.GetDvd
{
    [ApiController]
    public class GetDvdByIdController : ControllerBase
    {
        private readonly IDvdRepository _dvdRepository;

        public GetDvdByIdController(IDvdRepository dvdRepository)
        {
            _dvdRepository = dvdRepository;
        }

        [HttpGet("/dvds/{id}")]
        public async Task<IActionResult> GetDvd(Guid id)
        {
            try
            {
                return Ok(await _dvdRepository.GetById(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}