using SysManager.Application.Contracts.Unity.Request;
using SysManager.Application.Data.MySql.Entities;
using SysManager.Application.Data.MySql.Repositories;
using SysManager.Application.Errors;
using SysManager.Application.Helpers;
using SysManager.Application.Validators.Unity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SysManager.Application.Services
{
    public class UnityService 
    {
        private readonly UnityRepository _unityRepository;

        public UnityService(UnityRepository unityRepository)
        {
            this._unityRepository = unityRepository;
        }

        public async Task<ResultData> PostAsync(UnityPostRequest request)
        {
            var validator = new UnityPostRequestValidator(_unityRepository);
            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
                return Utils.ErrorData(validatorResult.Errors.ToErrorList());
            var entity = new UnityEntity(request);
            var response = await _unityRepository.CreateAsync(entity);
            return Utils.SuccessData(response);
        }

        public async Task<ResultData> PutAsync(UnityPutRequest request)
        {
            var validator = new UnityPutRequestValidator(_unityRepository);
            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
                return Utils.ErrorData(validatorResult.Errors.ToErrorList());

            var entity = new UnityEntity(request);
            var response = await _unityRepository.UpdateAsync(entity);
            return Utils.SuccessData(response);
        }

        public async Task<ResultData> GetByIdAsync(Guid id)
        {
            var response = await _unityRepository.GetByIdAsync(id);

            if (response != null)
                return Utils.SuccessData(response);

            return Utils.ErrorData(SysManagerErrors.Unity_Get_BadRequest_Id_Is_Invalid_Or_Inexistent.Description());
        }

        public async Task<ResultData> DeleteByIdAsync(Guid id)
        {
            var response = await _unityRepository.GetByIdAsync(id);
            if (response != null)
            {
                var result = await _unityRepository.DeleteByIdAsync(id);
                return Utils.SuccessData(result);
            }
            return Utils.ErrorData(SysManagerErrors.Unity_Delete_BadRequest_Id_Is_Invalid_Or_Inexistent.Description());
        }
    }
}
