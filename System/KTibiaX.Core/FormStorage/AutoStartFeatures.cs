using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTibiaX.UI.Util {
    public class AutoStartFeatures {

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoStartFeatures"/> class.
        /// </summary>
        public AutoStartFeatures() {
        }

        /// <summary>
        /// Gets or sets the features.
        /// </summary>
        /// <value>The features.</value>
        public FeatureStartCollection Features { get; set; }

        /// <summary>
        /// Sets the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="autoStart">if set to <c>true</c> [auto start].</param>
        public void Set(string form, bool autoStart) {
            if (Features == null) Features = new FeatureStartCollection();
            if (Features.ContainsForm(form)) {
                Features[form].AutStart = autoStart;
            }
            else { Features.Add(form, autoStart); }
        }

        /// <summary>
        /// Starts the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool MustStart(string name) {
            if (Features == null) { Features = new FeatureStartCollection(); return false; }
            return Features.MustStart(name);
        }
    }
    public class FeatureStart {

        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        /// <value>The form.</value>
        public string FormName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [aut start].
        /// </summary>
        /// <value><c>true</c> if [aut start]; otherwise, <c>false</c>.</value>
        public bool AutStart { get; set; }
    }

    public class FeatureStartCollection : List<FeatureStart> {

        /// <summary>
        /// Determines whether the specified name contains form.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// 	<c>true</c> if the specified name contains form; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsForm(string name) {
            return this.Where(frm => frm.FormName == name).Count() > 0;
        }
        
        /// <summary>
        /// Musts the start.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool MustStart(string name) {
            var res = this.Where(frm => frm.FormName == name);
            return res.Count() > 0 ? res.First().AutStart : false;
        }

        /// <summary>
        /// Adds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="start">if set to <c>true</c> [start].</param>
        public FeatureStart Add(string name, bool start) {
            var res = new FeatureStart() { FormName = name, AutStart = start };
            Add(res); return res;
        }

        /// <summary>
        /// Gets or sets the <see cref="KTibiaX.UI.Util.FeatureStart"/> with the specified name.
        /// </summary>
        /// <value></value>
        public FeatureStart this[string name] {
            get {
                var res = this.Where(frm => frm.FormName == name);
                if (res.Count() > 0) { return res.First(); }
                else { return Add(name, false); }
            }
            set {
                var res = this.Where(frm => frm.FormName == name);
                if (res.Count() > 0) {
                    this[IndexOf(res.First())].AutStart = value.AutStart;
                }
                else { Add(value); }
            }
        }
    }
}
