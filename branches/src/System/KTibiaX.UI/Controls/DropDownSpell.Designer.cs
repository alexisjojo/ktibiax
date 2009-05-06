namespace KTibiaX.UI.Controls {
    partial class DropDownSpell {
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
            this.ddlSpells = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlSpells.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlSpells
            // 
            this.ddlSpells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddlSpells.Location = new System.Drawing.Point(0, 0);
            this.ddlSpells.Name = "ddlSpells";
            this.ddlSpells.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold);
            this.ddlSpells.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.ddlSpells.Properties.Appearance.Options.UseFont = true;
            this.ddlSpells.Properties.Appearance.Options.UseForeColor = true;
            this.ddlSpells.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Verdana", 7F);
            this.ddlSpells.Properties.AppearanceDropDown.Options.UseFont = true;
            this.ddlSpells.Properties.AutoSearchColumnIndex = 1;
            this.ddlSpells.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, " Edit ", -1, false, true, false, DevExpress.XtraEditors.ImageLocation.Default, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "Edit selected item"),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, " New ", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.Default, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "Add new item"),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Close, "Close", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "Clear current selection")});
            this.ddlSpells.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ddlSpells.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idt_spell", "Id", 20, DevExpress.Utils.FormatType.Numeric, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_spell", "Name", 100),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("dsc_words", "Spell", 115),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("flg_premium", "Premium", 50),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("num_mana", "Mana", 50),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("num_level", "Level", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idt_type", "Type", 50)});
            this.ddlSpells.Properties.DisplayMember = "dsc_words";
            this.ddlSpells.Properties.NullText = "";
            this.ddlSpells.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.ddlSpells.Properties.ValueMember = "idt_spell";
            this.ddlSpells.Size = new System.Drawing.Size(206, 20);
            this.ddlSpells.TabIndex = 2;
            // 
            // DropDownSpell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ddlSpells);
            this.Name = "DropDownSpell";
            this.Size = new System.Drawing.Size(206, 21);
            ((System.ComponentModel.ISupportInitialize)(this.ddlSpells.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit ddlSpells;
    }
}
