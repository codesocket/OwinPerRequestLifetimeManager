using Microsoft.Owin;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiFromGround
{
    public class PerRequestLifetimeManager : LifetimeManager
    {
        private IOwinContext context;
        private readonly object lifetimeKey = new object();

        public PerRequestLifetimeManager(IOwinContext context)
        {
            this.context = context;
        }

        public override object GetValue()
        {
            return OwinPerRequestLifetimeManagerMiddleware.GetValue(this.lifetimeKey, this.context);
        }

        public override void SetValue(object newValue)
        {
            OwinPerRequestLifetimeManagerMiddleware.SetValue(this.lifetimeKey, newValue, this.context);
        }

        public override void RemoveValue()
        {
            IDisposable disposable = this.GetValue() as IDisposable;
            if (disposable != null)
                disposable.Dispose();
            OwinPerRequestLifetimeManagerMiddleware.SetValue(this.lifetimeKey, (object)null, this.context);
        }
    }
}