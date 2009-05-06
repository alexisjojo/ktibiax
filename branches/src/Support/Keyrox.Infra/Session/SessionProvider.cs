using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using Keyrox.Shared.Objects;

namespace Keyrox.Infra.Session {
    public class SessionProvider : ISessionProvider {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionProvider"/> class.
        /// </summary>
        public SessionProvider() {
            Initialize();
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public Dictionary<object, ISession> Content { get; private set; }

        #region "[rgn] Internal Properties "
        private bool SendSessionStatistics { get; set; }
        private bool IsInitialized { get; set; }
        private bool IsInitializing { get; set; }
        #endregion

        /// <summary>
        /// Gets the ISession value with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object this[object key] {
            get {
                if (!IsInitialized) Initialize();
                if (Content.ContainsKey(key)) {
                    return Content[key].Value;
                }
                return null;
            }
            set { Set(key, value); }
        }

        /// <summary>
        /// Sets the ISession with the specified key and value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Set(object key, object value) {
            if (!IsInitialized) Initialize();
            if (Content.ContainsKey(key)) Content[key] = Session.Build(key, value);
            else Content.Add(key, Session.Build(key, value));
        }


        /// <summary>
        /// Initializes the session context.
        /// </summary>
        private void Initialize() {
            try {
                //Set initializing Lock.
                if (IsInitializing || IsInitialized) return;
                IsInitializing = true;
                if (ConfigurationManager.AppSettings.AllKeys.Contains("SendSessionStatistics"))
                    SendSessionStatistics = ConfigurationManager.AppSettings["SendSessionStatistics"].ToString().ToBoolean();
                Content = new Dictionary<object, ISession>();
                IsInitialized = true;

                //Unlock initializing.
                IsInitializing = false;
            }
            catch (Exception ex) {
                var exMsg = "Unable to initialize Application Session!";
                string.Concat(exMsg, Environment.NewLine, ex.Message);
                throw new Exception(exMsg, ex);
            }
        }
    }
}
