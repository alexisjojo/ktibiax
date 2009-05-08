using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibia.Memory;
using Tibia.Features.Objects;
using KTibiaX.Shared.Objects;

namespace Tibia.Features.Actions.Graphics {
    public class DatReader {

        /// <summary>
        /// Initializes a new instance of the <see cref="DatReader"/> class.
        /// </summary>
        /// <param name="memory">The memory.</param>
        public DatReader(MemoryProvider memory) {
            Memory = memory;
            BaseAddr = (uint)Memory.Reader.Uint(Memory.Addresses.Client.DatPointer);
            ItemInfoAddr = (uint)Memory.Reader.Uint(BaseAddr + 8);
        }

        #region "[rgn] Object Properties "
        MemoryProvider Memory;
        uint BaseAddr { get; set; }
        uint ItemInfoAddr { get; set; }
        #endregion

        /// <summary>
        /// Items the count.
        /// </summary>
        /// <returns></returns>
        public int ItemCount() {
            return Memory.Reader.Uint(BaseAddr + 4).ToInt32();
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="itemId">The item id.</param>
        /// <returns></returns>
        public DatItem GetItem(uint itemId) {
            return new DatItem(Memory, ItemInfoAddr + 0x4C * (itemId - 100), itemId);
        }

    }
}
