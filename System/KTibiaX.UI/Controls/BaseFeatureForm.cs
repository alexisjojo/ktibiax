using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Keyrox.Shared.Controls;
using KTibiaX.Core.FormStorage;
using KTibiaX.Core.FormStorage.Contracts;
using KTibiaX.UI.Components;
using KTibiaX.UI.Contracts;
using Tibia.Client;
using Tibia.Features.Model;

namespace KTibiaX.UI.Controls {
    public class BaseFeatureForm : XtraForm, IStorable, IFeatureForm {

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFeatureForm"/> class.
        /// </summary>
        public BaseFeatureForm() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFeatureForm"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public BaseFeatureForm(TibiaClient client) {
            TibiaClient = client;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e) {
            if (!DesignMode) {
                Initialize();

            }
            base.OnLoad(e);
        }

        /// <summary>
        /// Gets or sets the storage manager.
        /// </summary>
        /// <value>The storage manager.</value>
        [Browsable(false)]
        public StorageManager StorageManager { get; set; }

        #region "[rgn] NonEdit Properties "
        [Browsable(false)]
        public TibiaClient TibiaClient { get; set; }

        [Browsable(false)]
        public Player Player { get { return TibiaClient != null ? TibiaClient.Features.Player : null; } }

        [Browsable(false)]
        private FeatureTimer Timer { get; set; }

        [Browsable(false)]
        private FeatureLog Log { get { return FeatureLog.Current; } }

        [Browsable(false)]
        public bool IsStarted { get; set; }

        [Browsable(false)]
        public bool IsInitialized { get; set; }

        [Browsable(false)]
        public long PlayerID {
            get {
                if (TibiaClient != null && TibiaClient.Connection != null) {
                    return TibiaClient.Connection.IsOtServer ? 0 : TibiaClient.Features.Player.Id;
                }
                return 0;
            }
        }

        [Browsable(false)]
        public string ServerName {
            get {
                if (TibiaClient != null && TibiaClient.Connection != null) {
                    return TibiaClient.Connection.IPLoginServer.ToString();
                }
                return string.Empty;
            }
        }

        [Browsable(false)]
        public string PlayerName {
            get {
                return Player != null ? Player.Name : string.Empty;
            }
        }
        #endregion

        #region "[rgn] Control Properties "
        [Category("KTibiaX"), Description("The name of the current feature.")]
        public string FeatureName { get; set; }

        [Category("KTibiaX"), Description("A set of controls to be stored and loaded from database.")]
        public KTibiaX.UI.Util.BaseEdit[] StorableControls { get; set; }

        [Category("KTibiaX"), Description("A set of grids to be stored and loaded its datasource from database.")]
        public KTibiaX.UI.Util.BaseGrid[] StorableGrids { get; set; }

        [Category("KTibiaX"), Description("A set of lists to be stored and loaded its datasource from database.")]
        public KTibiaX.UI.Util.BaseList[] StorableLists { get; set; }

        [Category("KTibiaX"), Description("A set of controls to disable when this feature form is started.")]
        public KTibiaX.UI.Util.BaseControl[] ControlsToDisable { get; set; }

        [Category("KTibiaX"), Description("The control used to start and stop this feature form.")]
        public SimpleButton StartButton { get; set; }

        [Category("KTibiaX"), Description("The control used to start this feature after player login.")]
        public CheckEdit AutomaticCheckControl { get; set; }

        [Category("KTibiaX"), Description("The interval used by the action timer."), DefaultValue((int)1000)]
        public int ActionInterval { get; set; }
        #endregion

        #region "[rgn] Control Events     "
        private void StartButton_Click(object sender, EventArgs e) {
            ChangeFormState();
        }
        private void AutomaticCheckControl_CheckedChanged(object sender, EventArgs e) {
            StorageManager.SetAutoStart(Name, AutomaticCheckControl.Checked);
        }
        public delegate void OnStartFeature();
        public delegate void OnStopFeature();

        public OnStartFeature OnStart;
        public OnStopFeature OnStop;
        #endregion

        #region "[rgn] Helper Methods     "
        private void ChangeFormState() {
            if (IsStarted) { Stop(); }
            else { Start(); }
        }
        private void ChangeControlsState(bool enable) {
            if (ControlsToDisable != null) {
                foreach (var control in ControlsToDisable) {
                    control.Enabled = enable;
                }
            }
        }
        private void SaveComplete(object sender, EventArgs e) {
            Log.Write(FeatureName, "Was complete saved!");
        }
        private void UpdateStartButtonAppearance() {
            if (!IsStarted) {
                StartButton.Text = "گtart";
                StartButton.Image = Properties.Resources.vista_start;
            }
            else {
                StartButton.Text = "گtop";
                StartButton.Image = Properties.Resources.vista_stop;
            }
        }
        #endregion

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize() {
            if (!base.DesignMode && !IsInitialized) {
                ChangeControlsState(true);

                if (StartButton != null) { StartButton.Click += StartButton_Click; }
                if (AutomaticCheckControl != null) { AutomaticCheckControl.CheckedChanged += AutomaticCheckControl_CheckedChanged; }
                if (ActionInterval == 0) { ActionInterval = 1000; }

                OnStart = OnBeforeStart;
                OnStop = OnAfterStop;

                StorageManager = new StorageManager(this);
                StorageManager.LoadPlayer();

                if (StorableControls != null) StorageManager.SetControls((from ctrl in StorableControls select ctrl.Control).ToArray());
                if (StorableGrids != null) StorageManager.SetControls((from ctrl in StorableGrids select ctrl.Control).ToArray());
                if (StorableLists != null) StorageManager.SetControls((from ctrl in StorableLists select ctrl.Control).ToArray());

                StorageManager.UpdateToControls();
                IsInitialized = true;
                Output.Add(this.FeatureName, " was INITIALIZED!");
            }
        }

        /// <summary>
        /// Determines whether this instance can start.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance can start; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanStart() { return true; }

        /// <summary>
        /// Called when [action execute].
        /// </summary>
        public virtual void OnActionExecute() { return; }

        /// <summary>
        /// Called when [before start].
        /// </summary>
        public virtual void OnBeforeStart() { }

        /// <summary>
        /// Called when [after stop].
        /// </summary>
        public virtual void OnAfterStop() { }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start() {
            if (!IsStarted && CanStart()) {
                IsStarted = true;
                ChangeControlsState(false);
                OnStart.Invoke();
                UpdateStartButtonAppearance();
                if (Timer == null) Timer = new FeatureTimer();
                Timer.Interval = ActionInterval;
                Timer.Start(OnActionExecute);
                Output.Add(this.FeatureName, " was STARTED!");
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop() {
            if (IsStarted) {
                IsStarted = false;
                ChangeControlsState(true);
                Timer.Stop();
                OnStop.Invoke();
                UpdateStartButtonAppearance();
                Output.Add(this.FeatureName, " was STOPED!");
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save() {
            StorageManager.UpdateFromControls();
            StorageManager.OnEndSave += SaveComplete;
            StorageManager.BeginSave();
            Output.Add(this.FeatureName, " was SAVED!");
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.FormClosing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (IsStarted) { e.Cancel = true; Hide(); return; }
            Save();
            Output.Add(this.FeatureName, " was CLOSED!");
            if (Timer != null) Timer.Dispose();
            Timer = null;
            base.OnFormClosing(e);
        }
    }
}
