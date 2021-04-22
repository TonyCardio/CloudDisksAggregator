using CloudDisksAggregatorUI.UI.ViewEntity;
using System.Windows.Forms;
using CloudDisksAggregator;
using CloudDisksAggregator.CloudDrives;

namespace CloudDisksAggregatorUI.UI.ViewControls
{
    partial class SelectCloudControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void AddCloudViewItem(DriveViewInfo driveViewInfo)
        {
            var itemPanel = new Panel();
            itemPanel.SuspendLayout();
            this.cloudsPanel.Controls.Add(itemPanel);
            var iconBox = CreateIconBox(driveViewInfo.DriveType);
            ((System.ComponentModel.ISupportInitialize)(iconBox)).BeginInit();
            itemPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            itemPanel.Controls.Add(CreateNameBox(driveViewInfo.Name));
            itemPanel.Controls.Add(iconBox);
            itemPanel.Controls.Add(CreateButton(driveViewInfo));
            itemPanel.Location = new System.Drawing.Point(23, 23);
            itemPanel.Name = "itemDiskPanel";
            itemPanel.Size = new System.Drawing.Size(240, 100);
            itemPanel.TabIndex = 0;
            ((System.ComponentModel.ISupportInitialize)(iconBox)).EndInit();
        }

        private Button CreateButton(DriveViewInfo tag)
        {
            var selectBtn = new System.Windows.Forms.Button();
            selectBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            selectBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            selectBtn.FlatAppearance.BorderSize = 0;
            selectBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(0)))));
            selectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            selectBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            selectBtn.Location = new System.Drawing.Point(0, 77);
            selectBtn.Name = "selectDiskBtn";
            selectBtn.Size = new System.Drawing.Size(240, 23);
            selectBtn.TabIndex = 0;
            selectBtn.Text = "Select";
            selectBtn.UseVisualStyleBackColor = false;
            selectBtn.Tag = tag;
            selectBtn.Click += AcceptCloudDrive;
            return selectBtn;
        }

        private PictureBox CreateIconBox(CloudDriveType driveType)
        {

            var iconBox = new System.Windows.Forms.PictureBox();
            iconBox.Dock = System.Windows.Forms.DockStyle.Left;
            iconBox.Image = driveType == CloudDriveType.YandexDisk ?
                global::CloudDisksAggregatorUI.Properties.Resources.yandexdiskicon :
                global::CloudDisksAggregatorUI.Properties.Resources.dropboxicon;
            iconBox.Location = new System.Drawing.Point(0, 0);
            iconBox.Name = "cloudIconBox";
            iconBox.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            iconBox.Size = new System.Drawing.Size(106, 77);
            iconBox.TabIndex = 1;
            iconBox.TabStop = false;
            return iconBox;
        }

        private TextBox CreateNameBox(string name)
        {
            var nameItemBox = new System.Windows.Forms.TextBox();

            nameItemBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
| System.Windows.Forms.AnchorStyles.Left)
| System.Windows.Forms.AnchorStyles.Right)));
            nameItemBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            nameItemBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            nameItemBox.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            nameItemBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
            nameItemBox.Location = new System.Drawing.Point(124, 25);
            nameItemBox.Name = "nameBox";
            nameItemBox.Text = name;
            nameItemBox.ReadOnly = true;
            nameItemBox.Size = new System.Drawing.Size(100, 27);
            nameItemBox.TabIndex = 2;
            nameItemBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            return nameItemBox;
        }
        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.cloudsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // cloudsPanel
            // 
            this.cloudsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.cloudsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cloudsPanel.Location = new System.Drawing.Point(0, 0);
            this.cloudsPanel.Margin = new System.Windows.Forms.Padding(10);
            this.cloudsPanel.Name = "cloudsPanel";
            this.cloudsPanel.Padding = new System.Windows.Forms.Padding(20, 20, 0, 0);
            this.cloudsPanel.Size = new System.Drawing.Size(499, 347);
            this.cloudsPanel.TabIndex = 0;
            // 
            // SelectCloudControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cloudsPanel);
            this.Name = "SelectCloudControl";
            this.Size = new System.Drawing.Size(499, 347);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel cloudsPanel;
        private System.Windows.Forms.Panel diskPanel;
    }
}
