
namespace CloudDisksAggregator.UI
{
    partial class BrowserAuthControl
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
            this.setNamePanel = new System.Windows.Forms.Panel();
            this.setNameBtn = new System.Windows.Forms.Button();
            this.cloudNameBox = new System.Windows.Forms.TextBox();
            this.setNamePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // setNamePanel
            // 
            this.setNamePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32)))));
            this.setNamePanel.Controls.Add(this.setNameBtn);
            this.setNamePanel.Controls.Add(this.cloudNameBox);
            this.setNamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.setNamePanel.Location = new System.Drawing.Point(0, 0);
            this.setNamePanel.Name = "setNamePanel";
            this.setNamePanel.Size = new System.Drawing.Size(575, 384);
            this.setNamePanel.TabIndex = 0;
            this.setNamePanel.Visible = false;
            // 
            // setNameBtn
            // 
            this.setNameBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.setNameBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.setNameBtn.FlatAppearance.BorderSize = 0;
            this.setNameBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(0)))));
            this.setNameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setNameBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.setNameBtn.Location = new System.Drawing.Point(199, 168);
            this.setNameBtn.Name = "setNameBtn";
            this.setNameBtn.Size = new System.Drawing.Size(192, 30);
            this.setNameBtn.TabIndex = 1;
            this.setNameBtn.Text = "Confirm";
            this.setNameBtn.UseVisualStyleBackColor = false;
            this.setNameBtn.Click += new System.EventHandler(this.setNameBtn_Click);
            // 
            // cloudNameBox
            // 
            this.cloudNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cloudNameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39)))));
            this.cloudNameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cloudNameBox.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cloudNameBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cloudNameBox.Location = new System.Drawing.Point(199, 131);
            this.cloudNameBox.Name = "cloudNameBox";
            this.cloudNameBox.PlaceholderText = "Enter cloud name";
            this.cloudNameBox.Size = new System.Drawing.Size(192, 31);
            this.cloudNameBox.TabIndex = 0;
            // 
            // AuthCloudControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.setNamePanel);
            this.Name = "BrowserAuthControl";
            this.Size = new System.Drawing.Size(575, 384);
            this.setNamePanel.ResumeLayout(false);
            this.setNamePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel setNamePanel;
        private System.Windows.Forms.Button setNameBtn;
        private System.Windows.Forms.TextBox cloudNameBox;
    }
}
