using System;

namespace Tibia.Connection.Packets
{
    public interface IPacket
    {
        uint Id { get; set; }
        int DataSize { get; }
        int TrueSize { get; }

        byte[] Data { get; set; }
        byte[] EncryptedData { get; set; }

        ConnectionProvider ConnectionSource { get; set; }
        PacketSource PacketSource { get; set; }
        PacketType PacketType { get; }

        string ToHexString();
        string ToDecString();
        string ToCryptHexString();
        
        Exception CryptoException { get; }
        bool IsValid { get; }
        DateTime InterceptedDate { get; set; }
    }
}
