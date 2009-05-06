namespace KTibiaX.Windows.Features.Scripts.Controls {
    partial class ctrl_AutoList {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrl_AutoList));
            this.imgTab = new DevExpress.Utils.ImageCollection(this.components);
            this.autoListTab = new DevExpress.XtraTab.XtraTabControl();
            this.pageLang = new DevExpress.XtraTab.XtraTabPage();
            this.lstLang = new DevExpress.XtraEditors.ImageListBoxControl();
            this.imgAutoList = new System.Windows.Forms.ImageList(this.components);
            this.pagePlayer = new DevExpress.XtraTab.XtraTabPage();
            this.lstPlayer = new DevExpress.XtraEditors.ImageListBoxControl();
            this.pageItems = new DevExpress.XtraTab.XtraTabPage();
            this.lstItems = new DevExpress.XtraEditors.ImageListBoxControl();
            this.pageSections = new DevExpress.XtraTab.XtraTabPage();
            this.lstSections = new DevExpress.XtraEditors.ImageListBoxControl();
            this.ListToolTipController = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgTab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoListTab)).BeginInit();
            this.autoListTab.SuspendLayout();
            this.pageLang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstLang)).BeginInit();
            this.pagePlayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstPlayer)).BeginInit();
            this.pageItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstItems)).BeginInit();
            this.pageSections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstSections)).BeginInit();
            this.SuspendLayout();
            // 
            // imgTab
            // 
            this.imgTab.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgTab.ImageStream")));
            // 
            // autoListTab
            // 
            this.autoListTab.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.autoListTab.Appearance.Options.UseBackColor = true;
            this.autoListTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoListTab.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.False;
            this.autoListTab.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.autoListTab.Images = this.imgTab;
            this.autoListTab.Location = new System.Drawing.Point(0, 0);
            this.autoListTab.LookAndFeel.SkinName = "Liquid Sky";
            this.autoListTab.LookAndFeel.UseDefaultLookAndFeel = false;
            this.autoListTab.Name = "autoListTab";
            this.autoListTab.SelectedTabPage = this.pageLang;
            this.autoListTab.ShowHeaderFocus = DevExpress.Utils.DefaultBoolean.False;
            this.autoListTab.Size = new System.Drawing.Size(157, 176);
            this.autoListTab.TabIndex = 2;
            this.autoListTab.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageLang,
            this.pagePlayer,
            this.pageItems,
            this.pageSections});
            this.autoListTab.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.autoListTab_SelectedPageChanged);
            // 
            // pageLang
            // 
            this.pageLang.Controls.Add(this.lstLang);
            this.pageLang.ImageIndex = 11;
            this.pageLang.Name = "pageLang";
            this.pageLang.Padding = new System.Windows.Forms.Padding(5);
            this.pageLang.Size = new System.Drawing.Size(153, 149);
            this.pageLang.Text = "Lang";
            // 
            // lstLang
            // 
            this.lstLang.Appearance.BackColor = System.Drawing.Color.White;
            this.lstLang.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.lstLang.Appearance.Options.UseBackColor = true;
            this.lstLang.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lstLang.DisplayMember = "Title";
            this.lstLang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLang.ImageIndexMember = "ImageIndex";
            this.lstLang.ImageList = this.imgAutoList;
            this.lstLang.Location = new System.Drawing.Point(5, 5);
            this.lstLang.Name = "lstLang";
            this.lstLang.Size = new System.Drawing.Size(143, 139);
            this.lstLang.TabIndex = 0;
            this.lstLang.ValueMember = "Text";
            // 
            // imgAutoList
            // 
            this.imgAutoList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgAutoList.ImageStream")));
            this.imgAutoList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgAutoList.Images.SetKeyName(0, "enumitem.png");
            this.imgAutoList.Images.SetKeyName(1, "ExtensionMethod.png");
            this.imgAutoList.Images.SetKeyName(2, "GenericArgument.png");
            this.imgAutoList.Images.SetKeyName(3, "Namespace.png");
            this.imgAutoList.Images.SetKeyName(4, "Operator.png");
            this.imgAutoList.Images.SetKeyName(5, "Property.png");
            this.imgAutoList.Images.SetKeyName(6, "ProtectedConstant.png");
            this.imgAutoList.Images.SetKeyName(7, "PublicDelegate.png");
            this.imgAutoList.Images.SetKeyName(8, "PublicEvent.png");
            this.imgAutoList.Images.SetKeyName(9, "Keyword.png");
            this.imgAutoList.Images.SetKeyName(10, "PublicMethod.png");
            this.imgAutoList.Images.SetKeyName(11, "XmlTag.png");
            this.imgAutoList.Images.SetKeyName(12, "Warning.png");
            // 
            // pagePlayer
            // 
            this.pagePlayer.Controls.Add(this.lstPlayer);
            this.pagePlayer.Name = "pagePlayer";
            this.pagePlayer.Padding = new System.Windows.Forms.Padding(5);
            this.pagePlayer.Size = new System.Drawing.Size(171, 200);
            this.pagePlayer.Text = "Player";
            // 
            // lstPlayer
            // 
            this.lstPlayer.Appearance.BackColor = System.Drawing.Color.White;
            this.lstPlayer.Appearance.Options.UseBackColor = true;
            this.lstPlayer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lstPlayer.DisplayMember = "Title";
            this.lstPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPlayer.ImageIndexMember = "ImageIndex";
            this.lstPlayer.ImageList = this.imgAutoList;
            this.lstPlayer.Location = new System.Drawing.Point(5, 5);
            this.lstPlayer.Name = "lstPlayer";
            this.lstPlayer.Size = new System.Drawing.Size(161, 190);
            this.lstPlayer.TabIndex = 0;
            this.lstPlayer.ValueMember = "Text";
            // 
            // pageItems
            // 
            this.pageItems.Controls.Add(this.lstItems);
            this.pageItems.Name = "pageItems";
            this.pageItems.Padding = new System.Windows.Forms.Padding(5);
            this.pageItems.Size = new System.Drawing.Size(171, 200);
            this.pageItems.Text = "Items";
            // 
            // lstItems
            // 
            this.lstItems.Appearance.BackColor = System.Drawing.Color.White;
            this.lstItems.Appearance.Options.UseBackColor = true;
            this.lstItems.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lstItems.DisplayMember = "Title";
            this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItems.ImageIndexMember = "ImageIndex";
            this.lstItems.ImageList = this.imgAutoList;
            this.lstItems.Location = new System.Drawing.Point(5, 5);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(161, 190);
            this.lstItems.TabIndex = 1;
            this.lstItems.ValueMember = "Text";
            // 
            // pageSections
            // 
            this.pageSections.Controls.Add(this.lstSections);
            this.pageSections.Name = "pageSections";
            this.pageSections.Padding = new System.Windows.Forms.Padding(5);
            this.pageSections.Size = new System.Drawing.Size(171, 200);
            this.pageSections.Text = "Sections";
            // 
            // lstSections
            // 
            this.lstSections.Appearance.BackColor = System.Drawing.Color.White;
            this.lstSections.Appearance.Options.UseBackColor = true;
            this.lstSections.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lstSections.DisplayMember = "Title";
            this.lstSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSections.ImageIndexMember = "ImageIndex";
            this.lstSections.ImageList = this.imgAutoList;
            this.lstSections.Location = new System.Drawing.Point(5, 5);
            this.lstSections.Name = "lstSections";
            this.lstSections.Size = new System.Drawing.Size(161, 190);
            this.lstSections.TabIndex = 2;
            this.lstSections.ValueMember = "Text";
            // 
            // ctrl_AutoList
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.autoListTab);
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "ctrl_AutoList";
            this.Size = new System.Drawing.Size(157, 176);
            this.ListToolTipController.SetSuperTip(this, null);
            ((System.ComponentModel.ISupportInitialize)(this.imgTab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoListTab)).EndInit();
            this.autoListTab.ResumeLayout(false);
            this.pageLang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstLang)).EndInit();
            this.pagePlayer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstPlayer)).EndInit();
            this.pageItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstItems)).EndInit();
            this.pageSections.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstSections)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imgTab;
        private DevExpress.XtraTab.XtraTabControl autoListTab;
        private DevExpress.XtraTab.XtraTabPage pageLang;
        private DevExpress.XtraEditors.ImageListBoxControl lstLang;
        private DevExpress.XtraTab.XtraTabPage pagePlayer;
        private DevExpress.XtraEditors.ImageListBoxControl lstPlayer;
        private DevExpress.XtraTab.XtraTabPage pageItems;
        private DevExpress.XtraEditors.ImageListBoxControl lstItems;
        private System.Windows.Forms.ImageList imgAutoList;
        private DevExpress.Utils.ToolTipController ListToolTipController;
        private DevExpress.XtraTab.XtraTabPage pageSections;
        private DevExpress.XtraEditors.ImageListBoxControl lstSections;
    }
}
