using System;

namespace Keyrox.Infra.Logger {
    public class LogInterceptor {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogInterceptor"/> class.
        /// </summary>
        public LogInterceptor() {
        }

        public event EventHandler<LogEventArgs> LogAdded;
        public event EventHandler LogInitialized;

        public void HandleLogAdded(string value) {
            if (LogAdded != null) LogAdded(this, new LogEventArgs(value));
        }

        public void HandleLogInitialized() {
            if (LogInitialized != null) LogInitialized(this, EventArgs.Empty);
        }
    }
}
