using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraBars.Ribbon;
using KTibiaX.UI.Contracts;
using Tibia.Client;
using KTibiaX.Core.FormStorage;

namespace KTibiaX.UI.Components {
    public class FeatureFormManager : Component {

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureFormManager"/> class.
        /// </summary>
        /// <param name="mdi">The MDI.</param>
        public FeatureFormManager(RibbonForm mdi) {
            MDI = mdi;
            InnerForms = new Dictionary<Type, IFeatureForm>();
        }

        #region "[rgn] Properties "
        public RibbonForm MDI { get; private set; }
        public Dictionary<Type, IFeatureForm> InnerForms { get; private set; }
        #endregion

        /// <summary>
        /// Shows the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        public void Show<T>(TibiaClient client) {
            if (InnerForms.ContainsKey(typeof(T))) {
                InnerForms[typeof(T)].Show();
            }
            else {
                var form = (IFeatureForm)Activator.CreateInstance<T>();
                form.TibiaClient = client;
                InnerForms.Add(typeof(T), form);
                form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(form_FormClosed);
                form.Show();
            }
        }

        /// <summary>
        /// Shows the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        public void Start<T>(TibiaClient client) {
            if (InnerForms.ContainsKey(typeof(T))) {
                InnerForms[typeof(T)].Start();
            }
            else {
                var form = (IFeatureForm)Activator.CreateInstance<T>();
                form.TibiaClient = client;
                InnerForms.Add(typeof(T), form);
                form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(form_FormClosed);
                form.Initialize();
                form.Start();
            }
        }

        /// <summary>
        /// Stops all.
        /// </summary>
        public void StopAll() {
            foreach (var feature in InnerForms) {
                feature.Value.Stop();
            }
        }

        /// <summary>
        /// Determines whether this instance is started.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// 	<c>true</c> if this instance is started; otherwise, <c>false</c>.
        /// </returns>
        public bool IsStarted<T>() {
            if (InnerForms.ContainsKey(typeof(T))) {
                var feature = InnerForms[typeof(T)];
                return feature.IsStarted;
            }
            return false;
        }

        /// <summary>
        /// Starts if needed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="formName">Name of the form.</param>
        /// <param name="client">The client.</param>
        public void StartIfNeeded<T>(string formName, TibiaClient client) {
            if (StorageManager.CheckAutoStart(formName)) {
                Start<T>(client);
            }
        }

        /// <summary>
        /// Handles the FormClosed event of the form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosedEventArgs"/> instance containing the event data.</param>
        private void form_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e) {
            InnerForms.Remove(((IFeatureForm)sender).GetType());
        }
    }
}
