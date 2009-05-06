using System;

namespace Tibia.Connection.Packets {
	public class Packet : IPacket {

		/// <summary>
		/// Default Object Constructor.
		/// </summary>
		public Packet() {
			InterceptedDate = DateTime.Now;
		}

		#region " Private Obj Variables "
		private byte[] data, encryptedData;
		private ConnectionProvider connectionSource;
		private Exception cryptoException;
		#endregion

		#region " Public Obj Properties "
		/// <summary>
		/// Packet ID.
		/// </summary>
		public uint Id {
			get {
				if (Data != null && Data.Length > 2) {
					return Convert.ToUInt32(Data[2]);
				}
				return 0x0;
			}
			set {
				if (Data == null && Data.Length > 2) { Data[2] = Convert.ToByte(value); }
			}
		}
		/// <summary>
		/// Packet Header Size.
		/// </summary>
		public int TrueSize {
			get {
				if (Data != null && Data.Length > 1) {
					return Convert.ToInt32(new[]{Data[0], Data[1]}.GetTheLong());
				}
				return -1;
			}
		}
		/// <summary>
		/// Packet Length.
		/// </summary>
		public int DataSize {
			get {
				if (Data != null && Data.Length > 1) {
					return Data.Length;
				}
				return -1;
			}
		}
		/// <summary>
		/// Verify if Packet Header Matches with Packet True Size.
		/// </summary>
		public bool IsValid {
			get {
				if (TrueSize == (DataSize - 2)) {
					return true;
				}
				return false;
			}
		}
		/// <summary>
		/// Verify if Packet is a Character List Packet.
		/// </summary>
		public bool CharacterList {
			get {
				if (Data != null && Data.Length > 4) {
					if (Data[4] == 0x14) {
						return true;
					}
				}
				return false;
			}
		}
		/// <summary>
		/// Packet Decrypted Data.
		/// </summary>
		public byte[] Data {
			get {
				if (data == null && encryptedData != null && connectionSource != null) {
					switch (CryptoProvider) {
						case CryptoType.Xtea:
                            var innerPackets = PacketBuilder.GetPacketData(encryptedData);
                            if (innerPackets.Count > 0) {
                                try {
                                    data = connectionSource.Xtea.DeCryptograph(innerPackets[0]);
                                }
                                catch (FormatException) { System.Diagnostics.Debugger.Break(); }
                            }
							break;
					}
				}
				return data;
			}
			set { data = value; }
		}
		/// <summary>
		/// Packet Enctypted Data.
		/// </summary>
		public byte[] EncryptedData {
			get {
				if (encryptedData == null && data != null && connectionSource != null) {
					encryptedData = connectionSource.Xtea.Cryptograph(data);
				}
				return encryptedData;
			}
			set { encryptedData = value; }
		}
		/// <summary>
		/// Connection Source of this Packet.
		/// </summary>
		public ConnectionProvider ConnectionSource {
			get { return connectionSource; }
			set { connectionSource = value; }
		}
		/// <summary>
		/// Packet Data Source.
		/// </summary>
        public PacketSource PacketSource { get; set; }
		/// <summary>
		/// Cryptograph Provider.
		/// </summary>
        public CryptoType CryptoProvider { get; set; }
		/// <summary>
		/// Packet Data Type.
		/// </summary>
		public PacketType PacketType {
			get { return GetPacketType(Id); }
		}
		/// <summary>
		/// Time of Packet Interception.
		/// </summary>
        public DateTime InterceptedDate { get; set; }
		/// <summary>
		/// Exeception Generated on Cryptograph Operation.
		/// </summary>
		public Exception CryptoException {
			get { return cryptoException; }
		}
		#endregion

		/// <summary>
		/// Returns the Hexadecimal Representation of Dectypted Data.
		/// </summary>
		public string ToHexString() {
			if (IsValid) {
				return Data.GetString(true);
			}
			return "";
		}

		/// <summary>
		/// Returns the Hexadecimal Representation of Dectypted Data.
		/// </summary>
		public string ToDecString() {
			if (IsValid) {
				return Data.GetUintString(Data.Length);
			}
			return "";
		}

		/// <summary>
		/// Returns the Hexadecimal Representation of Enctypted Data.
		/// </summary>
		public string ToCryptHexString() {
			if (IsValid) {
				return EncryptedData.GetString(true);
			}
			return "";
		}

		/// <summary>
		/// Get the Type Based on Packet ID.
		/// </summary>
		/// <param name="id">Packet Id.</param>
		/// <returns></returns>
		public static PacketType GetPacketType(uint id) {

            var data = new[] { id.LowByteOfLong(), id.HighByteOfLong() };
			switch (id) {

				case 0x1E: return PacketType.Ping;
				case 0xBE: return PacketType.Stop;
				case 0x14: return PacketType.Logout;
				case 0x8C: return PacketType.Look;
				case 0x64: return PacketType.Move;
				case 0x82: return PacketType.Use;

				case 0x6F: return PacketType.Turn;
				case 0x70: return PacketType.Turn;
				case 0x71: return PacketType.Turn;
				case 0x72: return PacketType.Turn;

				case 0x65: return PacketType.Walk;
				case 0x66: return PacketType.Walk;
				case 0x67: return PacketType.Walk;
				case 0x68: return PacketType.Walk;

				case 0x83: return PacketType.UseWith;
				case 0x78: return PacketType.Stack;

				case 0xA1: return PacketType.Attack;
				case 0xA0: return PacketType.AttackMode;
				case 0xD3: return PacketType.Outfit;

				case 0xB4: return PacketType.SystemMSG;
				case 0xAA: return PacketType.ArrivedMSG;
			}
			if (data[0] == 0x96) {
				if (data[1] == 0x01) { return PacketType.DefaultMSG; }
				if (data[1] == 0x02) { return PacketType.DefaultMSG; }
				if (data[1] == 0x03) { return PacketType.DefaultMSG; }
				if (data[1] == 0x04) { return PacketType.PrivateMSG; }
				if (data[1] == 0x05) { return PacketType.TradeMSG; }
			}
			return PacketType.Unknow;
		}

		/// <summary>
		/// Set an Exception from Cryptograph 
		/// </summary>
		internal void SetCriptoException(Exception ex) {
			cryptoException = ex;
		}

		/// <summary>
		/// Set a Connection Source.
		/// </summary>
		public void SetConnectionProvider(ConnectionProvider connection) {
			connectionSource = connection;
		}

		/// <summary>
		/// Returns the String Representation of the Packet.
		/// </summary>
		public override string ToString() {
			return string.Format("Length: {0}", TrueSize);
		}
	}
}
