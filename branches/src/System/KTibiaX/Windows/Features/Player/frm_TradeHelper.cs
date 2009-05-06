using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KTibiaX.UI.Controls;
using Tibia.Connection;

namespace KTibiaX.Windows.Features.Player {
    public partial class frm_TradeHelper : BaseFeatureForm {
        /// <summary>
        /// Initializes a new instance of the <see cref="frm_TradeHelper"/> class.
        /// </summary>
        public frm_TradeHelper() {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frm_TradeHelper control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_TradeHelper_Load(object sender, EventArgs e) {
            btnRem.Enabled = false;
            MessagesFound = new List<string>();
        }

        #region "[rgn] Properties     "
        public bool SendToTrade {
            get { return ckTrade.Checked; }
            set { ckTrade.Checked = value; }
        }
        public bool SendToDefault {
            get { return ckYells.Checked; }
            set { ckYells.Checked = value; }
        }
        public string Message {
            get { return txtMsgToSent.Text; }
            set { txtMsgToSent.Text = value; }
        }
        public List<string> MessageToWatch {
            get {
                if (lstMsg.DataSource == null) { lstMsg.DataSource = new List<string>(); }
                return lstMsg.DataSource as List<string>;
            }
            set { lstMsg.DataSource = value; }
        }
        public string Messages {
            get {
                var res = string.Empty;
                foreach (var str in MessageToWatch) { res += string.Concat(" ", str); }
                return res;
            }
        }
        public List<string> MessagesFound { get; set; }
        public DateTime LastMessageTime { get; set; }
        public string LastMessageText { get; set; }
        #endregion

        #region "[rgn] Control Events "
        private void btnAdd_Click(object sender, EventArgs e) {
            if (txtMsgToList.Text.Length < 3) { dxErrorProvider1.SetError(txtMsgToList, "You must type a message!"); return; }
            else { dxErrorProvider1.SetError(txtMsgToList, string.Empty); }
            MessageToWatch.Add(txtMsgToList.Text);
            txtMsgToList.Text = "";
            txtMsgToList.Focus();
            lstMsg.Refresh();
        }
        private void btnRem_Click(object sender, EventArgs e) {
            MessageToWatch.RemoveAt(lstMsg.SelectedIndex);
            lstMsg.Refresh();
        }
        private void btnClear_Click(object sender, EventArgs e) {
            MessageToWatch.Clear();
            lstMsg.Refresh();
        }
        private void lstMsg_SelectedIndexChanged(object sender, EventArgs e) {
            if (lstMsg.SelectedIndex > -1) {
                btnRem.Enabled = true;
            }
            else { btnRem.Enabled = false; }
        }
        #endregion

        #region "[rgn] Form Actions   "
        public override bool CanStart() {
            if (Message != string.Empty && (SendToTrade || SendToDefault)) {
                return true;
            }
            else if (MessageToWatch.Count > 0) {
                return true;
            }
            MessageBox.Show("You must set a message to send or a message to watch, before start this feature!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        public override void OnBeforeStart() { TibiaClient.Connection.PacketChanged += Connection_PacketChanged; }
        public override void OnAfterStop() { TibiaClient.Connection.PacketChanged -= Connection_PacketChanged; }
        #endregion

        /// <summary>
        /// Called when [action execute].
        /// </summary>
        public override void OnActionExecute() {
            if (!SendToDefault && !SendToTrade) { return; }
            var elapsed = LastMessageTime != DateTime.MinValue ? DateTime.Now.Subtract(LastMessageTime) : new TimeSpan(0, 2, 0);
            if (SendToTrade && elapsed.Minutes >= 2) { Player.SayOnTrade(Message); LastMessageTime = DateTime.Now; }
            if (SendToDefault && elapsed.Minutes >= 1) { Player.YellsOnDefault(Message); LastMessageTime = DateTime.Now; }
        }

        /// <summary>
        /// Handles the PacketChanged event of the Connection control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Tibia.Connection.Events.PacketEventArgs"/> instance containing the event data.</param>
        private void Connection_PacketChanged(object sender, Tibia.Connection.Events.PacketEventArgs e) {
            if (e.Packet.PacketSource != PacketSource.Server && e.Packet.InType != IncomingPacketType.ChatMessage) { return; }

            KeyValuePair<string, string> message = Tibia.Features.Actions.Messages.Helper.GetArivedMsgInfo(e.Packet.Data);
            if (string.IsNullOrEmpty(message.Value)) { return; }
            if (!message.Value.Contains(Messages)) { return; }

            if (message.Key != Player.Name && LastMessageText.ToLower() != message.Value.ToLower()) {
                LastMessageText = message.Value;
                TibiaClient.Actions.Message.Server.Private(Player.Name, "[" + message.Key + "] - " + message.Value);
            }
        }

    }
}
