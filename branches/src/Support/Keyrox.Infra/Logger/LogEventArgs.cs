using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Infra.Logger {
    public class LogEventArgs : EventArgs {

        public LogEventArgs(string value) {
            Value = value;
        }

        public string Value { get; set; }
    }
}
