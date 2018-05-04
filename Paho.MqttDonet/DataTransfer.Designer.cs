namespace YsApp
{
    partial class DataTransfer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataTransfer));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboPortName = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.lab_com_send_count = new System.Windows.Forms.Label();
            this.lab_com_GetCount = new System.Windows.Forms.Label();
            this.buttonOpenClose = new System.Windows.Forms.Button();
            this.comboBaudrate = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lab_mqtt_data_recive = new System.Windows.Forms.Label();
            this.lab_mqtt_data_send = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lab_show_using_channel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.box_channel = new System.Windows.Forms.ComboBox();
            this.auto_switch_check = new System.Windows.Forms.CheckBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_Port = new System.Windows.Forms.TextBox();
            this.txb_SendIP = new System.Windows.Forms.TextBox();
            this.btn_CloseListen = new System.Windows.Forms.Button();
            this.btn_Listen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifymenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示主界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.lab_tcp_data_get = new System.Windows.Forms.Label();
            this.lab_tcp_data_send = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.notifymenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboPortName);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.lab_com_send_count);
            this.groupBox1.Controls.Add(this.lab_com_GetCount);
            this.groupBox1.Controls.Add(this.buttonOpenClose);
            this.groupBox1.Controls.Add(this.comboBaudrate);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(18, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(291, 149);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口设置";
            // 
            // comboPortName
            // 
            this.comboPortName.BackColor = System.Drawing.Color.White;
            this.comboPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPortName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboPortName.FormattingEnabled = true;
            this.comboPortName.Location = new System.Drawing.Point(98, 26);
            this.comboPortName.Margin = new System.Windows.Forms.Padding(4);
            this.comboPortName.Name = "comboPortName";
            this.comboPortName.Size = new System.Drawing.Size(160, 23);
            this.comboPortName.TabIndex = 14;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 9F);
            this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button2.Location = new System.Drawing.Point(139, 91);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 29);
            this.button2.TabIndex = 13;
            this.button2.Text = "搜索串口";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lab_com_send_count
            // 
            this.lab_com_send_count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_com_send_count.AutoSize = true;
            this.lab_com_send_count.Font = new System.Drawing.Font("宋体", 9F);
            this.lab_com_send_count.Location = new System.Drawing.Point(164, 129);
            this.lab_com_send_count.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_com_send_count.Name = "lab_com_send_count";
            this.lab_com_send_count.Size = new System.Drawing.Size(55, 15);
            this.lab_com_send_count.TabIndex = 12;
            this.lab_com_send_count.Text = "Send:0";
            // 
            // lab_com_GetCount
            // 
            this.lab_com_GetCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_com_GetCount.AutoSize = true;
            this.lab_com_GetCount.Font = new System.Drawing.Font("宋体", 9F);
            this.lab_com_GetCount.Location = new System.Drawing.Point(37, 129);
            this.lab_com_GetCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_com_GetCount.Name = "lab_com_GetCount";
            this.lab_com_GetCount.Size = new System.Drawing.Size(47, 15);
            this.lab_com_GetCount.TabIndex = 11;
            this.lab_com_GetCount.Text = "Get:0";
            // 
            // buttonOpenClose
            // 
            this.buttonOpenClose.Font = new System.Drawing.Font("宋体", 9F);
            this.buttonOpenClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonOpenClose.Location = new System.Drawing.Point(12, 91);
            this.buttonOpenClose.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOpenClose.Name = "buttonOpenClose";
            this.buttonOpenClose.Size = new System.Drawing.Size(100, 29);
            this.buttonOpenClose.TabIndex = 10;
            this.buttonOpenClose.Text = "打开";
            this.buttonOpenClose.UseVisualStyleBackColor = true;
            this.buttonOpenClose.Click += new System.EventHandler(this.buttonOpenClose_Click);
            // 
            // comboBaudrate
            // 
            this.comboBaudrate.BackColor = System.Drawing.Color.White;
            this.comboBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBaudrate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBaudrate.FormattingEnabled = true;
            this.comboBaudrate.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBaudrate.Location = new System.Drawing.Point(98, 60);
            this.comboBaudrate.Margin = new System.Windows.Forms.Padding(4);
            this.comboBaudrate.Name = "comboBaudrate";
            this.comboBaudrate.Size = new System.Drawing.Size(160, 23);
            this.comboBaudrate.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 63);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "波特率：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 29);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "串口号：";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(7, 51);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(107, 33);
            this.button5.TabIndex = 4;
            this.button5.TabStop = false;
            this.button5.Text = "连接服务器";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(140, 51);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(126, 33);
            this.button7.TabIndex = 3;
            this.button7.Text = "连接设备";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(7, 18);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4);
            this.textBox5.MaxLength = 20;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(298, 25);
            this.textBox5.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lab_mqtt_data_recive);
            this.groupBox2.Controls.Add(this.lab_mqtt_data_send);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Location = new System.Drawing.Point(329, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 151);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "4G数传：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 101);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 33);
            this.button1.TabIndex = 11;
            this.button1.Text = "统计清零";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(137, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "宇时接收数据：";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // lab_mqtt_data_recive
            // 
            this.lab_mqtt_data_recive.AutoSize = true;
            this.lab_mqtt_data_recive.Location = new System.Drawing.Point(255, 101);
            this.lab_mqtt_data_recive.Name = "lab_mqtt_data_recive";
            this.lab_mqtt_data_recive.Size = new System.Drawing.Size(15, 15);
            this.lab_mqtt_data_recive.TabIndex = 9;
            this.lab_mqtt_data_recive.Text = "0";
            // 
            // lab_mqtt_data_send
            // 
            this.lab_mqtt_data_send.AutoSize = true;
            this.lab_mqtt_data_send.Location = new System.Drawing.Point(255, 130);
            this.lab_mqtt_data_send.Name = "lab_mqtt_data_send";
            this.lab_mqtt_data_send.Size = new System.Drawing.Size(15, 15);
            this.lab_mqtt_data_send.TabIndex = 5;
            this.lab_mqtt_data_send.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(137, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "宇时发送数据：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lab_tcp_data_send);
            this.groupBox3.Controls.Add(this.lab_tcp_data_get);
            this.groupBox3.Controls.Add(this.lab_show_using_channel);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.box_channel);
            this.groupBox3.Controls.Add(this.auto_switch_check);
            this.groupBox3.Controls.Add(this.linkLabel2);
            this.groupBox3.Controls.Add(this.linkLabel1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txb_Port);
            this.groupBox3.Controls.Add(this.txb_SendIP);
            this.groupBox3.Controls.Add(this.btn_CloseListen);
            this.groupBox3.Controls.Add(this.btn_Listen);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Location = new System.Drawing.Point(18, 169);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(657, 159);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "TCP数据转发服务";
            // 
            // lab_show_using_channel
            // 
            this.lab_show_using_channel.AutoSize = true;
            this.lab_show_using_channel.Location = new System.Drawing.Point(164, 124);
            this.lab_show_using_channel.Name = "lab_show_using_channel";
            this.lab_show_using_channel.Size = new System.Drawing.Size(197, 15);
            this.lab_show_using_channel.TabIndex = 18;
            this.lab_show_using_channel.Text = "正在使用宇时4G通信通道...";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(219, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 17;
            this.label9.Text = "数据通道：";
            // 
            // box_channel
            // 
            this.box_channel.BackColor = System.Drawing.Color.White;
            this.box_channel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.box_channel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.box_channel.FormattingEnabled = true;
            this.box_channel.Items.AddRange(new object[] {
            "串口数据转发",
            "宇时数据转发"});
            this.box_channel.Location = new System.Drawing.Point(222, 42);
            this.box_channel.Margin = new System.Windows.Forms.Padding(4);
            this.box_channel.Name = "box_channel";
            this.box_channel.Size = new System.Drawing.Size(106, 23);
            this.box_channel.TabIndex = 15;
            this.box_channel.SelectedIndexChanged += new System.EventHandler(this.box_channel_SelectedIndexChanged);
            // 
            // auto_switch_check
            // 
            this.auto_switch_check.AutoSize = true;
            this.auto_switch_check.Checked = true;
            this.auto_switch_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.auto_switch_check.Location = new System.Drawing.Point(222, 80);
            this.auto_switch_check.Name = "auto_switch_check";
            this.auto_switch_check.Size = new System.Drawing.Size(119, 19);
            this.auto_switch_check.TabIndex = 16;
            this.auto_switch_check.Text = "自动选择通道";
            this.auto_switch_check.UseVisualStyleBackColor = true;
            this.auto_switch_check.CheckedChanged += new System.EventHandler(this.auto_switch_CheckedChanged);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(510, 124);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(129, 15);
            this.linkLabel2.TabIndex = 9;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "获取新版本：4erv";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(491, 89);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(178, 15);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "作者旺旺：whq461305365";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "服务端IP:";
            // 
            // txb_Port
            // 
            this.txb_Port.Location = new System.Drawing.Point(124, 40);
            this.txb_Port.MaxLength = 5;
            this.txb_Port.Name = "txb_Port";
            this.txb_Port.Size = new System.Drawing.Size(72, 25);
            this.txb_Port.TabIndex = 6;
            this.txb_Port.Text = "5760";
            // 
            // txb_SendIP
            // 
            this.txb_SendIP.Location = new System.Drawing.Point(6, 40);
            this.txb_SendIP.Name = "txb_SendIP";
            this.txb_SendIP.Size = new System.Drawing.Size(112, 25);
            this.txb_SendIP.TabIndex = 2;
            this.txb_SendIP.Text = "127.0.0.1";
            // 
            // btn_CloseListen
            // 
            this.btn_CloseListen.Location = new System.Drawing.Point(124, 71);
            this.btn_CloseListen.Name = "btn_CloseListen";
            this.btn_CloseListen.Size = new System.Drawing.Size(84, 35);
            this.btn_CloseListen.TabIndex = 1;
            this.btn_CloseListen.Text = "停止服务";
            this.btn_CloseListen.UseVisualStyleBackColor = true;
            this.btn_CloseListen.Click += new System.EventHandler(this.btn_CloseListen_Click);
            // 
            // btn_Listen
            // 
            this.btn_Listen.Location = new System.Drawing.Point(6, 67);
            this.btn_Listen.Name = "btn_Listen";
            this.btn_Listen.Size = new System.Drawing.Size(98, 38);
            this.btn_Listen.TabIndex = 1;
            this.btn_Listen.Text = "启动服务";
            this.btn_Listen.UseVisualStyleBackColor = true;
            this.btn_Listen.Click += new System.EventHandler(this.btn_Listen_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(125, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "端口:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "本机IP";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(369, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(116, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifymenu
            // 
            this.notifymenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.notifymenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示主界面ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.notifymenu.Name = "notifymenu";
            this.notifymenu.Size = new System.Drawing.Size(115, 56);
            // 
            // 显示主界面ToolStripMenuItem
            // 
            this.显示主界面ToolStripMenuItem.Name = "显示主界面ToolStripMenuItem";
            this.显示主界面ToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.显示主界面ToolStripMenuItem.Text = "显示";
            this.显示主界面ToolStripMenuItem.Click += new System.EventHandler(this.显示主界面ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "宇时4G数传";
            this.notifyIcon1.ContextMenuStrip = this.notifymenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "宇时4G数传";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 5000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // lab_tcp_data_get
            // 
            this.lab_tcp_data_get.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_tcp_data_get.AutoSize = true;
            this.lab_tcp_data_get.Font = new System.Drawing.Font("宋体", 9F);
            this.lab_tcp_data_get.Location = new System.Drawing.Point(14, 108);
            this.lab_tcp_data_get.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_tcp_data_get.Name = "lab_tcp_data_get";
            this.lab_tcp_data_get.Size = new System.Drawing.Size(47, 15);
            this.lab_tcp_data_get.TabIndex = 19;
            this.lab_tcp_data_get.Text = "Get:0";
            // 
            // lab_tcp_data_send
            // 
            this.lab_tcp_data_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_tcp_data_send.AutoSize = true;
            this.lab_tcp_data_send.Font = new System.Drawing.Font("宋体", 9F);
            this.lab_tcp_data_send.Location = new System.Drawing.Point(14, 141);
            this.lab_tcp_data_send.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_tcp_data_send.Name = "lab_tcp_data_send";
            this.lab_tcp_data_send.Size = new System.Drawing.Size(55, 15);
            this.lab_tcp_data_send.TabIndex = 15;
            this.lab_tcp_data_send.Text = "Send:0";
            // 
            // DataTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 340);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "DataTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "宇时4G数传";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataTransfer_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DataTransfer_FormClosed);
            this.Load += new System.EventHandler(this.FormCloudDllDemo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.notifymenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txb_SendIP;
        private System.Windows.Forms.Button btn_CloseListen;
        private System.Windows.Forms.Button btn_Listen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_Port;
        private System.Windows.Forms.Label lab_mqtt_data_send;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lab_mqtt_data_recive;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip notifymenu;
        private System.Windows.Forms.ToolStripMenuItem 显示主界面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lab_com_send_count;
        private System.Windows.Forms.Label lab_com_GetCount;
        private System.Windows.Forms.Button buttonOpenClose;
        private System.Windows.Forms.ComboBox comboBaudrate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboPortName;
        private System.Windows.Forms.ComboBox box_channel;
        private System.Windows.Forms.CheckBox auto_switch_check;
        private System.Windows.Forms.Label lab_show_using_channel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lab_tcp_data_send;
        private System.Windows.Forms.Label lab_tcp_data_get;
    }
}

