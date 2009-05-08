namespace Keyrox.Builder.Features.AutoList {
    partial class AutoListBox {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoListBox));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.imgSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.xtraPageAutoList = new DevExpress.XtraTab.XtraTabPage();
            this.lstAutoList = new DevExpress.XtraEditors.ImageListBoxControl();
            this.imgAutoList = new DevExpress.Utils.ImageCollection(this.components);
            this.AutoListToolTip = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmall)).BeginInit();
            this.xtraPageAutoList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstAutoList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAutoList)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.xtraTabControl1.Appearance.Options.UseBackColor = true;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.xtraTabControl1.Images = this.imgSmall;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.LookAndFeel.SkinName = "Liquid Sky";
            this.xtraTabControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.Center;
            this.xtraTabControl1.SelectedTabPage = this.xtraPageAutoList;
            this.xtraTabControl1.Size = new System.Drawing.Size(165, 152);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraPageAutoList});
            // 
            // imgSmall
            // 
            this.imgSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSmall.ImageStream")));
            // 
            // xtraPageAutoList
            // 
            this.xtraPageAutoList.Controls.Add(this.lstAutoList);
            this.xtraPageAutoList.ImageIndex = 8;
            this.xtraPageAutoList.Name = "xtraPageAutoList";
            this.xtraPageAutoList.Padding = new System.Windows.Forms.Padding(5);
            this.xtraPageAutoList.Size = new System.Drawing.Size(137, 148);
            this.xtraPageAutoList.Text = "Page";
            // 
            // lstAutoList
            // 
            this.lstAutoList.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lstAutoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAutoList.ImageList = this.imgAutoList;
            this.lstAutoList.Location = new System.Drawing.Point(5, 5);
            this.lstAutoList.LookAndFeel.SkinName = "London Liquid Sky";
            this.lstAutoList.LookAndFeel.UseWindowsXPTheme = true;
            this.lstAutoList.Name = "lstAutoList";
            this.lstAutoList.Size = new System.Drawing.Size(127, 138);
            this.lstAutoList.TabIndex = 0;
            this.lstAutoList.DoubleClick += new System.EventHandler(this.lstAutoList_DoubleClick);
            this.lstAutoList.SelectedIndexChanged += new System.EventHandler(this.lstAutoList_SelectedIndexChanged);
            this.lstAutoList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstAutoList_KeyDown);
            // 
            // imgAutoList
            // 
            this.imgAutoList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgAutoList.ImageStream")));
            // 
            // AutoListBox
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LookAndFeel.UseWindowsXPTheme = true;
            this.Name = "AutoListBox";
            this.Size = new System.Drawing.Size(165, 152);
            this.AutoListToolTip.SetSuperTip(this, null);
            this.VisibleChanged += new System.EventHandler(this.AutoListBox_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgSmall)).EndInit();
            this.xtraPageAutoList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstAutoList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAutoList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraPageAutoList;
        private DevExpress.Utils.ImageCollection imgSmall;
        private DevExpress.XtraEditors.ImageListBoxControl lstAutoList;
        private DevExpress.Utils.ToolTipController AutoListToolTip;
        public DevExpress.Utils.ImageCollection imgAutoList;
    }
}