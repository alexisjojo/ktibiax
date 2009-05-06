using System.Collections.Generic;
using Tibia.Features.Model;
using Tibia.Features.Structures;

namespace Tibia.Features.Actions.Graphics {
    /// <summary>
    /// Map Reader Utility Class.
    /// </summary>
    public class MapReader {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapReader"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        public MapReader(Model.Player player) {
            Player = player;
        }

        #region "[rgn] Public Properties "
        public Features.Graphics.MapReader Reader {
            get {
                if (Player != null) {
                    return new Features.Graphics.MapReader(Player.Memory, Player.Address);
                }
                return null;
            }
        }
        public Model.Player Player { get; set; }
        #endregion

        /// <summary>
        /// Gets objects in defined location.
        /// </summary>
        /// <param name="sqm">The SQM Location.</param>
        /// <returns></returns>
        public List<TileObject> GetTileObjectsList(Location sqm) {
            return new List<TileObject>(Reader.GetTileObjects(new LocationDefinition((int)sqm.X, (int)sqm.Y, (int)sqm.Z)));
        }

        /// <summary>
        /// Gets objects in defined location.
        /// </summary>
        /// <param name="sqm">The SQM Location.</param>
        /// <returns></returns>
        public TileObject[] GetTileObjectsArray(LocationDefinition sqm) {
            return Reader.GetTileObjects(sqm);
        }

        /// <summary>
        /// Gets items in defined location.
        /// </summary>
        /// <param name="sqm">The SQM Location.</param>
        /// <returns></returns>
        public List<Item> GetItems(Location sqm) {
            var returnItems = new List<Item>();
            var items = GetTileObjectsList(sqm);
            foreach (TileObject item in items) {
                returnItems.Add(new Item((uint)item.GetObjectID, (uint)item.GetData));
            }
            return returnItems;
        }

        /// <summary>
        /// Verify if exists the item in defined Location.
        /// </summary>
        /// <param name="sqm">The SQM.</param>
        /// <param name="itemID">The item ID.</param>
        /// <returns></returns>
        public bool ExistItem(Location sqm, uint itemID) {
            List<TileObject> items = GetTileObjectsList(sqm);
            foreach (TileObject item in items) {
                if (item.GetObjectID == (int)itemID) {
                    return true;
                }
            }
            return false;
        }

    }
}
