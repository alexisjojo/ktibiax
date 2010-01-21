using System;
using Tibia.Features.Model.Items;
using Tibia.Memory;
using Tibia.Features.Model.Contracts;
using Tibia.Connection.Providers;

namespace Tibia.Features.Model.Graphics {
    public class Tile {

        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class.
        /// </summary>
        public Tile(uint address, uint stackPosition, TibiaMemoryProvider memory) {
            this.Address = address;
            this.Memory = memory;
            this.StackPosition = StackPosition;
        }

        #region "[rgn] Public Properties   "
        public uint Address { get; private set; }
        public uint StackPosition { get; private set; }
        public TibiaMemoryProvider Memory { get; private set; }

        public IItem Item {
            get { return new Item(TileId) { Memory = this.Memory }; }
        }        

        public uint TileId {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.Map.ObjectIdDist); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.Map.ObjectIdDist, value); }
        }
        public uint Data {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.Map.ObjectDataDist); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.Map.ObjectDataDist, value); }
        }
        public uint ExtraData {
            get { return Memory.Reader.Uint(Address + Memory.Addresses.Map.ObjectDataExDist); }
            set { Memory.Writer.Uint(Address + Memory.Addresses.Map.ObjectDataExDist, value); }
        }

        public bool IsGround { get { return Item.DatItem.GetFlag(DatItemFlag.IsGround); } }
        public bool GoUp { get { return DatItemTypes.UP.Contains(TileId) || DatItemTypes.UPUse.Contains(TileId) || DatItemTypes.Rope.Contains(TileId); } }
        public bool GoDown { get { return DatItemTypes.Down.Contains(TileId) || DatItemTypes.DownUse.Contains(TileId) || DatItemTypes.Shovel.Contains(TileId); } }
        public bool IsDepot { get { return Item.DatItem.LensHelp == DatItemDefination.IsDepot; } }
        public bool IsContainer { get { return Item.DatItem.GetFlag(DatItemFlag.IsContainer); ; } }
        public bool IsBlocking { get { return Item.DatItem.GetFlag(DatItemFlag.Blocking); } }
        #endregion

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString() {
            return string.Format("ID: {0}", TileId);
        }

    }
}
