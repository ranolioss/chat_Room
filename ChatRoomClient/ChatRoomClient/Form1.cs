using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatRoomClient
{
    public partial class Form1 : Form
    {
        
       
        Socket soClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
     
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void getmsg()
        {
            try
            {
                while (true)
                {
                    byte[] b = new byte[1024];
                    int recb = soClient.Receive(b);
                    if (recb > 0)
                    {
                        listBox1.Items.Add("Server: "+ Encoding.Unicode.GetString(b, 0, recb));
                    }
                }

            }
            catch
            {
              ;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            IPEndPoint Ipserver = new IPEndPoint(IPAddress.Parse(txtIP.Text), int.Parse(txtPort.Text));
            try
            {
                soClient.Connect(Ipserver);
                MessageBox.Show("connect");
                Thread tr = new Thread(new ThreadStart(getmsg));
                tr.Start();
            }
            catch
            {
                MessageBox.Show("SERVER NOT CONNETC");
            }

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (soClient != null)
                {
                    soClient.Shutdown(SocketShutdown.Both);
                }
                Environment.Exit(Environment.ExitCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (soClient != null)
                {
                    soClient.Shutdown(SocketShutdown.Both);
                }
                Environment.Exit(Environment.ExitCode);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(Environment.ExitCode);
                Application.Exit();
            }
            finally
            {
                Environment.Exit(Environment.ExitCode);
                Application.Exit();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

                byte[] barray = new byte[1024];
                barray = Encoding.Unicode.GetBytes(Txtmsg.Text);
               soClient.Send(barray);
        }
    }
    
}

