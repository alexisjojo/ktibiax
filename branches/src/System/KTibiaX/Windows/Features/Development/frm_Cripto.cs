using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Tibia.Client;
using Keyrox.Shared.Objects;
using System.Runtime.InteropServices;
using Tibia.Connection;
using Tibia.Connection.Core;

namespace KTibiaX.Windows.Features {
    public partial class frm_Cripto : DevExpress.XtraEditors.XtraForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Cripto"/> class.
        /// </summary>
        public frm_Cripto() { InitializeComponent(); }

        /// <summary>
        /// Handles the Load event of the frm_Cripto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_Cripto_Load(object sender, EventArgs e) {

        }

        #region "[rgn] Public Properties   "
        private byte[] key;
        public byte[] Key { get { return key; } set { key = value; } }
        public TibiaClient TibiaClient { get; set; }

        public byte[] DecData { get; set; }
        public byte[] EncData { get; set; }
        #endregion

        #region "[rgn] Private Form Events "
        private void FormEncript() {
            try {
                if (!string.IsNullOrEmpty(txtData.Text)) { DecData = txtData.Text.GetBytes(true); }
                if (!string.IsNullOrEmpty(txtKey.Text)) { Key = txtKey.Text.GetBytes(true); }

                byte[] NewData = Cryptograph(DecData);
                EncData = NewData;
                txtCriptoData.Text = EncData.GetString(true);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void FormDecript() {
            try {
                if (!string.IsNullOrEmpty(txtCriptoData.Text)) { EncData = txtCriptoData.Text.GetBytes(true); }
                if (!string.IsNullOrEmpty(txtKey.Text)) { Key = txtKey.Text.GetBytes(true); }

                byte[] NewData = DeCryptograph(EncData);
                DecData = NewData;
                txtData.Text = DecData.GetString(true);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void LoadGameKey() {
            var procs = Process.GetProcesses();
            Process.GetProcesses().ToList().ForEach(pr => FixTibiaPrc(pr));

            if (TibiaClient != null) {
                TibiaClient.Connection.Memory.Reader.Byte(TibiaClient.Connection.Memory.Addresses.Client.XTeaKey, 16, out key);
                txtKey.Text = Key.GetString(true);
                return;
            }
            MessageBox.Show("Tibia Client Not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void FixTibiaPrc(Process prc) {
            if (prc.ProcessName == "Tibia") {
                TibiaClient = new TibiaClient(prc);
                TibiaClient.Connection = new Local(TibiaClient.Memory, TibiaClient.Process.Id);
                TibiaClient.Connection.Connect();
            }
        }
        #endregion

        #region "[rgn] Form Control Events "
        private void btnCripto_Click(object sender, EventArgs e) {
            FormEncript();
        }
        private void btnDecripto_Click(object sender, EventArgs e) {
            FormDecript();
        }
        private void btnKey_Click(object sender, EventArgs e) {
            LoadGameKey();
        }
        private void btnExit_Click(object sender, EventArgs e) {
            Application.ExitThread();
        }
        #endregion

        #region "[rgn] Form Extern Methods "
        [DllImport("crackd.dll")]
        protected static extern int EncipherTibiaProtectedSP(
            [MarshalAs(UnmanagedType.U1)] ref byte firstPacketByte,
            [MarshalAs(UnmanagedType.U1)] ref byte firstKeyByte,
            int uboundpacket,
            int uboundkey);

        [DllImport("crackd.dll")]
        protected static extern int DecipherTibiaProtectedSP(
            [MarshalAs(UnmanagedType.U1)] ref byte firstPacketByte,
            [MarshalAs(UnmanagedType.U1)] ref byte firstKeyByte,
            int uboundpacket,
            int uboundkey);

        [DllImport("crackd.dll")]
        protected static extern int GetTibiaCRC(
            [MarshalAs(UnmanagedType.U1)] ref byte firstPacketByte,
            int uboundpacketMinus6);
        #endregion

        public byte[] Cryptograph(byte[] data) {
            //InitializeKey();
            byte[] NewByte = Format(data);

            var ErrorCode = EncipherTibiaProtectedSP(
                ref NewByte[0], ref Key[0], NewByte.Length - 1, Key.Length - 1);
            FillCRCBytes(ref NewByte);

            if (ErrorCode == 0) { return NewByte; }
            throw GetFormatException(ErrorCode);
        }

        public byte[] DeCryptograph(byte[] data) {
            if (data.Length >= 8) {
                //InitializeKey();
                byte[] NewByte = data;

                var ErrorCode = DecipherTibiaProtectedSP(ref NewByte[0], ref Key[0], NewByte.Length - 1, Key.Length - 1);
                RemmCRCBytes(ref NewByte);

                return NewByte;
                throw GetFormatException(ErrorCode);
            }
            throw GetFormatException(-5);
        }

        public byte[] Format(byte[] data) {
            try {
                if (data.Length > 0) {
                    //Calcule the new packet size.
                    var totalLong = Convert.ToInt32(new[] { data[0], data[1] }.GetTheLong());
                    var extrab = 8 - ((totalLong + 2) % 8);
                    if (extrab < 8) { totalLong = totalLong + extrab; }

                    //Construct the new Byte.
                    totalLong = totalLong + 6;
                    var goodPacket = new byte[totalLong + 2];

                    //Update the Data and Header.
                    Array.Copy(data, 0, goodPacket, 6, data.Length);
                    goodPacket[0] = Convert.ToUInt32(goodPacket.Length - 2).LowByteOfLong();
                    goodPacket[1] = Convert.ToUInt32(goodPacket.Length - 2).HighByteOfLong();

                    //Return the formated Byte.
                    return goodPacket;
                }
                return data;
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex.InnerException); }
        }

        public void FillCRCBytes(ref byte[] data) {
            var theDamnCRC = GetTibiaCRC(ref data[6], data.Length - 6); //(number of bytes - 6)
            var crc = BitConverter.GetBytes(theDamnCRC);
            data[2] = crc[0];
            data[3] = crc[1];
            data[4] = crc[2];
            data[5] = crc[3];
        }

        public void RemmCRCBytes(ref byte[] data) {
            //Remove CRC four bytes and Fix Packet length.
            var newData = new byte[data.Length - 6];
            Array.Copy(data, 6, newData, 0, (data.Length - 6));

            var trueSize = new byte[] { newData[0], newData[1] }.GetTheLong();
            var trueData = new byte[trueSize + 2];

            Array.Copy(newData, 0, trueData, 0, trueData.Length);
            data = trueData;
        }

        public static FormatException GetFormatException(int errorCode) {
            switch (errorCode) {
                case -1: return new FormatException("Packet header is not multiplier of 8");
                case -2: return new FormatException("Wrong size of key (ubound must be 15)");
                case -3: return new FormatException("Header of packet doesn't match with real size of the packet");
                case -4: return new FormatException("This is not a packet");
                case -5: return new FormatException("This is not a criptographed packet");
            }
            return new FormatException("Unknow Criptograph Error!");
        }
    }
}