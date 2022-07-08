using System;
using System.Collections.Generic;
using System.Text;

namespace SysManager.Application.Helpers
{

    public class ResultData
    {
        public ResultData(object _data, bool _success)
        {
            this.Success = _success;
            this.Data = _data;
        }

        public bool Success { get; set; }
        public object Data { get; set; }

        /*
        public class ResultData
        {
            public ResultData(bool _success)
            {
                this.Success = _success;
            }

            public bool Success { get; set; }
        }

        public class ResultData<T> : ResultData
        {
            public ResultData(T _data) : base(true)
            {
                this.Data = _data;
            }
            public T Data { get; set; }
        }
        */
    }
}