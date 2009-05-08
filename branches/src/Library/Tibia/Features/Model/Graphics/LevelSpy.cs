using Tibia.Memory;
using Tibia.Features.Model;
using Keyrox.Shared.Objects;
using Tibia.Connection.Providers;
using Tibia.Features.Structures;
using Tibia.Features.Providers;

namespace Tibia.Features.Graphics {
    /// <summary>
    /// Classe de Controle do Spy Level.
    /// </summary>
    public class LevelSpy {
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelSpy"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public LevelSpy(ConnectionProvider connection) {
            this.Connection = connection;
        }

        #region " Internal Object Properties "
        public bool IsActivated { get; private set; }
        public uint OriginalZ { get; set; }
        public int CurrentFloor { get; set; }
        public Light OriginalLight { get; set; }
        public Player Player { get { return new Player(Connection); } }
        public ConnectionProvider Connection { get; private set; }
        public TibiaMemoryProvider Memory { get { return Connection.Memory; } }
        #endregion

        /// <summary>
        /// Activate Level Spy.
        /// </summary>
        public void Activate() {
            if (IsActivated) return;

            CurrentFloor = 0;
            OriginalZ = Player.Location.Z;
            OriginalLight = Player.Light;
            new GraphicsProvider(Connection).ActivateFullLight();

            Memory.Writer.Bytes(Memory.Addresses.SpyLevel.LevelSpy1, Memory.Addresses.SpyLevel.Nops);
            Memory.Writer.Bytes(Memory.Addresses.SpyLevel.LevelSpy2, Memory.Addresses.SpyLevel.Nops);
            Memory.Writer.Bytes(Memory.Addresses.SpyLevel.LevelSpy3, Memory.Addresses.SpyLevel.Nops);
            IsActivated = true;
        }

        /// <summary>
        /// Deactivate Level Spy.
        /// </summary>
        public void DeActivate() {
            if (!IsActivated) return;
            
            CurrentFloor = 0;
            Player.Location.Z = OriginalZ;
            Player.Light = OriginalLight;
            new GraphicsProvider(Connection).DeactivateFullLight();

            Memory.Writer.Bytes(Memory.Addresses.SpyLevel.LevelSpy1, Memory.Addresses.SpyLevel.LevelSpyDefault);
            Memory.Writer.Bytes(Memory.Addresses.SpyLevel.LevelSpy2, Memory.Addresses.SpyLevel.LevelSpyDefault);
            Memory.Writer.Bytes(Memory.Addresses.SpyLevel.LevelSpy3, Memory.Addresses.SpyLevel.LevelSpyDefault);
            IsActivated = false;
        }

        /// <summary>
        /// Spies up.
        /// </summary>
        public void SpyUp() {
            CurrentFloor++;
            if (!SwithFloor(CurrentFloor)) { DeActivate(); }
        }

        /// <summary>
        /// Spies down.
        /// </summary>
        public void SpyDown() {
            if (CurrentFloor > 0) {
                CurrentFloor--;
                if (!SwithFloor(CurrentFloor)) { DeActivate(); }
            }
            else { DeActivate(); }
        }

        /// <summary>
        /// Swithes the floor.
        /// </summary>
        /// <param name="floor">The floor.</param>
        /// <returns></returns>
        public bool SwithFloor(int floor) {
            if (!IsActivated) Activate();
            uint pointer = Memory.Reader.Uint(Memory.Addresses.SpyLevel.LevelSpyPtr) + Memory.Addresses.SpyLevel.LevelSpyAdd1;
            pointer = Memory.Reader.Uint(pointer) + Memory.Addresses.SpyLevel.LevelSpyAdd2;

            if (floor > -1 && floor < 8) {
                Player.Location.Z = floor.ToUInt32();
                Memory.Writer.Uint(pointer, floor.ToUInt32());
                Player.Light = new Light(LightIntensity.Huge.GetHashCode().ToUInt32(), LightColor.White.GetHashCode().ToUInt32());
                return true;
            }
            return false;
        }
    }
}
