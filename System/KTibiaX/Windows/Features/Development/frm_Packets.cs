using System;
using System.Collections.Generic;
using KTibiaX.UI.Controls;
using Tibia.Connection.Model;

namespace KTibiaX.Windows.Features.Development {
    public partial class frm_Packets : BaseFeatureForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_Packets"/> class.
        /// </summary>
        public frm_Packets() {
            InitializeComponent();
            btnStop.Enabled = false;
        }

        /// <summary>
        /// Gets or sets the packet source.
        /// </summary>
        /// <value>The packet source.</value>
        public List<PacketDTO> PacketSource { get; set; }

        #region "[rgn] Event Handler "
        private void btnStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            StartListener();
        }
        private void btnStop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            StopListener();
        }
        private void Connection_PacketChanged(object sender, Tibia.Connection.Events.PacketEventArgs e) {
            PacketSource.Add(new PacketDTO(e.Packet, e.Source.ToString()));
            gridControl1.RefreshDataSource();
            this.BeginInvoke(new Callback(delegate() { bandedGridView1.MakeRowVisible(PacketSource.Count - 1, false); }));
        }
        private void btnView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var frmView = new frm_PackView();
            frmView.SetText(PacketSource[bandedGridView1.FocusedRowHandle].Packet.Data.GetString(false));
            frmView.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Starts the listener.
        /// </summary>
        public void StartListener() {
            PacketSource = new List<PacketDTO>();
            gridControl1.DataSource = PacketSource;
            gridControl1.Refresh();
            btnStop.Enabled = true;
            btnStart.Enabled = false;
            this.TibiaClient.Connection.PacketChanged += Connection_PacketChanged;
        }

        /// <summary>
        /// Stops the listener.
        /// </summary>
        public void StopListener() {
            btnStop.Enabled = false;
            btnStart.Enabled = true;
            this.TibiaClient.Connection.PacketChanged -= Connection_PacketChanged;
        }

    }

    public class PacketDTO {

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketDTO"/> class.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="source">The source.</param>
        public PacketDTO(Packet packet, string source) {
            this.Packet = packet;
            Date = DateTime.Now;
            Source = source;
            Length = packet.Data.Length;
            Content = packet.Data.GetString(true);

            if (packet.InType != Tibia.Connection.IncomingPacketType.Unknow) { Type = packet.InType.ToString(); }
            else if (packet.OutType != Tibia.Connection.OutgoingPacketType.Unknow) { Type = packet.OutType.ToString(); }
        }

        public DateTime Date { get; set; }

        public int Length { get; set; }

        public string Content { get; set; }

        public string Source { get; set; }

        public string Type { get; set; }

        public Packet Packet { get; set; }

    }
}