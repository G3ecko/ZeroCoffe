using System;
using System.Collections.Generic;
using System.Text;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.Handlers.Test.Common
{
    public interface IRequestMock1 : IRequest { };
    public interface IRequestMock2 : IRequest { };

    public interface IRequestHandlerMock : IRequestHandler { };
}

