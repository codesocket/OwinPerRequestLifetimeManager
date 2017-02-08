using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApiFromGround.Controllers
{
    public class TestController : ApiController
    {
        public Model Get()
        {
            var context = HttpContext.Current;
            var config = GlobalConfiguration.Configuration;

            return new Model { Id = 1 };
        }

        public Model GetById(int id)
        {
            var context = HttpContext.Current;
            var config = GlobalConfiguration.Configuration;

            return new Model { Id = 2 };
        }
    }
}