using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysManager.Application.Contracts.ProductsType.Request;
using SysManager.Application.Helpers;
using SysManager.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysManager.API.Admin.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly ProductTypeService _productTypeService;

        public ProductTypeController(ProductTypeService service)
        {
            this._productTypeService = service;
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Post([FromBody] ProductTypePostRequest request)
        {
            var response = await _productTypeService.PostAsync(request);
            return Utils.Convert(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put([FromBody] ProductTypePutRequest request)
        {
            var response = await _productTypeService.PutAsync(request);
            return Utils.Convert(response);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await _productTypeService.GetByIdAsync(id);
            return Utils.Convert(response);
        }

        [HttpGet("getbyfilter")]
        public async Task<IActionResult> GetByFilter([FromQuery] ProductTypeGetFilterRequest request)
        {
            var response = await _productTypeService.GetByFilterAsync(request);
            return Utils.Convert(response);
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var response = await _productTypeService.DeleteByIdAsync(id);
            return Utils.Convert(response);
        }
    }
}
