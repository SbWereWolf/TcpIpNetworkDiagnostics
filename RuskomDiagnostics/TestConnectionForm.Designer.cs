using System.ComponentModel;
using System.Windows.Forms;

namespace RuskomDiagnostics
{
    sealed partial class TestConnectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestConnectionForm));
            this.DoCopyLog = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.DoTestConnectionButton = new System.Windows.Forms.Button();
            this.DoCloseFormButton = new System.Windows.Forms.Button();
            this.HomeSplitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.RequsitesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.PingGatewayGroupBox = new System.Windows.Forms.GroupBox();
            this.PingGatewayTextBox = new System.Windows.Forms.TextBox();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.PingDnsTextBox = new System.Windows.Forms.TextBox();
            this.TestHomeLogGroupBox = new System.Windows.Forms.GroupBox();
            this.HomeNetworkSplitContainer = new System.Windows.Forms.SplitContainer();
            this.PingHomeTextBox = new System.Windows.Forms.TextBox();
            this.TraceHomeTextBox = new System.Windows.Forms.TextBox();
            this.CityWorldSplitContainer = new System.Windows.Forms.SplitContainer();
            this.TestCityLogGroupBox = new System.Windows.Forms.GroupBox();
            this.CityNetworkSplitContainer = new System.Windows.Forms.SplitContainer();
            this.PingCityTextBox = new System.Windows.Forms.TextBox();
            this.TraceCityTextBox = new System.Windows.Forms.TextBox();
            this.TestWordLogGroupBox = new System.Windows.Forms.GroupBox();
            this.WorldSplitContainer = new System.Windows.Forms.SplitContainer();
            this.PingCountryTextBox = new System.Windows.Forms.TextBox();
            this.TraceCountryTextBox = new System.Windows.Forms.TextBox();
            this.OutputCommandSplitContainer = new System.Windows.Forms.SplitContainer();
            this.TestingProgressBar = new System.Windows.Forms.ProgressBar();
            this.DoReportProblemButton = new System.Windows.Forms.Button();
            this.HomeSplitContainer.Panel1.SuspendLayout();
            this.HomeSplitContainer.Panel2.SuspendLayout();
            this.HomeSplitContainer.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.RequsitesSplitContainer.Panel1.SuspendLayout();
            this.RequsitesSplitContainer.Panel2.SuspendLayout();
            this.RequsitesSplitContainer.SuspendLayout();
            this.PingGatewayGroupBox.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.TestHomeLogGroupBox.SuspendLayout();
            this.HomeNetworkSplitContainer.Panel1.SuspendLayout();
            this.HomeNetworkSplitContainer.Panel2.SuspendLayout();
            this.HomeNetworkSplitContainer.SuspendLayout();
            this.CityWorldSplitContainer.Panel1.SuspendLayout();
            this.CityWorldSplitContainer.Panel2.SuspendLayout();
            this.CityWorldSplitContainer.SuspendLayout();
            this.TestCityLogGroupBox.SuspendLayout();
            this.CityNetworkSplitContainer.Panel1.SuspendLayout();
            this.CityNetworkSplitContainer.Panel2.SuspendLayout();
            this.CityNetworkSplitContainer.SuspendLayout();
            this.TestWordLogGroupBox.SuspendLayout();
            this.WorldSplitContainer.Panel1.SuspendLayout();
            this.WorldSplitContainer.Panel2.SuspendLayout();
            this.WorldSplitContainer.SuspendLayout();
            this.OutputCommandSplitContainer.Panel1.SuspendLayout();
            this.OutputCommandSplitContainer.Panel2.SuspendLayout();
            this.OutputCommandSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // DoCopyLog
            // 
            this.DoCopyLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DoCopyLog.ImageIndex = 24;
            this.DoCopyLog.ImageList = this.imageList;
            this.DoCopyLog.Location = new System.Drawing.Point(155, 0);
            this.DoCopyLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DoCopyLog.Name = "DoCopyLog";
            this.DoCopyLog.Size = new System.Drawing.Size(238, 37);
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
            // DoTestConnectionButton
            // 
            this.DoTestConnectionButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.DoTestConnectionButton.ImageIndex = 54;
            this.DoTestConnectionButton.ImageList = this.imageList;
            this.DoTestConnectionButton.Location = new System.Drawing.Point(0, 0);
            this.DoTestConnectionButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DoTestConnectionButton.Name = "DoTestConnectionButton";
            this.DoTestConnectionButton.Size = new System.Drawing.Size(155, 37);
            this.DoTestConnectionButton.TabIndex = 2;
            this.DoTestConnectionButton.Text = "Проверить";
            this.DoTestConnectionButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoTestConnectionButton.UseVisualStyleBackColor = true;
            this.DoTestConnectionButton.Click += new System.EventHandler(this.DoTestConnectionButton_Click);
            // 
            // DoCloseFormButton
            // 
            this.DoCloseFormButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.DoCloseFormButton.ImageIndex = 46;
            this.DoCloseFormButton.ImageList = this.imageList;
            this.DoCloseFormButton.Location = new System.Drawing.Point(652, 0);
            this.DoCloseFormButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DoCloseFormButton.Name = "DoCloseFormButton";
            this.DoCloseFormButton.Size = new System.Drawing.Size(125, 37);
            this.DoCloseFormButton.TabIndex = 1;
            this.DoCloseFormButton.Text = "Ok";
            this.DoCloseFormButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoCloseFormButton.UseVisualStyleBackColor = true;
            this.DoCloseFormButton.Click += new System.EventHandler(this.DoCloseFormButton_Click);
            // 
            // HomeSplitContainer
            // 
            this.HomeSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HomeSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.HomeSplitContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HomeSplitContainer.Name = "HomeSplitContainer";
            this.HomeSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // HomeSplitContainer.Panel1
            // 
            this.HomeSplitContainer.Panel1.Controls.Add(this.splitContainer1);
            // 
            // HomeSplitContainer.Panel2
            // 
            this.HomeSplitContainer.Panel2.Controls.Add(this.CityWorldSplitContainer);
            this.HomeSplitContainer.Size = new System.Drawing.Size(777, 497);
            this.HomeSplitContainer.SplitterDistance = 246;
            this.HomeSplitContainer.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.RequsitesSplitContainer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TestHomeLogGroupBox);
            this.splitContainer1.Size = new System.Drawing.Size(777, 246);
            this.splitContainer1.SplitterDistance = 120;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // RequsitesSplitContainer
            // 
            this.RequsitesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RequsitesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.RequsitesSplitContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RequsitesSplitContainer.Name = "RequsitesSplitContainer";
            // 
            // RequsitesSplitContainer.Panel1
            // 
            this.RequsitesSplitContainer.Panel1.Controls.Add(this.PingGatewayGroupBox);
            // 
            // RequsitesSplitContainer.Panel2
            // 
            this.RequsitesSplitContainer.Panel2.Controls.Add(this.groupBox);
            this.RequsitesSplitContainer.Size = new System.Drawing.Size(777, 120);
            this.RequsitesSplitContainer.SplitterDistance = 385;
            this.RequsitesSplitContainer.TabIndex = 0;
            // 
            // PingGatewayGroupBox
            // 
            this.PingGatewayGroupBox.Controls.Add(this.PingGatewayTextBox);
            this.PingGatewayGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PingGatewayGroupBox.Location = new System.Drawing.Point(0, 0);
            this.PingGatewayGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PingGatewayGroupBox.Name = "PingGatewayGroupBox";
            this.PingGatewayGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PingGatewayGroupBox.Size = new System.Drawing.Size(385, 120);
            this.PingGatewayGroupBox.TabIndex = 1;
            this.PingGatewayGroupBox.TabStop = false;
            this.PingGatewayGroupBox.Text = "Связь со шлюзом";
            // 
            // PingGatewayTextBox
            // 
            this.PingGatewayTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PingGatewayTextBox.Location = new System.Drawing.Point(4, 19);
            this.PingGatewayTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PingGatewayTextBox.Multiline = true;
            this.PingGatewayTextBox.Name = "PingGatewayTextBox";
            this.PingGatewayTextBox.ReadOnly = true;
            this.PingGatewayTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PingGatewayTextBox.Size = new System.Drawing.Size(377, 97);
            this.PingGatewayTextBox.TabIndex = 0;
            this.PingGatewayTextBox.Text = "В это поле будет выведен результат проверки связи со шлюзом";
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.PingDnsTextBox);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox.Name = "groupBox";
            this.groupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox.Size = new System.Drawing.Size(388, 120);
            this.groupBox.TabIndex = 2;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Связь с Днс сервром";
            // 
            // PingDnsTextBox
            // 
            this.PingDnsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PingDnsTextBox.Location = new System.Drawing.Point(4, 19);
            this.PingDnsTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PingDnsTextBox.Multiline = true;
            this.PingDnsTextBox.Name = "PingDnsTextBox";
            this.PingDnsTextBox.ReadOnly = true;
            this.PingDnsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PingDnsTextBox.Size = new System.Drawing.Size(380, 97);
            this.PingDnsTextBox.TabIndex = 1;
            this.PingDnsTextBox.Text = "В это поле будет выведен результат проверки связи с ДэНэЭс сервером";
            // 
            // TestHomeLogGroupBox
            // 
            this.TestHomeLogGroupBox.Controls.Add(this.HomeNetworkSplitContainer);
            this.TestHomeLogGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestHomeLogGroupBox.Location = new System.Drawing.Point(0, 0);
            this.TestHomeLogGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestHomeLogGroupBox.Name = "TestHomeLogGroupBox";
            this.TestHomeLogGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestHomeLogGroupBox.Size = new System.Drawing.Size(777, 121);
            this.TestHomeLogGroupBox.TabIndex = 0;
            this.TestHomeLogGroupBox.TabStop = false;
            this.TestHomeLogGroupBox.Text = "Проверка связи в домашней сети";
            // 
            // HomeNetworkSplitContainer
            // 
            this.HomeNetworkSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HomeNetworkSplitContainer.Location = new System.Drawing.Point(4, 19);
            this.HomeNetworkSplitContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HomeNetworkSplitContainer.Name = "HomeNetworkSplitContainer";
            // 
            // HomeNetworkSplitContainer.Panel1
            // 
            this.HomeNetworkSplitContainer.Panel1.Controls.Add(this.PingHomeTextBox);
            // 
            // HomeNetworkSplitContainer.Panel2
            // 
            this.HomeNetworkSplitContainer.Panel2.Controls.Add(this.TraceHomeTextBox);
            this.HomeNetworkSplitContainer.Size = new System.Drawing.Size(769, 98);
            this.HomeNetworkSplitContainer.SplitterDistance = 381;
            this.HomeNetworkSplitContainer.TabIndex = 0;
            // 
            // PingHomeTextBox
            // 
            this.PingHomeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PingHomeTextBox.Location = new System.Drawing.Point(0, 0);
            this.PingHomeTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PingHomeTextBox.Multiline = true;
            this.PingHomeTextBox.Name = "PingHomeTextBox";
            this.PingHomeTextBox.ReadOnly = true;
            this.PingHomeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PingHomeTextBox.Size = new System.Drawing.Size(381, 98);
            this.PingHomeTextBox.TabIndex = 0;
            this.PingHomeTextBox.Text = "В это поле будет выведен результат проверки связи во внутреней сети Рускома";
            // 
            // TraceHomeTextBox
            // 
            this.TraceHomeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TraceHomeTextBox.Location = new System.Drawing.Point(0, 0);
            this.TraceHomeTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TraceHomeTextBox.Multiline = true;
            this.TraceHomeTextBox.Name = "TraceHomeTextBox";
            this.TraceHomeTextBox.ReadOnly = true;
            this.TraceHomeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TraceHomeTextBox.Size = new System.Drawing.Size(384, 98);
            this.TraceHomeTextBox.TabIndex = 1;
            this.TraceHomeTextBox.Text = "В это поле будет выведен результат построения маршрута по внутреней сети Рускома";
            // 
            // CityWorldSplitContainer
            // 
            this.CityWorldSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CityWorldSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.CityWorldSplitContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CityWorldSplitContainer.Name = "CityWorldSplitContainer";
            this.CityWorldSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // CityWorldSplitContainer.Panel1
            // 
            this.CityWorldSplitContainer.Panel1.Controls.Add(this.TestCityLogGroupBox);
            // 
            // CityWorldSplitContainer.Panel2
            // 
            this.CityWorldSplitContainer.Panel2.Controls.Add(this.TestWordLogGroupBox);
            this.CityWorldSplitContainer.Size = new System.Drawing.Size(777, 247);
            this.CityWorldSplitContainer.SplitterDistance = 117;
            this.CityWorldSplitContainer.TabIndex = 2;
            // 
            // TestCityLogGroupBox
            // 
            this.TestCityLogGroupBox.Controls.Add(this.CityNetworkSplitContainer);
            this.TestCityLogGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestCityLogGroupBox.Location = new System.Drawing.Point(0, 0);
            this.TestCityLogGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestCityLogGroupBox.Name = "TestCityLogGroupBox";
            this.TestCityLogGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestCityLogGroupBox.Size = new System.Drawing.Size(777, 117);
            this.TestCityLogGroupBox.TabIndex = 1;
            this.TestCityLogGroupBox.TabStop = false;
            this.TestCityLogGroupBox.Text = "Проверка связи в городской сети";
            // 
            // CityNetworkSplitContainer
            // 
            this.CityNetworkSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CityNetworkSplitContainer.Location = new System.Drawing.Point(4, 19);
            this.CityNetworkSplitContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CityNetworkSplitContainer.Name = "CityNetworkSplitContainer";
            // 
            // CityNetworkSplitContainer.Panel1
            // 
            this.CityNetworkSplitContainer.Panel1.Controls.Add(this.PingCityTextBox);
            // 
            // CityNetworkSplitContainer.Panel2
            // 
            this.CityNetworkSplitContainer.Panel2.Controls.Add(this.TraceCityTextBox);
            this.CityNetworkSplitContainer.Size = new System.Drawing.Size(769, 94);
            this.CityNetworkSplitContainer.SplitterDistance = 381;
            this.CityNetworkSplitContainer.TabIndex = 1;
            // 
            // PingCityTextBox
            // 
            this.PingCityTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PingCityTextBox.Location = new System.Drawing.Point(0, 0);
            this.PingCityTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PingCityTextBox.Multiline = true;
            this.PingCityTextBox.Name = "PingCityTextBox";
            this.PingCityTextBox.ReadOnly = true;
            this.PingCityTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PingCityTextBox.Size = new System.Drawing.Size(381, 94);
            this.PingCityTextBox.TabIndex = 1;
            this.PingCityTextBox.Text = "В это поле будет выведен результат проверки связи в городской сети";
            // 
            // TraceCityTextBox
            // 
            this.TraceCityTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TraceCityTextBox.Location = new System.Drawing.Point(0, 0);
            this.TraceCityTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TraceCityTextBox.Multiline = true;
            this.TraceCityTextBox.Name = "TraceCityTextBox";
            this.TraceCityTextBox.ReadOnly = true;
            this.TraceCityTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TraceCityTextBox.Size = new System.Drawing.Size(384, 94);
            this.TraceCityTextBox.TabIndex = 2;
            this.TraceCityTextBox.Text = "В это поле будет выведен результат построения маршрута по гороской сети";
            // 
            // TestWordLogGroupBox
            // 
            this.TestWordLogGroupBox.Controls.Add(this.WorldSplitContainer);
            this.TestWordLogGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestWordLogGroupBox.Location = new System.Drawing.Point(0, 0);
            this.TestWordLogGroupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestWordLogGroupBox.Name = "TestWordLogGroupBox";
            this.TestWordLogGroupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestWordLogGroupBox.Size = new System.Drawing.Size(777, 126);
            this.TestWordLogGroupBox.TabIndex = 2;
            this.TestWordLogGroupBox.TabStop = false;
            this.TestWordLogGroupBox.Text = "Проверка связи во внешней сети";
            // 
            // WorldSplitContainer
            // 
            this.WorldSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorldSplitContainer.Location = new System.Drawing.Point(4, 19);
            this.WorldSplitContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.WorldSplitContainer.Name = "WorldSplitContainer";
            // 
            // WorldSplitContainer.Panel1
            // 
            this.WorldSplitContainer.Panel1.Controls.Add(this.PingCountryTextBox);
            // 
            // WorldSplitContainer.Panel2
            // 
            this.WorldSplitContainer.Panel2.Controls.Add(this.TraceCountryTextBox);
            this.WorldSplitContainer.Size = new System.Drawing.Size(769, 103);
            this.WorldSplitContainer.SplitterDistance = 381;
            this.WorldSplitContainer.TabIndex = 1;
            // 
            // PingCountryTextBox
            // 
            this.PingCountryTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PingCountryTextBox.Location = new System.Drawing.Point(0, 0);
            this.PingCountryTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PingCountryTextBox.Multiline = true;
            this.PingCountryTextBox.Name = "PingCountryTextBox";
            this.PingCountryTextBox.ReadOnly = true;
            this.PingCountryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PingCountryTextBox.Size = new System.Drawing.Size(381, 103);
            this.PingCountryTextBox.TabIndex = 2;
            this.PingCountryTextBox.Text = "В это поле будет выведен результат проверки связи во внешней сети";
            // 
            // TraceCountryTextBox
            // 
            this.TraceCountryTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TraceCountryTextBox.Location = new System.Drawing.Point(0, 0);
            this.TraceCountryTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TraceCountryTextBox.Multiline = true;
            this.TraceCountryTextBox.Name = "TraceCountryTextBox";
            this.TraceCountryTextBox.ReadOnly = true;
            this.TraceCountryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TraceCountryTextBox.Size = new System.Drawing.Size(384, 103);
            this.TraceCountryTextBox.TabIndex = 3;
            this.TraceCountryTextBox.Text = "В это поле будет выведен результат построения маршрута по внешней сети";
            // 
            // OutputCommandSplitContainer
            // 
            this.OutputCommandSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputCommandSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.OutputCommandSplitContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OutputCommandSplitContainer.Name = "OutputCommandSplitContainer";
            this.OutputCommandSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // OutputCommandSplitContainer.Panel1
            // 
            this.OutputCommandSplitContainer.Panel1.Controls.Add(this.HomeSplitContainer);
            // 
            // OutputCommandSplitContainer.Panel2
            // 
            this.OutputCommandSplitContainer.Panel2.Controls.Add(this.DoCopyLog);
            this.OutputCommandSplitContainer.Panel2.Controls.Add(this.DoReportProblemButton);
            this.OutputCommandSplitContainer.Panel2.Controls.Add(this.DoCloseFormButton);
            this.OutputCommandSplitContainer.Panel2.Controls.Add(this.DoTestConnectionButton);
            this.OutputCommandSplitContainer.Size = new System.Drawing.Size(777, 538);
            this.OutputCommandSplitContainer.SplitterDistance = 497;
            this.OutputCommandSplitContainer.TabIndex = 2;
            // 
            // TestingProgressBar
            // 
            this.TestingProgressBar.BackColor = System.Drawing.SystemColors.Info;
            this.TestingProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TestingProgressBar.Location = new System.Drawing.Point(0, 538);
            this.TestingProgressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestingProgressBar.Name = "TestingProgressBar";
            this.TestingProgressBar.Size = new System.Drawing.Size(777, 12);
            this.TestingProgressBar.Step = 1;
            this.TestingProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.TestingProgressBar.TabIndex = 3;
            this.TestingProgressBar.Value = 33;
            // 
            // DoReportProblemButton
            // 
            this.DoReportProblemButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.DoReportProblemButton.Location = new System.Drawing.Point(393, 0);
            this.DoReportProblemButton.Name = "DoReportProblemButton";
            this.DoReportProblemButton.Size = new System.Drawing.Size(259, 37);
            this.DoReportProblemButton.TabIndex = 3;
            this.DoReportProblemButton.Text = "Сообщить в техподдержку";
            this.DoReportProblemButton.UseVisualStyleBackColor = true;
            this.DoReportProblemButton.Visible = false;
            this.DoReportProblemButton.Click += new System.EventHandler(this.DoReportProblemButton_Click);
            // 
            // TestConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 550);
            this.Controls.Add(this.OutputCommandSplitContainer);
            this.Controls.Add(this.TestingProgressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TestConnectionForm";
            this.Text = "Проверка связи";
            this.Load += new System.EventHandler(this.TestConnectionForm_Load);
            this.Shown += new System.EventHandler(this.TestConnectionForm_Shown);
            this.HomeSplitContainer.Panel1.ResumeLayout(false);
            this.HomeSplitContainer.Panel2.ResumeLayout(false);
            this.HomeSplitContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.RequsitesSplitContainer.Panel1.ResumeLayout(false);
            this.RequsitesSplitContainer.Panel2.ResumeLayout(false);
            this.RequsitesSplitContainer.ResumeLayout(false);
            this.PingGatewayGroupBox.ResumeLayout(false);
            this.PingGatewayGroupBox.PerformLayout();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.TestHomeLogGroupBox.ResumeLayout(false);
            this.HomeNetworkSplitContainer.Panel1.ResumeLayout(false);
            this.HomeNetworkSplitContainer.Panel1.PerformLayout();
            this.HomeNetworkSplitContainer.Panel2.ResumeLayout(false);
            this.HomeNetworkSplitContainer.Panel2.PerformLayout();
            this.HomeNetworkSplitContainer.ResumeLayout(false);
            this.CityWorldSplitContainer.Panel1.ResumeLayout(false);
            this.CityWorldSplitContainer.Panel2.ResumeLayout(false);
            this.CityWorldSplitContainer.ResumeLayout(false);
            this.TestCityLogGroupBox.ResumeLayout(false);
            this.CityNetworkSplitContainer.Panel1.ResumeLayout(false);
            this.CityNetworkSplitContainer.Panel1.PerformLayout();
            this.CityNetworkSplitContainer.Panel2.ResumeLayout(false);
            this.CityNetworkSplitContainer.Panel2.PerformLayout();
            this.CityNetworkSplitContainer.ResumeLayout(false);
            this.TestWordLogGroupBox.ResumeLayout(false);
            this.WorldSplitContainer.Panel1.ResumeLayout(false);
            this.WorldSplitContainer.Panel1.PerformLayout();
            this.WorldSplitContainer.Panel2.ResumeLayout(false);
            this.WorldSplitContainer.Panel2.PerformLayout();
            this.WorldSplitContainer.ResumeLayout(false);
            this.OutputCommandSplitContainer.Panel1.ResumeLayout(false);
            this.OutputCommandSplitContainer.Panel2.ResumeLayout(false);
            this.OutputCommandSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button DoCloseFormButton;
        private Button DoCopyLog;
        private GroupBox TestWordLogGroupBox;
        private SplitContainer WorldSplitContainer;
        private TextBox PingCountryTextBox;
        private TextBox TraceCountryTextBox;
        private GroupBox TestCityLogGroupBox;
        private SplitContainer CityNetworkSplitContainer;
        private TextBox PingCityTextBox;
        private TextBox TraceCityTextBox;
        private GroupBox TestHomeLogGroupBox;
        private SplitContainer HomeNetworkSplitContainer;
        private TextBox PingHomeTextBox;
        private TextBox TraceHomeTextBox;
        private SplitContainer HomeSplitContainer;
        private SplitContainer CityWorldSplitContainer;
        private SplitContainer OutputCommandSplitContainer;
        private ImageList imageList;
        private Button DoTestConnectionButton;
        private ProgressBar TestingProgressBar;
        private SplitContainer splitContainer1;
        private SplitContainer RequsitesSplitContainer;
        private TextBox PingGatewayTextBox;
        private TextBox PingDnsTextBox;
        private GroupBox PingGatewayGroupBox;
        private GroupBox groupBox;
        private Button DoReportProblemButton;
    }
}