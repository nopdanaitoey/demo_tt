using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_tt.Responses
{
    public class BaseResponse
    {

        public bool IsSuccess { get; set; }
        public object? Data { get; set; } = null;
        public string Message { get; set; }

        public BaseResponse Success(object data, string message = "Success")
        {
            return new BaseResponse()
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
        }
         public BaseResponse Success(string message = "Success")
        {
            return new BaseResponse()
            {
                IsSuccess = true,
                Data = null,
                Message = message
            };
        }

        public BaseResponse Fail(string message = "", object? data = null)
        {
            return new BaseResponse()
            {
                IsSuccess = false,
                Data = data,
                Message = message,
            };
        }
    }
}