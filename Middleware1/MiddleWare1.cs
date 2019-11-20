using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace MiddleWare1
{


    public partial class MiddleWare1 : Form
    {
        private int count = 1;
        private int clock = 0;
        private string id = "Middleware 1";
        private string idAbre = "M1";
        private List<int>[] ackSeq = new List<int>[100];   

        private class MSG
        {
            public string Msg { get; set; }
            public string Msgid { get; set; }
            public string Middlewareid { get; set; }
            public int ProposedTimeStamp { get; set; }
            public Boolean Deliverable { get; set; }
        }
        private List<MSG> msgSeq = new List<MSG>();

        // private string finalAck; 
        public MiddleWare1()
        {
            InitializeComponent();
            ReceiveMulticast();

        }
        int myPort = 8082;

        private async void ReceiveMulticast()
        {

          /*  ackSeq[0] = new List<int>();
            ackSeq[1] = new List<int>();
            ackSeq[2] = new List<int>();
            ackSeq[3] = new List<int>();
            ackSeq[4] = new List<int>(); */
            byte[] bytes = new Byte[1024];
           

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = null;
            foreach (IPAddress ip in ipHostInfo.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip;
                    break;
                }
            }
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, myPort);

            TcpListener listener = new TcpListener(localEndPoint);
            listener.Start(10);
            try
            {
                string data = null;
                //Waiting for incoming connection forever
                while (true)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    data = null;

                    //wait for incomming msg
                    while (true)
                    {
                        bytes = new byte[1024];
                        NetworkStream stream = client.GetStream();
                        int bytesRead = await stream.ReadAsync(bytes, 0, 1024);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRead);

                        //to depend the reaction on what the received msg is 
                        if (data.IndexOf("EOM") > -1)
                        {
                            //if the message is an ACK message
                            if (data.IndexOf("ACK") > -1)
                            {
                                //if the sender receive the ACK,  send a FinACK msg if #seq >=5, else append it to the ackSeq list
                                if (data.IndexOf(idAbre) > -1)
                                {
                                    string timeStamp = GetTimeStamp(data);
                                    string[] time = timeStamp.Split(',');
                                    string msgId = GetMsgId(data, 2, 3);
                                    int ackIndex = Int32.Parse(msgId.Substring(msgId.IndexOf("#") + 1));
                                    //MessageBox.Show("msgId: " + msgId + " ackIndex: " + ackIndex + " ackSeqLength: " + ackSeq.Length());
                                    if (ackSeq[ackIndex]==null)
                                    {
                                        ackSeq[ackIndex] = new List<int>();
                                    }
                                    ackSeq[ackIndex].Add(Int32.Parse(time[0]));                                   
                                    if (ackSeq[ackIndex].Count >= 5)
                                    {
                                        int agreedTime = ackSeq[ackIndex].Max();
                                        //string msgId = GetMsgId(data, 2, 3);
                                        string finalAck = "FinAck for " + msgId + " from " + id + ": (" + agreedTime + "," + idAbre + ") <EOM>\n";
                                        bytes = Encoding.ASCII.GetBytes(finalAck);
                                        SendMsg(bytes);
                                        ackSeq[ackIndex].Clear();
                                    }

                                    //parse the                                                                               

                                    stream.Close();
                                    break;
                                }
                                //if the receiver receive ACK msg, drop it and continue listening for income msg
                                else
                                {
                                    break;
                                }
                            }

                            //if the message is an FinACK msg
                            else if (data.IndexOf("FinAck") > -1)
                            {
                                //need to update this line
                                string timeStamp = GetTimeStamp(data);
                                string[] time = timeStamp.Split(',');
                                string msgId = GetMsgId(data,2,3);
                                int index = msgSeq.FindIndex(item => item.Msg.Contains(time[1]) && item.Msg.Contains(msgId));
                               // MessageBox.Show(id + "count: " + msgSeq.Count.ToString() + "\n" + "index: " + index);
                                msgSeq[index].ProposedTimeStamp = Int32.Parse(time[0]);
                                msgSeq[index].Deliverable = true;
                                msgSeq.Sort(delegate (MSG x, MSG y)
                                     {
                                         if (x.ProposedTimeStamp == y.ProposedTimeStamp)
                                         {
                                             if (Int32.Parse(x.Middlewareid.Substring(1)) < Int32.Parse(y.Middlewareid.Substring(1)))
                                             { return -1; }
                                             else if (Int32.Parse(x.Middlewareid.Substring(1)) == Int32.Parse(y.Middlewareid.Substring(1)))
                                             {
                                                 if (Int32.Parse(x.Msgid.Substring(5)) < Int32.Parse(y.Msgid.Substring(5)))
                                                 {
                                                     return -1;
                                                 }
                                                 else return 1;
                                             }
                                             else return 1;
                                         }
                                         else {
                                             return x.ProposedTimeStamp.CompareTo(y.ProposedTimeStamp);
                                         }
                                        
                                     });
                                while (true)
                                {
                                    if (msgSeq.Count != 0 && msgSeq[0].Deliverable == true)
                                    {
                                        listBoxReady.Items.Add(msgSeq[0].Msg);
                                        msgSeq.RemoveAt(0);
                                      
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                            }
                            else
                            {
                                //in case the data redandunt 
                                if (listBoxReceived.Items.IndexOf(data) > -1) { break; }
                                listBoxReceived.Items.Add(data);
                                string timeStamp = GetTimeStamp(data);
                                //update local logical clock when receiving a message
                                string[] time = timeStamp.Split(',');
                                clock = Math.Max(Int32.Parse(time[0]), clock) + 1;
                                string msgId = GetMsgId(data, 0, 1);
                                // msgSeq.Add(data + "[undeliverable]");
                                string ack = "ACK for " + msgId + " from " + id + ": (" + clock + "," + time[1] + ") <EOM>\n";
                                msgSeq.Add(new MSG() { Msg = data, Msgid = msgId, Middlewareid = time[1], ProposedTimeStamp = clock, Deliverable = false });
                                bytes = Encoding.ASCII.GetBytes(ack);
                                SendMsg(bytes);
                                break;

                            }
                            stream.Close();
                            break;
                        }


                    }
                }
            }
            catch (Exception e)
            { MessageBox.Show(e.ToString()); }
        }

        public string GetTimeStamp(string data)
        {
            int timeStampStart = data.IndexOf("(");
            int timeStampEnd = data.IndexOf(")");
            string timeStamp = data.Substring(timeStampStart + 1, timeStampEnd - timeStampStart - 1);
            return timeStamp;
        }

        public string GetMsgId(string data, int m, int n)
        {
            string[] finAck = data.Split(' ');
            string msgId = finAck[m] + " " + finAck[n];
            return msgId;
        }

        public void SendMsg(byte[] bytes)
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = null;
                foreach (IPAddress ip in ipHostInfo.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip;
                        break;
                    }
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8081);
                Socket sendSocket;
                try
                {
                    // Create a TCP/IP  socket.
                    sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // Connect to the Network 
                    sendSocket.Connect(remoteEP);


                    // Send the data to the network.
                    int bytesSent = sendSocket.Send(bytes);

                    sendSocket.Shutdown(SocketShutdown.Both);
                    sendSocket.Close();

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        public void DoWork()
        {
            //send ACK
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = null;
                foreach (IPAddress ip in ipHostInfo.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip;
                        break;
                    }
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8081);
                Socket sendSocket;
                try
                {
                    // Create a TCP/IP  socket.
                    sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // Connect to the Network 
                    sendSocket.Connect(remoteEP);

              
                    
                    string msgStr = "Msg #" + count + " from " + id + ": (" + clock + "," + idAbre + ") <EOM>\n";
                    count++;
                    listBoxSent.Items.Add(msgStr);
                    byte[] msg = Encoding.ASCII.GetBytes(msgStr);

                    // Send the data to the network.
                    byte[] bytes = new byte[256];
                    int bytesSent = sendSocket.Send(msg);
                    clock++;
              
                    sendSocket.Shutdown(SocketShutdown.Both);
                    sendSocket.Close();

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }


        }
        private void sendButton_Click(object sender, EventArgs e)
        {
            DoWork();

        }

    }

}

