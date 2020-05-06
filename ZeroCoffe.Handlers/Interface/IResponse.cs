using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroCoffe.Handlers.Interface
{
    public interface IResponse
    {
        bool AnyError { get; set; }
        string HandledBy { get; set; }

    }
}
