namespace KTibiaX.Windows.Features.Player {
    partial class frm_Light {
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
            KTibiaX.UI.Util.BaseControl baseControl1 = new KTibiaX.UI.Util.BaseControl();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Light));
            KTibiaX.UI.Util.BaseEdit baseEdit1 = new KTibiaX.UI.Util.BaseEdit();
            KTibiaX.UI.Util.BaseEdit baseEdit2 = new KTibiaX.UI.Util.BaseEdit();
            KTibiaX.UI.Util.BaseEdit baseEdit3 = new KTibiaX.UI.Util.BaseEdit();
            this.gpLight = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.ckAutomaticStart = new DevExpress.XtraEditors.CheckEdit();
            this.ddlColor = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.ddlItensity = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gpLight)).BeginInit();
            this.gpLight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckAutomaticStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlItensity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // gpLight
            // 
            this.gpLight.Controls.Add(this.layoutControl2);
            this.gpLight.Location = new System.Drawing.Point(160, 7);
            this.gpLight.Name = "gpLight";
            this.gpLight.Size = new System.Drawing.Size(192, 120);
            this.gpLight.TabIndex = 5;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.ckAutomaticStart);
            this.layoutControl2.Controls.Add(this.ddlColor);
            this.layoutControl2.Controls.Add(this.ddlItensity);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(2, 20);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(188, 98);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // ckAutomaticStart
            // 
            this.ckAutomaticStart.Location = new System.Drawing.Point(7, 69);
            this.ckAutomaticStart.Name = "ckAutomaticStart";
            this.ckAutomaticStart.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ckAutomaticStart.Properties.Appearance.Options.UseBackColor = true;
            this.ckAutomaticStart.Properties.Caption = " Activate automaticaly on Login!";
            this.ckAutomaticStart.Size = new System.Drawing.Size(175, 18);
            this.ckAutomaticStart.StyleController = this.layoutControl2;
            this.ckAutomaticStart.TabIndex = 6;
            // 
            // ddlColor
            // 
            this.ddlColor.EditValue = 215;
            this.ddlColor.Location = new System.Drawing.Point(73, 38);
            this.ddlColor.Name = "ddlColor";
            this.ddlColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ddlColor.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("White", 215, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Yellow", 192, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("LightYellow", 210, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Orange", 186, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("LightOrange", 222, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("DarkRed", 144, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Red", 180, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("LightRed", 216, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Purple", 183, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("LightPurple", 218, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("LightBlue", 227, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("LightGreen", 138, -1)});
            this.ddlColor.Size = new System.Drawing.Size(109, 20);
            this.ddlColor.StyleController = this.layoutControl2;
            this.ddlColor.TabIndex = 5;
            // 
            // ddlItensity
            // 
            this.ddlItensity.EditValue = 12;
            this.ddlItensity.Location = new System.Drawing.Point(73, 7);
            this.ddlItensity.Name = "ddlItensity";
            this.ddlItensity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ddlItensity.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Huge", 12, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Large", 8, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Medium", 6, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("None", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Small", 4, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("VeryLarge", 10, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("VerySmall", 2, -1)});
            this.ddlItensity.Size = new System.Drawing.Size(109, 20);
            this.ddlItensity.StyleController = this.layoutControl2;
            this.ddlItensity.TabIndex = 4;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(188, 98);
            this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ddlItensity;
            this.layoutControlItem4.CustomizationFormText = " Intensity: ";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(186, 31);
            this.layoutControlItem4.Text = " Intensity: ";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(61, 20);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ddlColor;
            this.layoutControlItem5.CustomizationFormText = " Light Color: ";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(186, 31);
            this.layoutControlItem5.Text = " Light Color: ";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(61, 20);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.ckAutomaticStart;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 62);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(186, 34);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnStart);
            this.layoutControl1.Controls.Add(this.gpLight);
            this.layoutControl1.Controls.Add(this.pictureEdit1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(358, 133);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnStart
            // 
            this.btnStart.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnStart.Appearance.Options.UseFont = true;
            this.btnStart.Appearance.Options.UseTextOptions = true;
            this.btnStart.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.btnStart.Image = global::KTibiaX.ImgButtons.vista_start;
            this.btnStart.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnStart.Location = new System.Drawing.Point(5, 85);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(146, 44);
            this.btnStart.StyleController = this.layoutControl1;
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = " ﮒTART ";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::KTibiaX.ImgTitles.light_form;
            this.pictureEdit1.Location = new System.Drawing.Point(7, 7);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Size = new System.Drawing.Size(142, 69);
            this.pictureEdit1.StyleController = this.layoutControl1;
            this.pictureEdit1.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(358, 133);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.pictureEdit1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(153, 80);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gpLight;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(153, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(203, 131);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnStart;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(153, 51);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(153, 51);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlItem3.Size = new System.Drawing.Size(153, 51);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // frm_Light
            // 
            this.ActionInterval = 1000;
            this.AutomaticCheckControl = this.ckAutomaticStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 133);
            this.Controls.Add(this.layoutControl1);
            baseControl1.Control = this.gpLight;
            baseControl1.Enabled = true;
            baseControl1.Name = "gpLight";
            baseControl1.Text = "";
            this.ControlsToDisable = new KTibiaX.UI.Util.BaseControl[] {
        baseControl1};
            this.FeatureName = "Player Light";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frm_Light";
            this.StartButton = this.btnStart;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            baseEdit1.Control = this.ddlColor;
            baseEdit1.Enabled = true;
            baseEdit1.Name = "ddlColor";
            baseEdit1.Value = 215;
            baseEdit2.Control = this.ddlItensity;
            baseEdit2.Enabled = true;
            baseEdit2.Name = "ddlItensity";
            baseEdit2.Value = 12;
            baseEdit3.Control = this.ckAutomaticStart;
            baseEdit3.Enabled = true;
            baseEdit3.Name = "ckAutomaticStart";
            baseEdit3.Value = false;
            this.StorableControls = new KTibiaX.UI.Util.BaseEdit[] {
        baseEdit1,
        baseEdit2,
        baseEdit3};
            this.Text = "Player Light";
            ((System.ComponentModel.ISupportInitialize)(this.gpLight)).EndInit();
            this.gpLight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckAutomaticStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlItensity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraEditors.GroupControl gpLight;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.CheckEdit ckAutomaticStart;
        private DevExpress.XtraEditors.ImageComboBoxEdit ddlColor;
        private DevExpress.XtraEditors.ImageComboBoxEdit ddlItensity;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}