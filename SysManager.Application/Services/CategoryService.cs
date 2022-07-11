using SysManager.Application.Contracts.Category.Request;
using SysManager.Application.Data.MySql.Entities;
using SysManager.Application.Data.MySql.Repositories;
using SysManager.Application.Errors;
using SysManager.Application.Helpers;
using SysManager.Application.Validators.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SysManager.Application.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<ResultData> PostAsync(CategoryPostRequest request)
        {
            var validator = new CategoryPostRequestValidator(_categoryRepository);
            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
                return Utils.ErrorData(validatorResult.Errors.ToErrorList());
            var entity = new CategoryEntity(request);
            var response = await _categoryRepository.CreateAsync(entity);
            return Utils.SuccessData(response);
        }

        public async Task<ResultData> PutAsync(CategoryPutRequest request)
        {
            var validator = new CategoryPutRequestValidator(_categoryRepository);
            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
                return Utils.ErrorData(validatorResult.Errors.ToErrorList());

            var entity = new CategoryEntity(request);
            var response = await _categoryRepository.UpdateAsync(entity);
            return Utils.SuccessData(response);
        }

        public async Task<ResultData> GetByIdAsync(Guid id)
        {
            var response = await _categoryRepository.GetByIdAsync(id);

            if (response != null)
                return Utils.SuccessData(response);

            return Utils.ErrorData(SysManagerErrors.Category_Get_BadRequest_Id_Is_Invalid_Or_Inexistent.Description());
        }

        public async Task<ResultData> GetByFilterAsync(CategoryGetFilterRequest request)
        {
            var result = await _categoryRepository.GetByFilterAsync(request);
            return Utils.SuccessData(result);
        }

        public async Task<ResultData> DeleteByIdAsync(Guid id)
        {
            var response = await _categoryRepository.GetByIdAsync(id);
            if (response != null)
            {
                var result = await _categoryRepository.DeleteByIdAsync(id);
                return Utils.SuccessData(result);
            }
            return Utils.ErrorData(SysManagerErrors.Category_Delete_BadRequest_Id_Is_Invalid_Or_Inexistent.Description());
        }
    }
}
