using System;
using System.Collections.Generic;
using System.Text;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.Handlers.Common
{
    public  class DefaultResponseError : IResponse
    {
        public DefaultResponseError(bool anyError, string errorMessage)
        {
            AnyError = anyError;
            ErrorMessage = errorMessage;
        }

        public bool AnyError { get; set ; }

        public string ErrorMessage { get; set; }
    }
}
