using System;
using System.Collections.Generic;
using System.Text;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.Handlers.Common
{
    public  class DefaultResponseError : BaseResponse
    {
        public DefaultResponseError(string errorMessage)
        {
            AnyError = true;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }

    }
}
