namespace KTibiaX.Windows.Features.Development {
    partial class frm_DataTest {
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
            KTibiaX.UI.Util.BaseEdit baseEdit1 = new KTibiaX.UI.Util.BaseEdit();
            KTibiaX.UI.Util.BaseEdit baseEdit2 = new KTibiaX.UI.Util.BaseEdit();
            KTibiaX.UI.Util.BaseEdit baseEdit3 = new KTibiaX.UI.Util.BaseEdit();
            KTibiaX.UI.Util.BaseEdit baseEdit4 = new KTibiaX.UI.Util.BaseEdit();
            KTibiaX.UI.Util.BaseEdit baseEdit5 = new KTibiaX.UI.Util.BaseEdit();
            KTibiaX.UI.Util.BaseList baseList1 = new KTibiaX.UI.Util.BaseList();
            this.ckNoUse = new DevExpress.XtraEditors.CheckEdit();
            this.ckUse = new DevExpress.XtraEditors.CheckEdit();
            this.ddlColors = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtHP = new DevExpress.XtraEditors.ButtonEdit();
            this.txtValor = new DevExpress.XtraEditors.SpinEdit();
            this.lstNames = new DevExpress.XtraEditors.ImageListBoxControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ckNoUse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckUse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlColors.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstNames)).BeginInit();
            this.SuspendLayout();
            // 
            // ckNoUse
            // 
            this.ckNoUse.Location = new System.Drawing.Point(95, 38);
            this.ckNoUse.Name = "ckNoUse";
            this.ckNoUse.Properties.Caption = "Não Usar";
            this.ckNoUse.Size = new System.Drawing.Size(75, 18);
            this.ckNoUse.TabIndex = 1;
            // 
            // ckUse
            // 
            this.ckUse.Location = new System.Drawing.Point(12, 39);
            this.ckUse.Name = "ckUse";
            this.ckUse.Properties.Caption = "Sim Usar";
            this.ckUse.Size = new System.Drawing.Size(75, 18);
            this.ckUse.TabIndex = 1;
            // 
            // ddlColors
            // 
            this.ddlColors.Location = new System.Drawing.Point(12, 64);
            this.ddlColors.Name = "ddlColors";
            this.ddlColors.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ddlColors.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Alto", 235, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Medio", 128, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Baixo", 73, -1)});
            this.ddlColors.Size = new System.Drawing.Size(158, 20);
            this.ddlColors.TabIndex = 2;
            // 
            // txtHP
            // 
            this.txtHP.Location = new System.Drawing.Point(12, 12);
            this.txtHP.Name = "txtHP";
            this.txtHP.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtHP.Size = new System.Drawing.Size(158, 20);
            this.txtHP.TabIndex = 0;
            // 
            // txtValor
            // 
            this.txtValor.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtValor.Location = new System.Drawing.Point(12, 193);
            this.txtValor.Name = "txtValor";
            this.txtValor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtValor.Size = new System.Drawing.Size(158, 20);
            this.txtValor.TabIndex = 4;
            // 
            // lstNames
            // 
            this.lstNames.DisplayMember = "Title";
            this.lstNames.Location = new System.Drawing.Point(12, 91);
            this.lstNames.Name = "lstNames";
            this.lstNames.Size = new System.Drawing.Size(158, 95);
            this.lstNames.TabIndex = 3;
            this.lstNames.ValueMember = "Id";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 220);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(94, 220);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(76, 23);
            this.btnLoad.TabIndex = 6;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // frm_DataTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(185, 254);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.lstNames);
            this.Controls.Add(this.ddlColors);
            this.Controls.Add(this.ckNoUse);
            this.Controls.Add(this.ckUse);
            this.Controls.Add(this.txtHP);
            this.FeatureName = "Teste de Dados";
            this.Name = "frm_DataTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            baseEdit1.Control = this.ckNoUse;
            baseEdit1.Enabled = true;
            baseEdit1.Name = "ckNoUse";
            baseEdit1.Value = false;
            baseEdit2.Control = this.ckUse;
            baseEdit2.Enabled = true;
            baseEdit2.Name = "ckUse";
            baseEdit2.Value = false;
            baseEdit3.Control = this.ddlColors;
            baseEdit3.Enabled = true;
            baseEdit3.Name = "ddlColors";
            baseEdit3.Value = null;
            baseEdit4.Control = this.txtHP;
            baseEdit4.Enabled = true;
            baseEdit4.Name = "txtHP";
            baseEdit4.Value = null;
            baseEdit5.Control = this.txtValor;
            baseEdit5.Enabled = true;
            baseEdit5.Name = "txtValor";
            baseEdit5.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.StorableControls = new KTibiaX.UI.Util.BaseEdit[] {
        baseEdit1,
        baseEdit2,
        baseEdit3,
        baseEdit4,
        baseEdit5};
            baseList1.Control = this.lstNames;
            this.StorableLists = new KTibiaX.UI.Util.BaseList[] {
        baseList1};
            this.Text = "frm_DataTest";
            ((System.ComponentModel.ISupportInitialize)(this.ckNoUse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckUse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlColors.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstNames)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit txtHP;
        private DevExpress.XtraEditors.CheckEdit ckUse;
        private DevExpress.XtraEditors.ImageComboBoxEdit ddlColors;
        private DevExpress.XtraEditors.ImageListBoxControl lstNames;
        private DevExpress.XtraEditors.SpinEdit txtValor;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private DevExpress.XtraEditors.CheckEdit ckNoUse;
    }
}