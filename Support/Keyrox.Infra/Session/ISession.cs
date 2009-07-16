using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Infra.Session {
    public interface ISession {

        IntPtr OID { get; }
        object Key { get; set; }
        object Value { get; set; }
        DateTime Creation { get; }
        bool Criptographed { get; set; }
    }
}
