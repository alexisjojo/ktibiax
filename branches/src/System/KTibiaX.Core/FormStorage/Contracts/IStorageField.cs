using System;

namespace KTibiaX.Core.FormStorage.Contracts {
    public interface IStorageField {

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        string Id { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        object Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the value.
        /// </summary>
        /// <value>The type of the value.</value>
        string ValueType { get; set; }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        void SetValue(object value);

        /// <summary>
        /// Updates the control value.
        /// </summary>
        void UpdateControlValue();

        /// <summary>
        /// Updates the field value.
        /// </summary>
        void UpdateFieldValue();
    }
}
