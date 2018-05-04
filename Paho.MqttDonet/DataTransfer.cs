using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace YsApp
{
    using System.Diagnostics;
    using System.IO;
    using System.IO.Ports;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;

    public partial class DataTransfer : Form
    {
        TUSR_ConnAckEvent FConnAck_CBF;
        TUSR_SubscribeAckEvent FSubscribeAck_CBF;
        TUSR_UnSubscribeAckEvent FUnSubscribeAck_CBF;
        TUSR_PubAckEvent FPubAck_CBF;
        TUSR_RcvParsedEvent FRcvParsedDataPointPush_CBF;
        TUSR_RcvParsedEvent FRcvParsedDevStatusPush_CBF;
        TUSR_RcvParsedEvent FRcvParsedDevAlarmPush_CBF;
        TUSR_RcvParsedEvent FRcvParsedOptionResponseReturn_CBF;
        TUSR_RcvRawFromDevEvent FRcvRawFromDev_CBF;


        /********
         * 
         * 
         * 
         * 串口部分 
         * 
         * 采用复合型的数据转发方式，算法，如果有手动选择，则采用手动选择的端口数据，如果没有手动选择数据
         * 则根据每5秒数据收发的数量自动切换通道 默认选择使用串口的数据
         *  
         * 
         * *********/
        private SerialPort comm = new SerialPort();
        private Int64 com_received_count = 0;//接收计数
        private Int64 com_send_count = 0;//发送计数



        private static Boolean is_auto_channel_swtich = true;
        private static Boolean is_use_comm_data = true; //判断当前是否使用串口数据 
        
        //串口接收到的数据数量 5S前
        private static Int64 data_last_recive_com=0;
        //MQTT接收到的数据数量 5S前
        private static Int64 data_last_revice_mqtt = 0;

        //UDP字段
        private static int RemotePort = 14550;
        private string rsa_private_key = @"<RSAKeyValue><Modulus>uOqxIXLFeDL9n1u+yo9LNzRxoV6U04YYenAXIbJ/CAO6wbjBaf9m6Sw9mJXoJr3IA9w6Plw1wgLrBLP0ngW7cFSQVbT/p2zx5NhDLTZ6NeHV+lZNLSFL6niUvDCBIyrims8VG7+FLXqrjBa0YOny5MhCdP2K67lNCBUwlDr+nLs=</Modulus><Exponent>AQAB</Exponent><P>xyscDUZcb7beXz60mUSMtTOuzxSCmqqXlgP1bhGr5RCBSADIeZoy8+k2ILfzTDorlwd9pqSbFBf/W4rVGp6d4w==</P><Q>7a6Gx+flU3M2YJFQ+8qVD08j8vjcoSG3JaSfUB5Z3WE1ZPug36LSjIgZ4EsZQ9mjreVgYkxylkhAwiDfSHG9SQ==</Q><DP>BoM7XJfDaAfDx7uGLkjWjQpOmgjiqGoRoN8qRFohk9DxWUhlRcysA9vJYFKDiyePy1V8X1mclJCgUf79Luym3w==</DP><DQ>vEaFyZDuXd5j8rbp2aqtzQS5y1xLGPCmLZFsCYEhWnYIX8fbtYs7Ecs2BDA5AUBDohqS8Qrxsg3mDmEPvkkq0Q==</DQ><InverseQ>stTU8PLYXfq391Er67p5yfYdsEdnXQBaQESIWAnxUE2QmqRZ00rX3MnbhkvcMlFxfoOrfOWJWTEQQKiTtHQs5g==</InverseQ><D>AZpZP7VPjaU+VU3VaJjD3KN8+gGJFCQ+CtLC+pwYBskwtMHu5/vxtVmnlx0TgqrGhac4iBGUXIox+ZO8b1FgB63o+1ihdAefG7siXpsL5adO85hjuQs1XIpC63Fm2WMi8yYNnS8QB0v7G+CKdTtWDEkfpyUBB74Kb9bihEZ1AHE=</D></RSAKeyValue>";

        private static Int64 tcp_data_recive=0;
        private static Int64 tcp_data_send = 0;
        private static Int64 mqtt_data_recive = 0;
        private static Int64 mqtt_data_send = 0;
        public DataTransfer()
        {
            InitializeComponent();
            //初始化下拉串口名称列表框
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            comboPortName.Items.AddRange(ports);
            comboPortName.SelectedIndex = comboPortName.Items.Count > 0 ? 0 : -1;
            comboBaudrate.SelectedIndex = comboBaudrate.Items.IndexOf("57600");
            //初始化SerialPort对象
            comm.NewLine = "\r\n";
            comm.RtsEnable = false;//根据实际情况吧。

            //添加事件注册
            comm.DataReceived += comm_DataReceived;

            btn_CloseListen.Enabled = false;

            FConnAck_CBF = new TUSR_ConnAckEvent(ConnAck_CBF);
            FSubscribeAck_CBF = new TUSR_SubscribeAckEvent(SubscribeAck_CBF);
            FUnSubscribeAck_CBF = new TUSR_UnSubscribeAckEvent(UnSubscribeAck_CBF);
            FPubAck_CBF = new TUSR_PubAckEvent(PubAck_CBF);
            FRcvParsedDataPointPush_CBF = new TUSR_RcvParsedEvent(RcvParsedDataPointPush_CBF);
            FRcvParsedDevStatusPush_CBF = new TUSR_RcvParsedEvent(RcvParsedDevStatusPush_CBF);
            FRcvParsedDevAlarmPush_CBF = new TUSR_RcvParsedEvent(RcvParsedDevAlarmPush_CBF);
            FRcvParsedOptionResponseReturn_CBF = new TUSR_RcvParsedEvent(RcvParsedOptionResponseReturn_CBF);
            FRcvRawFromDev_CBF = new TUSR_RcvRawFromDevEvent(RcvRawFromDev_CBF);
            this.Resize += Form1_Resize;
        }
        void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                this.notifyIcon1.Visible = true;
            }
        }

        void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
            com_received_count += n;//增加接收计数
            comm.Read(buf, 0, n);//读取缓冲数据
            //如果使用串口的数据，TCP转发
            if (is_use_comm_data)
                tcp_send(buf);
            
        }
        void com_send_data(byte[] data,int length)
        {
            if (comm.IsOpen)
            {
                try
                {
                    comm.Write(data, 0, length);
                    com_send_count += length;
                }
                catch
                {
                }
            }
        }
        private void buttonOpenClose_Click(object sender, EventArgs e)
        {
            //根据当前串口对象，来判断操作
            if (comm.IsOpen)
            {
                //打开时点击，则关闭串口
                comm.Close();
            }
            else
            {
                //关闭时点击，则设置好端口，波特率后打开
                comm.PortName = comboPortName.Text;
                comm.BaudRate = int.Parse(comboBaudrate.Text);
                try
                {
                    comm.Open();
                  
                }
                catch (Exception ex)
                {
                    //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                    comm = new SerialPort();
                    //现实异常信息给客户。
                    MessageBox.Show(ex.Message);
                }
            }
            set_comm_state();
        }

        private void set_comm_state()
        {
            //设置按钮的状态
            if (comm.IsOpen)
            {
                buttonOpenClose.Text = "关闭";
                comboPortName.Enabled = false;
                comboBaudrate.Enabled = false;
                buttonOpenClose.BackColor = Color.Green;
                button2.Enabled = false;
            }
            else
            {
                is_use_comm_data = false;
                buttonOpenClose.Text = "打开";
                comboPortName.Enabled = true;
                comboBaudrate.Enabled = true;
                buttonOpenClose.BackColor = Color.Red;
                button2.Enabled = true;
                is_use_comm_data = false;
            }
            if (is_use_comm_data)
                lab_show_using_channel.Text = "正在使用串口通信信道...";
            else
                lab_show_using_channel.Text = "正在使用宇时4G通信通道...";
        }
        #region
        //初始化   
        private void buttonInit_Click()
        {
            //  string ip = "clouddata.usr.cn";//透传云服务器地址, 打死都不改
            string ip = "clouddata.usr.cn";
            ushort port = 1883;//透传云服务器端口, 打死都不改
            int vertion = 1;
            if (USR_Init(ip, port, vertion))
            {
                Log("初始化成功", true);
                USR_OnConnAck(FConnAck_CBF);
                USR_OnSubscribeAck(FSubscribeAck_CBF);
                USR_OnUnSubscribeAck(FUnSubscribeAck_CBF);
                USR_OnPubAck(FPubAck_CBF);
                USR_OnRcvParsedDataPointPush(FRcvParsedDataPointPush_CBF);
                USR_OnRcvParsedDevStatusPush(FRcvParsedDevStatusPush_CBF);
                USR_OnRcvParsedDevAlarmPush(FRcvParsedDevAlarmPush_CBF);
                USR_OnRcvParsedOptionResponseReturn(FRcvParsedOptionResponseReturn_CBF);
                USR_OnRcvRawFromDev(FRcvRawFromDev_CBF);
            }
            else
            {
                Log("初始化失败", true);
            }
        }
        #endregion
        public string Read_device_id()
        {
            string return_data = null; ;
                try
                {
                // 创建一个 StreamReader 的实例来读取文件 
                // using 语句也能关闭 StreamReader
                string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                string file_path = str + "keyconfig";
                using (StreamReader sr = new StreamReader(file_path))
                    {
                        string line;

                        // 从文件读取并显示行，直到文件的末尾 
                        while ((line = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        return_data += line;
                        }
                    }
                return return_data;
                }
                catch 
                {
                return null;
                }
            }

        //解密
        private string Decrypt(string ciphertext)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(rsa_private_key);
                byte[] encryptdata = Convert.FromBase64String(ciphertext);
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                return Encoding.Default.GetString(decryptdata);
            }
        }
        private void generate_priavet_key()
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = "add by wugege";
         //   param.Flags = CspProviderFlags.UseDefaultKeyContainer;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param);
            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string file_path1 = str + "PublicKey.xml";
            string file_path2 = str + "PrivateKey.xml";
            using (StreamWriter sw = new StreamWriter(file_path1))//产生公匙
            {
                sw.WriteLine(rsa.ToXmlString(false));
            }
            using (StreamWriter sw = new StreamWriter(file_path2))//产生私匙（也包含私匙）
            {
                sw.WriteLine(rsa.ToXmlString(true));
            }
        }


        //连接
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                buttonInit_Click();
                string userName = "18780126369";
                string passWord = "5234970";
                if (USR_Connect(userName, passWord))
                {
                    Log("连接已发起", true);
                    button5.BackColor = Color.Green;
                }
                else
                {
                    button5.BackColor = Color.Red;
                }
            }
            catch
            {
                MessageBox.Show("连接到服务器失败，请检查网络是否畅通！");
                button5.BackColor = Color.Red;
            }
        }


        //订阅设备原始数据流
        private void button7_Click(object sender, EventArgs e)
        {
            //  generate_priavet_key();
            string devId = Read_device_id();
            try
            {
                 devId = Decrypt(devId);
            }
            catch
            {
                MessageBox.Show("加载设备文件失败，请联系供淘宝商家:whq461305365");
                //this.Close();
                return;
            }
            textBox5.Text= devId;
            int messageId = USR_SubscribeDevRaw(devId);
            if (messageId > -1)
            {
                button7.BackColor = Color.Green;
                Log("订阅已发起  MsgId:" + messageId.ToString(), true);
            }
            else
            {
                button7.BackColor = Color.Red;
            }

        }

        //取消订阅
        private void button8_Click(object sender, EventArgs e)
        {
            string devId = textBox5.Text;
            int messageId = USR_UnSubscribeDevRaw(devId);
            if (messageId > -1)
            {
                Log("取消订阅已发起  MsgId:" + messageId.ToString(), true);
            }
        }

        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        private void mqtt_send(byte[] buff,int len)
        {
            string devId = textBox5.Text;
            if (devId == null)
                return;
            int messageId = USR_PublishRawToDev(devId, buff, len);
            if (messageId > -1)
            {

                mqtt_data_send += len;
            }
        }

        private void Log(string str, Boolean bInsTime = false)
        {
        }

        private void ConnAck_CBF(int returnCode, IntPtr description)
        {
            Log("【连接事件】", true);
            Log("returnCode: " + returnCode.ToString() + "  " + Marshal.PtrToStringAuto(description));
            if (returnCode == 0)
            {
                Log("连接成功");
            }
            else
            {
                Log("连接失败");
            }

        }


        /* 自定义回调函数,用于判断订阅结果 */
        private void SubscribeAck_CBF(int messageID, IntPtr SubFunName, IntPtr SubParam, IntPtr returnCode)
        {
            string sSubFunName = Marshal.PtrToStringAuto(SubFunName);
            string sSubParam = Marshal.PtrToStringAuto(SubParam);
            string sReturnCode = Marshal.PtrToStringAuto(returnCode);
            string[] SubParamArray = sSubParam.Split(',');
            string[] retCodeArray = sReturnCode.Split(',');
            int len = SubParamArray.Length;
            Log("【订阅事件】", true);
            Log("MsgId:" + messageID.ToString());
            // Log("函数名称:" + sSubFunName);
            for (int i = 0; i < len; ++i)
            {
                Log("设备ID(或用户名)：" + SubParamArray[i] +
                 "  订阅结果：" + retCodeArray[i]);
            }
        }

        /* 自定义回调函数,用于判断取消订阅结果 */
        private void UnSubscribeAck_CBF(int messageID, IntPtr UnSubFunName, IntPtr UnSubParam)
        {
            string sUnSubFunName = Marshal.PtrToStringAuto(UnSubFunName);
            string sUnSubParam = Marshal.PtrToStringAuto(UnSubParam);
            Log("【取消订阅事件】", true);
            Log("MsgId:" + messageID.ToString());
            Log("函数名称：" + sUnSubFunName);
            Log("设备ID或用户名：" + sUnSubParam);
        }


        protected void PubAck_CBF(int messageID)
        {
            Log("【推送回调】", true);
            Log("MsgId:" + messageID.ToString());
        }

        /* 自定义回调函数,用于接收数据点值推送 */
        private void RcvParsedDataPointPush_CBF(
            int messageID, IntPtr DevId, IntPtr JsonStr)
        {
            string sDevId = Marshal.PtrToStringAuto(DevId);
            string sJsonStr = Marshal.PtrToStringAuto(JsonStr);
            Log("【数据点值推送事件】", true);
            Log("设备ID   : " + sDevId);
            Log("MsgId    : " + messageID.ToString());
            Log("JSON数据: " + sJsonStr);

            //todo json解析
        }

        /* 自定义回调函数,用于接收上下线推送 */
        private void RcvParsedDevStatusPush_CBF(
            int messageID, IntPtr DevId, IntPtr JsonStr)
        {
            string sDevId = Marshal.PtrToStringAuto(DevId);
            string sJsonStr = Marshal.PtrToStringAuto(JsonStr);
            Log("【设备上下线推送事件】", true);
            Log("设备ID   : " + sDevId);
            Log("MsgId    : " + messageID.ToString());
            Log("JSON数据: " + sJsonStr);
        }

        /* 自定义回调函数,用于接收报警推送 */
        private void RcvParsedDevAlarmPush_CBF(
            int messageID, IntPtr DevId, IntPtr JsonStr)
        {
            string sDevId = Marshal.PtrToStringAuto(DevId);
            string sJsonStr = Marshal.PtrToStringAuto(JsonStr);
            Log("【设备报警推送事件】", true);
            Log("设备ID   : " + sDevId);
            Log("MsgId    : " + messageID.ToString());
            Log("JSON数据: " + sJsonStr);
        }

        /* 自定义回调函数,用于接收数据点操作应答 */
        private void RcvParsedOptionResponseReturn_CBF(
            int messageID, IntPtr DevId, IntPtr JsonStr)
        {
            string sDevId = Marshal.PtrToStringAuto(DevId);
            string sJsonStr = Marshal.PtrToStringAuto(JsonStr);
            Log("【数据点操作应答事件】", true);
            Log("设备ID   : " + sDevId);
            Log("MsgId    : " + messageID.ToString());
            Log("JSON数据: " + sJsonStr);
        }

        /* 自定义回调函数,用于接收设备原始数据流 */
        private void RcvRawFromDev_CBF(
            int messageID, IntPtr devId, IntPtr pData, int DataLen)
        {
            string sDevId = Marshal.PtrToStringAuto(devId);
            byte[] byteArr = new byte[DataLen];
            Marshal.Copy(pData, byteArr, 0, DataLen);
                //如果使用MQTT数据链路
            if(!is_use_comm_data)
            tcp_send(byteArr);
            mqtt_data_recive += byteArr.Length;
        }


        ///////////////////////////////
        ///////  初始化和释放  ////////
        ///////////////////////////////

        /// <summary>
        /// 获取版本
        /// </summary>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_GetVer", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_GetVer();
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="vertion"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_Init", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_Init(string host, ushort port, int vertion);
        /// <summary>
        /// 释放
        /// </summary>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_Release", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_Release();

        ///////////////////////////////
        ////////  连接和断开  /////////
        ///////////////////////////////

        /// <summary>
        /// 连接回调
        /// </summary>
        /// <param name="returnCode"></param>
        /// <param name="description"></param>
        public delegate void TUSR_ConnAckEvent(int returnCode, IntPtr description);
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_OnConnAck", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_OnConnAck(TUSR_ConnAckEvent OnConnAck);
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_Connect", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_Connect(string username, string password);
        /// <summary>
        /// 断开
        /// </summary>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_DisConnect", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_DisConnect();

        ///////////////////////////////
        //////  订阅和取消订阅  ///////
        ///////////////////////////////

        /// <summary>
        /// 订阅回调
        /// </summary>
        /// <param name="messageID">消息ID</param>
        /// <param name="SubFunName">函数名称,用于判断用户执行的哪个订阅函数, 得到了服务器的回应。可能的取值有:SubscribeDevParsed,SubscribeUserParsed,SubscribeDevRaw,SubscribeUserRaw</param>
        /// <param name="SubParam">SubParam值跟执行的订阅函数有关:如果订阅的是”单个设备的消息”, 则SubParam为设备ID;如果订阅的是”用户所有设备的消息”, 则SubParam为用户名</param>
        /// <param name="returnCode">0、1、2:订阅成功;  128:订阅失败</param>
        public delegate void TUSR_SubscribeAckEvent(int messageID, IntPtr SubFunName, IntPtr SubParam, IntPtr returnCode);
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_OnSubscribeAck", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_OnSubscribeAck(TUSR_SubscribeAckEvent OnSubscribeAck);
        /// <summary>
        /// 取消订阅回调
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="UnSubFunName"></param>
        /// <param name="UnSubParam"></param>
        public delegate void TUSR_UnSubscribeAckEvent(int messageID, IntPtr UnSubFunName, IntPtr UnSubParam);
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_OnUnSubscribeAck", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_OnUnSubscribeAck(TUSR_UnSubscribeAckEvent OnUnSubscribeAck);

        /// <summary>
        /// 订阅单个设备解析后的数据  【云组态】
        /// </summary>
        /// <param name="devId"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_SubscribeDevParsed", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_SubscribeDevParsed(string devId);
        /// <summary>
        /// 订阅账户下所有设备解析后的数据  【云组态】
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_SubscribeUserParsed", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_SubscribeUserParsed(string Username);
        /// <summary>
        /// 取消订阅单个设备解析后的数据  【云组态】
        /// </summary>
        /// <param name="DevId"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_UnSubscribeDevParsed", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_UnSubscribeDevParsed(string DevId);
        /// <summary>
        /// 取消订阅账户下所有设备解析后的数据  【云组态】
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_UnSubscribeUserParsed", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_UnSubscribeUserParsed(string Username);

        /// <summary>
        /// 订阅单个设备原始数据流 【云交换机】
        /// </summary>
        /// <param name="devId"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_SubscribeDevRaw", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_SubscribeDevRaw(string devId);
        /// <summary>
        /// 订阅账户下所有设备原始数据流 【云交换机】
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_SubscribeUserRaw", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_SubscribeUserRaw(string Username);
        /// <summary>
        /// 取消订阅单个设备原始数据流 【云交换机】
        /// </summary>
        /// <param name="DevId"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_UnSubscribeDevRaw", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_UnSubscribeDevRaw(string DevId);
        /// <summary>
        /// 取消订阅账户下所有设备原始数据流 【云交换机】
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_UnSubscribeUserRaw", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_UnSubscribeUserRaw(string Username);

        //订阅回调 【不再推荐使用】
        public delegate void TUSR_SubAckEvent(int messageID, IntPtr devId, IntPtr returnCode);
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_OnSubAck", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_OnSubAck(TUSR_SubAckEvent OnSubAck);
        //订阅 【不再推荐使用】
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_Subscribe", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_Subscribe(string devId);
        //取消订阅回调 【不再推荐使用】
        public delegate void TUSR_UnSubAckEvent(int messageID, IntPtr devId);
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_OnUnSubAck", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_OnUnSubAck(TUSR_UnSubAckEvent OnUnSubAck);
        //取消订阅 【不再推荐使用】
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_UnSubscribe", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_UnSubscribe(string devId);

        ///////////////////////////////
        /////////  推送消息 ///////////
        ///////////////////////////////
        /// <summary>
        /// 推送回调
        /// </summary>
        /// <param name="MessageID"></param>
        public delegate void TUSR_PubAckEvent(int MessageID);
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_OnPubAck", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_OnPubAck(TUSR_PubAckEvent OnPubAck);

        /// <summary>
        /// 设置数据点值【云组态】
        /// </summary>
        /// <param name="DevId"></param>
        /// <param name="SlaveIndex"></param>
        /// <param name="PointId"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_PublishParsedSetSlaveDataPoint", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_PublishParsedSetSlaveDataPoint(string DevId, string SlaveIndex, string PointId, string Value);
        /// <summary>
        /// 查询数据点值【云组态】
        /// </summary>
        /// <param name="DevId"></param>
        /// <param name="SlaveIndex"></param>
        /// <param name="PointId"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_PublishParsedQuerySlaveDataPoint", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_PublishParsedQuerySlaveDataPoint(string DevId, string SlaveIndex, string PointId);

        /// <summary>
        /// 设置单台设备数据点值【云组态】 ---- 已弃, 用 USR_PublishParsedQuerySlaveDataPoint 代替
        /// </summary>
        /// <param name="DevId"></param>
        /// <param name="PointId"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_PublishParsedSetDataPoint", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_PublishParsedSetDataPoint(string DevId, string PointId, string Value);
        /// <summary>
        /// 查询单台设备数据点值【云组态】 ---- 已弃, 用 USR_PublishParsedQuerySlaveDataPoint 代替
        /// </summary>
        /// <param name="DevId"></param>
        /// <param name="PointId"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_PublishParsedQueryDataPoint", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_PublishParsedQueryDataPoint(string DevId, string PointId);

        /// <summary>
        /// 向单台设备推送原始数据流 【云交换机】
        /// </summary>
        /// <param name="DevId"></param>
        /// <param name="pData"></param>
        /// <param name="DataLen"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_PublishRawToDev", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_PublishRawToDev(string DevId, byte[] pData, int DataLen);

        /// <summary>
        /// 向账户下所有设备推送原始数据流 【云交换机】
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="pData"></param>
        /// <param name="DataLen"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_PublishRawToUser", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_PublishRawToUser(string Username, byte[] pData, int DataLen);

        //推送【不再推荐使用】
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_Publish", CallingConvention = CallingConvention.StdCall)]
        public static extern int USR_Publish(string DevId, byte[] pData, int DataLen);

        ///////////////////////////////
        /////////  接收消息 ///////////
        ///////////////////////////////
        /// <summary>
        /// 接收设备解析后的数据 事件定义 【云组态】
        /// </summary>
        /// <param name="MessageID"></param>
        /// <param name="DevId"></param>
        /// <param name="JsonStr"></param>
        public delegate void TUSR_RcvParsedEvent(int MessageID, IntPtr DevId, IntPtr JsonStr);
        /// <summary>
        /// 设置 接收数据点推送 回调函数 【云组态】
        /// </summary>
        /// <param name="OnRcvParsed"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_OnRcvParsedDataPointPush", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_OnRcvParsedDataPointPush(TUSR_RcvParsedEvent OnRcvParsed);
        /// <summary>
        /// 设置 接收设备上下线推送 回调函数 【云组态】
        /// </summary>
        /// <param name="OnRcvParsed"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_OnRcvParsedDevStatusPush", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_OnRcvParsedDevStatusPush(TUSR_RcvParsedEvent OnRcvParsed);
        /// <summary>
        /// 设置 接收设备报警推送 回调函数 【云组态】
        /// </summary>
        /// <param name="OnRcvParsed"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_OnRcvParsedDevAlarmPush", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_OnRcvParsedDevAlarmPush(TUSR_RcvParsedEvent OnRcvParsed);
        /// <summary>
        /// 设置 接收数据点操作应答 【云组态】
        /// </summary>
        /// <param name="OnRcvParsed"></param>
        /// <returns></returns>
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_OnRcvParsedOptionResponseReturn", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_OnRcvParsedOptionResponseReturn(TUSR_RcvParsedEvent OnRcvParsed);

        /// <summary>
        /// 接收设备原始数据流 事件定义 【云交换机】 
        /// </summary>
        /// <param name="MessageID"></param>
        /// <param name="DevId"></param>
        /// <param name="pData"></param>
        /// <param name="DataLen"></param>
        public delegate void TUSR_RcvRawFromDevEvent(int MessageID, IntPtr DevId, IntPtr pData, int DataLen);
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_OnRcvRawFromDev", CallingConvention = CallingConvention.StdCall)]
        /// <summary>
        /// 设置 接收设备原始数据流 回调函数 【云交换机】
        /// </summary>
        /// <param name="OnRcvRawFromDev"></param>
        /// <returns></returns>
        public static extern bool USR_OnRcvRawFromDev(TUSR_RcvRawFromDevEvent OnRcvRawFromDev);

        //接收回调 【不再推荐使用】
        public delegate void TUSR_OnRcvEvent(int MessageID, IntPtr DevId, IntPtr pData, int DataLen);
        [DllImport("TStransfer.dll", CharSet = CharSet.Auto, EntryPoint = "USR_OnRcv", CallingConvention = CallingConvention.StdCall)]
        public static extern bool USR_OnRcv(TUSR_OnRcvEvent OnRcvEvent);


        private void richTextBoxLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }


        private void FormCloudDllDemo_Load(object sender, EventArgs e)
        {
            this.Focus();
            //初始化
            string ip = "clouddata.usr.cn";//透传云服务器地址, 打死都不改
            ushort port = 1883;//透传云服务器端口, 打死都不改
            int vertion = 1;
            try
            {
                if (USR_Init(ip, port, vertion))
                {
                    Log("初始化成功", true);
                    USR_OnConnAck(FConnAck_CBF);
                    USR_OnSubscribeAck(FSubscribeAck_CBF);
                    USR_OnUnSubscribeAck(FUnSubscribeAck_CBF);
                    USR_OnPubAck(FPubAck_CBF);
                    USR_OnRcvParsedDataPointPush(FRcvParsedDataPointPush_CBF);
                    USR_OnRcvParsedDevStatusPush(FRcvParsedDevStatusPush_CBF);
                    USR_OnRcvParsedDevAlarmPush(FRcvParsedDevAlarmPush_CBF);
                    USR_OnRcvParsedOptionResponseReturn(FRcvParsedOptionResponseReturn_CBF);
                    USR_OnRcvRawFromDev(FRcvRawFromDev_CBF);
                }
                else
                {
                    Log("初始化失败", true);
                }
                connect();
            }
            catch
            {
                MessageBox.Show("连接到服务器失败，请检查网络是否畅通！");
                button5.BackColor = Color.Red;
            }
        }

        private void connect()
        {
            string userName = "18780126369";
            string passWord = "5234970";
            if (USR_Connect(userName, passWord))
            {
                Log("连接已发起", true);
                button5.BackColor = Color.Green;
            }
            else
            {
                button5.BackColor = Color.Red;
            }
        }
        //监听函数，用来接收消息
        private bool tcp_thread_run =true;
        private IPAddress serverIP = null;
        Dictionary<string, TcpClient> dict_client = new Dictionary<string, TcpClient>();//存放套接字
        Dictionary<string, Thread> dict_Thread = new Dictionary<string, Thread>();//存放线程
        // 已接受的Tcp连接列表  
        Thread myThead = null;
        TcpListener server = null;
        private void BeginListen()
        {
            tcp_thread_run = true;
            // Buffer for reading data
            while (tcp_thread_run)
            { 
                //blocks until a client has connected to the server
                TcpClient client = this.server.AcceptTcpClient();

                //create a thread to handle communication
                //with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
                dict_client.Add(client.Client.RemoteEndPoint.ToString(), client);
                dict_Thread.Add(client.Client.RemoteEndPoint.ToString(), clientThread);
            }
        }


        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[1024];
            int bytesRead;
            while (true)
            {
                bytesRead = 0;
                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 1024);
                    if (bytesRead > 0)
                    {
                        tcp_data_recive += bytesRead;
                        if (!is_use_comm_data)
                            mqtt_send(message, bytesRead);
                        else
                            com_send_data(message,bytesRead);

                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    //a socket error has occured
                    break;
                }
            }
            try
            {
                dict_client.Remove(tcpClient.Client.RemoteEndPoint.ToString());
                dict_Thread.Remove(tcpClient.Client.RemoteEndPoint.ToString());
                tcpClient.Close();
            }
            catch
            { }
        }
        private void tcp_send(byte[] data)
        {
            try {
                foreach (var client in dict_client)
                {
                    NetworkStream clientStream = client.Value.GetStream();
                    clientStream.Write(data, 0, data.Length);
                }
                tcp_data_send += data.Length;
            }
            catch
                { }
        }
        #region 事件响应函数
        private void btn_Listen_Click(object sender, EventArgs e)
        {
            RemotePort = GetPort();
            if (RemotePort <= 0)
            {
                return;
            }
            try
            {
              serverIP = IPAddress.Parse(txb_SendIP.Text);   //IP
              server = new TcpListener(serverIP, int.Parse(txb_Port.Text));
               server.Start(1);
            }
            catch
            {
              MessageBox.Show("服务开启失败，请检查本机IP是否正确，端口是否被占用！");
                return;
            }

            myThead = new Thread(BeginListen);
            myThead.Start();
            this.btn_Listen.Enabled = false;
            this.btn_CloseListen.Enabled = true;
            txb_Port.Enabled = false;
            txb_SendIP.Enabled = false;
        }
        private void DataTransfer_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                myThead.Abort();
            }
            catch 
            {

            }
        }

        private void btn_CloseListen_Click(object sender, EventArgs e)
        {
            try
            {
                tcp_thread_run = false;
                myThead.Abort();           
                server.Stop();
                btn_Listen.Enabled = true;
                this.btn_CloseListen.Enabled = false;
                txb_SendIP.Enabled = true;
                txb_Port.Enabled = true;
                close_all_client();
            }
            catch
            {

            }
        }
        #endregion


        private void close_all_client()
        {
            foreach (var client in dict_client)
            {
                try
                {
                    client.Value.Close();
                }
                catch { }
            }
            foreach (var thread in dict_Thread)
            {
                try
                {
                    thread.Value.Abort();
                }
                catch { }
            }
            dict_client.Clear();
            dict_Thread.Clear();
        }
        public int GetPort()
        {
            try
            {
                if (int.Parse(txb_Port.Text) <-1)
                {
                    MessageBox.Show("端口错误 ","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return -1;
                }
                return  int.Parse(txb_Port.Text);               
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取本地端口失败:" + ex.Message,"ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);               
                return -1;
            }
        }

        private void txb_EditMessage_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lab_mqtt_data_send.Text = mqtt_data_send.ToString();
            lab_mqtt_data_recive.Text = mqtt_data_recive.ToString();
            lab_com_GetCount.Text = "Get:" + com_received_count.ToString();
            lab_com_send_count.Text = "Send:" + com_send_count.ToString();
            lab_tcp_data_get.Text = "Get:" + tcp_data_recive.ToString();
            lab_tcp_data_send.Text = "Send:" + tcp_data_send.ToString();
            btn_Listen.BackColor = Color.Red;
            if (dict_client.Count > 0)
            {
                btn_Listen.BackColor = Color.Green;
            }
            //设置串口状态
            set_comm_state();

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            tcp_data_recive = 0;
            tcp_data_send = 0;
            mqtt_data_send = 0;
            mqtt_data_recive = 0;
            com_send_count = 0;
            com_received_count = 0;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://shop70051014.taobao.com/?spm=a1z10.1-c-s.0.0.49abb772zxKspC");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://pan.baidu.com/s/1gfZrOyv");
        }

        private void 显示主界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                close_all_client();
                try
                {
                tcp_thread_run = false;
                myThead.Abort();
                server.Stop();
                  }
                catch
            { }
            // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void DataTransfer_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("确定要退出吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                close_all_client();
                try
                {
                    tcp_thread_run = false;
                    myThead.Abort();
                    server.Stop();
                }
                catch
                { }
                this.Dispose();
                this.Close();
                e.Cancel = false; ;
            }
            else
            {
                e.Cancel = true;             
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            comboPortName.Items.Clear();
            comboPortName.Items.AddRange(ports);
            comboPortName.SelectedIndex = comboPortName.Items.Count > 0 ? 0 : -1;
        }

        private void auto_switch_CheckedChanged(object sender, EventArgs e)
        {
            is_auto_channel_swtich = auto_switch_check.Checked;
        }

        private void box_channel_SelectedIndexChanged(object sender, EventArgs e)
        {
            is_auto_channel_swtich = false;
            auto_switch_check.Checked = false;
            if (box_channel.Text.Equals("串口数据转发"))
            {
                if (comm.IsOpen)
                {
                    is_use_comm_data = true;
                }
                else
                {
                    is_use_comm_data = false;
                    MessageBox.Show("请先开启串口！");
                }
            }
            else
            {
                is_use_comm_data = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //如果使用自动切换通道，就根据每5S接收的数据
            if (is_auto_channel_swtich)
            {
                if (!comm.IsOpen)
                {
                    is_use_comm_data = false;
                    return;
                }
                Int64 mqtt_data_diff = mqtt_data_recive - data_last_revice_mqtt;
                Int64 com_data_diff = com_received_count - data_last_recive_com;
                if (mqtt_data_diff > com_data_diff)
                    is_use_comm_data = false;
                else
                    is_use_comm_data = true;
                data_last_recive_com = com_received_count;
                data_last_revice_mqtt = mqtt_data_recive;
            }
        }

    }
}
