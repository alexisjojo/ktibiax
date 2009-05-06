namespace Tibia.Connection.CriptoProviders {

    public interface ICryptoManager {
        void InitializeKey();
        ConnectionProvider ConnectionSource { get; set; }

        CryptoType Type { get; set; }
        byte[] Key { get; }
        uint KeyAddress { get; set; }

        byte[] Cryptograph(byte[] Data);
        byte[] Cryptograph(byte[] Data, byte[] Key);

        byte[] DeCryptograph(byte[] Data);
        byte[] DeCryptograph(byte[] Data, byte[] Key);
    }
}
