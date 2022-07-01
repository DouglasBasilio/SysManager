using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public static List<string> ToErrorList(this IList<ValidationFailure> list)
        {
            var _result = new List<string>();
            foreach (var item in list)
                _result.Add(item.ErrorMessage);
            return _result;
        }

        public static T GetAttribute<T>(this Enum valorEnum) where T : System.Attribute
        {
            var type = valorEnum.GetType();
            var memInfo = type.GetMember(valorEnum.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static string Description(this Enum valorEnum)
        {
            return valorEnum.GetAttribute<DescriptionAttribute>().Description;
        }
    }
}
