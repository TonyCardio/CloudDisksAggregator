using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CloudDisksAggregator.Core;

namespace CloudDisksAggregatorUI.UI
{
    partial class SearchBox : TextBox
    {
        public event EventHandler Press;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                OnEvent();
            else
                base.OnKeyDown(e);
        }
        
        private void OnEvent() {
            if (Press != null) {
                Press(this, EventArgs.Empty);
            }
        }
    }
    partial class CloudContentControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private IContainer components = null;

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

        private SearchBox createSearchBox()
        {
            var searchBox = new SearchBox()
            {
                BackColor = Color.FromArgb(
                    ((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39))))),
                Dock = DockStyle.Top,
                Font = new Font(
                    "Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point),
                ForeColor = SystemColors.ButtonHighlight,
                Location = new Point(0, 0),
                Name = "searchBox",
                PlaceholderText = "Search",
                Size = new Size(719, 43),
                TabIndex = 0
            };
            searchBox.Press += SearchBox_Press;
            return searchBox;
        }

        private ListView createListView(ImageList iconList)
        {
            return new ListView()
            {
                BackColor = Color.FromArgb(
                    ((int)(((byte)(23)))), ((int)(((byte)(21)))), ((int)(((byte)(32))))),
                BorderStyle = BorderStyle.None,
                Dock = DockStyle.Fill,
                HideSelection = false,
                LargeImageList = iconList,
                Location = new Point(0, 43),
                Margin = new Padding(10),
                MultiSelect = false,
                Name = "viewContentList",
                Size = new Size(720, 250),
                TabIndex = 1,
                TabStop = false,
                UseCompatibleStateImageBehavior = false,
            };
        }
        
        private ImageList createImageList(IContainer components, ComponentResourceManager resources)
        {
            var imageList = new ImageList(components)
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageStream = ((ImageListStreamer)(resources.GetObject("iconList.ImageStream"))),
                TransparentColor = Color.Transparent
            };
            imageList.Images.SetKeyName(0, "fileIcon.png");
            imageList.Images.SetKeyName(1, "folderIcon.png");
            return imageList;
        }

        private FlowLayoutPanel createFolderPanel()
        {
            return new FlowLayoutPanel()
            {
                BackColor = Color.FromArgb(
                    ((int)(((byte)(35)))), ((int)(((byte)(32)))), ((int)(((byte)(39))))),
                Dock = DockStyle.Bottom,
                Location = new Point(0, 379),
                Margin = new Padding(3, 0, 3, 0),
                Name = "folderPanel",
                Size = new Size(719, 33),
                TabIndex = 2
            };
        }

        private ValueTuple<ListView, FlowLayoutPanel> initializeDrivePanel(IContainer components, ComponentResourceManager resources)
        {
            var iconList = createImageList(components, resources);
            var viewContentList = createListView(iconList);
            var folderPanel = createFolderPanel();
            var panel = new Panel()
            {
                Dock = DockStyle.Bottom,
                Location = new Point(0, 0),
                Name = "panel",
                Size = new Size(700, 600),
                TabIndex = 0,
                Controls =
                {
                    folderPanel,
                    viewContentList,
                }
            };
            Controls.Add(panel);
            
            return (viewContentList, folderPanel);
        }
        
        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>

        private void InitializeControl()
        {
            Dock = DockStyle.Fill;
            AutoScroll = true;
            AllowDrop = true;
            DragEnter += CloudContentControl_DragEnter;
            DragDrop += CloudContentControl_DragDrop;
            Controls.Add(createSearchBox());
        }
        
        // private void InitializeComponent()
        // {
        //     var components = new System.ComponentModel.Container();
        //     var resources 
        //         = new System.ComponentModel.ComponentResourceManager(typeof(CloudContentControl));
        //     var searchBox = createSearchBox();
        //     var iconList = createImageList(components, resources);
        //     this.viewContentList = createListView(iconList);
        //     this.folderPanel = createFolderPanel();
        //     this.SuspendLayout();
        //     
        //     this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        //     this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //     this.Controls.Add(folderPanel);
        //     this.Controls.Add(viewContentList);
        //     this.Controls.Add(searchBox);
        //     this.Name = "CloudContentControl";
        //     this.Size = new System.Drawing.Size(700, 400);
        //     this.ResumeLayout(false);
        //     this.PerformLayout();
        //     this.AutoScroll = true;
        // }

        #endregion

        private void AddFolderBtn(DriveEntityInfo driveEntity, string driveName)
        {
            var directoryBtn = new Button()
            {
                FlatAppearance =
                {
                    BorderSize = 0,
                    MouseDownBackColor = System.Drawing.Color.FromArgb(
                        ((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(0)))))
                },
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                ForeColor = System.Drawing.SystemColors.ButtonHighlight,
                Size = new System.Drawing.Size(98, 34),
                Name = "directoryBtn",
                TabIndex = 1,
                Text = driveEntity.Name.Equals("") ? driveName + " >" : driveEntity.Name + " >",
                Tag = driveEntity,
                UseVisualStyleBackColor = true
            };
            directoryBtn.Click += DirectoryBtn_Click;
            folderPanels[driveEntity.DriveEngine].Controls.Add(directoryBtn);
        }

        // private SearchBox searchBox;
        // private System.Windows.Forms.ListView viewContentList;
        // private System.Windows.Forms.ImageList iconList;
        // private System.Windows.Forms.FlowLayoutPanel folderPanel;
    }
}
