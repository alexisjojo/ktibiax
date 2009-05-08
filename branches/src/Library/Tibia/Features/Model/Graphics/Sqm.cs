using System.Linq;
using System.Collections.Generic;
using Keyrox.Shared.Objects;
using Tibia.Memory;
using Tibia.Features.Providers;
using Tibia.Features.Structures;
using Tibia.Connection.Providers;

namespace Tibia.Features.Model.Graphics {
    public class Sqm {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sqm"/> class.
        /// </summary>
        /// <param name="sqmNumber">The SQM number.</param>
        /// <param name="map">The map.</param>
        /// <param name="playerSqm">The player SQM.</param>
         public Sqm(uint sqmNumber, GraphicsProvider map, Sqm playerSqm) {
            Map = map;
            SqmNumber = sqmNumber;
            Address = Map.SqmNumberToSqmAddress(SqmNumber);
            MemoryLocation = map.GetSqmLocation(SqmNumber);
            WorldLocation = map.MemoryToWorld(MemoryLocation, playerSqm);
        }

         /// <summary>
         /// Initializes a new instance of the <see cref="Sqm"/> class.
         /// </summary>
         /// <param name="sqmNumber">The SQM number.</param>
         /// <param name="map">The map.</param>
         /// <param name="worldLocation">The world location.</param>
         public Sqm(uint sqmNumber, GraphicsProvider map, Location worldLocation) {
             Map = map;
             SqmNumber = sqmNumber;
             Address = Map.SqmNumberToSqmAddress(SqmNumber);
             MemoryLocation = map.GetSqmLocation(SqmNumber);
             WorldLocation = worldLocation;
         }

        #region "[rgn] Public Properties  "
        public GraphicsProvider Map { get; set; }
        public ConnectionProvider Connection { get { return Map.Connection; } }
        public TibiaMemoryProvider Memory { get { return Connection.Memory; } }

        public uint SqmNumber { get; private set; }
        public uint Address { get; private set; }

        public Location WorldLocation { get; private set; }
        public Location MemoryLocation { get; private set; }
        public List<Tile> Tiles {
            get {
                var tiles = new List<Tile>();
                uint pointer = Address + Memory.Addresses.Map.ObjectsDist;
                uint count = Memory.Reader.Uint(Address) - 1;

                for (int i = 0; i < count; i++) {
                    pointer += Memory.Addresses.Map.StepObjectDist;
                    tiles.Add(new Tile(pointer, i.ToUInt32(), Memory));
                }
                return tiles;
            }
        }        
        #endregion

        /// <summary>
        /// Determines whether this instance has container.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance has container; otherwise, <c>false</c>.
        /// </returns>
        public bool HasContainer() {
            return Tiles.Count(t => t.IsContainer == true) > 0;
        }
        /// <summary>
        /// Determines whether this instance has depot.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance has depot; otherwise, <c>false</c>.
        /// </returns>
        public bool HasDepot() {
            return Tiles.Count(t => t.IsDepot == true) > 0;
        }
        /// <summary>
        /// Changes the floor.
        /// </summary>
        /// <returns></returns>
        public bool ChangeFloor() {
            return Tiles.Count(t => t.GoUp == true || t.GoDown == true) > 0;
        }
        /// <summary>
        /// Determines whether this instance has player.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance has player; otherwise, <c>false</c>.
        /// </returns>
        public bool HasPlayer() {
            return Tiles.Count(t => t.TileId == 0x63) > 0;
        }
        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(uint item) {
            return Tiles.Count(t => t.Data == item) > 0;
        }
    }
}
