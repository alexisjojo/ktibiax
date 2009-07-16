using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Infra.Session {
    public interface ISessionProvider {

        object this[object key] { get; set; }
        void Set(object key, object value);
    }
}
