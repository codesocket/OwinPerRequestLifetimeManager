using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiFromGround
{
    public static class MyUnityContainer
    {
        public static IUnityContainer Instance
        {
            get
            {
                return container.Value;
            }
        }

        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() => new UnityContainer());
    }
}