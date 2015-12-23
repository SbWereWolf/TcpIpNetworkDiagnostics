using System.ComponentModel;
using System.Windows.Forms;

namespace RuskomDiagnostics
{
    sealed partial class ShowMessageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowMessageForm));
            this.ResultOutputGroupBox = new System.Windows.Forms.GroupBox();
            this.FormMessageTextBox = new System.Windows.Forms.TextBox();
            this.CommandLineGroupBox = new System.Windows.Forms.GroupBox();
            this.DoCopyResultButton = new System.Windows.Forms.Button();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.DoCloseButton = new System.Windows.Forms.Button();
            this.ResultOutputGroupBox.SuspendLayout();
            this.CommandLineGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ResultOutputGroupBox
            // 
            this.ResultOutputGroupBox.Controls.Add(this.FormMessageTextBox);
            this.ResultOutputGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultOutputGroupBox.Location = new System.Drawing.Point(0, 0);
            this.ResultOutputGroupBox.Name = "ResultOutputGroupBox";
            this.ResultOutputGroupBox.Size = new System.Drawing.Size(627, 384);
            this.ResultOutputGroupBox.TabIndex = 0;
            this.ResultOutputGroupBox.TabStop = false;
            this.ResultOutputGroupBox.Text = "Результат";
            // 
            // FormMessageTextBox
            // 
            this.FormMessageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormMessageTextBox.Location = new System.Drawing.Point(3, 16);
            this.FormMessageTextBox.Multiline = true;
            this.FormMessageTextBox.Name = "FormMessageTextBox";
            this.FormMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.FormMessageTextBox.Size = new System.Drawing.Size(621, 365);
            this.FormMessageTextBox.TabIndex = 0;
            this.FormMessageTextBox.Text = "Пустое сообщение";
            // 
            // CommandLineGroupBox
            // 
            this.CommandLineGroupBox.Controls.Add(this.DoCopyResultButton);
            this.CommandLineGroupBox.Controls.Add(this.DoCloseButton);
            this.CommandLineGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CommandLineGroupBox.Location = new System.Drawing.Point(0, 384);
            this.CommandLineGroupBox.Name = "CommandLineGroupBox";
            this.CommandLineGroupBox.Size = new System.Drawing.Size(627, 64);
            this.CommandLineGroupBox.TabIndex = 1;
            this.CommandLineGroupBox.TabStop = false;
            // 
            // DoCopyResultButton
            // 
            this.DoCopyResultButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DoCopyResultButton.ImageIndex = 24;
            this.DoCopyResultButton.ImageList = this.ImageList;
            this.DoCopyResultButton.Location = new System.Drawing.Point(3, 16);
            this.DoCopyResultButton.Name = "DoCopyResultButton";
            this.DoCopyResultButton.Size = new System.Drawing.Size(467, 45);
            this.DoCopyResultButton.TabIndex = 0;
            this.DoCopyResultButton.Text = "Копировать всё";
            this.DoCopyResultButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoCopyResultButton.UseVisualStyleBackColor = true;
            this.DoCopyResultButton.Click += new System.EventHandler(this.DoCopyResultButton_Click);
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "application.png");
            this.ImageList.Images.SetKeyName(1, "application_warning.png");
            this.ImageList.Images.SetKeyName(2, "balance.png");
            this.ImageList.Images.SetKeyName(3, "calculator.png");
            this.ImageList.Images.SetKeyName(4, "calendar.png");
            this.ImageList.Images.SetKeyName(5, "camera.png");
            this.ImageList.Images.SetKeyName(6, "clock.png");
            this.ImageList.Images.SetKeyName(7, "coffee.png");
            this.ImageList.Images.SetKeyName(8, "computer.png");
            this.ImageList.Images.SetKeyName(9, "direction_down.png");
            this.ImageList.Images.SetKeyName(10, "direction_left.png");
            this.ImageList.Images.SetKeyName(11, "direction_right.png");
            this.ImageList.Images.SetKeyName(12, "direction_up.png");
            this.ImageList.Images.SetKeyName(13, "disc.png");
            this.ImageList.Images.SetKeyName(14, "diskette.png");
            this.ImageList.Images.SetKeyName(15, "document.png");
            this.ImageList.Images.SetKeyName(16, "document_add.png");
            this.ImageList.Images.SetKeyName(17, "document_delete.png");
            this.ImageList.Images.SetKeyName(18, "document_edit.png");
            this.ImageList.Images.SetKeyName(19, "document_search.png");
            this.ImageList.Images.SetKeyName(20, "document_warning.png");
            this.ImageList.Images.SetKeyName(21, "file.png");
            this.ImageList.Images.SetKeyName(22, "file_add.png");
            this.ImageList.Images.SetKeyName(23, "file_delete.png");
            this.ImageList.Images.SetKeyName(24, "file_edit.png");
            this.ImageList.Images.SetKeyName(25, "file_search.png");
            this.ImageList.Images.SetKeyName(26, "file_warning.png");
            this.ImageList.Images.SetKeyName(27, "folder.png");
            this.ImageList.Images.SetKeyName(28, "folder_add.png");
            this.ImageList.Images.SetKeyName(29, "folder_delete.png");
            this.ImageList.Images.SetKeyName(30, "folder_empty.png");
            this.ImageList.Images.SetKeyName(31, "folder_search.png");
            this.ImageList.Images.SetKeyName(32, "folder_warning.png");
            this.ImageList.Images.SetKeyName(33, "Help_32x32.png");
            this.ImageList.Images.SetKeyName(34, "home.png");
            this.ImageList.Images.SetKeyName(35, "load_download.png");
            this.ImageList.Images.SetKeyName(36, "load_upload.png");
            this.ImageList.Images.SetKeyName(37, "mail.png");
            this.ImageList.Images.SetKeyName(38, "mail_delete.png");
            this.ImageList.Images.SetKeyName(39, "mail_receive.png");
            this.ImageList.Images.SetKeyName(40, "mail_search.png");
            this.ImageList.Images.SetKeyName(41, "mail_send.png");
            this.ImageList.Images.SetKeyName(42, "mail_warning.png");
            this.ImageList.Images.SetKeyName(43, "mail_write.png");
            this.ImageList.Images.SetKeyName(44, "message.png");
            this.ImageList.Images.SetKeyName(45, "notification_add.png");
            this.ImageList.Images.SetKeyName(46, "notification_done.png");
            this.ImageList.Images.SetKeyName(47, "notification_error.png");
            this.ImageList.Images.SetKeyName(48, "notification_remove.png");
            this.ImageList.Images.SetKeyName(49, "notification_warning.png");
            this.ImageList.Images.SetKeyName(50, "pay.png");
            this.ImageList.Images.SetKeyName(51, "piechart.png");
            this.ImageList.Images.SetKeyName(52, "player_fastforward.png");
            this.ImageList.Images.SetKeyName(53, "player_pause.png");
            this.ImageList.Images.SetKeyName(54, "player_play.png");
            this.ImageList.Images.SetKeyName(55, "player_record.png");
            this.ImageList.Images.SetKeyName(56, "player_rewind.png");
            this.ImageList.Images.SetKeyName(57, "player_stop.png");
            this.ImageList.Images.SetKeyName(58, "Properties_32x32.png");
            this.ImageList.Images.SetKeyName(59, "Refresh_32x32.png");
            this.ImageList.Images.SetKeyName(60, "Refresh_32x321.png");
            this.ImageList.Images.SetKeyName(61, "rss.png");
            this.ImageList.Images.SetKeyName(62, "search.png");
            this.ImageList.Images.SetKeyName(63, "security_key.png");
            this.ImageList.Images.SetKeyName(64, "security_keyandlock.png");
            this.ImageList.Images.SetKeyName(65, "security_lock.png");
            this.ImageList.Images.SetKeyName(66, "security_unlock.png");
            this.ImageList.Images.SetKeyName(67, "shoppingcart.png");
            this.ImageList.Images.SetKeyName(68, "shoppingcart_add.png");
            this.ImageList.Images.SetKeyName(69, "shoppingcart_checkout.png");
            this.ImageList.Images.SetKeyName(70, "shoppingcart_remove.png");
            this.ImageList.Images.SetKeyName(71, "shoppingcart_warning.png");
            this.ImageList.Images.SetKeyName(72, "star_empty.png");
            this.ImageList.Images.SetKeyName(73, "star_full.png");
            this.ImageList.Images.SetKeyName(74, "star_half.png");
            this.ImageList.Images.SetKeyName(75, "user.png");
            this.ImageList.Images.SetKeyName(76, "user_add.png");
            this.ImageList.Images.SetKeyName(77, "user_delete.png");
            this.ImageList.Images.SetKeyName(78, "user_manage.png");
            this.ImageList.Images.SetKeyName(79, "user_warning.png");
            this.ImageList.Images.SetKeyName(80, "volume.png");
            this.ImageList.Images.SetKeyName(81, "volume_down.png");
            this.ImageList.Images.SetKeyName(82, "volume_mute.png");
            this.ImageList.Images.SetKeyName(83, "volume_up.png");
            // 
            // DoCloseButton
            // 
            this.DoCloseButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.DoCloseButton.ImageIndex = 46;
            this.DoCloseButton.ImageList = this.ImageList;
            this.DoCloseButton.Location = new System.Drawing.Point(470, 16);
            this.DoCloseButton.Name = "DoCloseButton";
            this.DoCloseButton.Size = new System.Drawing.Size(154, 45);
            this.DoCloseButton.TabIndex = 1;
            this.DoCloseButton.Text = "Ok";
            this.DoCloseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoCloseButton.UseVisualStyleBackColor = true;
            this.DoCloseButton.Click += new System.EventHandler(this.DoCloseButton_Click);
            // 
            // ShowMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 448);
            this.Controls.Add(this.ResultOutputGroupBox);
            this.Controls.Add(this.CommandLineGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowMessageForm";
            this.Text = "Сообщение";
            this.Load += new System.EventHandler(this.MessageForm_Load);
            this.Shown += new System.EventHandler(this.MessageForm_Shown);
            this.ResultOutputGroupBox.ResumeLayout(false);
            this.ResultOutputGroupBox.PerformLayout();
            this.CommandLineGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox ResultOutputGroupBox;
        private GroupBox CommandLineGroupBox;
        private Button DoCopyResultButton;
        private TextBox FormMessageTextBox;
        private Button DoCloseButton;
        private ImageList ImageList;
    }
}