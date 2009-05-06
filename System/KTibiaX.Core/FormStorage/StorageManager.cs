using System;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using KTibiaX.Core.FormStorage.Contracts;
using KTibiaX.Core.Model;
using KTibiaX.Core.Properties;

namespace KTibiaX.Core.FormStorage {
    public class StorageManager {

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageManager"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public StorageManager(IStorable owner) {
            Owner = owner;
            Fields = new StorageFieldCollection();
        }

        #region "[rgn] Public Properties "
        public StorageFieldCollection Fields { get; set; }
        public IStorable Owner { get; set; }
        public PlayerFeature PlayerFeature { get; set; }
        #endregion

        #region "[rgn] Controls Setup    "
        /// <summary>
        /// Sets the controls.
        /// </summary>
        /// <param name="controls">The controls.</param>
        public void SetControls(params BaseEdit[] controls) {
            foreach (var control in controls) {
                Fields.Add(new StorageField(control, control.Name));
            }
            UpdateFieldValues();
        }

        /// <summary>
        /// Sets the controls.
        /// </summary>
        /// <param name="controls">The controls.</param>
        public void SetControls(params GridControl[] controls) {
            foreach (var control in controls) {
                Fields.Add(new StorageGrid(control, control.Name));
            }
            UpdateFieldValues();
        }

        /// <summary>
        /// Sets the controls.
        /// </summary>
        /// <param name="controls">The controls.</param>
        public void SetControls(params BaseListBoxControl[] controls) {
            foreach (var control in controls) {
                Fields.Add(new StorageList(control, control.Name));
            }
            UpdateFieldValues();
        }
        #endregion
        
        #region "[rgn] Update Controls   "
        /// <summary>
        /// Updates the field values.
        /// </summary>
        public void UpdateFieldValues() {
            foreach (var field in Fields.List) {
                var fieldData = GetFieldData(field.Value.Id);
                if (fieldData != null) {
                    Fields[field.Value.Id].Value = fieldData.Value;
                    Fields[field.Value.Id].ValueType = fieldData.Type;
                }
            }
        }

        /// <summary>
        /// Updates to controls.
        /// </summary>
        public void UpdateToControls() {
            foreach (var field in Fields.List) {
                field.Value.UpdateControlValue();
            }
        }

        /// <summary>
        /// Updates from controls.
        /// </summary>
        public void UpdateFromControls() {
            foreach (var field in Fields.List) {
                field.Value.UpdateFieldValue();
            }
        }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <returns></returns>
        public PlayerFeature GetPlayerFeature() {
            if (Owner.PlayerID > 0) {
                var res = Data.PlayerFeature.Repository.LoadAll(Owner.PlayerID, Owner.FeatureName);
                return res.Count > 0 ? res.First() : null;
            }
            else {
                var res = Data.PlayerFeature.Repository.LoadAll(Owner.PlayerName, Owner.ServerName, Owner.FeatureName);
                return res.Count > 0 ? res.First() : null;
            }
        }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <returns></returns>
        public FeatureData GetFieldData(string name) {
            if (Owner.PlayerID > 0) {
                var res = Data.FeatureData.Repository.LoadAll(PlayerFeature.PlayerId, PlayerFeature.FeatureName, name);
                return res.Count > 0 ? res.First() : null;
            }
            else {
                var res = Data.FeatureData.Repository.LoadAll(PlayerFeature.PlayerName, PlayerFeature.ServerName, PlayerFeature.FeatureName, name);
                return res.Count > 0 ? res.First() : null;
            }
        }
        #endregion
        
        /// <summary>
        /// Occurs when [on end save].
        /// </summary>
        public event EventHandler OnEndSave;

        /// <summary>
        /// Loads the player.
        /// </summary>
        public void LoadPlayer() {
            var player = GetPlayerFeature();
            if (player != null) { PlayerFeature = player; }
            else {
                PlayerFeature = new PlayerFeature() { FeatureName = Owner.FeatureName, PlayerId = Owner.PlayerID, PlayerName = Owner.PlayerName, ServerName = Owner.ServerName };
                Data.PlayerFeature.Repository.SaveNew(PlayerFeature);
                PlayerFeature.Reload();
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save() {
            foreach (var field in Fields.List) {
                if (field.Value.Value != null) {
                    
                    var fieldData = GetFieldData(field.Value.Id);
                    if (fieldData != null) {
                        fieldData.Value = field.Value.Value.ToString();
                        fieldData.LastChange = DateTime.Now;
                        Data.FeatureData.Repository.Update(fieldData);
                    }
                    else {
                        var newField = new FeatureData() {
                            Feature = PlayerFeature,
                            Type = field.Value.ValueType,
                            Name = field.Value.Id,
                            Value = field.Value.Value.ToString(),
                            LastChange = DateTime.Now
                        };
                        Data.FeatureData.Repository.SaveNew(newField);
                    }
                }
            }
        }

        /// <summary>
        /// Begins the save.
        /// </summary>
        public void BeginSave() {
            Callback call = Save;
            call.BeginInvoke(EndSave, call);
        }

        /// <summary>
        /// Ends the save.
        /// </summary>
        /// <param name="result">The result.</param>
        private void EndSave(IAsyncResult result) {
            if (OnEndSave != null) { OnEndSave(this, EventArgs.Empty); }
        }

        /// <summary>
        /// Sets the auto start.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="start">if set to <c>true</c> [start].</param>
        public static void SetAutoStart(string form, bool start) {
            if (Settings.Default.AutoStartFeatures == null) {
                Settings.Default.AutoStartFeatures = new KTibiaX.UI.Util.AutoStartFeatures();
                Settings.Default.Save();
            }
            Settings.Default.AutoStartFeatures.Set(form, start);
            Settings.Default.Save();
        }

        /// <summary>
        /// Checks the auto start.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns></returns>
        public static bool CheckAutoStart(string form) {
            if (Settings.Default.AutoStartFeatures == null) {
                Settings.Default.AutoStartFeatures = new KTibiaX.UI.Util.AutoStartFeatures();
                Settings.Default.Save();
            }
            return Settings.Default.AutoStartFeatures.MustStart(form);
        }
    }
}
