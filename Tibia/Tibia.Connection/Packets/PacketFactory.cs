using System;
using Tibia.Connection;

namespace Tibia.Connection.Packets {
    public class PacketFactory {

        public static Packet BindPacket(byte[] encryptedData, DateTime interceptedDate, ConnectionProvider connection, PacketSource source, CryptoType cripto) {
            return new Packet {
                EncryptedData = encryptedData,
                ConnectionSource = connection,
                InterceptedDate = interceptedDate,
                PacketSource = source,
                CryptoProvider = cripto
            };
        }

        public static Packet BindPacket(byte[] encryptedData, ConnectionProvider connection, CryptoType cripto) {
            return new Packet {
                EncryptedData = encryptedData,
                ConnectionSource = connection,
                InterceptedDate = DateTime.Now,
                CryptoProvider = cripto
            };
        }

        public static Packet BindPacket(byte[] data, DateTime interceptedDate, ConnectionProvider connection, PacketSource source) {
            return new Packet {
                Data = data,
                ConnectionSource = connection,
                InterceptedDate = interceptedDate,
                PacketSource = source,
            };
        }

        public static Packet BindPacket(byte[] data, ConnectionProvider connection) {
            return new Packet {
                Data = data,
                ConnectionSource = connection,
                InterceptedDate = DateTime.Now,
            };
        }

    }
}
