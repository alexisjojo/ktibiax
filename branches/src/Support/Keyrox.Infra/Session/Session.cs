using System;
using System.Xml.Serialization;

namespace Keyrox.Infra.Session {
    [Serializable]
    public class Session : ISession {
        /// <summary>
        /// Initializes a new instance of the <see cref="Session"/> class.
        /// </summary>
        public Session() {
            Creation = DateTime.Now;
        }

        [XmlIgnore]
        private IntPtr oID = IntPtr.Zero;

        [XmlAttribute]
        public IntPtr OID {
            get {
                if (oID == IntPtr.Zero) {
                    var time = Creation.ToOADate();
                    var hash = Key != null ? Key.GetHashCode() : new Random().Next(Convert.ToInt32(time));
                    oID = (IntPtr)Convert.ToUInt32(hash + time);
                }
                return oID;
            }
        }

        [XmlAttribute]
        public object Key { get; set; }

        [XmlAttribute]
        public object Value { get; set; }

        [XmlAttribute]
        public bool Criptographed { get; set; }

        [XmlAttribute]
        public DateTime Creation { get; private set; }

        /// <summary>
        /// Builds the session object with the specified key and value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="criptographed">Use criptograph.</param>
        /// <returns></returns>
        public static ISession Build(object key, object value, bool criptographed) {
            return new Session() { Key = key, Value = value, Criptographed = criptographed };
        }

        /// <summary>
        /// Builds the session object with the specified key and value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static ISession Build(object key, object value) {
            return Build(key, value, false);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return Key != null ? Key.ToString() : base.ToString();
        }
    }
}
