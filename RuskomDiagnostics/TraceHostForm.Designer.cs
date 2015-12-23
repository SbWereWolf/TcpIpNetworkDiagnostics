using System.ComponentModel;
using System.Windows.Forms;

namespace RuskomDiagnostics
{
	partial class TraceHostForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TraceHostForm));
            this.HostAddressTextBox = new System.Windows.Forms.TextBox();
            this.DoTraceRouteButton = new System.Windows.Forms.Button();
            this.FormImageList = new System.Windows.Forms.ImageList(this.components);
            this.RouteListView = new System.Windows.Forms.ListView();
            this.hostIPHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hopHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hostNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.roundTripTimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TraceRouteGroupBox = new System.Windows.Forms.GroupBox();
            this.TraceSplitContainer = new System.Windows.Forms.SplitContainer();
            this.HostAddressGroupBox = new System.Windows.Forms.GroupBox();
            this.DoCopyAll = new System.Windows.Forms.Button();
            this.DoÑloseFormButton = new System.Windows.Forms.Button();
            this.RouteGroupBox = new System.Windows.Forms.GroupBox();
            this.Tracert = new RuskomDiagnostics.Tracert();
            this.TraceRouteGroupBox.SuspendLayout();
            this.TraceSplitContainer.Panel1.SuspendLayout();
            this.TraceSplitContainer.Panel2.SuspendLayout();
            this.TraceSplitContainer.SuspendLayout();
            this.HostAddressGroupBox.SuspendLayout();
            this.RouteGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // HostAddressTextBox
            // 
            this.HostAddressTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.HostAddressTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HostAddressTextBox.Location = new System.Drawing.Point(2, 15);
            this.HostAddressTextBox.Name = "HostAddressTextBox";
            this.HostAddressTextBox.Size = new System.Drawing.Size(366, 20);
            this.HostAddressTextBox.TabIndex = 1;
            this.HostAddressTextBox.Text = "RK1.RU";
            this.HostAddressTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HostAddressTextBox_KeyDown);
            // 
            // DoTraceRouteButton
            // 
            this.DoTraceRouteButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.DoTraceRouteButton.ImageIndex = 54;
            this.DoTraceRouteButton.ImageList = this.FormImageList;
            this.DoTraceRouteButton.Location = new System.Drawing.Point(0, 0);
            this.DoTraceRouteButton.Name = "DoTraceRouteButton";
            this.DoTraceRouteButton.Size = new System.Drawing.Size(127, 53);
            this.DoTraceRouteButton.TabIndex = 2;
            this.DoTraceRouteButton.Text = "Ïðîâåðèòü";
            this.DoTraceRouteButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoTraceRouteButton.UseVisualStyleBackColor = true;
            this.DoTraceRouteButton.Click += new System.EventHandler(this.DoTraceRouteButton_Click);
            // 
            // FormImageList
            // 
            this.FormImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FormImageList.ImageStream")));
            this.FormImageList.TransparentColor = System.Drawing.Color.Yellow;
            this.FormImageList.Images.SetKeyName(0, "application.png");
            this.FormImageList.Images.SetKeyName(1, "application_warning.png");
            this.FormImageList.Images.SetKeyName(2, "balance.png");
            this.FormImageList.Images.SetKeyName(3, "calculator.png");
            this.FormImageList.Images.SetKeyName(4, "calendar.png");
            this.FormImageList.Images.SetKeyName(5, "camera.png");
            this.FormImageList.Images.SetKeyName(6, "clock.png");
            this.FormImageList.Images.SetKeyName(7, "coffee.png");
            this.FormImageList.Images.SetKeyName(8, "computer.png");
            this.FormImageList.Images.SetKeyName(9, "direction_down.png");
            this.FormImageList.Images.SetKeyName(10, "direction_left.png");
            this.FormImageList.Images.SetKeyName(11, "direction_right.png");
            this.FormImageList.Images.SetKeyName(12, "direction_up.png");
            this.FormImageList.Images.SetKeyName(13, "disc.png");
            this.FormImageList.Images.SetKeyName(14, "diskette.png");
            this.FormImageList.Images.SetKeyName(15, "document.png");
            this.FormImageList.Images.SetKeyName(16, "document_add.png");
            this.FormImageList.Images.SetKeyName(17, "document_delete.png");
            this.FormImageList.Images.SetKeyName(18, "document_edit.png");
            this.FormImageList.Images.SetKeyName(19, "document_search.png");
            this.FormImageList.Images.SetKeyName(20, "document_warning.png");
            this.FormImageList.Images.SetKeyName(21, "file.png");
            this.FormImageList.Images.SetKeyName(22, "file_add.png");
            this.FormImageList.Images.SetKeyName(23, "file_delete.png");
            this.FormImageList.Images.SetKeyName(24, "file_edit.png");
            this.FormImageList.Images.SetKeyName(25, "file_search.png");
            this.FormImageList.Images.SetKeyName(26, "file_warning.png");
            this.FormImageList.Images.SetKeyName(27, "folder.png");
            this.FormImageList.Images.SetKeyName(28, "folder_add.png");
            this.FormImageList.Images.SetKeyName(29, "folder_delete.png");
            this.FormImageList.Images.SetKeyName(30, "folder_empty.png");
            this.FormImageList.Images.SetKeyName(31, "folder_search.png");
            this.FormImageList.Images.SetKeyName(32, "folder_warning.png");
            this.FormImageList.Images.SetKeyName(33, "Help_32x32.png");
            this.FormImageList.Images.SetKeyName(34, "home.png");
            this.FormImageList.Images.SetKeyName(35, "load_download.png");
            this.FormImageList.Images.SetKeyName(36, "load_upload.png");
            this.FormImageList.Images.SetKeyName(37, "mail.png");
            this.FormImageList.Images.SetKeyName(38, "mail_delete.png");
            this.FormImageList.Images.SetKeyName(39, "mail_receive.png");
            this.FormImageList.Images.SetKeyName(40, "mail_search.png");
            this.FormImageList.Images.SetKeyName(41, "mail_send.png");
            this.FormImageList.Images.SetKeyName(42, "mail_warning.png");
            this.FormImageList.Images.SetKeyName(43, "mail_write.png");
            this.FormImageList.Images.SetKeyName(44, "message.png");
            this.FormImageList.Images.SetKeyName(45, "notification_add.png");
            this.FormImageList.Images.SetKeyName(46, "notification_done.png");
            this.FormImageList.Images.SetKeyName(47, "notification_error.png");
            this.FormImageList.Images.SetKeyName(48, "notification_remove.png");
            this.FormImageList.Images.SetKeyName(49, "notification_warning.png");
            this.FormImageList.Images.SetKeyName(50, "pay.png");
            this.FormImageList.Images.SetKeyName(51, "piechart.png");
            this.FormImageList.Images.SetKeyName(52, "player_fastforward.png");
            this.FormImageList.Images.SetKeyName(53, "player_pause.png");
            this.FormImageList.Images.SetKeyName(54, "player_play.png");
            this.FormImageList.Images.SetKeyName(55, "player_record.png");
            this.FormImageList.Images.SetKeyName(56, "player_rewind.png");
            this.FormImageList.Images.SetKeyName(57, "player_stop.png");
            this.FormImageList.Images.SetKeyName(58, "Properties_32x32.png");
            this.FormImageList.Images.SetKeyName(59, "Refresh_32x32.png");
            this.FormImageList.Images.SetKeyName(60, "Refresh_32x321.png");
            this.FormImageList.Images.SetKeyName(61, "rss.png");
            this.FormImageList.Images.SetKeyName(62, "search.png");
            this.FormImageList.Images.SetKeyName(63, "security_key.png");
            this.FormImageList.Images.SetKeyName(64, "security_keyandlock.png");
            this.FormImageList.Images.SetKeyName(65, "security_lock.png");
            this.FormImageList.Images.SetKeyName(66, "security_unlock.png");
            this.FormImageList.Images.SetKeyName(67, "shoppingcart.png");
            this.FormImageList.Images.SetKeyName(68, "shoppingcart_add.png");
            this.FormImageList.Images.SetKeyName(69, "shoppingcart_checkout.png");
            this.FormImageList.Images.SetKeyName(70, "shoppingcart_remove.png");
            this.FormImageList.Images.SetKeyName(71, "shoppingcart_warning.png");
            this.FormImageList.Images.SetKeyName(72, "star_empty.png");
            this.FormImageList.Images.SetKeyName(73, "star_full.png");
            this.FormImageList.Images.SetKeyName(74, "star_half.png");
            this.FormImageList.Images.SetKeyName(75, "user.png");
            this.FormImageList.Images.SetKeyName(76, "user_add.png");
            this.FormImageList.Images.SetKeyName(77, "user_delete.png");
            this.FormImageList.Images.SetKeyName(78, "user_manage.png");
            this.FormImageList.Images.SetKeyName(79, "user_warning.png");
            this.FormImageList.Images.SetKeyName(80, "volume.png");
            this.FormImageList.Images.SetKeyName(81, "volume_down.png");
            this.FormImageList.Images.SetKeyName(82, "volume_mute.png");
            this.FormImageList.Images.SetKeyName(83, "volume_up.png");
            // 
            // RouteListView
            // 
            this.RouteListView.AllowColumnReorder = true;
            this.RouteListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hostIPHeader,
            this.hopHeader,
            this.hostNameHeader,
            this.roundTripTimeHeader});
            this.RouteListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RouteListView.GridLines = true;
            this.RouteListView.Location = new System.Drawing.Point(2, 15);
            this.RouteListView.Name = "RouteListView";
            this.RouteListView.Size = new System.Drawing.Size(783, 481);
            this.RouteListView.TabIndex = 3;
            this.RouteListView.UseCompatibleStateImageBehavior = false;
            this.RouteListView.View = System.Windows.Forms.View.Details;
            // 
            // hostIPHeader
            // 
            this.hostIPHeader.DisplayIndex = 1;
            this.hostIPHeader.Text = "Àäðåñ óçëà";
            this.hostIPHeader.Width = 133;
            // 
            // hopHeader
            // 
            this.hopHeader.DisplayIndex = 0;
            this.hopHeader.Text = "ï/ï";
            this.hopHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.hopHeader.Width = 42;
            // 
            // hostNameHeader
            // 
            this.hostNameHeader.Text = "Èìÿ óçëà";
            this.hostNameHeader.Width = 300;
            // 
            // roundTripTimeHeader
            // 
            this.roundTripTimeHeader.Text = "Âðåìÿ";
            this.roundTripTimeHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.roundTripTimeHeader.Width = 62;
            // 
            // TraceRouteGroupBox
            // 
            this.TraceRouteGroupBox.Controls.Add(this.TraceSplitContainer);
            this.TraceRouteGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TraceRouteGroupBox.Location = new System.Drawing.Point(0, 498);
            this.TraceRouteGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.TraceRouteGroupBox.Name = "TraceRouteGroupBox";
            this.TraceRouteGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.TraceRouteGroupBox.Size = new System.Drawing.Size(787, 70);
            this.TraceRouteGroupBox.TabIndex = 6;
            this.TraceRouteGroupBox.TabStop = false;
            this.TraceRouteGroupBox.Text = "Ïàíåëü óïðàâëåíèÿ";
            // 
            // TraceSplitContainer
            // 
            this.TraceSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TraceSplitContainer.Location = new System.Drawing.Point(2, 15);
            this.TraceSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.TraceSplitContainer.Name = "TraceSplitContainer";
            // 
            // TraceSplitContainer.Panel1
            // 
            this.TraceSplitContainer.Panel1.Controls.Add(this.HostAddressGroupBox);
            // 
            // TraceSplitContainer.Panel2
            // 
            this.TraceSplitContainer.Panel2.Controls.Add(this.DoCopyAll);
            this.TraceSplitContainer.Panel2.Controls.Add(this.DoÑloseFormButton);
            this.TraceSplitContainer.Panel2.Controls.Add(this.DoTraceRouteButton);
            this.TraceSplitContainer.Size = new System.Drawing.Size(783, 53);
            this.TraceSplitContainer.SplitterDistance = 370;
            this.TraceSplitContainer.SplitterWidth = 3;
            this.TraceSplitContainer.TabIndex = 7;
            // 
            // HostAddressGroupBox
            // 
            this.HostAddressGroupBox.Controls.Add(this.HostAddressTextBox);
            this.HostAddressGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HostAddressGroupBox.Location = new System.Drawing.Point(0, 0);
            this.HostAddressGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.HostAddressGroupBox.Name = "HostAddressGroupBox";
            this.HostAddressGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.HostAddressGroupBox.Size = new System.Drawing.Size(370, 53);
            this.HostAddressGroupBox.TabIndex = 2;
            this.HostAddressGroupBox.TabStop = false;
            this.HostAddressGroupBox.Text = "Àäðåñ";
            // 
            // DoCopyAll
            // 
            this.DoCopyAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DoCopyAll.ImageIndex = 24;
            this.DoCopyAll.ImageList = this.FormImageList;
            this.DoCopyAll.Location = new System.Drawing.Point(127, 0);
            this.DoCopyAll.Name = "DoCopyAll";
            this.DoCopyAll.Size = new System.Drawing.Size(178, 53);
            this.DoCopyAll.TabIndex = 4;
            this.DoCopyAll.Text = "Êîïèðîâàòü âñ¸";
            this.DoCopyAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoCopyAll.UseVisualStyleBackColor = true;
            this.DoCopyAll.Click += new System.EventHandler(this.DoCopyAll_Click);
            // 
            // DoÑloseFormButton
            // 
            this.DoÑloseFormButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.DoÑloseFormButton.ImageIndex = 46;
            this.DoÑloseFormButton.ImageList = this.FormImageList;
            this.DoÑloseFormButton.Location = new System.Drawing.Point(305, 0);
            this.DoÑloseFormButton.Name = "DoÑloseFormButton";
            this.DoÑloseFormButton.Size = new System.Drawing.Size(105, 53);
            this.DoÑloseFormButton.TabIndex = 3;
            this.DoÑloseFormButton.Text = "Ok";
            this.DoÑloseFormButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoÑloseFormButton.UseVisualStyleBackColor = true;
            this.DoÑloseFormButton.Click += new System.EventHandler(this.DoCloseFormButton_Click);
            // 
            // RouteGroupBox
            // 
            this.RouteGroupBox.Controls.Add(this.RouteListView);
            this.RouteGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RouteGroupBox.Location = new System.Drawing.Point(0, 0);
            this.RouteGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.RouteGroupBox.Name = "RouteGroupBox";
            this.RouteGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.RouteGroupBox.Size = new System.Drawing.Size(787, 498);
            this.RouteGroupBox.TabIndex = 7;
            this.RouteGroupBox.TabStop = false;
            this.RouteGroupBox.Text = "Ìàðøðóò";
            // 

            // 
            // TraceHostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 568);
            this.Controls.Add(this.RouteGroupBox);
            this.Controls.Add(this.TraceRouteGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TraceHostForm";
            this.Text = "Ïðîâåðèòü ìàðøðóò";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TraceToHostForm_FormClosing);
            this.Load += new System.EventHandler(this.TraceHostForm_Load);
            this.Shown += new System.EventHandler(this.TraceHostForm_Shown);
            this.TraceRouteGroupBox.ResumeLayout(false);
            this.TraceSplitContainer.Panel1.ResumeLayout(false);
            this.TraceSplitContainer.Panel2.ResumeLayout(false);
            this.TraceSplitContainer.ResumeLayout(false);
            this.HostAddressGroupBox.ResumeLayout(false);
            this.HostAddressGroupBox.PerformLayout();
            this.RouteGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

            // Tracert
            // 
            this.Tracert.HostNameOrAddress = null;
            this.Tracert.MaxHops = 30;
            this.Tracert.TimeOut = 5000;
            this.Tracert.Done += new System.EventHandler(this.tracert_Done);
            this.Tracert.RouteNodeFound += new System.EventHandler<RuskomDiagnostics.RouteNodeFoundEventArgs>(this.tracert_RouteNodeFound);

		}

		#endregion

		private TextBox HostAddressTextBox;
		private Button DoTraceRouteButton;
		private ListView RouteListView;
        private Tracert Tracert;
		private ColumnHeader hostIPHeader;
		private ColumnHeader hopHeader;
		private ColumnHeader hostNameHeader;
		private ColumnHeader roundTripTimeHeader;
        private GroupBox TraceRouteGroupBox;
        private GroupBox HostAddressGroupBox;
        private GroupBox RouteGroupBox;
        private SplitContainer TraceSplitContainer;
        private ImageList FormImageList;
        private Button DoÑloseFormButton;
        private Button DoCopyAll;
	}
}

