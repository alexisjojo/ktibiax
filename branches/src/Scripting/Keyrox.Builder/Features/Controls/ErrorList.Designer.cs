namespace Keyrox.Builder.Features.Controls {
    partial class ErrorList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorList));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.gcMessage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLine = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.imgSmall = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmall)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1});
            this.gridControl1.Size = new System.Drawing.Size(503, 192);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView2});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // gridView1
            // 
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcType,
            this.gcMessage,
            this.gcLine});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.OptionsView.ShowVertLines = false;
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.gridView1.Click += new System.EventHandler(this.gridView1_Click);
            // 
            // gcType
            // 
            this.gcType.ColumnEdit = this.repositoryItemPictureEdit1;
            this.gcType.FieldName = "Icon";
            this.gcType.Name = "gcType";
            this.gcType.OptionsColumn.AllowEdit = false;
            this.gcType.OptionsColumn.FixedWidth = true;
            this.gcType.OptionsFilter.AllowAutoFilter = false;
            this.gcType.OptionsFilter.AllowFilter = false;
            this.gcType.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.gcType.Visible = true;
            this.gcType.VisibleIndex = 0;
            this.gcType.Width = 20;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            // 
            // gcMessage
            // 
            this.gcMessage.AppearanceCell.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcMessage.AppearanceCell.ForeColor = System.Drawing.Color.SlateGray;
            this.gcMessage.AppearanceCell.Options.UseFont = true;
            this.gcMessage.AppearanceCell.Options.UseForeColor = true;
            this.gcMessage.Caption = "Message";
            this.gcMessage.FieldName = "Message";
            this.gcMessage.Name = "gcMessage";
            this.gcMessage.OptionsColumn.AllowEdit = false;
            this.gcMessage.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.gcMessage.Visible = true;
            this.gcMessage.VisibleIndex = 2;
            this.gcMessage.Width = 438;
            // 
            // gcLine
            // 
            this.gcLine.AppearanceCell.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.gcLine.AppearanceCell.ForeColor = System.Drawing.Color.SlateGray;
            this.gcLine.AppearanceCell.Options.UseFont = true;
            this.gcLine.AppearanceCell.Options.UseForeColor = true;
            this.gcLine.AppearanceCell.Options.UseTextOptions = true;
            this.gcLine.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcLine.AppearanceHeader.Options.UseTextOptions = true;
            this.gcLine.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcLine.Caption = "Line";
            this.gcLine.FieldName = "LineNumber";
            this.gcLine.MinWidth = 40;
            this.gcLine.Name = "gcLine";
            this.gcLine.OptionsColumn.AllowEdit = false;
            this.gcLine.OptionsColumn.FixedWidth = true;
            this.gcLine.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.gcLine.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.gcLine.Visible = true;
            this.gcLine.VisibleIndex = 1;
            this.gcLine.Width = 40;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.Name = "gridView2";
            // 
            // imgSmall
            // 
            this.imgSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imgSmall.ImageStream")));
            // 
            // ErrorList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "ErrorList";
            this.Size = new System.Drawing.Size(503, 192);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgSmall)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gcType;
        private DevExpress.XtraGrid.Columns.GridColumn gcMessage;
        private DevExpress.XtraGrid.Columns.GridColumn gcLine;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.Utils.ImageCollection imgSmall;
    }
}
