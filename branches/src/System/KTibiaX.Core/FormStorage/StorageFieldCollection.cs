using System;
using System.Collections.Generic;
using System.Linq;
using KTibiaX.Core.FormStorage.Contracts;

namespace KTibiaX.Core.FormStorage {
    public class StorageFieldCollection {

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageFieldCollection"/> class.
        /// </summary>
        public StorageFieldCollection() {
            List = new Dictionary<string, IStorageField>();
        }

        /// <summary>
        /// Gets or sets the list.
        /// </summary>
        /// <value>The list.</value>
        public IDictionary<string, IStorageField> List { get; private set; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get { return List.Count; } }

        /// <summary>
        /// Gets or sets the <see cref="KTibiaX.Core.FormStorage.Contracts.IStorageField"/> with the specified id.
        /// </summary>
        /// <value></value>
        public IStorageField this[string id] {
            get { return List.ContainsKey(id) ? List[id] : null; }
            set { if (List.ContainsKey(value.Id)) { List[value.Id] = value; } else { Add(value); } }
        }

        /// <summary>
        /// Adds the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        public void Add(IStorageField field) {
            if (List.ContainsKey(field.Id)) {
                this[field.Id] = field;
            }
            else { List.Add(field.Id, field); }
        }

        /// <summary>
        /// Removes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public bool Remove(string id) {
            if (List.ContainsKey(id)) return false;
            List.Remove(id); return true;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear() { List.Clear(); }
    }
}
