using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SysManager.Application.Helpers
{
    public static class Utils
    {
        public static ResultData<T> SuccessData<T>(T _data)
        {
            var result = new ResultData<T>(_data);
            result.Success = true;
            return result;
        }

        public static ResultData<T> ErrorData<T>(T _data)
        {
            var result = new ResultData<T>(_data);
            result.Success = false;
            return result;
        }

        public static IActionResult Convert(ResultData _resultData)
        {
            if (_resultData.Success)
                return new ObjectResult(_resultData) { StatusCode = (int)HttpStatusCode.OK };

            return new BadRequestObjectResult(_resultData);
        }
    }
}
