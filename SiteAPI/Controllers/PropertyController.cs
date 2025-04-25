using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteAPI.Applications.DTO;
using SiteAPI.Applications.Interfaces;
using SiteAPI.Domain.Models;

namespace SiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        public readonly IPropertyServices _propertyServices;
        public PropertyController(IPropertyServices propertyServices)
        {
            _propertyServices = propertyServices;
        }

        [HttpPost("create-property")]
        public async Task<IActionResult> createPropertyAsync([FromBody] CreatePropertyDTO properties)
        {
            var property = await _propertyServices.createPropertyAsync(properties);
            return Ok(property);
        }

        [HttpGet("get-property")]
        public async Task<IActionResult> getAllPropertyAsync()
        {
            var property = await _propertyServices.getAllPropertiesAsync();
            return Ok(property);
        }

    }
}
