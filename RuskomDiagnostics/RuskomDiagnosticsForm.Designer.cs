using System.ComponentModel;
using System.Windows.Forms;

namespace RuskomDiagnostics
{
    sealed partial class RuskomDiagnosticsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuskomDiagnosticsForm));
            this.HostMenuNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.HostMenuContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.requisitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.проверитьМаршрутToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.проверитьСкоростиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.балансToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оплатитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.контактыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autorunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проверитьОбновлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DoShowCurrentSpeedButton = new System.Windows.Forms.Button();
            this.FormImageList = new System.Windows.Forms.ImageList(this.components);
            this.DoShowRequisitesButton = new System.Windows.Forms.Button();
            this.DoTestConnectionButton = new System.Windows.Forms.Button();
            this.doSendDiagnosticInfoButton = new System.Windows.Forms.Button();
            this.CommunicationTimer = new System.Windows.Forms.Timer(this.components);
            this.DoCloseApplicationButton = new System.Windows.Forms.Button();
            this.CheckUpdateButton = new System.Windows.Forms.Button();
            this.OpenTracerButton = new System.Windows.Forms.Button();
            this.DoViewBalanceButton = new System.Windows.Forms.Button();
            this.DoOpenPayWebPageButton = new System.Windows.Forms.Button();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.HostMenuContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // HostMenuNotifyIcon
            // 
            this.HostMenuNotifyIcon.BalloonTipText = global::RuskomDiagnostics.Properties.Settings.Default.BalanceString;
            this.HostMenuNotifyIcon.ContextMenuStrip = this.HostMenuContextMenuStrip;
            this.HostMenuNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("HostMenuNotifyIcon.Icon")));
            this.HostMenuNotifyIcon.Text = global::RuskomDiagnostics.Properties.Settings.Default.BalanceString;
            this.HostMenuNotifyIcon.Visible = true;
            this.HostMenuNotifyIcon.DoubleClick += new System.EventHandler(this.HostMenuNotifyIcon_DoubleClick);
            // 
            // HostMenuContextMenuStrip
            // 
            this.HostMenuContextMenuStrip.BackColor = System.Drawing.SystemColors.Menu;
            this.HostMenuContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.HostMenuContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.requisitesToolStripMenuItem,
            this.pingToolStripMenuItem1,
            this.проверитьМаршрутToolStripMenuItem1,
            this.проверитьСкоростиToolStripMenuItem,
            this.балансToolStripMenuItem,
            this.оплатитьToolStripMenuItem,
            this.контактыToolStripMenuItem,
            this.autorunToolStripMenuItem,
            this.проверитьОбновлениеToolStripMenuItem,
            this.toolStripSeparator1,
            this.выходToolStripMenuItem});
            this.HostMenuContextMenuStrip.Name = "HostMenuContextMenuStrip";
            this.HostMenuContextMenuStrip.Size = new System.Drawing.Size(197, 270);
            // 
            // requisitesToolStripMenuItem
            // 
            this.requisitesToolStripMenuItem.Image = global::RuskomDiagnostics.Properties.Resources.Properties_32x32;
            this.requisitesToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.requisitesToolStripMenuItem.Name = "requisitesToolStripMenuItem";
            this.requisitesToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.requisitesToolStripMenuItem.Text = "Сетевые реквизиты";
            this.requisitesToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.requisitesToolStripMenuItem.ToolTipText = "Показать сетевые реквизиты адаптеров";
            this.requisitesToolStripMenuItem.Click += new System.EventHandler(this.requisitesToolStripMenuItem_Click);
            // 
            // pingToolStripMenuItem1
            // 
            this.pingToolStripMenuItem1.Image = global::RuskomDiagnostics.Properties.Resources.Help_32x32;
            this.pingToolStripMenuItem1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pingToolStripMenuItem1.Name = "pingToolStripMenuItem1";
            this.pingToolStripMenuItem1.Size = new System.Drawing.Size(196, 26);
            this.pingToolStripMenuItem1.Text = "Проверить связь";
            this.pingToolStripMenuItem1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pingToolStripMenuItem1.Click += new System.EventHandler(this.pingToolStripMenuItem1_Click);
            // 
            // проверитьМаршрутToolStripMenuItem1
            // 
            this.проверитьМаршрутToolStripMenuItem1.Image = global::RuskomDiagnostics.Properties.Resources.direction_right;
            this.проверитьМаршрутToolStripMenuItem1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.проверитьМаршрутToolStripMenuItem1.Name = "проверитьМаршрутToolStripMenuItem1";
            this.проверитьМаршрутToolStripMenuItem1.Size = new System.Drawing.Size(196, 26);
            this.проверитьМаршрутToolStripMenuItem1.Text = "Проверить маршрут";
            this.проверитьМаршрутToolStripMenuItem1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.проверитьМаршрутToolStripMenuItem1.Click += new System.EventHandler(this.проверитьМаршрутToolStripMenuItem1_Click);
            // 
            // проверитьСкоростиToolStripMenuItem
            // 
            this.проверитьСкоростиToolStripMenuItem.Image = global::RuskomDiagnostics.Properties.Resources.clock;
            this.проверитьСкоростиToolStripMenuItem.Name = "проверитьСкоростиToolStripMenuItem";
            this.проверитьСкоростиToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.проверитьСкоростиToolStripMenuItem.Text = "Проверить скорости";
            this.проверитьСкоростиToolStripMenuItem.Click += new System.EventHandler(this.проверитьСкоростиToolStripMenuItem_Click);
            // 
            // балансToolStripMenuItem
            // 
            this.балансToolStripMenuItem.Image = global::RuskomDiagnostics.Properties.Resources.balance;
            this.балансToolStripMenuItem.Name = "балансToolStripMenuItem";
            this.балансToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.балансToolStripMenuItem.Text = "Баланс";
            this.балансToolStripMenuItem.Click += new System.EventHandler(this.балансToolStripMenuItem_Click);
            // 
            // оплатитьToolStripMenuItem
            // 
            this.оплатитьToolStripMenuItem.Image = global::RuskomDiagnostics.Properties.Resources.pay;
            this.оплатитьToolStripMenuItem.Name = "оплатитьToolStripMenuItem";
            this.оплатитьToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.оплатитьToolStripMenuItem.Text = "Оплатить";
            this.оплатитьToolStripMenuItem.Click += new System.EventHandler(this.оплатитьToolStripMenuItem_Click);
            // 
            // контактыToolStripMenuItem
            // 
            this.контактыToolStripMenuItem.Image = global::RuskomDiagnostics.Properties.Resources.address_book_blue_4873;
            this.контактыToolStripMenuItem.Name = "контактыToolStripMenuItem";
            this.контактыToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.контактыToolStripMenuItem.Text = "Контакты";
            this.контактыToolStripMenuItem.Click += new System.EventHandler(this.контактыToolStripMenuItem_Click);
            // 
            // autorunToolStripMenuItem
            // 
            this.autorunToolStripMenuItem.CheckOnClick = true;
            this.autorunToolStripMenuItem.Name = "autorunToolStripMenuItem";
            this.autorunToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.autorunToolStripMenuItem.Text = "Автозапуск";
            this.autorunToolStripMenuItem.Click += new System.EventHandler(this.autorunToolStripMenuItem_Click);
            // 
            // проверитьОбновлениеToolStripMenuItem
            // 
            this.проверитьОбновлениеToolStripMenuItem.Image = global::RuskomDiagnostics.Properties.Resources.Refresh_32x321;
            this.проверитьОбновлениеToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.проверитьОбновлениеToolStripMenuItem.Name = "проверитьОбновлениеToolStripMenuItem";
            this.проверитьОбновлениеToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.проверитьОбновлениеToolStripMenuItem.Text = "Проверить обновление";
            this.проверитьОбновлениеToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.проверитьОбновлениеToolStripMenuItem.Click += new System.EventHandler(this.проверитьОбновлениеToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.ВыходToolStripMenuItemClick);
            // 
            // pingToolStripMenuItem
            // 
            this.pingToolStripMenuItem.Name = "pingToolStripMenuItem";
            this.pingToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // DoShowCurrentSpeedButton
            // 
            this.DoShowCurrentSpeedButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DoShowCurrentSpeedButton.ImageIndex = 20;
            this.DoShowCurrentSpeedButton.ImageList = this.FormImageList;
            this.DoShowCurrentSpeedButton.Location = new System.Drawing.Point(0, 0);
            this.DoShowCurrentSpeedButton.Margin = new System.Windows.Forms.Padding(2);
            this.DoShowCurrentSpeedButton.Name = "DoShowCurrentSpeedButton";
            this.DoShowCurrentSpeedButton.Size = new System.Drawing.Size(632, 32);
            this.DoShowCurrentSpeedButton.TabIndex = 1;
            this.DoShowCurrentSpeedButton.Text = "Проверить скорость";
            this.DoShowCurrentSpeedButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoShowCurrentSpeedButton.UseVisualStyleBackColor = true;
            this.DoShowCurrentSpeedButton.Click += new System.EventHandler(this.DoShowCurrentSpeedButton_Click);
            // 
            // FormImageList
            // 
            this.FormImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FormImageList.ImageStream")));
            this.FormImageList.TransparentColor = System.Drawing.Color.Yellow;
            this.FormImageList.Images.SetKeyName(0, "application.png");
            this.FormImageList.Images.SetKeyName(1, "application_warning.png");
            this.FormImageList.Images.SetKeyName(2, "calculator.png");
            this.FormImageList.Images.SetKeyName(3, "calendar.png");
            this.FormImageList.Images.SetKeyName(4, "camera.png");
            this.FormImageList.Images.SetKeyName(5, "clock.png");
            this.FormImageList.Images.SetKeyName(6, "coffee.png");
            this.FormImageList.Images.SetKeyName(7, "computer.png");
            this.FormImageList.Images.SetKeyName(8, "direction_down.png");
            this.FormImageList.Images.SetKeyName(9, "direction_left.png");
            this.FormImageList.Images.SetKeyName(10, "direction_right.png");
            this.FormImageList.Images.SetKeyName(11, "direction_up.png");
            this.FormImageList.Images.SetKeyName(12, "disc.png");
            this.FormImageList.Images.SetKeyName(13, "diskette.png");
            this.FormImageList.Images.SetKeyName(14, "document.png");
            this.FormImageList.Images.SetKeyName(15, "document_add.png");
            this.FormImageList.Images.SetKeyName(16, "document_delete.png");
            this.FormImageList.Images.SetKeyName(17, "document_edit.png");
            this.FormImageList.Images.SetKeyName(18, "document_search.png");
            this.FormImageList.Images.SetKeyName(19, "document_warning.png");
            this.FormImageList.Images.SetKeyName(20, "file.png");
            this.FormImageList.Images.SetKeyName(21, "file_add.png");
            this.FormImageList.Images.SetKeyName(22, "file_delete.png");
            this.FormImageList.Images.SetKeyName(23, "file_edit.png");
            this.FormImageList.Images.SetKeyName(24, "file_search.png");
            this.FormImageList.Images.SetKeyName(25, "file_warning.png");
            this.FormImageList.Images.SetKeyName(26, "folder.png");
            this.FormImageList.Images.SetKeyName(27, "folder_add.png");
            this.FormImageList.Images.SetKeyName(28, "folder_delete.png");
            this.FormImageList.Images.SetKeyName(29, "folder_empty.png");
            this.FormImageList.Images.SetKeyName(30, "folder_search.png");
            this.FormImageList.Images.SetKeyName(31, "folder_warning.png");
            this.FormImageList.Images.SetKeyName(32, "Help_32x32.png");
            this.FormImageList.Images.SetKeyName(33, "home.png");
            this.FormImageList.Images.SetKeyName(34, "load_download.png");
            this.FormImageList.Images.SetKeyName(35, "load_upload.png");
            this.FormImageList.Images.SetKeyName(36, "mail.png");
            this.FormImageList.Images.SetKeyName(37, "mail_delete.png");
            this.FormImageList.Images.SetKeyName(38, "mail_receive.png");
            this.FormImageList.Images.SetKeyName(39, "mail_search.png");
            this.FormImageList.Images.SetKeyName(40, "mail_send.png");
            this.FormImageList.Images.SetKeyName(41, "mail_warning.png");
            this.FormImageList.Images.SetKeyName(42, "mail_write.png");
            this.FormImageList.Images.SetKeyName(43, "message.png");
            this.FormImageList.Images.SetKeyName(44, "notification_add.png");
            this.FormImageList.Images.SetKeyName(45, "notification_done.png");
            this.FormImageList.Images.SetKeyName(46, "notification_error.png");
            this.FormImageList.Images.SetKeyName(47, "notification_remove.png");
            this.FormImageList.Images.SetKeyName(48, "notification_warning.png");
            this.FormImageList.Images.SetKeyName(49, "piechart.png");
            this.FormImageList.Images.SetKeyName(50, "player_fastforward.png");
            this.FormImageList.Images.SetKeyName(51, "player_pause.png");
            this.FormImageList.Images.SetKeyName(52, "player_play.png");
            this.FormImageList.Images.SetKeyName(53, "player_record.png");
            this.FormImageList.Images.SetKeyName(54, "player_rewind.png");
            this.FormImageList.Images.SetKeyName(55, "player_stop.png");
            this.FormImageList.Images.SetKeyName(56, "Properties_32x32.png");
            this.FormImageList.Images.SetKeyName(57, "Refresh_32x32.png");
            this.FormImageList.Images.SetKeyName(58, "Refresh_32x321.png");
            this.FormImageList.Images.SetKeyName(59, "rss.png");
            this.FormImageList.Images.SetKeyName(60, "search.png");
            this.FormImageList.Images.SetKeyName(61, "security_key.png");
            this.FormImageList.Images.SetKeyName(62, "security_keyandlock.png");
            this.FormImageList.Images.SetKeyName(63, "security_lock.png");
            this.FormImageList.Images.SetKeyName(64, "security_unlock.png");
            this.FormImageList.Images.SetKeyName(65, "shoppingcart.png");
            this.FormImageList.Images.SetKeyName(66, "shoppingcart_add.png");
            this.FormImageList.Images.SetKeyName(67, "shoppingcart_checkout.png");
            this.FormImageList.Images.SetKeyName(68, "shoppingcart_remove.png");
            this.FormImageList.Images.SetKeyName(69, "shoppingcart_warning.png");
            this.FormImageList.Images.SetKeyName(70, "star_empty.png");
            this.FormImageList.Images.SetKeyName(71, "star_full.png");
            this.FormImageList.Images.SetKeyName(72, "star_half.png");
            this.FormImageList.Images.SetKeyName(73, "user.png");
            this.FormImageList.Images.SetKeyName(74, "user_add.png");
            this.FormImageList.Images.SetKeyName(75, "user_delete.png");
            this.FormImageList.Images.SetKeyName(76, "user_manage.png");
            this.FormImageList.Images.SetKeyName(77, "user_warning.png");
            this.FormImageList.Images.SetKeyName(78, "volume.png");
            this.FormImageList.Images.SetKeyName(79, "volume_down.png");
            this.FormImageList.Images.SetKeyName(80, "volume_mute.png");
            this.FormImageList.Images.SetKeyName(81, "volume_up.png");
            this.FormImageList.Images.SetKeyName(82, "balance.png");
            this.FormImageList.Images.SetKeyName(83, "pay.png");
            this.FormImageList.Images.SetKeyName(84, "address_book_blue_4873.png");
            // 
            // DoShowRequisitesButton
            // 
            this.DoShowRequisitesButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DoShowRequisitesButton.ImageIndex = 56;
            this.DoShowRequisitesButton.ImageList = this.FormImageList;
            this.DoShowRequisitesButton.Location = new System.Drawing.Point(0, 32);
            this.DoShowRequisitesButton.Margin = new System.Windows.Forms.Padding(2);
            this.DoShowRequisitesButton.Name = "DoShowRequisitesButton";
            this.DoShowRequisitesButton.Size = new System.Drawing.Size(632, 32);
            this.DoShowRequisitesButton.TabIndex = 2;
            this.DoShowRequisitesButton.Text = "Реквизиты сетевых карт";
            this.DoShowRequisitesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoShowRequisitesButton.UseVisualStyleBackColor = true;
            this.DoShowRequisitesButton.Click += new System.EventHandler(this.DoShowRequisitesButton_Click);
            // 
            // DoTestConnectionButton
            // 
            this.DoTestConnectionButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DoTestConnectionButton.ImageIndex = 32;
            this.DoTestConnectionButton.ImageList = this.FormImageList;
            this.DoTestConnectionButton.Location = new System.Drawing.Point(0, 64);
            this.DoTestConnectionButton.Margin = new System.Windows.Forms.Padding(2);
            this.DoTestConnectionButton.Name = "DoTestConnectionButton";
            this.DoTestConnectionButton.Size = new System.Drawing.Size(632, 32);
            this.DoTestConnectionButton.TabIndex = 3;
            this.DoTestConnectionButton.Text = "Проверить связь";
            this.DoTestConnectionButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoTestConnectionButton.UseVisualStyleBackColor = true;
            this.DoTestConnectionButton.Click += new System.EventHandler(this.DoTestConnectionButton_Click);
            // 
            // doSendDiagnosticInfoButton
            // 
            this.doSendDiagnosticInfoButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.doSendDiagnosticInfoButton.ImageIndex = 40;
            this.doSendDiagnosticInfoButton.ImageList = this.FormImageList;
            this.doSendDiagnosticInfoButton.Location = new System.Drawing.Point(0, 96);
            this.doSendDiagnosticInfoButton.Margin = new System.Windows.Forms.Padding(2);
            this.doSendDiagnosticInfoButton.Name = "doSendDiagnosticInfoButton";
            this.doSendDiagnosticInfoButton.Size = new System.Drawing.Size(632, 32);
            this.doSendDiagnosticInfoButton.TabIndex = 4;
            this.doSendDiagnosticInfoButton.Text = "Провести диагностику и отправить в техподдержку";
            this.doSendDiagnosticInfoButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.doSendDiagnosticInfoButton.UseVisualStyleBackColor = true;
            this.doSendDiagnosticInfoButton.Click += new System.EventHandler(this.doSendDiagnosticInfoButton_Click);
            // 
            // CommunicationTimer
            // 
            this.CommunicationTimer.Interval = 999999;
            this.CommunicationTimer.Tick += new System.EventHandler(this.CommunicationTimer_Tick);
            // 
            // DoCloseApplicationButton
            // 
            this.DoCloseApplicationButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DoCloseApplicationButton.ImageIndex = 46;
            this.DoCloseApplicationButton.ImageList = this.FormImageList;
            this.DoCloseApplicationButton.Location = new System.Drawing.Point(0, 128);
            this.DoCloseApplicationButton.Name = "DoCloseApplicationButton";
            this.DoCloseApplicationButton.Size = new System.Drawing.Size(632, 32);
            this.DoCloseApplicationButton.TabIndex = 5;
            this.DoCloseApplicationButton.Text = "Закрыть приложение";
            this.DoCloseApplicationButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoCloseApplicationButton.UseVisualStyleBackColor = true;
            this.DoCloseApplicationButton.Click += new System.EventHandler(this.DoCloseApplicationButton_Click);
            // 
            // CheckUpdateButton
            // 
            this.CheckUpdateButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.CheckUpdateButton.ImageIndex = 57;
            this.CheckUpdateButton.ImageList = this.FormImageList;
            this.CheckUpdateButton.Location = new System.Drawing.Point(0, 160);
            this.CheckUpdateButton.Name = "CheckUpdateButton";
            this.CheckUpdateButton.Size = new System.Drawing.Size(632, 32);
            this.CheckUpdateButton.TabIndex = 6;
            this.CheckUpdateButton.Text = "Проверить обновление";
            this.CheckUpdateButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CheckUpdateButton.UseVisualStyleBackColor = true;
            this.CheckUpdateButton.Click += new System.EventHandler(this.CheckUpdateButton_Click);
            // 
            // OpenTracerButton
            // 
            this.OpenTracerButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.OpenTracerButton.ImageIndex = 10;
            this.OpenTracerButton.ImageList = this.FormImageList;
            this.OpenTracerButton.Location = new System.Drawing.Point(0, 192);
            this.OpenTracerButton.Margin = new System.Windows.Forms.Padding(2);
            this.OpenTracerButton.Name = "OpenTracerButton";
            this.OpenTracerButton.Size = new System.Drawing.Size(632, 32);
            this.OpenTracerButton.TabIndex = 7;
            this.OpenTracerButton.Text = "Проверить маршрут";
            this.OpenTracerButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.OpenTracerButton.UseVisualStyleBackColor = true;
            this.OpenTracerButton.Click += new System.EventHandler(this.OpenTracerButton_Click);
            // 
            // DoViewBalanceButton
            // 
            this.DoViewBalanceButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DoViewBalanceButton.ImageIndex = 82;
            this.DoViewBalanceButton.ImageList = this.FormImageList;
            this.DoViewBalanceButton.Location = new System.Drawing.Point(0, 224);
            this.DoViewBalanceButton.Margin = new System.Windows.Forms.Padding(2);
            this.DoViewBalanceButton.Name = "DoViewBalanceButton";
            this.DoViewBalanceButton.Size = new System.Drawing.Size(632, 32);
            this.DoViewBalanceButton.TabIndex = 8;
            this.DoViewBalanceButton.Text = "Баланс";
            this.DoViewBalanceButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoViewBalanceButton.UseVisualStyleBackColor = true;
            this.DoViewBalanceButton.Click += new System.EventHandler(this.DoViewBalanceButton_Click);
            // 
            // DoOpenPayWebPageButton
            // 
            this.DoOpenPayWebPageButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.DoOpenPayWebPageButton.ImageIndex = 83;
            this.DoOpenPayWebPageButton.ImageList = this.FormImageList;
            this.DoOpenPayWebPageButton.Location = new System.Drawing.Point(0, 256);
            this.DoOpenPayWebPageButton.Margin = new System.Windows.Forms.Padding(2);
            this.DoOpenPayWebPageButton.Name = "DoOpenPayWebPageButton";
            this.DoOpenPayWebPageButton.Size = new System.Drawing.Size(632, 32);
            this.DoOpenPayWebPageButton.TabIndex = 9;
            this.DoOpenPayWebPageButton.Text = "Оплатить";
            this.DoOpenPayWebPageButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DoOpenPayWebPageButton.UseVisualStyleBackColor = true;
            this.DoOpenPayWebPageButton.Click += new System.EventHandler(this.DoOpenPayWebPageButton_Click);
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Interval = 999999;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // RuskomDiagnosticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.DoOpenPayWebPageButton);
            this.Controls.Add(this.DoViewBalanceButton);
            this.Controls.Add(this.OpenTracerButton);
            this.Controls.Add(this.CheckUpdateButton);
            this.Controls.Add(this.DoCloseApplicationButton);
            this.Controls.Add(this.doSendDiagnosticInfoButton);
            this.Controls.Add(this.DoTestConnectionButton);
            this.Controls.Add(this.DoShowRequisitesButton);
            this.Controls.Add(this.DoShowCurrentSpeedButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RuskomDiagnosticsForm";
            this.Text = "Диагностика интернет соединения ( Руском )";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RuskomDiagnosticsForm_FormClosing);
            this.Load += new System.EventHandler(this.RuskomDiagnosticsForm_Load);
            this.Resize += new System.EventHandler(this.RuskomDiagnosticsForm_Resize);
            this.HostMenuContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private NotifyIcon HostMenuNotifyIcon;
        private ContextMenuStrip HostMenuContextMenuStrip;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem requisitesToolStripMenuItem;
        private ToolStripMenuItem pingToolStripMenuItem;
        private Button DoShowCurrentSpeedButton;
        private Button DoShowRequisitesButton;
        private Button DoTestConnectionButton;
        private ToolStripMenuItem pingToolStripMenuItem1;
        private Button doSendDiagnosticInfoButton;
        private Timer CommunicationTimer;
        private Button DoCloseApplicationButton;
        private ToolStripMenuItem autorunToolStripMenuItem;
        private Button CheckUpdateButton;
        private ToolStripMenuItem проверитьОбновлениеToolStripMenuItem;
        private Button OpenTracerButton;
        
        private ToolStripMenuItem проверитьМаршрутToolStripMenuItem1;
        private ImageList FormImageList;
        private ToolStripMenuItem балансToolStripMenuItem;
        private ToolStripMenuItem оплатитьToolStripMenuItem;
        private Button DoViewBalanceButton;
        private Button DoOpenPayWebPageButton;
        private ToolStripMenuItem проверитьСкоростиToolStripMenuItem;
        private Timer UpdateTimer;
        private ToolStripMenuItem контактыToolStripMenuItem;
    }
}

