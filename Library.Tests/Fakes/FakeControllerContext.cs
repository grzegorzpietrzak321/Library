using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library.Tests.Fakes
{
    class FakeControllerContext : ControllerContext
    {
        private HttpContextBase _context = new FakeHttpContext();

        public override HttpContextBase HttpContext
        {
            get { return _context; }
            set { _context = value; }
        }
    }

    class FakeHttpContext : HttpContextBase
    {
        private HttpRequestBase _request = new FakeHttpRequest();

        public override HttpRequestBase Request
        {
            get { return _request; }
        }
    }

    class FakeHttpRequest : HttpRequestBase
    {
        public override string this[string key]
        {
            get { return null; }
        }

        public override NameValueCollection Headers
        {
            get
            {
                return new NameValueCollection();
            }
        }
    }
}
