using System.Drawing;
using System.Windows.Forms;
using CloudDisksAggregator.API;
using CloudDisksAggregator.Core;
using CloudDisksAggregatorUI.Properties;

namespace CloudDisksAggregatorUI.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.sideMenuPanel = new System.Windows.Forms.Panel();
            this.helpBtn = new System.Windows.Forms.Button();
            this.addNewBtn = new System.Windows.Forms.Button();
            this.allBtn = new System.Windows.Forms.Button();
            this.logoPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.addDiskSelectPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.helpPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.sideMenuPanel.SuspendLayout();
            this.logoPanel.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.addDiskSelectPanel.SuspendLayout();
            this.helpPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // helpBtn
            // 
            this.helpBtn.FlatAppearance.BorderSize = 0;
            this.helpBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(0)))));
            this.helpBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.helpBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.helpBtn.Location = new System.Drawing.Point(0, 190);
            this.helpBtn.Name = "helpBtn";
            this.helpBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.helpBtn.Size = new System.Drawing.Size(200, 45);
            this.helpBtn.TabIndex = 6;
            this.helpBtn.TabStop = false;
            this.helpBtn.Text = "Help";
            this.helpBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.helpBtn.UseVisualStyleBackColor = true;
            this.helpBtn.Click += OnHelpButton_Click;
            // 
            // addNewBtn
            // 
            this.addNewBtn.FlatAppearance.BorderSize = 0;
            this.addNewBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(0)))));
            this.addNewBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addNewBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.addNewBtn.Location = new System.Drawing.Point(0, 145);
            this.addNewBtn.Name = "addNewBtn";
            this.addNewBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.addNewBtn.Size = new System.Drawing.Size(200, 45);
            this.addNewBtn.TabIndex = 1;
            this.addNewBtn.TabStop = false;
            this.addNewBtn.Text = "Add new";
            this.addNewBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addNewBtn.UseVisualStyleBackColor = true;
            this.addNewBtn.Click += new System.EventHandler(this.OnAddNewButton_Click);
            // 
            // allButton
            // 
            this.allBtn.FlatAppearance.BorderSize = 0;
            this.allBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(0)))));
            this.allBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.allBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.allBtn.Location = new System.Drawing.Point(0, 100);
            this.allBtn.Name = "allBtn";
            this.allBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.allBtn.Size = new System.Drawing.Size(200, 45);
            this.allBtn.TabIndex = 1;
            this.allBtn.TabStop = false;
            this.allBtn.Text = "All";
            this.allBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.allBtn.UseVisualStyleBackColor = true;
            this.allBtn.Click += OnAllButton_Click;
            // 
            // logoPanel
            // 
            this.logoPanel.Controls.Add(this.label1);
            this.logoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.logoPanel.Location = new System.Drawing.Point(0, 0);
            this.logoPanel.Name = "logoPanel";
            this.logoPanel.Size = new System.Drawing.Size(200, 100);
            this.logoPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.label1.Size = new System.Drawing.Size(197, 77);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nodoby";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sideMenuPanel
            // 
            this.sideMenuPanel.AutoScroll = true;
            this.sideMenuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.sideMenuPanel.Controls.Add(this.helpBtn);
            this.sideMenuPanel.Controls.Add(this.addNewBtn);
            this.sideMenuPanel.Controls.Add(this.allBtn);
            foreach (var account in accounts) AddNewDiskSelectButton(account);
            this.sideMenuPanel.Controls.Add(this.logoPanel);
            this.sideMenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sideMenuPanel.Location = new System.Drawing.Point(0, 0);
            this.sideMenuPanel.Name = "sideMenuPanel";
            this.sideMenuPanel.Size = new System.Drawing.Size(200, 761);
            this.sideMenuPanel.TabIndex = 0;
            // 
            // controlPanel
            // 
            this.controlPanel.AutoScroll = true;
            this.controlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.controlPanel.Controls.Add(this.addDiskSelectPanel);
            this.controlPanel.Controls.Add(this.helpPanel);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel.Location = new System.Drawing.Point(200, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(784, 761);
            this.controlPanel.TabIndex = 1;
            // 
            // addDiskSelectPanel
            // 
            this.addDiskSelectPanel.AutoScroll = true;
            foreach (var cloudApi in apis) AddNewDiskAddingButton(cloudApi);
            this.addDiskSelectPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addDiskSelectPanel.Location = new System.Drawing.Point(0, 0);
            this.addDiskSelectPanel.Name = "addDiskSelectPanel";
            this.addDiskSelectPanel.Padding = new System.Windows.Forms.Padding(25, 25, 0, 0);
            this.addDiskSelectPanel.Size = new System.Drawing.Size(784, 761);
            this.addDiskSelectPanel.TabIndex = 0;
            this.addDiskSelectPanel.Visible = false;
            // 
            // helpPanel
            // 
            this.helpPanel.AutoScroll = true;
            this.helpPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpPanel.Name = "helpPanel";
            this.helpPanel.Padding = new System.Windows.Forms.Padding(25, 25, 0, 0);
            this.helpPanel.Size = new System.Drawing.Size(784, 761);
            this.helpPanel.TabIndex = 0;
            this.helpPanel.Visible = false;
            var headerLabel = new Label();
            
            var contactLabel = new Label();
            headerLabel.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            contactLabel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            headerLabel.Size = new System.Drawing.Size(400, 50);
            contactLabel.Size = new System.Drawing.Size(400, 50);
            contactLabel.Location = new Point(0,50);
            headerLabel.Text="Cloud Disks Aggregator";
            contactLabel.Text="for all questions, please write to disk.aggregator@yandex.ru";
            headerLabel.ForeColor = Color.White;
            contactLabel.ForeColor = Color.White;
            
            var pnl = new Panel();
            pnl.Size = new Size(700,400);
            pnl.Controls.Add(headerLabel);
            pnl.Controls.Add(contactLabel);
            this.helpPanel.Controls.Add(pnl);
            //
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.sideMenuPanel);
            this.Icon = Icon.FromHandle(global::CloudDisksAggregatorUI.Properties.Resources.appicon.GetHicon());
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "MainForm";
            this.Text = "Cloud Disks Aggregator";
            this.sideMenuPanel.ResumeLayout(false);
            this.logoPanel.ResumeLayout(false);
            this.logoPanel.PerformLayout();
            this.controlPanel.ResumeLayout(false);
            this.addDiskSelectPanel.ResumeLayout(false);
            this.helpPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private void AddNewDiskSelectButton(UserAccount acc)
        {
            var btn = new Button();
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(173, 20, 0);
            btn.FlatStyle = FlatStyle.Flat;
            btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            btn.Location = this.allBtn.Location;
            ShiftButtonDown(this.helpBtn,45);
            ShiftButtonDown(this.addNewBtn,45);
            ShiftButtonDown(this.allBtn,45);
            btn.Name = acc.Name;
            btn.Padding = new Padding(10, 0, 0, 0);
            btn.Size = new System.Drawing.Size(200, 45);
            btn.TabIndex = 1;
            btn.TabStop = false;
            btn.Text = acc.Name;
            btn.Tag = acc.DriveEngine;
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btn.UseVisualStyleBackColor = true;
            btn.Click += OnSelectDriveButton_Click;
            this.sideMenuPanel.Controls.Add(btn);
        }
        
        private void AddNewDiskAddingButton(ICloudApi api)
        {
            var btn = new Button();
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            btn.Image = api.Resources.MainLogo;
            btn.Location = new System.Drawing.Point(35, 35);
            btn.Margin = new System.Windows.Forms.Padding(10);
            btn.Size = new System.Drawing.Size(200, 190);
            btn.TabIndex = 0;
            btn.TabStop = false;
            btn.Tag = api.Drive;
            btn.Text = api.Resources.DriveTypeName;
            btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            btn.UseVisualStyleBackColor = true;
            btn.Click += OnNewDriveButton_Click;
            this.addDiskSelectPanel.Controls.Add(btn);
        }

        private void ShiftButtonDown(Button button,int value)
        {
            button.Location = new Point(button.Location.X,button.Location.Y+value);
        }

        private System.Windows.Forms.Panel sideMenuPanel;
        private System.Windows.Forms.Button addNewBtn;
        private System.Windows.Forms.Button allBtn;
        private System.Windows.Forms.Panel logoPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button helpBtn;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.FlowLayoutPanel addDiskSelectPanel;
        private System.Windows.Forms.Panel helpPanel;
    }
}