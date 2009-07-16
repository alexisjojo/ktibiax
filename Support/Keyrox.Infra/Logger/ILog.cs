using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keyrox.Infra.Logger {
    public interface ILog {
        void Add(string value, bool allowIntercept);
        void Add(string value);
        string FileName { get; set; }
        void ResetLogFile();
        StringBuilder Content { get; }
        LogInterceptor Interceptor { get; set; }
    }
}
