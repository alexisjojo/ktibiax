using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Reflection;
using Keyrox.Infra;

namespace Keyrox.Infra.Logger {
    public class Log : ILog {
        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public Log() {
            Initialize();
        }
        
        /// <summary>
        /// Writes the value on LogFile.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="allowIntercept">if set to <c>true</c> [allow intercept].</param>
        public void Add(string value, bool allowIntercept) {
            if (string.IsNullOrEmpty(value)) return;
            if (IsInitialized) Initialize();
            WriteInLog(value);
            if(allowIntercept) Interceptor.HandleLogAdded(value);
        }

        /// <summary>
        /// Writes the value on LogFile.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(string value) {
            Add(value, true);
        }

        /// <summary>
        /// Writes the in log.
        /// </summary>
        /// <param name="value">The value.</param>
        private void WriteInLog(string value) {
            var line = new StringBuilder();
            line.Append(Prefix);
            line.Append(" - ");
            line.Append(DateTime.Now.ToString());
            line.Append(" - ");
            line.Append(value);
            using (var writer = new StreamWriter(FileName, true)) {
                writer.WriteLine(line.ToString());
                writer.Flush(); writer.Close();
            }
        }

        /// <summary>
        /// Resets the log file.
        /// </summary>
        public void ResetLogFile() {
            IsInitialized = false;
            Initialize();
        }

        #region "[rgn] Object Properties "
        public string FileName { get; set; }
        public string Prefix { get; set; }
        public StringBuilder Content {
            get {
                if (IsInitialized) Initialize();
                using (var reader = new StreamReader(FileName)) {
                    var result = new StringBuilder(reader.ReadToEnd());
                    reader.Close();
                    return result;
                }
            }
        }
        public LogInterceptor Interceptor { get; set; }
        private bool IsInitialized { get; set; }
        private bool IsInitializing { get; set; }
        #endregion

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize() {
            try {
                //Set initializing Lock.
                if (IsInitializing || IsInitialized) return;
                IsInitializing = true;

                //Initialize File Settings.
                if (ConfigurationManager.AppSettings.AllKeys.Contains("LogFileName"))
                    FileName = ConfigurationManager.AppSettings["LogFileName"];
                else { FileName = Path.Combine(Environment.CurrentDirectory, "log.txt"); }
                if (ConfigurationManager.AppSettings.AllKeys.Contains("LogPrefix"))
                    Prefix = ConfigurationManager.AppSettings["LogPrefix"];
                else { Prefix = "[Logger]"; }

                //Initialize Log File.
                if (File.Exists(FileName)) File.Delete(FileName);
                var HeaderNamespace = "Keyrox.Infra.Properties.LogHeader.txt";
                using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(HeaderNamespace)) {
                    if (resourceStream == null) { throw new Exception(HeaderNamespace + " Not Found!"); }
                    using (var reader = new StreamReader(resourceStream)) {
                        var contents = reader.ReadToEnd();
                        contents = string.Format(contents,
                            Assembly.GetEntryAssembly().GetName().GetBuildDate().ToString("dd/MM/yyyy"),
                            Assembly.GetEntryAssembly().GetName().Version.FillLeftSpaces(14),
                            "8.3.1");
                        using (var writer = new StreamWriter(FileName)) {
                            writer.WriteLine(contents);
                            writer.Flush();
                            writer.Close();
                        }
                    }
                }
                //Unlock initializing.
                Interceptor = new LogInterceptor();
                IsInitialized = true;
                IsInitializing = false;
                System.Threading.Thread.Sleep(300);
            }
            catch (Exception ex) {
                var exMsg = "Unable to initialize Application Log!";
                string.Concat(exMsg, Environment.NewLine, ex.Message);
                throw new Exception(exMsg, ex);
            }
        }


    }
}
