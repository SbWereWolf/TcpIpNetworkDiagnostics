using System.ComponentModel;
using System.Windows.Forms;

namespace RuskomDiagnostics
{
    sealed partial class ShowNetworkRequisitesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowNetworkRequisitesForm));
            this.CommandGroupBox = new System.Windows.Forms.GroupBox();
            this.DoCopyLog = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.DoShowRequisitesButton = new System.Windows.Forms.Button();
            this.DoCloseFormButton = new System.Windows.Forms.Button();
            this.RequisitesGroupBox = new System.Windows.Forms.GroupBox();
            this.RequisitesTextBox = new System.Windows.Forms.TextBox();
            this.OutputCommandSplitContainer = new System.Windows.Forms.SplitContainer();
            this.BatchProgressBar = new System.Windows.Forms.ProgressBar();
            this.CommandGroupBox.SuspendLayout();
            this.RequisitesGroupBox.SuspendLayout();
            this.OutputCommandSplitContainer.Panel1.SuspendLayout();
            this.OutputCommandSplitContainer.Panel2.SuspendLayout();
            this.OutputCommandSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // CommandGroupBox
            // 
            this.CommandGroupBox.Controls.Add(this.DoCopyLog);
            this.CommandGroupBox.Controls.Add(this.DoShowRequisitesButton);
            this.CommandGroupBox.Controls.Add(this.DoCloseFormButton);
            this.CommandGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommandGroupBox.Location = new System.Drawing.Point(0, 0);
            this.CommandGroupBox.Name = "CommandGroupBox";
            this.CommandGroupBox.Size = new System.Drawing.Size(583, 59);
            this.CommandGroupBox.TabIndex = 1;
            this.CommandGroupBox.TabStop = false;
            this.CommandGroupBox.Text = "Команды";
            // 
            // DoCopyLog
            // 
            this.DoCopyLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DoCopyLog.ImageIndex = 24;
            this.DoCopyLog.ImageList = this.imageList;
            this.DoCopyLog.Location = new System.Drawing.Point(181, 16);
            this.DoCopyLog.Name = "DoCopyLog";
            this.DoCopyLog.Size = new System.Drawing.Size(284, 40);
            this.DoCopyLog.TabIndex = 0;
            this.DoCopyLog.Text = "Копировать всё";
            this.DoCopyLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoCopyLog.UseVisualStyleBackColor = true;
            this.DoCopyLog.Click += new System.EventHandler(this.DoCopyLog_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "application.png");
            this.imageList.Images.SetKeyName(1, "application_warning.png");
            this.imageList.Images.SetKeyName(2, "balance.png");
            this.imageList.Images.SetKeyName(3, "calculator.png");
            this.imageList.Images.SetKeyName(4, "calendar.png");
            this.imageList.Images.SetKeyName(5, "camera.png");
            this.imageList.Images.SetKeyName(6, "clock.png");
            this.imageList.Images.SetKeyName(7, "coffee.png");
            this.imageList.Images.SetKeyName(8, "computer.png");
            this.imageList.Images.SetKeyName(9, "direction_down.png");
            this.imageList.Images.SetKeyName(10, "direction_left.png");
            this.imageList.Images.SetKeyName(11, "direction_right.png");
            this.imageList.Images.SetKeyName(12, "direction_up.png");
            this.imageList.Images.SetKeyName(13, "disc.png");
            this.imageList.Images.SetKeyName(14, "diskette.png");
            this.imageList.Images.SetKeyName(15, "document.png");
            this.imageList.Images.SetKeyName(16, "document_add.png");
            this.imageList.Images.SetKeyName(17, "document_delete.png");
            this.imageList.Images.SetKeyName(18, "document_edit.png");
            this.imageList.Images.SetKeyName(19, "document_search.png");
            this.imageList.Images.SetKeyName(20, "document_warning.png");
            this.imageList.Images.SetKeyName(21, "file.png");
            this.imageList.Images.SetKeyName(22, "file_add.png");
            this.imageList.Images.SetKeyName(23, "file_delete.png");
            this.imageList.Images.SetKeyName(24, "file_edit.png");
            this.imageList.Images.SetKeyName(25, "file_search.png");
            this.imageList.Images.SetKeyName(26, "file_warning.png");
            this.imageList.Images.SetKeyName(27, "folder.png");
            this.imageList.Images.SetKeyName(28, "folder_add.png");
            this.imageList.Images.SetKeyName(29, "folder_delete.png");
            this.imageList.Images.SetKeyName(30, "folder_empty.png");
            this.imageList.Images.SetKeyName(31, "folder_search.png");
            this.imageList.Images.SetKeyName(32, "folder_warning.png");
            this.imageList.Images.SetKeyName(33, "Help_32x32.png");
            this.imageList.Images.SetKeyName(34, "home.png");
            this.imageList.Images.SetKeyName(35, "load_download.png");
            this.imageList.Images.SetKeyName(36, "load_upload.png");
            this.imageList.Images.SetKeyName(37, "mail.png");
            this.imageList.Images.SetKeyName(38, "mail_delete.png");
            this.imageList.Images.SetKeyName(39, "mail_receive.png");
            this.imageList.Images.SetKeyName(40, "mail_search.png");
            this.imageList.Images.SetKeyName(41, "mail_send.png");
            this.imageList.Images.SetKeyName(42, "mail_warning.png");
            this.imageList.Images.SetKeyName(43, "mail_write.png");
            this.imageList.Images.SetKeyName(44, "message.png");
            this.imageList.Images.SetKeyName(45, "notification_add.png");
            this.imageList.Images.SetKeyName(46, "notification_done.png");
            this.imageList.Images.SetKeyName(47, "notification_error.png");
            this.imageList.Images.SetKeyName(48, "notification_remove.png");
            this.imageList.Images.SetKeyName(49, "notification_warning.png");
            this.imageList.Images.SetKeyName(50, "pay.png");
            this.imageList.Images.SetKeyName(51, "piechart.png");
            this.imageList.Images.SetKeyName(52, "player_fastforward.png");
            this.imageList.Images.SetKeyName(53, "player_pause.png");
            this.imageList.Images.SetKeyName(54, "player_play.png");
            this.imageList.Images.SetKeyName(55, "player_record.png");
            this.imageList.Images.SetKeyName(56, "player_rewind.png");
            this.imageList.Images.SetKeyName(57, "player_stop.png");
            this.imageList.Images.SetKeyName(58, "Properties_32x32.png");
            this.imageList.Images.SetKeyName(59, "Refresh_32x32.png");
            this.imageList.Images.SetKeyName(60, "Refresh_32x321.png");
            this.imageList.Images.SetKeyName(61, "rss.png");
            this.imageList.Images.SetKeyName(62, "search.png");
            this.imageList.Images.SetKeyName(63, "security_key.png");
            this.imageList.Images.SetKeyName(64, "security_keyandlock.png");
            this.imageList.Images.SetKeyName(65, "security_lock.png");
            this.imageList.Images.SetKeyName(66, "security_unlock.png");
            this.imageList.Images.SetKeyName(67, "shoppingcart.png");
            this.imageList.Images.SetKeyName(68, "shoppingcart_add.png");
            this.imageList.Images.SetKeyName(69, "shoppingcart_checkout.png");
            this.imageList.Images.SetKeyName(70, "shoppingcart_remove.png");
            this.imageList.Images.SetKeyName(71, "shoppingcart_warning.png");
            this.imageList.Images.SetKeyName(72, "star_empty.png");
            this.imageList.Images.SetKeyName(73, "star_full.png");
            this.imageList.Images.SetKeyName(74, "star_half.png");
            this.imageList.Images.SetKeyName(75, "user.png");
            this.imageList.Images.SetKeyName(76, "user_add.png");
            this.imageList.Images.SetKeyName(77, "user_delete.png");
            this.imageList.Images.SetKeyName(78, "user_manage.png");
            this.imageList.Images.SetKeyName(79, "user_warning.png");
            this.imageList.Images.SetKeyName(80, "volume.png");
            this.imageList.Images.SetKeyName(81, "volume_down.png");
            this.imageList.Images.SetKeyName(82, "volume_mute.png");
            this.imageList.Images.SetKeyName(83, "volume_up.png");
            // 
            // DoShowRequisitesButton
            // 
            this.DoShowRequisitesButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.DoShowRequisitesButton.ImageIndex = 54;
            this.DoShowRequisitesButton.ImageList = this.imageList;
            this.DoShowRequisitesButton.Location = new System.Drawing.Point(3, 16);
            this.DoShowRequisitesButton.Name = "DoShowRequisitesButton";
            this.DoShowRequisitesButton.Size = new System.Drawing.Size(178, 40);
            this.DoShowRequisitesButton.TabIndex = 2;
            this.DoShowRequisitesButton.Text = "Проверить";
            this.DoShowRequisitesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoShowRequisitesButton.UseVisualStyleBackColor = true;
            this.DoShowRequisitesButton.Click += new System.EventHandler(this.DoShowRequisitesButton_Click);
            // 
            // DoCloseFormButton
            // 
            this.DoCloseFormButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.DoCloseFormButton.ImageIndex = 46;
            this.DoCloseFormButton.ImageList = this.imageList;
            this.DoCloseFormButton.Location = new System.Drawing.Point(465, 16);
            this.DoCloseFormButton.Name = "DoCloseFormButton";
            this.DoCloseFormButton.Size = new System.Drawing.Size(115, 40);
            this.DoCloseFormButton.TabIndex = 1;
            this.DoCloseFormButton.Text = "Ok";
            this.DoCloseFormButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoCloseFormButton.UseVisualStyleBackColor = true;
            this.DoCloseFormButton.Click += new System.EventHandler(this.DoCloseFormButton_Click);
            // 
            // RequisitesGroupBox
            // 
            this.RequisitesGroupBox.Controls.Add(this.RequisitesTextBox);
            this.RequisitesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RequisitesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.RequisitesGroupBox.Name = "RequisitesGroupBox";
            this.RequisitesGroupBox.Size = new System.Drawing.Size(583, 362);
            this.RequisitesGroupBox.TabIndex = 0;
            this.RequisitesGroupBox.TabStop = false;
            this.RequisitesGroupBox.Text = "Реквизиты сетевых подключений";
            // 
            // RequisitesTextBox
            // 
            this.RequisitesTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RequisitesTextBox.Location = new System.Drawing.Point(3, 16);
            this.RequisitesTextBox.Multiline = true;
            this.RequisitesTextBox.Name = "RequisitesTextBox";
            this.RequisitesTextBox.Size = new System.Drawing.Size(577, 343);
            this.RequisitesTextBox.TabIndex = 0;
            // 
            // OutputCommandSplitContainer
            // 
            this.OutputCommandSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputCommandSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.OutputCommandSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.OutputCommandSplitContainer.Name = "OutputCommandSplitContainer";
            this.OutputCommandSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // OutputCommandSplitContainer.Panel1
            // 
            this.OutputCommandSplitContainer.Panel1.Controls.Add(this.RequisitesGroupBox);
            // 
            // OutputCommandSplitContainer.Panel2
            // 
            this.OutputCommandSplitContainer.Panel2.Controls.Add(this.CommandGroupBox);
            this.OutputCommandSplitContainer.Size = new System.Drawing.Size(583, 424);
            this.OutputCommandSplitContainer.SplitterDistance = 362;
            this.OutputCommandSplitContainer.SplitterWidth = 3;
            this.OutputCommandSplitContainer.TabIndex = 2;
            // 
            // BatchProgressBar
            // 
            this.BatchProgressBar.BackColor = System.Drawing.SystemColors.Info;
            this.BatchProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BatchProgressBar.Location = new System.Drawing.Point(0, 424);
            this.BatchProgressBar.Name = "BatchProgressBar";
            this.BatchProgressBar.Size = new System.Drawing.Size(583, 23);
            this.BatchProgressBar.Step = 1;
            this.BatchProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BatchProgressBar.TabIndex = 3;
            this.BatchProgressBar.Value = 33;
            // 
            // ShowNetworkRequisitesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 447);
            this.Controls.Add(this.OutputCommandSplitContainer);
            this.Controls.Add(this.BatchProgressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowNetworkRequisitesForm";
            this.Text = "Проверка реквизитов сетевых подключений";
            this.Load += new System.EventHandler(this.ShowNetworkRequisitesForm_Load);
            this.Shown += new System.EventHandler(this.ShowNetworkRequisitesForm_Shown);
            this.CommandGroupBox.ResumeLayout(false);
            this.RequisitesGroupBox.ResumeLayout(false);
            this.RequisitesGroupBox.PerformLayout();
            this.OutputCommandSplitContainer.Panel1.ResumeLayout(false);
            this.OutputCommandSplitContainer.Panel2.ResumeLayout(false);
            this.OutputCommandSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox CommandGroupBox;
        private Button DoCloseFormButton;
        private Button DoCopyLog;
        private GroupBox RequisitesGroupBox;
        private SplitContainer OutputCommandSplitContainer;
        private ImageList imageList;
        private Button DoShowRequisitesButton;
        private ProgressBar BatchProgressBar;
        private TextBox RequisitesTextBox;
    }
}