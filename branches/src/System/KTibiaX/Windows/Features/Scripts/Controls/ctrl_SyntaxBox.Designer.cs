namespace KTibiaX.Windows.Features.Scripts.Controls {
    partial class ctrl_SyntaxBox {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrl_SyntaxBox));
            this.imgAutoList = new System.Windows.Forms.ImageList(this.components);
            this.ScriptBox = new Alsing.Windows.Forms.SyntaxBoxControl();
            this.ScriptDocument = new Alsing.SourceCode.SyntaxDocument(this.components);
            this.ScriptToolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.ScriptAutoList = new KTibiaX.Windows.Features.Scripts.Controls.ctrl_AutoList();
            this.SuspendLayout();
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
            // ScriptBox
            // 
            this.ScriptBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
            this.ScriptBox.AutoListIcons = this.imgAutoList;
            this.ScriptBox.AutoListPosition = null;
            this.ScriptBox.AutoListSelectedText = "a123";
            this.ScriptBox.AutoListVisible = false;
            this.ScriptBox.BackColor = System.Drawing.Color.White;
            this.ScriptBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            this.ScriptBox.CopyAsRTF = false;
            this.ScriptBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScriptBox.Document = this.ScriptDocument;
            this.ScriptBox.FontName = "Courier new";
            this.ScriptBox.GutterMarginBorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ScriptBox.GutterMarginColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ScriptBox.HighLightActiveLine = true;
            this.ScriptBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ScriptBox.InfoTipCount = 1;
            this.ScriptBox.InfoTipPosition = null;
            this.ScriptBox.InfoTipSelectedIndex = 1;
            this.ScriptBox.InfoTipVisible = false;
            this.ScriptBox.LineNumberBackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ScriptBox.LineNumberBorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ScriptBox.LineNumberForeColor = System.Drawing.Color.White;
            this.ScriptBox.Location = new System.Drawing.Point(0, 0);
            this.ScriptBox.LockCursorUpdate = false;
            this.ScriptBox.Name = "ScriptBox";
            this.ScriptBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ScriptBox.ShowScopeIndicator = false;
            this.ScriptBox.Size = new System.Drawing.Size(702, 506);
            this.ScriptBox.SmoothScroll = false;
            this.ScriptBox.SplitviewH = -4;
            this.ScriptBox.SplitviewV = -4;
            this.ScriptToolTipController.SetSuperTip(this.ScriptBox, null);
            this.ScriptBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(234)))));
            this.ScriptBox.TabIndex = 0;
            this.ScriptBox.Text = "syntaxBoxControl1";
            this.ScriptBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            this.ScriptBox.RowMouseMove += new Alsing.Windows.Forms.SyntaxBox.RowMouseHandler(this.ScriptBox_RowMouseMove);
            this.ScriptBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ScriptBox_KeyPress);
            // 
            // ScriptDocument
            // 
            this.ScriptDocument.Lines = new string[] {
        ""};
            this.ScriptDocument.MaxUndoBufferSize = 1000;
            this.ScriptDocument.Modified = false;
            this.ScriptDocument.UndoStep = 0;
            // 
            // ScriptToolTipController
            // 
            this.ScriptToolTipController.ImageList = this.imgAutoList;
            this.ScriptToolTipController.Rounded = true;
            // 
            // ScriptAutoList
            // 
            this.ScriptAutoList.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ScriptAutoList.Appearance.Options.UseBackColor = true;
            this.ScriptAutoList.Location = new System.Drawing.Point(540, 338);
            this.ScriptAutoList.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.ScriptAutoList.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ScriptAutoList.Name = "ScriptAutoList";
            this.ScriptAutoList.Size = new System.Drawing.Size(144, 165);
            this.ScriptToolTipController.SetSuperTip(this.ScriptAutoList, null);
            this.ScriptAutoList.TabIndex = 1;
            // 
            // ctrl_SyntaxBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ScriptAutoList);
            this.Controls.Add(this.ScriptBox);
            this.Name = "ctrl_SyntaxBox";
            this.Size = new System.Drawing.Size(702, 506);
            this.ScriptToolTipController.SetSuperTip(this, null);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imgAutoList;
        private Alsing.Windows.Forms.SyntaxBoxControl ScriptBox;
        private Alsing.SourceCode.SyntaxDocument ScriptDocument;
        private DevExpress.Utils.ToolTipController ScriptToolTipController;
        private ctrl_AutoList ScriptAutoList;
    }
}
