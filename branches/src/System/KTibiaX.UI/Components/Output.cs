using System.Linq;
using Keyrox.Shared.Messaging;
using Keyrox.Shared.Messaging.Contracts;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Text;

namespace KTibiaX.UI.Components {
    /// <summary>
    /// Implementation of a singleton to message queue to 
    /// provide a output message trading facility.
    /// </summary>
    public sealed class Output : Component {

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        private static List<IMessage> Items { get; set; }

        /// <summary>
        /// Adds the specified message to current item list.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        public static void Add(string message, MessageType type) {
            if (Items == null) { Items = new List<IMessage>(); }
            Items.Add(new Message() {
                ApplicationName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name,
                Type = type,
                Content = message
            });
        }

        /// <summary>
        /// Adds the specified message to current item list.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Add(string message) {
            Add(message, MessageType.Normal);
        }

        /// <summary>
        /// Adds the specified message to current item list.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Add(params string[] message) {
            var msg = string.Empty;
            foreach (var nmsg in message) { msg += nmsg; }
            Add(msg);
        }

        /// <summary>
        /// Adds the specified highlight.
        /// </summary>
        /// <param name="highlight">if set to <c>true</c> [highlight].</param>
        /// <param name="message">The message.</param>
        public static void Add(bool highlight, params string[] message) {
            if (highlight) {
                AddBlankLine();
                Add(message);
                AddBlankLine();
            }
            else { Add(message); }
        }

        /// <summary>
        /// Adds a separator to current item list.
        /// </summary>
        public static void AddSeparator() {
            Add("---------------------------------------------------------------------------------", MessageType.Normal);
        }

        /// <summary>
        /// Adds a separator to current item list.
        /// </summary>
        public static void AddBlankLine() {
            Add(Environment.NewLine, MessageType.Normal);
        }

        /// <summary>
        /// Adds the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void Add(Exception ex) {
            AddSeparator();
            var sb = new StringBuilder();
            var lastException = ex;
            sb.Append("*** [EXCEPTION] ***");
            sb.Append(Environment.NewLine);

            while (lastException != null) {
                sb.Append(ex.Message);
                sb.Append(Environment.NewLine);
                sb.Append(ex.Source);
                lastException = ex.InnerException;
            }
            Add(sb.ToString());
        }

        /// <summary>
        /// Reads the next message addressed for calling application
        /// and remove this message from current list.
        /// </summary>
        /// <returns></returns>
        public static IMessage Read() {
            if (Items != null && Items.Count > 0) {
                var result = from item in Items
                             where item.ApplicationName == System.Reflection.Assembly.GetEntryAssembly().GetName().Name
                             select item;
                if (result.Count() > 0) { var msg = Items.First(); Items.Remove(msg); return msg; }
            }
            return null;
        }
    }
}
