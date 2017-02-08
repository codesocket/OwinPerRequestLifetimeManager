using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;

namespace WebApiFromGround.Controllers
{
    [RoutePrefix("Dummy")]
    public class ValuesController : ApiController
    {
        [Route("1x1.json")]
        public MyService Get()
        {
            var identity = User.Identity;
            var context = HttpContext.Current;
            var config = GlobalConfiguration.Configuration;
            var owinContext = Request.GetOwinContext();
            var myService1 = MyUnityContainer.Instance.Resolve<MyService>();

            myService1.Id = 1;

            var myService2 = MyUnityContainer.Instance.Resolve<MyService>();

            

            return myService2;
        }


        [Route("1x1.xml")]
        public Model Get2()
        {
            var context = HttpContext.Current;
            var config = GlobalConfiguration.Configuration;

            return new Model { Id = 2 };
        }
    }

    [DataContract]
    public class Model
    {
        [DataMember]
        public int Id { get; set; }
    }
}