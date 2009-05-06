namespace KTibiaX.UI.Controls {
    partial class DropDownItem {
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
            this.ddlItems = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlItems.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlItems
            // 
            this.ddlItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddlItems.Location = new System.Drawing.Point(0, 0);
            this.ddlItems.Name = "ddlItems";
            this.ddlItems.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.ddlItems.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.ddlItems.Properties.Appearance.Options.UseFont = true;
            this.ddlItems.Properties.Appearance.Options.UseForeColor = true;
            this.ddlItems.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 7F);
            this.ddlItems.Properties.AppearanceDropDown.Options.UseFont = true;
            this.ddlItems.Properties.AutoSearchColumnIndex = 1;
            this.ddlItems.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, " Edit ", -1, false, true, false, DevExpress.XtraEditors.ImageLocation.Default, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "Edit selected item"),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, " New ", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.Default, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "Add new item"),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Close, "Close", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "Clear current selection")});
            this.ddlItems.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ddlItems.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idt_item", "ID", 40, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Near, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_item", "Name", 100, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("num_weigth", "Oz", 50, DevExpress.Utils.FormatType.Numeric, "0 Oz", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("num_level", "Lvl", 40, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("num_attack", "Atk", 40, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("num_defence", "Def", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("num_volume", "Vol", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("num_reg_sec", "Reg/Sec", 50),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("num_reg_oz", "Reg/Oz", 50)});
            this.ddlItems.Properties.DisplayMember = "dsc_item";
            this.ddlItems.Properties.NullText = "[ » SELECT « ]";
            this.ddlItems.Properties.ValueMember = "idt_item";
            this.ddlItems.Size = new System.Drawing.Size(206, 20);
            this.ddlItems.TabIndex = 1;
            // 
            // DropDownItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ddlItems);
            this.Name = "DropDownItem";
            this.Size = new System.Drawing.Size(206, 21);
            ((System.ComponentModel.ISupportInitialize)(this.ddlItems.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit ddlItems;
    }
}
