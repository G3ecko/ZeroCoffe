using System;
using System.Collections.Generic;
using System.Text;
using ZeroCoffe.Handlers.Interface;

namespace ZeroCoffe.Handlers.Common
{
    public class TestRequest1 : IRequest
    { 
     public string text { get; set; }
    }
    public class TestRequest2 : IRequest
    {
        public string text { get; set; }
    }
    public class TestResponse1 : BaseResponse
    {
        public string text { get; set; }
      
    }

    public class TestResponse2 : BaseResponse
    {
        public string text { get; set; }
      
    }
}
