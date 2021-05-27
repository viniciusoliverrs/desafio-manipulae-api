using System.Threading.Tasks;
using DesafioManipulae.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioManipulae.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        public readonly IDesafioManipulaeRepository _context;
        public VideoController(IDesafioManipulaeRepository context)
        {
            _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get() {
            return Ok("Ok");
        }
    }
}