using SysManager.Application.Contracts.ProductsType.Request;
using SysManager.Application.Data.MySql.Entities;
using SysManager.Application.Data.MySql.Repositories;
using SysManager.Application.Errors;
using SysManager.Application.Helpers;
using SysManager.Application.Validators.ProductType;
using System;
using System.Threading.Tasks;

namespace SysManager.Application.Services
{
    public class ProductTypeService
    {
        private readonly ProductTypeRepository _productTypeRepository;

        public ProductTypeService(ProductTypeRepository productTypeRepository)
        {
            this._productTypeRepository = productTypeRepository;
        }

        public async Task<ResultData> PostAsync(ProductTypePostRequest request)
        {
            var validator = new ProductTypePostRequestValidator(_productTypeRepository);
            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
                return Utils.ErrorData(validatorResult.Errors.ToErrorList());
            var entity = new ProductTypeEntity(request);
            var response = await _productTypeRepository.CreateAsync(entity);
            return Utils.SuccessData(response);
        }

        public async Task<ResultData> PutAsync(ProductTypePutRequest request)
        {
            var validator = new ProductTypePutRequestValidator(_productTypeRepository);
            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
                return Utils.ErrorData(validatorResult.Errors.ToErrorList());

            var entity = new ProductTypeEntity(request);
            var response = await _productTypeRepository.UpdateAsync(entity);
            return Utils.SuccessData(response);
        }

        public async Task<ResultData> GetByIdAsync(Guid id)
        {
            var response = await _productTypeRepository.GetByIdAsync(id);

            if (response != null)
                return Utils.SuccessData(response);

            return Utils.ErrorData(SysManagerErrors.ProductType_Get_BadRequest_Id_Is_Invalid_Or_Inexistent.Description());
        }

        public async Task<ResultData> GetByFilterAsync(ProductTypeGetFilterRequest request)
        {
            var result = await _productTypeRepository.GetByFilterAsync(request);
            return Utils.SuccessData(result);
        }

        public async Task<ResultData> DeleteByIdAsync(Guid id)
        {
            var response = await _productTypeRepository.GetByIdAsync(id);
            if (response != null)
            {
                var result = await _productTypeRepository.DeleteByIdAsync(id);
                return Utils.SuccessData(result);
            }
            return Utils.ErrorData(SysManagerErrors.ProductType_Delete_BadRequest_Id_Is_Invalid_Or_Inexistent.Description());
        }
    }
}
