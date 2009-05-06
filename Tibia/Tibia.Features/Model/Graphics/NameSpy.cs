using Tibia.Memory;
using Tibia.Connection;
using Tibia.Connection.Providers;

namespace Tibia.Features.Graphics {
    public class NameSpy {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameSpy"/> class.
        /// </summary>
        /// <param name="connectionSource">The connection source.</param>
        public NameSpy(ConnectionProvider connectionSource) {
            this.Connection = connectionSource;
        }

        #region "[rgn] Public Properties "
        public ConnectionProvider Connection { get; private set; }
        public TibiaMemoryProvider Memory { get { return Connection.Memory; } }
        #endregion
        /// <summary>
        /// Activate Name Spy.
        /// </summary>
        public void Activate() {
            uint nameSpyNop;
            Memory.Reader.Uint(Memory.Addresses.SpyLevel.NameSpy1, 2, out nameSpyNop);

            uint nameSpyNop2;
            Memory.Reader.Uint(Memory.Addresses.SpyLevel.NameSpy2, 2, out nameSpyNop2);

            if ((nameSpyNop == Memory.Addresses.SpyLevel.NameSpy1Default) || (nameSpyNop2 == Memory.Addresses.SpyLevel.NameSpy2Default)) {
                WriteNops(Memory.Addresses.SpyLevel.NameSpy1, 2);
                WriteNops(Memory.Addresses.SpyLevel.NameSpy2, 2);
            }
        }

        /// <summary>
        /// Deactivate Name Spy.
        /// </summary>
        public void Deactivate() {
            Memory.Writer.Uint(Memory.Addresses.SpyLevel.NameSpy1, Memory.Addresses.SpyLevel.NameSpy1Default, 2);
            Memory.Writer.Uint(Memory.Addresses.SpyLevel.NameSpy2, Memory.Addresses.SpyLevel.NameSpy2Default, 2);
        }

        /// <summary>
        /// Write Memory Nop's.
        /// </summary>
        private void WriteNops(uint address, uint nops) {
            for (uint i = 0; i < nops; i++) {
                Memory.Writer.Uint(address + i, 0x90, 1);
            }
        }
    }
}
