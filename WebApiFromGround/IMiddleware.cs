using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiFromGround
{
    public interface IMiddleware
    {
        object GetValue(object key);
        void SetValue(object key, object value);
    }
}
