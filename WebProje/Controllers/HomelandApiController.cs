using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProje.Data;

namespace WebProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomelandApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomelandApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _context.Homeland.ToList();

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
