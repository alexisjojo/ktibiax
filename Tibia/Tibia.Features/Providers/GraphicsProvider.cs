using System.Linq;
using System.Collections.Generic;
using Keyrox.Shared.Objects;
using Tibia.Memory;
using Tibia.Features.Model;
using Tibia.Features.Structures;
using Tibia.Connection.Providers;
using Tibia.Features.Model.Graphics;
using Tibia.Features.Graphics;

namespace Tibia.Features.Providers {
    /// <summary>
    /// Graphic Actions Provider.
    /// </summary>
    public class GraphicsProvider {
        /// <summary>
        /// Initializes a new instance of the GraphicProvider class.
        /// </summary>
        /// <param name="connection">The client connection.</param>
        public GraphicsProvider(ConnectionProvider connection) {
            Connection = connection;
            LevelSpy = new LevelSpy(connection);
            NameSpy = new NameSpy(connection);
        }

        #region "[rgn] Public Properties "
        public ConnectionProvider Connection { get; set; }
        public TibiaMemoryProvider Memory { get { return Connection.Memory; } }
        public Player Player { get { return new Player(Connection); } }
        public LevelSpy LevelSpy { get; private set; }
        public NameSpy NameSpy { get; private set; }
        #endregion

        /// <summary>
        /// Gets the player SQM.
        /// </summary>
        /// <returns></returns>
        public Sqm GetPlayerSqm() {
            for (uint i = 0; i < (Memory.Addresses.Map.TilesMax + 1); i++) {
                var sqm = new Sqm(i, this, Player.Location);
                var res = (from tile in sqm.Tiles where tile.TileId == 0x63 && tile.Data == Player.Id select tile);
                if (res.Count() > 0) { return sqm; }
            }
            return null;
        }

        /// <summary>
        /// Gets the SQM.
        /// </summary>
        /// <param name="loc">The loc.</param>
        /// <returns></returns>
        public Sqm GetSQM(Location loc) {
            var playerSqm = GetPlayerSqm();
            var memoryLocation = WorldToMemory(loc, playerSqm);
            var sqmNumber = GetSqmNumber(memoryLocation);
            return new Sqm(sqmNumber, this, playerSqm);
        }

        /// <summary>
        /// Gets all SQMS.
        /// </summary>
        /// <returns></returns>
        public List<Sqm> GetAllSqms() {
            var sqms = new List<Sqm>();
            var playerSqm = GetPlayerSqm();
            for (uint i = 0; i < (Memory.Addresses.Map.TilesMax + 1); i++) {
                sqms.Add(new Sqm(i, this, playerSqm));
            }
            return sqms;
        }

        /// <summary>
        /// Gets the SQMS with object.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <returns></returns>
        public List<Sqm> GetSqmsWithObject(uint objectId) {
            var sqms = new List<Sqm>();
            var playerSqm = GetPlayerSqm();
            for (uint i = 0; i < (Memory.Addresses.Map.TilesMax + 1); i++) {
                var sqm = new Sqm(i, this, playerSqm);
                if (sqm.Contains(objectId)) { sqms.Add(sqm); }
            }
            return sqms;
        }

        /// <summary>
        /// Replaces the objects.
        /// </summary>
        /// <param name="oldObject">The old object.</param>
        /// <param name="newObject">The new object.</param>
        public void ReplaceObjects(uint oldObject, uint newObject) {
            var playerSqm = GetPlayerSqm();
            for (uint i = 0; i < (Memory.Addresses.Map.TilesMax + 1); i++) {
                var sqm = new Sqm(i, this, playerSqm);
                var tiles = (from tile in sqm.Tiles where tile.TileId == oldObject select tile);
                if (tiles.Count() > 0) { tiles.ToList().ForEach(t => t.TileId = newObject); }
            }
        }

        /// <summary>
        /// Gets the SQM location.
        /// </summary>
        /// <param name="sqmNumber">The SQM number.</param>
        /// <returns></returns>
        internal Location GetSqmLocation(uint sqmNumber) {
            var location = new Location();
            location.Z = sqmNumber / (14 * 18);
            location.Y = (sqmNumber - location.Z * 14 * 18) / 18;
            location.X = (sqmNumber - location.Z * 14 * 18) - location.Y * 18;
            return location;
        }

        /// <summary>
        /// Gets the SQM number.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        internal uint GetSqmNumber(Location location) {
            return (location.X + location.Y * 18 + location.Z * 14 * 18).ToUInt32();
        }

        /// <summary>
        /// SQMs the number to SQM address.
        /// </summary>
        /// <param name="sqmNumber">The SQM number.</param>
        /// <returns></returns>
        internal uint SqmNumberToSqmAddress(uint sqmNumber) {
            uint mapBegin = Memory.Reader.Uint(Memory.Addresses.Map.Pointer);
            uint address = mapBegin + (Memory.Addresses.Map.StepDist * sqmNumber);
            return address;
        }
        
        /// <summary>
        /// Gets the SQms.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        public SqmCollection GetSQMs(int range, Location location) {
            var sqms = new SqmCollection();
            var centerSqm = ((range - 1) / 2);
            int rows = range, columns = range;

            for (int r = 0; r < rows; r++) {
                for (int c = 0; c < columns; c++) {

                    var loc = new Location();
                    loc.Z = location.Z;
                    loc.X = location.X + ((uint)((-centerSqm) + c));
                    loc.Y = location.Y + ((uint)((-centerSqm) + r));
                    sqms.Add(GetSQM(loc));
                }
            }
            return sqms;
        }

        /// <summary>
        /// Gets the SQms.
        /// </summary>
        /// <param name="screens">The screens.</param>
        /// <param name="allflorrs">if set to <c>true</c> [allflorrs].</param>
        /// <returns></returns>
        public SqmCollection GetSQMs(int screens, bool allflorrs) {
            var sqms = GetSQMs(screens * 7, Player.Location);
            return allflorrs ? sqms : new SqmCollection(sqms.Where(s => s.WorldLocation.Z == Player.Location.Z).ToList());
        }

        /// <summary>
        /// Gets the SQms.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public SqmCollection GetSQMs(int range) {
            return GetSQMs(range, Player.Location);
        }

        /// <summary>
        /// Activates the full light.
        /// </summary>
        public void ActivateFullLight() {
            Memory.Writer.Bytes(Memory.Addresses.Map.FullLightNop, Memory.Addresses.Map.FullLightNopValue);
            Memory.Writer.Uint(Memory.Addresses.Map.FullLight, Memory.Addresses.Map.FullLightValue);
        }

        /// <summary>
        /// Deactivates the full light.
        /// </summary>
        public void DeactivateFullLight() {
            Memory.Writer.Bytes(Memory.Addresses.Map.FullLightNop, Memory.Addresses.Map.FullLightNopDefault);
            Memory.Writer.Uint(Memory.Addresses.Map.FullLight, Memory.Addresses.Map.FullLightDefault);
        }

        /// <summary>
        /// Memories to world.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="playerSqm">The player SQM.</param>
        /// <returns></returns>
        internal Location MemoryToWorld(Location location, Sqm playerSqm) {
            var memoryLocation = playerSqm.MemoryLocation;
            uint x = Player.Location.X - memoryLocation.X;
            uint y = Player.Location.Y - memoryLocation.Y;
            uint z = Player.Location.Z - memoryLocation.Z;
            return new Location(location.X + x, location.Y + y, location.Z + z);
        }

        /// <summary>
        /// Worlds to memory.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="playerSqm">The player SQM.</param>
        /// <returns></returns>
        internal Location WorldToMemory(Location location, Sqm playerSqm) {
            var memoryLocation = playerSqm.MemoryLocation;
            uint x = Player.Location.X - memoryLocation.X;
            uint y = Player.Location.Y - memoryLocation.Y;
            uint z = Player.Location.Z - memoryLocation.Z;
            return new Location(location.X - x, location.Y - y, location.Z - z);
        }
    }
}
