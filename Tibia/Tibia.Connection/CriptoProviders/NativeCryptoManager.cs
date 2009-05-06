using System;
using System.Runtime.InteropServices;

namespace Tibia.Connection.CriptoProviders {
    public class NativeCryptoManager : ICryptoManager {

        #region " Object Constructors   "
        public NativeCryptoManager(ConnectionProvider source) {
            ConnectionSource = source;
            Type = CryptoType.Xtea;
            InitializeKey();
        }
        public NativeCryptoManager(CryptoType type, ConnectionProvider source)
            : this(source) {
            Type = type;
            InitializeKey();
        }
        public NativeCryptoManager(byte[] key, CryptoType type, ConnectionProvider source)
            : this(type, source) {
            Key = key;
        }
        #endregion

        /// <summary>
        /// Load Cryptograph Key.
        /// </summary>
        public void InitializeKey() {
            if (Type == CryptoType.Xtea) {
                byte[] outKey;
                KeyAddress = ConnectionSource.Memory.Addresses.Client.XTeaKey;
                ConnectionSource.Memory.Reader.Byte(KeyAddress, 16, out outKey);
                Key = outKey;
            }
            //else {
            //TODO: Implements the RSA Key Constructor. */ 
            //}
        }

        #region " Public Obj Properties "
        /// <summary>
        /// Cryptograph Type.
        /// </summary>
        public CryptoType Type { get; set; }
        /// <summary>
        /// Cryptograph Key.
        /// </summary>
        public byte[] Key { get; private set; }
        /// <summary>
        /// Cryptograph Key Memory Address.
        /// </summary>
        public uint KeyAddress { get; set; }
        /// <summary>
        /// Source Client Connection.
        /// </summary>
        public ConnectionProvider ConnectionSource { get; set; }
        #endregion

        
        /// <summary>
        /// Cryptograph desired Data.
        /// </summary>
        public byte[] Cryptograph(byte[] data) {
            AnalysePacketIntegrity(data, false);
            return Encrypt(data, Key);
        }
        /// <summary>
        /// Cryptograph desired Data with defined Key.
        /// </summary>
        public byte[] Cryptograph(byte[] data, byte[] tibiaKey) {
            AnalysePacketIntegrity(data, false);
            return Encrypt(data, tibiaKey);
        }
        /// <summary>
        /// Decryptograph desired Data.
        /// </summary>
        public byte[] DeCryptograph(byte[] data) {
            AnalysePacketIntegrity(data, true);
            return Decrypt(data, Key);
        }
        /// <summary>
        /// Decryptograph desired Data with defined Key.
        /// </summary>
        public byte[] DeCryptograph(byte[] data, byte[] tibiaKey) {
            AnalysePacketIntegrity(data, true);
            return Decrypt(data, tibiaKey);
        }

        #region "[rgn] Cryptograph Functions "
        private byte[] Decrypt(byte[] packet, byte[] key) {
            if (packet.Length == 0)
                return packet;

            // The first two bytes are the length
            byte[] payload = new byte[packet.Length - 2];

            Array.Copy(packet, 2, payload, 0, payload.Length);

            uint[] payloadprep = payload.ToUintArray();
            uint[] keyprep = key.ToUintArray();

            for (int i = 0; i < payloadprep.Length; i += 2) {
                Decode(payloadprep, i, keyprep);
            }

            // Remove the junk bytes
            byte[] decrypted = payloadprep.ToByteArray();
            int length = BitConverter.ToInt16(decrypted, 0) + 2;
            byte[] decryptedprep = new byte[length];
            Array.Copy(decrypted, decryptedprep, length);

            return decryptedprep;
            //return decrypted;
        }
        private byte[] Encrypt(byte[] packet, byte[] key) {
            if (packet.Length == 0)
                return packet;

            uint[] keyprep = key.ToUintArray();

            // Pad the packet with extra bytes for encryption
            int pad = packet.Length % 8;
            byte[] packetprep = new byte[packet.Length + (8 - pad)];
            Array.Copy(packet, packetprep, packet.Length);

            uint[] payloadprep = packetprep.ToUintArray();

            for (int i = 0; i < payloadprep.Length; i += 2) {
                Encode(payloadprep, i, keyprep);
            }

            byte[] encrypted = new byte[packetprep.Length + 2];

            Array.Copy(payloadprep.ToByteArray(), 0, encrypted, 2, packetprep.Length);

            Array.Copy(BitConverter.GetBytes((short)packetprep.Length), 0, encrypted, 0, 2);

            return encrypted;
        }
        private void Encode(uint[] v, int index, uint[] k) {
            uint y = v[index];
            uint z = v[index + 1];
            uint sum = 0;
            uint delta = 0x9e3779b9;
            uint n = 32;

            while (n-- > 0) {
                y += (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
                sum += delta;
                z += (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
            }

            v[index] = y;
            v[index + 1] = z;
        }
        private void Decode(uint[] v, int index, uint[] k) {
            try {
                uint n = 32;
                uint sum;
                uint y = v[index];
                uint z = v[index + 1];
                uint delta = 0x9e3779b9;

                sum = delta << 5;

                while (n-- > 0) {
                    z -= (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
                    sum -= delta;
                    y -= (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
                }

                v[index] = y;
                v[index + 1] = z;
            }
            catch {

            }
        }
        #endregion

        /// <summary>
        /// Analyses the packet integrity.
        /// </summary>
        public void AnalysePacketIntegrity(byte[] data, bool decrypting) {
            if (data.Length < 3) 
                throw new FormatException("Invalid packet!");
            
            if (data.GetDataValueLenght() != data.Length) 
                throw new FormatException("Header of packet doesn't match with real size of the packet");

            if(decrypting && data.GetDataValueLenght() % 8 != 0)
                throw new FormatException("Packet header is not multiplier of 8");
        }

        /// <summary>
        /// Return the Hexadecimal Represetantion of this Key.
        /// </summary>
        public override string ToString() {
            if (Key != null) {
                return "[Key] " + Key.GetString(true);
            }
            return "Key was not Initialized!";
        }

    }
}
