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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptBox));
            this.scriptEditor1 = new Keyrox.Scripting.Controls.ScriptEditor();
            this.imgGutter = new System.Windows.Forms.ImageList(this.components);
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
            this.scriptEditor1.BreakPointBackColor = System.Drawing.Color.IndianRed;
            this.scriptEditor1.BreakPointForeColor = System.Drawing.Color.MistyRose;
            this.scriptEditor1.ChildBorderColor = System.Drawing.Color.White;
            this.scriptEditor1.ChildBorderStyle = Keyrox.Windows.Forms.BorderStyle.None;
            this.scriptEditor1.CopyAsRTF = false;
            this.scriptEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptEditor1.FontName = "Consolas";
            this.scriptEditor1.GutterIcons = this.imgGutter;
            this.scriptEditor1.GutterMarginBorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.scriptEditor1.GutterMarginColor = System.Drawing.Color.LightSteelBlue;
            this.scriptEditor1.GutterMarginWidth = 22;
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
            this.scriptEditor1.SplitView = false;
            this.scriptEditor1.SplitviewH = -4;
            this.scriptEditor1.SplitviewV = -4;
            this.scriptEditor1.SupressAutoComplete = true;
            this.scriptEditor1.SupressInfoTips = true;
            this.scriptEditor1.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(234)))));
            this.scriptEditor1.TabIndex = 1;
            this.scriptEditor1.Text = "scriptEditor1";
            this.scriptEditor1.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // imgGutter
            // 
            this.imgGutter.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgGutter.ImageStream")));
            this.imgGutter.TransparentColor = System.Drawing.Color.Transparent;
            this.imgGutter.Images.SetKeyName(0, "00185.png");
            this.imgGutter.Images.SetKeyName(1, "07801.png");
            this.imgGutter.Images.SetKeyName(2, "00136.png");
            this.imgGutter.Images.SetKeyName(3, "00037.png");
            this.imgGutter.Images.SetKeyName(4, "00126.png");
            this.imgGutter.Images.SetKeyName(5, "00141.png");
            this.imgGutter.Images.SetKeyName(6, "00184.png");
            this.imgGutter.Images.SetKeyName(7, "00459.png");
            this.imgGutter.Images.SetKeyName(8, "00463.png");
            this.imgGutter.Images.SetKeyName(9, "00688.png");
            this.imgGutter.Images.SetKeyName(10, "02081.png");
            this.imgGutter.Images.SetKeyName(11, "02554.png");
            this.imgGutter.Images.SetKeyName(12, "arrow_join.png");
            this.imgGutter.Images.SetKeyName(13, "arrow_right.png");
            this.imgGutter.Images.SetKeyName(14, "arrow_rotate_anticlockwise.png");
            this.imgGutter.Images.SetKeyName(15, "error.png");
            this.imgGutter.Images.SetKeyName(16, "exclamation.png");
            this.imgGutter.Images.SetKeyName(17, "information.png");
            this.imgGutter.Images.SetKeyName(18, "lightning.png");
            this.imgGutter.Images.SetKeyName(19, "timeline_marker.png");
            this.imgGutter.Images.SetKeyName(20, "weather_sun.png");
            this.imgGutter.Images.SetKeyName(21, "07817.png");
            this.imgGutter.Images.SetKeyName(22, "00051.png");
            this.imgGutter.Images.SetKeyName(23, "00126.png");
            this.imgGutter.Images.SetKeyName(24, "00156.png");
            this.imgGutter.Images.SetKeyName(25, "00157.png");
            this.imgGutter.Images.SetKeyName(26, "00178.png");
            this.imgGutter.Images.SetKeyName(27, "00183.png");
            this.imgGutter.Images.SetKeyName(28, "00237.png");
            this.imgGutter.Images.SetKeyName(29, "00238.png");
            this.imgGutter.Images.SetKeyName(30, "00239.png");
            this.imgGutter.Images.SetKeyName(31, "00588.png");
            this.imgGutter.Images.SetKeyName(32, "00644.png");
            this.imgGutter.Images.SetKeyName(33, "01038.png");
            this.imgGutter.Images.SetKeyName(34, "07772.png");
            this.imgGutter.Images.SetKeyName(35, "00163.png");
            this.imgGutter.Images.SetKeyName(36, "00194.png");
            this.imgGutter.Images.SetKeyName(37, "00205.png");
            this.imgGutter.Images.SetKeyName(38, "01750.png");
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
        private System.Windows.Forms.ImageList imgGutter;
    }
}
