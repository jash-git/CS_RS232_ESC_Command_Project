using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS2015_RS232_ESC_Command
{
    public partial class Form1 : Form
    {
        public int POS_S_TextOut(String StrMsg)
        {
            byte[] Command00 = new byte[3] { 0x1B, 0x39, 0x03 };//ESC,9,1 ~ 选择中文代码格式,1:UTF-8编码 ,3:BIG5 繁体编码
            var charMsg = StrMsg.ToCharArray();
            byte[] Command01 = Encoding.GetEncoding("big5").GetBytes(charMsg);
            //byte[] Command00 = new byte[3] { 0x1B, 0x39, 0x01 };//ESC,9,1 ~ 选择中文代码格式,1:UTF-8编码 ,3:BIG5 繁体编码
            //var charMsg = StrMsg.ToCharArray();
            //byte[] Command01 = Encoding.GetEncoding("utf-8").GetBytes(charMsg);
            //Array.Reverse(Command01);
            if (m_port.IsOpen)
            {
                m_port.Write(Command00, 0, Command00.Length);
                m_port.Write(Command01, 0, Command01.Length);

            }
            return 0;
        }

        public int POS_CutPaper()
        {
            byte[] Command = new byte[4] { 0x1D, 0x56, 0x41, 0x00 };//GS,V,A,空字元 ~ 选择切纸模式并切纸
            if (m_port.IsOpen)
            {
                m_port.Write(Command, 0, Command.Length);
            }
            return 0;
        }

        public int POS_Reset()
        {
            byte[] Command = new byte[2] { 0x1B, 0x40 };//ESC,@ ~ 初始化打印机
            if (m_port.IsOpen)
            {
                m_port.Write(Command, 0, Command.Length);
            }
            return 0;
        }

        public int POS_FeedLine()
        {
            byte[] Command = new byte[1] { 0x0A };//LF ~ 換行鍵
            if (m_port.IsOpen)
            {
                m_port.Write(Command, 0, Command.Length);
            }
            return 0;
        }

        public Form1()
        {
            InitializeComponent();
        }

        public SerialPort m_port = new SerialPort();
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!m_port.IsOpen)
            {
                m_port.PortName = "COM3";
                m_port.BaudRate = 19200;
                m_port.DataBits = 8;
                m_port.StopBits = StopBits.One;
                m_port.Parity = Parity.None;
                m_port.ReadTimeout = 1;
                m_port.ReadTimeout = 3000; //单位毫秒
                m_port.WriteTimeout = 3000; //单位毫秒
                //串口控件成员变量，字面意思为接收字节阀值，
                //串口对象在收到这样长度的数据之后会触发事件处理函数
                //一般都设为1
                m_port.ReceivedBytesThreshold = 1;
                m_port.DataReceived += new SerialDataReceivedEventHandler(CommDataReceived); //设置数据接收事件（监听）
                m_port.Open();
                button1.Text = "Disconnect";
            }
            else
            {
                button1.Text = "Connect";
                m_port.Close();
            }
        }

        public void CommDataReceived(Object sender, SerialDataReceivedEventArgs e)
        {
            //https://www.cnblogs.com/xinaixia/p/6216745.html
            try
            {
                //Comm.BytesToRead中为要读入的字节长度
                int len = m_port.BytesToRead;
                Byte[] readBuffer = new Byte[len];
                m_port.Read(readBuffer, 0, len); //将数据读入缓存
                //处理readBuffer中的数据，自定义处理过程
            }
            catch (Exception ex)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!m_port.IsOpen)
            {
                m_port.PortName = "COM3";
                m_port.BaudRate = 19200;
                m_port.DataBits = 8;
                m_port.StopBits = StopBits.One;
                m_port.Parity = Parity.None;
                m_port.ReadTimeout = 1;
                m_port.ReadTimeout = 3000; //单位毫秒
                m_port.WriteTimeout = 3000; //单位毫秒
                //串口控件成员变量，字面意思为接收字节阀值，
                //串口对象在收到这样长度的数据之后会触发事件处理函数
                //一般都设为1
                m_port.ReceivedBytesThreshold = 1;
                m_port.DataReceived += new SerialDataReceivedEventHandler(CommDataReceived); //设置数据接收事件（监听）
                m_port.Open();
                button1.Text = "Disconnect";
            }
            else
            {
                button1.Text = "Connect";
                m_port.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            POS_Reset();
            POS_FeedLine();
            POS_FeedLine();
            POS_S_TextOut("Good morning");
            POS_FeedLine();
            POS_S_TextOut("早安");
            POS_FeedLine();
            POS_S_TextOut("おはようございます");
            POS_FeedLine();
            POS_FeedLine();
            POS_FeedLine();
            POS_FeedLine();
            POS_FeedLine();
            POS_FeedLine();
            POS_CutPaper();
        }
    }
}
