namespace Keyrox.Scripting.Controls {
    partial class ScriptBox {
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
            this.scriptEditor1 = new Keyrox.Scripting.Controls.ScriptEditor();
            this.SuspendLayout();
            // 
            // scriptEditor1
            // 
            this.scriptEditor1.ActiveView = Keyrox.Windows.Forms.ActiveView.BottomRight;
            this.scriptEditor1.AutoListAutoSelect = false;
            this.scriptEditor1.AutoListPosition = null;
            this.scriptEditor1.AutoListSelectedText = "a123";
            this.scriptEditor1.AutoListVisible = false;
            this.scriptEditor1.BackColor = System.Drawing.Color.White;
            this.scriptEditor1.BorderStyle = Keyrox.Windows.Forms.BorderStyle.None;
            this.scriptEditor1.BracketBorderColor = System.Drawing.Color.SlateGray;
            this.scriptEditor1.ChildBorderColor = System.Drawing.Color.White;
            this.scriptEditor1.ChildBorderStyle = Keyrox.Windows.Forms.BorderStyle.None;
            this.scriptEditor1.CopyAsRTF = false;
            this.scriptEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptEditor1.FontName = "Consolas";
            this.scriptEditor1.GutterMarginBorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.scriptEditor1.GutterMarginColor = System.Drawing.Color.LightSteelBlue;
            this.scriptEditor1.HighLightActiveLine = true;
            this.scriptEditor1.HighLightedLineColor = System.Drawing.Color.WhiteSmoke;
            this.scriptEditor1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.scriptEditor1.InfoTipCount = 1;
            this.scriptEditor1.InfoTipPosition = null;
            this.scriptEditor1.InfoTipSelectedIndex = 1;
            this.scriptEditor1.InfoTipVisible = false;
            this.scriptEditor1.LineNumberBackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.scriptEditor1.LineNumberBorderColor = System.Drawing.Color.CornflowerBlue;
            this.scriptEditor1.LineNumberForeColor = System.Drawing.Color.LightSteelBlue;
            this.scriptEditor1.Location = new System.Drawing.Point(0, 0);
            this.scriptEditor1.LockCursorUpdate = false;
            this.scriptEditor1.Name = "scriptEditor1";
            this.scriptEditor1.ParseOnPaste = true;
            this.scriptEditor1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.scriptEditor1.SeparatorColor = System.Drawing.SystemColors.ActiveBorder;
            this.scriptEditor1.ShowScopeIndicator = false;
            this.scriptEditor1.Size = new System.Drawing.Size(762, 605);
            this.scriptEditor1.SmoothScroll = false;
            this.scriptEditor1.SplitviewH = -4;
            this.scriptEditor1.SplitviewV = -4;
            this.scriptEditor1.SupressAutoComplete = true;
            this.scriptEditor1.SupressInfoTips = true;
            this.scriptEditor1.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(234)))));
            this.scriptEditor1.TabIndex = 1;
            this.scriptEditor1.Text = "scriptEditor1";
            this.scriptEditor1.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // ScriptBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scriptEditor1);
            this.Name = "ScriptBox";
            this.Size = new System.Drawing.Size(762, 605);
            this.ResumeLayout(false);

        }

        #endregion

        private ScriptEditor scriptEditor1;
    }
}
