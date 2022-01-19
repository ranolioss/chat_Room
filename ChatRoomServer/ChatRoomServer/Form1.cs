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

namespace ChatRoomServer
{
    public partial class Form1 : Form
    {
        Socket socSever = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket socClient=null;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void getmsg()
        {
            try
            {
                while (true)
                {
                    byte[] b = new byte[1024];
                    int recb = socClient.Receive(b);
                    if (recb > 0)
                    {
                        listBox1.Items.Add("Client: "+txtIP.Text+ Encoding.Unicode.GetString(b, 0, recb));
                    }
                }

            }
            catch
            {
                ;
            }
        }
        public void startServer()
        {
            IPEndPoint Ipserver = new IPEndPoint(IPAddress.Any, int.Parse(txtPort.Text));

            socSever.Bind(Ipserver);
            socSever.Listen(100);
            socClient = socSever.Accept();
            Thread tr = new Thread(new ThreadStart(getmsg));
            tr.Start();
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {

            Thread trstart = new Thread(new ThreadStart(startServer));
            trstart.Start();
    
           
          
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] barray = new byte[1024];
            barray = Encoding.Unicode.GetBytes(Txtmsg.Text);
            socClient.Send(barray);

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (socClient != null)
                {
                    socClient.Shutdown(SocketShutdown.Both);
                }

                if (socSever != null)
                {
                    socSever.Shutdown(SocketShutdown.Both);
                }
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
                if (socClient != null)
                {
                    socClient.Shutdown(SocketShutdown.Both);
                }

                if (socSever != null)
                {
                    socSever.Shutdown(SocketShutdown.Both);
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
    }
}
    
