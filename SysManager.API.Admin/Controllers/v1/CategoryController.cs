using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysManager.Application.Contracts.Category.Request;
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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService service)
        {
            this._categoryService = service;
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Post([FromBody] CategoryPostRequest request)
        {
            var response = await _categoryService.PostAsync(request);
            return Utils.Convert(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put([FromBody] CategoryPutRequest request)
        {
            var response = await _categoryService.PutAsync(request);
            return Utils.Convert(response);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return Utils.Convert(response);
        }

        [HttpGet("getbyfilter")]
        public async Task<IActionResult> GetByFilter([FromQuery] CategoryGetFilterRequest request)
        {
            var response = await _categoryService.GetByFilterAsync(request);
            return Utils.Convert(response);
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var response = await _categoryService.DeleteByIdAsync(id);
            return Utils.Convert(response);
        }
    }
}
