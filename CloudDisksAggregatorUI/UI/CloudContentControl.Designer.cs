using CloudDisksAggregator.Core;

namespace CloudDisksAggregatorUI.UI
{
    partial class CloudContentControl
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloudContentControl));
            this.searchBox = new System.Windows.Forms.TextBox();
            this.viewContentList = new System.Windows.Forms.ListView();
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.folderPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.searchBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchBox.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.searchBox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.searchBox.Location = new System.Drawing.Point(0, 0);
            this.searchBox.Name = "searchBox";
            this.searchBox.PlaceholderText = "Search";
            this.searchBox.Size = new System.Drawing.Size(719, 43);
            this.searchBox.TabIndex = 0;
            // 
            // viewContentList
            // 
            this.viewContentList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.viewContentList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.viewContentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewContentList.HideSelection = false;
            this.viewContentList.LargeImageList = this.iconList;
            this.viewContentList.Location = new System.Drawing.Point(0, 43);
            this.viewContentList.Margin = new System.Windows.Forms.Padding(10);
            this.viewContentList.MultiSelect = false;
            this.viewContentList.Name = "viewContentList";
            this.viewContentList.Size = new System.Drawing.Size(719, 369);
            this.viewContentList.TabIndex = 1;
            this.viewContentList.TabStop = false;
            this.viewContentList.UseCompatibleStateImageBehavior = false;
            // 
            // iconList
            // 
            this.iconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconList.Images.SetKeyName(0, "fileIcon.png");
            this.iconList.Images.SetKeyName(1, "folderIcon.png");
            // 
            // folderPanel
            // 
            this.folderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.folderPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.folderPanel.Location = new System.Drawing.Point(0, 379);
            this.folderPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.folderPanel.Name = "folderPanel";
            this.folderPanel.Size = new System.Drawing.Size(719, 33);
            this.folderPanel.TabIndex = 2;
            // 
            // CloudContentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.folderPanel);
            this.Controls.Add(this.viewContentList);
            this.Controls.Add(this.searchBox);
            this.Name = "CloudContentControl";
            this.Size = new System.Drawing.Size(719, 412);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void AddFolderBtn(DriveEntityInfo driveEntity)
        {
            var directoryBtn = new System.Windows.Forms.Button();
            directoryBtn.FlatAppearance.BorderSize = 0;
            directoryBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(0)))));
            directoryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            directoryBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            directoryBtn.Size = new System.Drawing.Size(98, 34);
            directoryBtn.Name = "directoryBtn";
            directoryBtn.TabIndex = 1;
            directoryBtn.Text = driveEntity.Name + " >";
            directoryBtn.Tag = driveEntity;
            directoryBtn.UseVisualStyleBackColor = true;
            directoryBtn.Click += DirectoryBtn_Click;
            this.folderPanel.Controls.Add(directoryBtn);
        }

        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.ListView viewContentList;
        private System.Windows.Forms.ImageList iconList;
        private System.Windows.Forms.FlowLayoutPanel folderPanel;
    }
}
