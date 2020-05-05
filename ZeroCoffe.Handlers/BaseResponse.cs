using System;
using System.Collections.Generic;
using System.Text;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.Handlers
{
    public abstract class BaseResponse : IResponse
    {
        public bool AnyError { get; set ; }
        public string HandledBy { get; set; }
    }
}
