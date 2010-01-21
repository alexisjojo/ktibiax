namespace KTibiaX.Windows.Features.Hunt {
    partial class frm_Cavebot {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Cavebot));
            this.ribbonControl1 = new KTibiaX.UI.Controls.CustomRibbon();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.imgSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.btnRun = new DevExpress.XtraBars.BarButtonItem();
            this.menuRunScript = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOpen = new DevExpress.XtraBars.BarButtonItem();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnOutput = new DevExpress.XtraBars.BarButtonItem();
            this.btnIssues = new DevExpress.XtraBars.BarButtonItem();
            this.btnPause = new DevExpress.XtraBars.BarButtonItem();
            this.btnStop = new DevExpress.XtraBars.BarButtonItem();
            this.btnCompile = new DevExpress.XtraBars.BarButtonItem();
            this.imgLarge = new DevExpress.Utils.ImageCollection(this.components);
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rgFile = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rgCode = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.barAndDockingController2 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.openScriptDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuRunScript)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLarge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController2)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationIcon = null;
            this.ribbonControl1.Controller = this.barAndDockingController1;
            this.ribbonControl1.Images = this.imgSmall;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnRun,
            this.btnOpen,
            this.btnNew,
            this.btnEdit,
            this.btnOutput,
            this.btnIssues,
            this.btnPause,
            this.btnStop,
            this.btnCompile,
            this.barButtonItem10});
            this.ribbonControl1.LargeImages = this.imgLarge;
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 11;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.SelectedPage = this.ribbonPage1;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.ShowOnMultiplePages;
            this.ribbonControl1.Size = new System.Drawing.Size(915, 96);
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // imgSmall
            // 
            this.imgSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSmall.ImageStream")));
            // 
            // btnRun
            // 
            this.btnRun.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.btnRun.Caption = " Run Script ";
            this.btnRun.DropDownControl = this.menuRunScript;
            this.btnRun.Enabled = false;
            this.btnRun.Id = 0;
            this.btnRun.LargeImageIndex = 33;
            this.btnRun.Name = "btnRun";
            this.btnRun.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRun_ItemClick);
            // 
            // menuRunScript
            // 
            this.menuRunScript.BottomPaneControlContainer = null;
            this.menuRunScript.ItemLinks.Add(this.barButtonItem10);
            this.menuRunScript.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.LargeImagesTextDescription;
            this.menuRunScript.Name = "menuRunScript";
            this.menuRunScript.Ribbon = this.ribbonControl1;
            this.menuRunScript.RightPaneControlContainer = null;
            // 
            // barButtonItem10
            // 
            this.barButtonItem10.Caption = "Debug Script";
            this.barButtonItem10.Description = "Execute the current script in Debug mode.";
            this.barButtonItem10.Id = 10;
            this.barButtonItem10.LargeImageIndex = 29;
            this.barButtonItem10.Name = "barButtonItem10";
            // 
            // btnOpen
            // 
            this.btnOpen.Caption = " Open Script ";
            this.btnOpen.Id = 1;
            this.btnOpen.LargeImageIndex = 15;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOpen_ItemClick);
            // 
            // btnNew
            // 
            this.btnNew.Caption = " Create New ";
            this.btnNew.Id = 2;
            this.btnNew.LargeImageIndex = 12;
            this.btnNew.Name = "btnNew";
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_ItemClick);
            // 
            // btnEdit
            // 
            this.btnEdit.Caption = "Edit Script";
            this.btnEdit.Enabled = false;
            this.btnEdit.Id = 3;
            this.btnEdit.LargeImageIndex = 13;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEdit_ItemClick);
            // 
            // btnOutput
            // 
            this.btnOutput.Caption = " Output View ";
            this.btnOutput.Enabled = false;
            this.btnOutput.Id = 4;
            this.btnOutput.LargeImageIndex = 4;
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOutput_ItemClick);
            // 
            // btnIssues
            // 
            this.btnIssues.Caption = " Code Issues ";
            this.btnIssues.Enabled = false;
            this.btnIssues.Id = 5;
            this.btnIssues.LargeImageIndex = 3;
            this.btnIssues.Name = "btnIssues";
            this.btnIssues.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnIssues_ItemClick);
            // 
            // btnPause
            // 
            this.btnPause.Caption = "  Pause  ";
            this.btnPause.Enabled = false;
            this.btnPause.Id = 6;
            this.btnPause.LargeImageIndex = 32;
            this.btnPause.Name = "btnPause";
            this.btnPause.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPause_ItemClick);
            // 
            // btnStop
            // 
            this.btnStop.Caption = "   Stop   ";
            this.btnStop.Enabled = false;
            this.btnStop.Id = 7;
            this.btnStop.LargeImageIndex = 30;
            this.btnStop.Name = "btnStop";
            this.btnStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStop_ItemClick);
            // 
            // btnCompile
            // 
            this.btnCompile.Caption = "Compile Script";
            this.btnCompile.Enabled = false;
            this.btnCompile.Id = 9;
            this.btnCompile.LargeImageIndex = 17;
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCompile_ItemClick);
            // 
            // imgLarge
            // 
            this.imgLarge.ImageSize = new System.Drawing.Size(32, 32);
            this.imgLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgLarge.ImageStream")));
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.rgFile,
            this.rgCode});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnRun);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnPause, true);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnStop);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Execution";
            // 
            // rgFile
            // 
            this.rgFile.ItemLinks.Add(this.btnOpen);
            this.rgFile.ItemLinks.Add(this.btnNew, true);
            this.rgFile.ItemLinks.Add(this.btnEdit);
            this.rgFile.Name = "rgFile";
            this.rgFile.Text = "File";
            // 
            // rgCode
            // 
            this.rgCode.ItemLinks.Add(this.btnCompile);
            this.rgCode.ItemLinks.Add(this.btnOutput, true);
            this.rgCode.ItemLinks.Add(this.btnIssues);
            this.rgCode.Name = "rgCode";
            this.rgCode.Text = "Code";
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.Controller = this.barAndDockingController1;
            this.xtraTabbedMdiManager1.HeaderButtons = DevExpress.XtraTab.TabButtons.None;
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.navBarControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 96);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(5);
            this.panelControl1.Size = new System.Drawing.Size(179, 518);
            this.panelControl1.TabIndex = 8;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.ContentButtonHint = null;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.None;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup2});
            this.navBarControl1.Location = new System.Drawing.Point(5, 5);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 169;
            this.navBarControl1.OptionsNavPane.ShowExpandButton = false;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControl1.Size = new System.Drawing.Size(169, 508);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "General Options";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "Features Monitor";
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // barAndDockingController2
            // 
            this.barAndDockingController2.PropertiesBar.AllowLinkLighting = false;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(179, 96);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 518);
            this.splitterControl1.TabIndex = 10;
            this.splitterControl1.TabStop = false;
            // 
            // openScriptDialog
            // 
            this.openScriptDialog.Filter = "Text Files|*.txt";
            // 
            // frm_Cavebot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 614);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frm_Cavebot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cavebot";
            this.Load += new System.EventHandler(this.frm_Cavebot_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuRunScript)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLarge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KTibiaX.UI.Controls.CustomRibbon ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController2;
        private DevExpress.Utils.ImageCollection imgSmall;
        private DevExpress.Utils.ImageCollection imgLarge;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraBars.BarButtonItem btnRun;
        private DevExpress.XtraBars.BarButtonItem btnOpen;
        private DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rgFile;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rgCode;
        private DevExpress.XtraBars.BarButtonItem btnEdit;
        private DevExpress.XtraBars.BarButtonItem btnOutput;
        private DevExpress.XtraBars.BarButtonItem btnIssues;
        private DevExpress.XtraBars.BarButtonItem btnPause;
        private DevExpress.XtraBars.BarButtonItem btnStop;
        private DevExpress.XtraBars.BarButtonItem btnCompile;
        private DevExpress.XtraBars.BarButtonItem barButtonItem10;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu menuRunScript;
        private System.Windows.Forms.OpenFileDialog openScriptDialog;

    }
}