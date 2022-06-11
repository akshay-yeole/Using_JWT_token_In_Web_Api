using Implement_JWT_token_In_Web_Api.DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Implement_JWT_token_In_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDataContext context;

        public ValuesController(ApplicationDataContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return Ok("Authorized User.............");
        }
    }
}
