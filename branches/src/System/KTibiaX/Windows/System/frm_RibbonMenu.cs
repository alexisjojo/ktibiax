using System;
using System.Drawing;
using System.Windows.Forms;
using Tibia.Client;
using KTibiaX.UI.Controls;
using KTibiaX.IPChanger.Data;
using Keyrox.Shared.Controls;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using KTibiaX.Modules;
using KTibiaX.Windows.Features.Player;
using KTibiaX.UI.Components;
using KTibiaX.Windows.Configuration;
using KTibiaX.Windows.Features.Hunt;
using KTibiaX.Windows.Features.Development;

namespace KTibiaX.Windows {
    public partial class frm_RibbonMenu : RibbonForm {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        public frm_RibbonMenu() {
            InitializeComponent();
            dockOutputPanel.Size = new Size(812, 261);
            CenterStartupPanel();
        }

        #region "[rgn] Public Properties    "
        public System.Diagnostics.Process ClientProcess { get; set; }
        public TibiaClient TibiaClient { get; set; }
        public LoginServer SelectedLoginServer { get; set; }
        public KTibiaXState LoginState { get; set; }
        public event EventHandler LoginStateChanged;
        public FeatureFormManager FormManager { get; set; }
        public LoginServer SelectedServer { get; set; }
        #endregion

        /// <summary>
        /// Handles the Load event of the frm_RibbonMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void frm_RibbonMenu_Load(object sender, EventArgs e) {
            InitializeStartupControls();
            ShowClientLauncher();
        }

        #region "[rgn] Startup Ctrl Helpers "
        private void InitializeStartupControls() {
            FormManager = new FeatureFormManager(this);

            Ribbon.Minimized = true;
            CenterStartupPanel();
            ChangeRibbonState(false);

            LoginStateChanged += LoginState_Changed;
            dockOutputPanel.ClosingPanel += dockOutputPanel_ClosingPanel;

            TMLogin.Start();
            TMFeatures.Start();
        }
        private void dockOutputPanel_ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e) {
            btnOutput.Checked = false;
        }
        private void ReorganizeHostControls() {
            if (pnHost.CurrentProcess != null) {
                pnHost.CenterProcesswindow();
            }
            else { CenterStartupPanel(); }
        }
        private void CenterStartupPanel() {
            MethodCall.ExecuteSafeThreadIn(new Callback(delegate() {
                var x = (pnHost.Width) - (pnLogo.Width) + 3;
                var y = (pnHost.Height) - (pnLogo.Height) + 3;
                pnLogo.Location = new Point(x, y);
                pnLogo.Visible = true;
            }), 100);
        }
        private void ChangeRibbonState(bool enable) {
            foreach (RibbonPage page in Ribbon.Pages) {
                if (page.Name != rbConfig.Name && page.Name != rbDev.Name && page.Name != rbHelp.Name) {
                    foreach (RibbonPageGroup group in page.Groups) {
                        foreach (BarItemLink item in group.ItemLinks) {
                            item.Item.Enabled = enable;
                        }
                    }
                }
            }
        }
        private void TMFeatures_Tick(object sender, EventArgs e) {
            btnLight.Down = FormManager.IsStarted<frm_Light>();
            btnSpeed.Down = FormManager.IsStarted<frm_Speed>();
            btnBattleList.Down = FormManager.IsStarted<frm_SpyBattleList>();
            btnOutfit.Down = FormManager.IsStarted<frm_Outfit>();
            btnTrade.Down = FormManager.IsStarted<frm_TradeHelper>();
        }
        #endregion

        #region "[rgn] Launch and Hosting   "
        private void btnStartClient_ItemClick(object sender, ItemClickEventArgs e) {
            ShowClientLauncher();
        }
        private void ShowClientLauncher() {
            this.BeginInvoke(new Callback(delegate() {
                var frmIPChanger = new KTibiaX.IPChanger.frm_StartClient();
                frmIPChanger.ClientOpenComplete += frmIPChanger_ClientOpenComplete;
                frmIPChanger.FormClosed += new FormClosedEventHandler(frmIPChanger_FormClosed);
                frmIPChanger.Show(this);
                MethodCall.ExecuteSafeThreadIn(new Callback(delegate() { frmIPChanger.Focus(); }), 500);
            }));
        }
        private void frmIPChanger_FormClosed(object sender, FormClosedEventArgs e) {
            if (((KTibiaX.IPChanger.frm_StartClient)sender).TibiaClient == null) {
                Application.ExitThread();
            }
        }
        private void frmIPChanger_ClientOpenComplete(object sender, ProcessEventArgs e) {
            Output.Add("Client: ", e.ClientProcess.MainModule.FileName, " (version: ", e.ClientProcess.MainModule.FileVersionInfo.ProductVersion, ") was started!");

            if (e.Server != null) { Output.Add("Selected Login Server: ", e.Server.Name, "(", e.Server.Ip, ":", e.Server.Port.ToString(), ")"); }
            else { Output.Add("Tibia global login server was selected."); }

            ClientProcess = e.ClientProcess;
            SelectedServer = e.Server;
            this.pnHost.Visible = true;
            ((KTibiaX.IPChanger.frm_StartClient)sender).Close();

            pnHost.CurrentProcess = e.ClientProcess;
            Callback host = pnHost.HostProcess;
            host.BeginInvoke(HostProcess_Complete, host);
        }
        private void HostProcess_Complete(IAsyncResult result) {
            Output.Add("Current client was hosted successfully.");
            Output.AddSeparator();
            Invoke(new Callback(() => {
                pnHost.FixHostProcess();
                pnHost.CurrentProcess.EnableRaisingEvents = true;
                pnLogo.Visible = false;
                ribbonStatusBar1.Visible = false;
            }));
            this.Invoke(new Callback(delegate() { ConnectClient(); }));
        }
        #endregion

        #region "[rgn] Ribbon Event Handler "
        private void ribbon_MouseEnter(object sender, EventArgs e) {
            var hi = Ribbon.CalcHitInfo(Cursor.Position);
            if (hi.Page != null) {
                if (ribbon.Minimized) {
                    ribbon.Minimized = false;
                    ReorganizeHostControls();
                    Keyrox.Shared.WindowsAPI.SwitchToThisWindow(Handle, true);
                }
            }
        }
        private void ribbon_MouseLeave(object sender, EventArgs e) {
            MethodCall.ExecuteSafeThreadIn(new Callback(delegate() {
                if (!ribbon.Minimized) {
                    ribbon.Minimized = true;
                    ReorganizeHostControls();
                    if (TibiaClient != null) {
                        Keyrox.Shared.WindowsAPI.SwitchToThisWindow(TibiaClient.MainWindowHandle, true);
                    }
                }
            }), 300);
        }
        private void ribbon_MouseMove(object sender, MouseEventArgs e) {
            var hi = ribbon.CalcHitInfo(e.Location);
            if (hi.Page != null && ribbon.SelectedPage.Name != hi.Page.Name) {
                Ribbon.Minimized = false;
                ribbon.SelectedPage = hi.Page;
                if (ActiveForm == null) {
                    MethodCall.ExecuteSafeThreadIn(new Callback(delegate() { Keyrox.Shared.WindowsAPI.SwitchToThisWindow(Handle, true); }), 250);
                }
            }
        }
        private void ribbon_MouseClick(object sender, MouseEventArgs e) {
            var hi = ribbon.CalcHitInfo(e.Location);
            if (hi.Page != null && ribbon.SelectedPage.Name != hi.Page.Name) {
                Ribbon.Minimized = false;
                ribbon.SelectedPage = hi.Page;
                if (ActiveForm == null) {
                    MethodCall.ExecuteSafeThreadIn(new Callback(delegate() { Keyrox.Shared.WindowsAPI.SwitchToThisWindow(Handle, true); }), 250);
                }
            }
        }
        private void ribbon_MinimizedChanged(object sender, EventArgs e) {
            ReorganizeHostControls();
        }
        private void frm_RibbonMenu_SizeChanged(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Maximized) { FixMaximizeBounds(); }
            ReorganizeHostControls();
        }
        private void FixMaximizeBounds() {
            WindowState = FormWindowState.Normal;
            var w = Screen.PrimaryScreen.Bounds.Width;
            var h = Screen.PrimaryScreen.Bounds.Height - 30;
            Location = new Point(0, 0);
            Size = new Size(w, h);
        }
        #endregion

        #region "[rgn] Login Listener       "
        private void TMLogin_Tick(object sender, EventArgs e) {
            if (TibiaClient != null && TibiaClient.Connection != null && TibiaClient.IsConnected && TibiaClient.Features.Player.IsConnected) {

                var playerInfo = string.Format(" {0} {1}", TibiaClient.Features.Player.Name, TibiaClient.Features.Player.Level);
                if (lblPlayerInfo.Caption != playerInfo) { lblPlayerInfo.Caption = playerInfo; }

                var serverInfo = string.Format(" {0}:{1}", TibiaClient.Connection.IPServer.ToString(), TibiaClient.Connection.ServerPort);
                if (lblServerInfo.Caption != serverInfo) { lblServerInfo.Caption = serverInfo; }

                if (LoginState == KTibiaXState.LoggedOut) {
                    LoginState = KTibiaXState.LoggedIn;
                    if (LoginStateChanged != null) { LoginStateChanged(this, EventArgs.Empty); }
                }
            }
            else if (TibiaClient == null || TibiaClient.Connection == null || !TibiaClient.IsConnected || !TibiaClient.Features.Player.IsConnected && (LoginState == KTibiaXState.LoggedIn)) {
                LoginState = KTibiaXState.LoggedOut;
                if (LoginStateChanged != null) { LoginStateChanged(this, EventArgs.Empty); }
            }
        }
        private void LoginState_Changed(object sender, EventArgs e) {
            if (LoginState == KTibiaXState.LoggedIn) {
                ChangeRibbonState(true);
                TibiaClient.Actions.Message.System.Send("Welcome to KTibiaX!", Tibia.Features.SystemMsgColor.Red);
                CheckAutoStart();
                //TibiaClient.Actions.Message.System.SendAnimated(Tibia.Features.AnimatedMsgColor.Orange, TibiaClient.Features.Player.Location, "Have Fun!");
            }
            else {
                FormManager.StopAll();
                ChangeRibbonState(false);
                lblPlayerInfo.Caption = string.Empty;
                lblServerInfo.Caption = string.Empty;                
            }
        }
        #endregion

        #region "[rgn] Ribbon Item Handler  "
        private void btnOutput_CheckedChanged(object sender, ItemClickEventArgs e) {
            if (btnOutput.Checked) {
                dockOutputPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                outputView1.ScrollOutput();
            }
            else { dockOutputPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden; }
        }
        private void btnAddress_ItemClick(object sender, ItemClickEventArgs e) {
            var frmAddress = new frm_Address();
            frmAddress.ShowDialog();
        }
        private void btnLight_ItemClick(object sender, ItemClickEventArgs e) {
            FormManager.Show<frm_Light>(TibiaClient);
        }
        private void btnSpeed_ItemClick(object sender, ItemClickEventArgs e) {
            FormManager.Show<frm_Speed>(TibiaClient);
        }
        private void btnBattleList_ItemClick(object sender, ItemClickEventArgs e) {
            FormManager.Show<frm_SpyBattleList>(TibiaClient);
        }
        private void btnOutfit_ItemClick(object sender, ItemClickEventArgs e) {
            FormManager.Show<frm_Outfit>(TibiaClient);
        }
        private void btnTrade_ItemClick(object sender, ItemClickEventArgs e) {
            FormManager.Show<frm_TradeHelper>(TibiaClient);
        }
        private void btnScript_ItemClick(object sender, ItemClickEventArgs e) {
            MessageBox.Show("TODO: Fazer um form que exiba informações detalhadas de cada script!");
        }
        private void btnCavebot_ItemClick(object sender, ItemClickEventArgs e) {
            var frmEditor = new Keyrox.Builder.Features.frm_Editor();
            frmEditor.TibiaClient = TibiaClient;
            frmEditor.Show();
        }
        private void btnPacketListener_ItemClick(object sender, ItemClickEventArgs e) {
            FormManager.Show<frm_Packets>(TibiaClient);
        }
        private void btnMapReader_ItemClick(object sender, ItemClickEventArgs e) {
            var frmMap = new Features.Graphics.frm_Graphics();
            frmMap.TibiaClient = this.TibiaClient;
            frmMap.Show();
        }
        private void btnBPReader_ItemClick(object sender, ItemClickEventArgs e) {
            FormManager.Show<frm_BPReader>(TibiaClient);
        }
        #endregion

        /// <summary>
        /// Connects the client.
        /// </summary>
        public void ConnectClient() {
            if (ClientProcess != null && !ClientProcess.HasExited) {
                Output.Add("Connecting Tibia Client on server: ", SelectedServer.Ip, ":", SelectedServer.Port.ToString());
                TibiaClient = new TibiaClient(ClientProcess);

                var connect = new Callback(delegate() { TibiaClient.Connect(SelectedServer.Ip, SelectedServer.Port, SelectedServer.IsOtServer); });
                connect.BeginInvoke(ConnectionComplete, connect);
            }
        }

        /// <summary>
        /// Connections the complete.
        /// </summary>
        /// <param name="result">The result.</param>
        private void ConnectionComplete(IAsyncResult result) {
            Output.Add("Tibia Client is successfully connected!");
        }

        /// <summary>
        /// Checks the auto start.
        /// </summary>
        private void CheckAutoStart() {
            this.BeginInvoke(new Callback(delegate() {
                FormManager.StartIfNeeded<frm_Light>("frm_Light", TibiaClient);
                FormManager.StartIfNeeded<frm_Speed>("frm_Speed", TibiaClient);
                FormManager.StartIfNeeded<frm_Outfit>("frm_Outfit", TibiaClient);
                FormManager.StartIfNeeded<frm_TradeHelper>("frm_TradeHelper", TibiaClient);
            }));
        }

        /// <summary>
        /// Handles the FormClosing event of the frm_Main control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void frm_Main_FormClosing(object sender, FormClosingEventArgs e) {
            if (pnHost.CurrentProcess != null) {
                var result = MessageBox.Show("Tibia client will be closed! Are you shure?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) {
                    e.Cancel = true;
                    return;
                }
                else { pnHost.CurrentProcess.Kill(); System.Threading.Thread.Sleep(1000); }
            }
        }

    }
}