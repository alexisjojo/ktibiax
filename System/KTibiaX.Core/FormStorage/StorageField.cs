using System;
using DevExpress.XtraEditors;
using KTibiaX.Core.FormStorage.Contracts;

namespace KTibiaX.Core.FormStorage {
    public class StorageField : IStorageField {

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageField"/> class.
        /// </summary>
        /// <param name="control">The control.</param>
        public StorageField(BaseEdit control, string id) {
            Control = control;
            Id = id;
            if (control.EditValue != null) {
                Value = control.EditValue;
                ValueType = control.EditValue.GetType().ToString();
            }
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the value.
        /// </summary>
        /// <value>The type of the value.</value>
        public string ValueType { get; set; }

        /// <summary>
        /// Gets or sets the control.
        /// </summary>
        /// <value>The control.</value>
        public BaseEdit Control { get; set; }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetValue(object value) {
            Value = value;
            ValueType = value.GetType().ToString();
            UpdateControlValue();
        }

        /// <summary>
        /// Updates the control value.
        /// </summary>
        public void UpdateControlValue() {
            if (Value != null) {
                Control.Properties.BeginUpdate();
                Control.EditValue = Convert.ChangeType(Value, Type.GetType(ValueType));
                Control.Properties.EndUpdate();
            }
        }

        /// <summary>
        /// Updates the field value.
        /// </summary>
        public void UpdateFieldValue() {
            if (Control.EditValue != null) {
                Value = Control.EditValue;
                ValueType = Control.EditValue.GetType().ToString();
            }
        }

    }
}
