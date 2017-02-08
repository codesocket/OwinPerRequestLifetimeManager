using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.Utility;
using Microsoft.Practices.Unity;

namespace WebApiFromGround
{
    public class OwinPerRequestLifetimeManagerMiddleware : OwinMiddleware
    {
        private static readonly string ModuleKey = Guid.NewGuid().ToString();
        Action<IOwinContext> callback;

        public OwinPerRequestLifetimeManagerMiddleware(OwinMiddleware next, Action<IOwinContext> action) : base(next)
        {
            this.callback = action;
        }

        public override async Task Invoke(IOwinContext context)
        {
            this.callback.Invoke(context);

            await Next.Invoke(context);

            DisposeObjects(context);
        }

        internal static object GetValue(object lifetimeManagerKey, IOwinContext context)
        {
            Dictionary<object, object> dictionary = OwinPerRequestLifetimeManagerMiddleware.GetDictionary(context);
            if (dictionary != null)
            {
                object obj = (object)null;
                if (dictionary.TryGetValue(lifetimeManagerKey, out obj))
                    return obj;
            }
            return (object)null;
        }

        internal static void SetValue(object lifetimeManagerKey, object value, IOwinContext context)
        {
            Dictionary<object, object> dictionary = OwinPerRequestLifetimeManagerMiddleware.GetDictionary(context);
            if (dictionary == null)
            {
                dictionary = new Dictionary<object, object>();
                context.Environment[OwinPerRequestLifetimeManagerMiddleware.ModuleKey] = (object)dictionary;
            }
            dictionary[lifetimeManagerKey] = value;
        }

        /// <summary>Disposes the resources used by this module.</summary>
        public void Dispose()
        {
        }

        private void DisposeObjects(IOwinContext context)
        {
            IDictionary<object, object> dictionary = OwinPerRequestLifetimeManagerMiddleware.GetDictionary(context);
            if (dictionary == null)
                return;
            foreach (IDisposable disposable in dictionary.Values.OfType<IDisposable>())
                disposable.Dispose();
        }

        private static Dictionary<object, object> GetDictionary(IOwinContext context)
        {
            if (context.Environment.ContainsKey(OwinPerRequestLifetimeManagerMiddleware.ModuleKey))
            {
                return (Dictionary<object, object>)context.Environment[OwinPerRequestLifetimeManagerMiddleware.ModuleKey];
            }

            return null;
        }
    }
}