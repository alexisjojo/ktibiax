using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KTibiaX.UI.Controls;
using Keyrox.Shared.Objects;
using KTibiaX.Core.Model.DTO;
using Tibia.Connection.Model;
using Tibia.Connection.Common;
using Keyrox.Shared.Controls;
using System.Net.NetworkInformation;

namespace KTibiaX.Windows.Features.Development {
    public partial class frm_DataTest : BaseFeatureForm {
        public frm_DataTest() {
            InitializeComponent();
        }


        private List<PlayerList> chars = new List<PlayerList>();

        private void btnLoad_Click(object sender, EventArgs e) {

            //var data = "68 00 14 1F 00 33 32 0A 53 65 6A 61 20 62 65 6D 20 76 69 6E 64 6F 20 61 6F 20 54 69 62 69 61 20 38 2E 34 21 64 02 0F 00 41 63 63 6F 75 6E 74 20 4D 61 6E 61 67 65 72 09 00 46 6F 72 67 6F 74 74 65 6E 05 D0 66 B3 03 1C 0D 00 5B 43 4D 5D 20 43 68 61 6C 65 72 6F 6E 09 00 46 6F 72 67 6F 74 74 65 6E 05 D0 66 B3 03 1C 00 00 ".GetBytes(true);
            var data = "68 00 14 1F 00 33 32 0A 53 65 6A 61 20 62 65 6D 20 76 69 6E 64 6F 20 61 6F 20 54 69 62 69 61 20 38 2E 34 21 64 02 0F 00 41 63 63 6F 75 6E 74 20 4D 61 6E 61 67 65 72 09 00 46 6F 72 67 6F 74 74 65 6E 05 D0 66 B3 03 1C 0D 00 5B 43 4D 5D 20 43 68 61 6C 65 72 6F 6E 09 00 46 6F 72 67 6F 74 74 65 6E 05 D0 66 B3 03 1C 00 00 ".GetBytes(true);

            var packet = new Packet() { Data = data };
            var result = ParsePlayerList(packet);
            txtValor.Text = result.GetString(false);

            //var source = new CustomListItemCollection();

            //source.Add(new CustomListItem() {
            //    Id = 1,
            //    Title = "Wagner Araujo",
            //    Description = "Araujo Alves Tecnologia Ltda."
            //});
            //source.Add(new CustomListItem() {
            //    Id = 1,
            //    Title = "Luis Inácio Lula da Silva",
            //    Description = "Republica Federativa do Brasil."
            //});
            //source.Add(new CustomListItem() {
            //    Id = 1,
            //    Title = "Fidel Castro",
            //    Description = "Republica não bem vinda Cuba."
            //});
            //lstNames.DataSource = source;
        }

        private byte[] ParsePlayerList(Packet packet) {

            var LocalIP = new byte[] { 127, 0, 0, 1 };
            var LocalPort = ((uint)7171).GetBytes().Redim(2);

            var index = 2;
            switch (packet.ReadInt(2)) {
                case 0x0A: Output.Add("Error Received: ", packet.ReadString(3)); return null;
                case 0x0B: Output.Add("For your information: ", packet.ReadString(3)); return null;
                case 0x14: Output.Add("MOTD: ", packet.ReadString(3)); index += (3 + packet.ReadString(3).Length); break;
                case 0x1E: Output.Add("Patching Message: ", packet.ReadString(3)); return null;
                case 0x20: Output.Add("New Version: ", packet.ReadString(3)); return null;
            }
            
            if (packet.ReadInt(index) == 0x64) {
                var charCount = packet.ReadInt(index + 1);


                var addler = index;
                var result = packet.Data;
                for (var i = 0; i < charCount; i++) {

                    addler += 2;
                    var charName = packet.ReadString(addler);

                    addler += charName.Length + 2;
                    var serverName = packet.ReadString(addler);

                    addler += serverName.Length + 2;
                    var serverIP = packet.ReadBytes(addler, 4);
                    result = result.Replace(LocalIP, addler);

                    addler += 4;
                    var serverPort = packet.ReadShort(addler);
                    result = result.Replace(LocalPort, addler);

                    chars.Add(new PlayerList(charName, serverName, serverIP, serverPort.ToInt32()));
                }
                return result;
            }
            return null;
        }
        public int GetNextAvailablePort(int port) {

            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            var currentPort = port;
            var res = from tcpi in tcpConnInfoArray where tcpi.LocalEndPoint.Port == currentPort select tcpi;

            while (res.Count() > 0) {
                currentPort += 1;
                res = from tcpi in tcpConnInfoArray where tcpi.LocalEndPoint.Port == currentPort select tcpi;
            }
            return currentPort;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            base.Save();
        }
    }
}