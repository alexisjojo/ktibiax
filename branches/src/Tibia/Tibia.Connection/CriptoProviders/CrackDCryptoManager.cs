using System;
using System.Runtime.InteropServices;

namespace Tibia.Connection.CriptoProviders {
    public class CrackDCryptoManager : ICryptoManager {

        #region " Object Constructors   "
        public CrackDCryptoManager(ConnectionProvider source) {
            ConnectionSource = source;
            Type = CryptoType.Xtea;
            InitializeKey();
        }
        public CrackDCryptoManager(CryptoType type, ConnectionProvider source)
            : this(source) {
            Type = type;
            InitializeKey();
        }
        public CrackDCryptoManager(byte[] key, CryptoType type, ConnectionProvider source)
            : this(type, source) {
            Key = key;
        }
        #endregion

        #region " Extern Methods Import "
        /// <summary>
        /// Cryptograph Extern Function.
        /// </summary>
        /// <returns></returns>
        [DllImport("crackd.dll")]
        protected static extern int EncipherTibiaProtected([MarshalAs(UnmanagedType.U1)] ref byte firstPacketByte, [MarshalAs(UnmanagedType.U1)] ref byte firstKeyByte, int uboundpacket, int uboundkey);

        /// <summary>
        /// Decryptograph Extern Function.
        /// </summary>
        /// <returns></returns>
        [DllImport("crackd.dll")]
        protected static extern int DecipherTibiaProtected([MarshalAs(UnmanagedType.U1)] ref byte firstPacketByte, [MarshalAs(UnmanagedType.U1)] ref byte firstKeyByte, int uboundpacket, int uboundkey);
        #endregion

        /// <summary>
        /// Load Cryptograph Key.
        /// </summary>
        public void InitializeKey() {
            if (Type == CryptoType.Xtea) {
                KeyAddress = ConnectionSource.Memory.Addresses.Client.XTeaKey;
                ConnectionSource.Memory.Reader.Byte(KeyAddress, 16, out key);
            }
            //else {
            //TODO: Implements the RSA Key Constructor. */ 
            //}
        }

        #region " Private Obj Variables "
        private byte[] key;
        #endregion

        #region " Public Obj Properties "
        /// <summary>
        /// Cryptograph Type.
        /// </summary>
        public CryptoType Type { get; set; }
        /// <summary>
        /// Cryptograph Key.
        /// </summary>
        public byte[] Key { get { return key; } set { key = value; } }
        /// <summary>
        /// Cryptograph Key Memory Address.
        /// </summary>
        public uint KeyAddress { get; set; }
        /// <summary>
        /// Source Client Connection.
        /// </summary>
        public ConnectionProvider ConnectionSource { get; set; }
        #endregion

        #region " Cryptograph Functions "
        /// <summary>
        /// Cryptograph desired Data.
        /// </summary>
        public byte[] Cryptograph(byte[] data) {
            InitializeKey(); byte[] NewByte = Format(data);
            var ErrorCode = EncipherTibiaProtected(ref NewByte[0], ref Key[0], NewByte.Length - 1, Key.Length - 1);

            if (ErrorCode == 0) { return NewByte; }
            throw GetFormatException(ErrorCode);
        }
        /// <summary>
        /// Cryptograph desired Data.
        /// </summary>
        public byte[] CryptographCharList(byte[] data) {
            InitializeKey(); byte[] NewByte = Format(data, true);
            var ErrorCode = EncipherTibiaProtected(ref NewByte[0], ref Key[0], NewByte.Length - 1, Key.Length - 1);

            if (ErrorCode == 0) { return NewByte; }
            throw GetFormatException(ErrorCode);
        }
        /// <summary>
        /// Cryptograph desired Data with defined Key.
        /// </summary>
        public byte[] Cryptograph(byte[] data, byte[] tibiaKey) {
            InitializeKey(); byte[] NewByte = Format(data);
            var ErrorCode = EncipherTibiaProtected(ref NewByte[0], ref tibiaKey[0], NewByte.Length - 1, tibiaKey.Length - 1);

            if (ErrorCode == 0) { return NewByte; }
            throw GetFormatException(ErrorCode);
        }
        /// <summary>
        /// Decryptograph desired Data.
        /// </summary>
        public byte[] DeCryptograph(byte[] data) {
            if (data.Length >= 8) {
                InitializeKey(); byte[] NewByte = data;
                var ErrorCode = DecipherTibiaProtected(ref NewByte[0], ref Key[0], NewByte.Length - 1, Key.Length - 1);

                if (ErrorCode == 0) { return UnFormat(NewByte); }
                throw GetFormatException(ErrorCode);
            }
            return new byte[1];
        }
        /// <summary>
        /// Decryptograph desired Data.
        /// </summary>
        public byte[] DeCryptograph(byte[] data, bool characterList) {
            var NewByte = data; InitializeKey();
            var ErrorCode = DecipherTibiaProtected(ref NewByte[0], ref Key[0], NewByte.Length - 1, Key.Length - 1);

            if (ErrorCode == 0) {
                if (!characterList) {
                    return UnFormat(NewByte);
                }
                return NewByte;
            }
            throw GetFormatException(ErrorCode);
        }
        /// <summary>
        /// Decryptograph desired Data with defined Key.
        /// </summary>
        public byte[] DeCryptograph(byte[] data, byte[] tibiaKey) {
            var NewByte = data;
            var ErrorCode = DecipherTibiaProtected(ref NewByte[0], ref tibiaKey[0], NewByte.Length - 1, tibiaKey.Length - 1);

            if (ErrorCode == 0) { return UnFormat(NewByte); }
            throw GetFormatException(ErrorCode);
        }
        /// <summary>
        /// Prepare Packet to Encryptation.
        /// </summary>
        /// <returns></returns>
        public byte[] Format(byte[] data) {
            try {
                if (data.Length > 0) {
                    //Calcule the new packet size.
                    var totalLong = Convert.ToInt32(new[] { data[0], data[1] }.GetTheLong());
                    var extrab = 8 - ((totalLong + 2) % 8);
                    if (extrab < 8) { totalLong = totalLong + extrab; }

                    //Construct the new Byte.
                    totalLong = totalLong + 2;
                    var goodPacket = new byte[totalLong + 2];

                    //Update the Data and Header.
                    Array.Copy(data, 0, goodPacket, 2, data.Length);
                    goodPacket[0] = totalLong.LowByteOfLong();
                    goodPacket[1] = totalLong.HighByteOfLong();

                    //Return the formated Byte.
                    return goodPacket;
                }
                return data;
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex.InnerException); }
        }
        /// <summary>
        /// Prepare Packet to Encryptation.
        /// </summary>
        /// <returns></returns>
        public byte[] Format(byte[] data, bool charList) {
            if (data.Length > 0) {
                //Calcula o novo tamanho do Byte de Retorno.
                var newSize = ((data.Length - 2) * 8);
                var newByte = new byte[newSize + 2];

                //Transfere as informações do Byte existente.
                Array.Copy(data, 2, newByte, 2, data.Length - 2);

                //Insere o novo tamanho no byte de retorno.
                newByte[0] = newSize.LowByteOfLong();
                newByte[1] = newSize.HighByteOfLong();

                //Retorna o Novo Byte pronto para Encriptar.
                return newByte;
            }
            return data;
        }
        /// <summary>
        /// Prepare Packet after Encryptation.
        /// </summary>
        /// <returns></returns>
        public byte[] UnFormat(byte[] data) {
            try {
                if (data.Length > 0) {
                    //Calcula o Tamanho do Byte após a Decryptação.
                    var nNewSize = Convert.ToInt32(new[] { data[2], data[3] }.GetTheLong());

                    //Teste para breakpoint.
                    if (nNewSize < data.Length) {

                        //Insere no Novo Byte o Tamanho Real.
                        var nNewByte = new byte[nNewSize + 2];
                        nNewByte[0] = data[2];
                        nNewByte[1] = data[3];

                        //Carrega os Valores do Byte Decryptado.
                        Array.Copy(data, 4, nNewByte, 2, nNewSize);

                        //Retorna o Novo Byte pronto para Encriptar.
                        return nNewByte;
                    }
                }
                return data;
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex.InnerException); }
        }
        /// <summary>
        /// Get the Correct Format Exception from defined Error Code.
        /// </summary>
        /// <returns></returns>
        public static FormatException GetFormatException(int errorCode) {
            switch (errorCode) {
                case -1: return new FormatException("Packet header is not multiplier of 8");
                case -2: return new FormatException("Wrong size of key (ubound must be 15)");
                case -3: return new FormatException("Header of packet doesn't match with real size of the packet");
                case -4: return new FormatException("This is not a packet");
            }
            return new FormatException("Unknow Criptograph Error!");
        }
        #endregion

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
