namespace Host
{
    using AxRDPCOMAPILib;
    using RDPCOMAPILib;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using Utils;
    using Utils.Commands;
    using Utils.Models;

    public partial class ViewerForm : Form
    {
        private int port;
        private TcpListener server = null;
        private Thread Listening;
        private int connectionId = 0;
        private bool stop = false;
        private Dictionary<string, Connection> connectionsDictionary;
        private List<Display> displays;
        private int maxDisplays;

        public ViewerForm()
        {
            InitializeComponent();
            txtMyIp.Text = CurrentIP.GetLocalIPAddress();
            Listening = new Thread(StartListening);
            connectionsDictionary = new Dictionary<string, Connection>();

            displays = new List<Display>();
            var rdpDisplays = tabPageAll.Controls.OfType<AxRDPViewer>().ToList();
            var displaysLabels = tabPageAll.Controls.OfType<Label>().ToList();

            var i = 0;
            foreach (var rdp in rdpDisplays)
            {
                var num = ParseControlId(rdp.Name, "rdpDisplay");
                var lbl = displaysLabels.Where(l => l.Name == $"lbl{num}").FirstOrDefault();
                displays.Add(new Display(i, rdp, lbl));
                i++;
            }

            maxDisplays = displays.Count();
        }

        private void btnStarter_Click(object sender, EventArgs e)
        {
            if (btnStarter.Text.StartsWith("Запустить"))
            {
                if (Int32.TryParse(txtPort.Text, out port))
                {
                    if (server == null)
                    {
                        stop = false;
                        server = new TcpListener(IPAddress.Any, port);
                        Listening = new Thread(StartListening);
                        Listening.Start();
                        UpdateGUI.UpdateControl(btnStarter, "Text", "Остановить прослушивание");
                        UpdateGUI.UpdateControl(txtStatus, "Text", "Прослушивание подключений...");
                    }
                }
                else
                {
                    MessageBox.Show("Порт введен некорректно.");
                }
            }
            else
            {
                StopListening();
                UpdateGUI.UpdateControl(btnStarter, "Text", "Запустить прослушивание");
                UpdateGUI.UpdateControl(txtStatus, "Text", "Прослушивание остановлено.");
            }
        }

        private void StartListening()
        {
            server.Start();

            UpdateGUI.UpdateControl(txtStatus, "Text", "Прослушивание подключений...");

            while (!stop)
            {
                if (server != null)
                {
                    if (server.Pending())
                    {
                        TcpClient client = server.AcceptTcpClient();
                        var clientIp = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                        connectionId++;
                        AddConnection(clientIp, client);
                        UpdateGUI.UpdateControl(txtStatus, "Text", $"Новое подключение: {clientIp}.");
                    }
                    else
                    {
                        ListenMessages();
                    }
                }                
            }
        }

        private void ListenMessages()
        {
            var removeItems = new List<string>();
            var delecteConnectionCommand = false;

            for (var i = 0; i < connectionsDictionary.Count(); i++)
            {
                var connection = connectionsDictionary.ElementAt(i);
                var broker = connection.Value.MessageBroker;
                var cmd = broker.ReceiveMessage();

                if (cmd != null)
                {
                    if (cmd as ConnectClientCommand != null)
                    {
                        ExecuteConnectClient(connection.Value, cmd);
                    }
                    else if (cmd as DisconnectClientCommand != null)
                    {
                        delecteConnectionCommand = true;
                        removeItems.Add(connection.Key);
                    }
                    else if (cmd as ProhibitControlCommand != null)
                    {
                        ExecuteProhibit(connection.Value, cmd);
                    }
                    else if (cmd as FileExchangeCommand != null)
                    {
                        SaveFile(connection.Value, (FileExchangeCommand)cmd);
                    }
                    else if (cmd as SendChatMessage != null && cmd != null)
                    {
                        GetMessageFromClient(connection.Value, (SendChatMessage)cmd);
                    }
                }
            }

            if (delecteConnectionCommand)
            {
                foreach (var removeItem in removeItems)
                {
                    DeleteConnection(removeItem, false);
                }
            }
        }


        private void ExecuteConnectClient(Connection connection, ICommand cmd)
        {
            User user = (User)cmd.GetData()[0];
            string connectionString = (string)cmd.GetData()[1];
            bool prohibit = (bool)cmd.GetData()[2];

            connection.User = user;
            connection.ProhibitDemo = prohibit;

            if (!String.IsNullOrEmpty(connectionString))
            {
                connection.ConnectionString = connectionString;

                var tab = CreateNewTab(connection.ClientIP, true, prohibit);
                AxRDPViewer viewer = (AxRDPViewer)tab.Controls.Find($"rdpViewer{connectionId}", false).FirstOrDefault();

                //Нужно добавить!!!
                viewer.OnConnectionTerminated += (reason, info) => DeleteConnection(connection.ClientIP, false);
                viewer.OnApplicationClose += (reason, info) => DeleteConnection(connection.ClientIP, false);
                viewer.OnAttendeeDisconnected += (reason, info) => DeleteConnection(connection.ClientIP, false);
                viewer.OnConnectionFailed += (reason, info) => DeleteConnection(connection.ClientIP, false);

                if (viewer != null)
                {
                    UpdateGUI.UpdateControl(viewer, "SmartSizing", true);
                    connection.RdpViewer = viewer;
                    connection.TabPage = tab;
                }
            }
            else
            {
                var tab = CreateNewTab(connection.ClientIP, false, prohibit);
                connection.TabPage = tab;
                MessageBox.Show($"Не удалось получить строку подключения от клиента {connection.ClientIP}. Мониторинг невозможен.");
                UpdateGUI.UpdateControl(txtStatus, "Text", $"Не удалось получить строку подключения от клиента {connection.ClientIP}.");
            }
        }

        private void ExecuteProhibit(Connection connection, ICommand cmd)
        {
            bool prohibit = (bool)cmd.GetData()[0];
            connection.ProhibitDemo = prohibit;

            connection.State = Utils.Models.ConnectionState.Demonstration;
            try
            {
                connection.RdpViewer.RequestControl(CTRL_LEVEL.CTRL_LEVEL_VIEW);
            }
            catch { }

            var btns = connection.TabPage.Controls.OfType<Button>().ToList();

            if (prohibit == true)
            {

                UpdateGUI.UpdateControl(btns[2], "Enabled", false);
                UpdateGUI.UpdateControl(btns[3], "Enabled", false);
            }
            else
            {
                UpdateGUI.UpdateControl(btns[2], "Enabled", true);
            }
        }

        private void DeleteConnection(string ip, bool disconnectRequest)
        {
            var connection = connectionsDictionary[ip];

            if (disconnectRequest)
                connection.MessageBroker.SendMessage(new DisconnectServerCommand(true));

            if (connection.RdpViewer != null)
                connection.RdpViewer.Disconnect();

            DeleteTabPage($"tabPage{connection.ConnectionId}");
            connectionsDictionary.Remove(ip);

            UntieDisplayToConnection(connection);
            var item = checkedListBox1.Items.IndexOf($"{connection.ClientIP}");

            this.Invoke(new Action(() => {
                checkedListBox1.Items.RemoveAt(item);
            }));
            UpdateGUI.UpdateControl(txtStatus, "Text", $"Клиент {ip} отключен.");
        }

        private void StopListening()
        {
            stop = true;
            var n = connectionsDictionary.Count();
            var keys = new List<string>(connectionsDictionary.Keys);

            if (n > 0)
            {
                for (var i = 0; i < n; i++)
                {
                    DeleteConnection(keys[i], true);
                }

                server.Stop();
                connectionId = 0;
                server = null;
            }
        }

        private Display FindEmptyDisplay()
        {
            var emptyDisplay = displays.FirstOrDefault(x => x.State == Status.Empty);
            return emptyDisplay;
        }

        private void AddConnection(string clientIp, TcpClient client)
        {
            if (!connectionsDictionary.ContainsKey(clientIp))
            {
                var messageBroker = new MessageBroker() { Client = client };
                var myAuthData = new AuthorizationData() { Name = null, GroupName = Environment.UserName, Password = "" };

                var connection = new Connection
                {
                    ConnectionId = connectionId,
                    ClientIP = clientIp,
                    Client = client,
                    MessageBroker = messageBroker,
                    AuthData = myAuthData,
                    State = Utils.Models.ConnectionState.Off
                };

                connectionsDictionary.Add(clientIp, connection);

                var display = FindEmptyDisplay();
                var displayId = display.Id;

                this.Invoke(new Action(() =>
                {
                    checkedListBox1.Items.Add($"{clientIp}", false);
                }));

            }
        }

        private void TieDisplayToConnection(Connection connection)
        {
            var display = FindEmptyDisplay();
            var displayId = display.Id;

            if (display != null)
            {
                if (connection.State == Utils.Models.ConnectionState.Demonstration)
                {
                    display.RdpDisplay.SmartSizing = true;
                    display.Reserve(connection.ConnectionId);
                    display.RdpDisplay.Connect(connection.ConnectionString, connection.AuthData.Name, connection.AuthData.Password);
                    var name = string.IsNullOrEmpty(connection.User.LabName) ? "" : connection.User.LabName;
                    UpdateGUI.UpdateControl(display.Label, "Text", $"{connection.ClientIP} {name}");
                }
            }
        }

        private void UntieDisplayToConnection(Connection connection)
        {
            var display = displays.Where(d => d.ConnectionId == connection.ConnectionId).FirstOrDefault();

            if (display != null)
            {
                display.Unreserve(connection.ConnectionId);
                display.RdpDisplay.Disconnect();
                UpdateGUI.UpdateControl(display.Label, "Text", string.Empty);
            }
        }

        private void DeleteTabPage(string name)
        {
            this.SuspendLayout();

            this.Invoke(new Action(() =>
            {
                var myTabPage = tabControl1.TabPages[name];
                if (myTabPage != null)
                    tabControl1.TabPages.Remove(myTabPage);
            }));

            this.ResumeLayout();
        }

        private TabPage InitTabPage(string clientIp)
        {
            var title = $"{clientIp}";
            var myTabPage = new TabPage(title);
            myTabPage.Name = $"tabPage{connectionId}";
            //myTabPage.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;

            return myTabPage;
        }

        private void CreateButtons(ref TabPage myTabPage, bool prohibitControl)
        {
            // Start Translation Button
            Button btnStartTranslation = new Button();
            btnStartTranslation.Name = $"btnStartView{connectionId}";
            btnStartTranslation.Location = new Point(0, 445);
            btnStartTranslation.Size = new Size(158, 30);
            btnStartTranslation.Text = "Включить демонстрацию";
            btnStartTranslation.Click += new EventHandler(StartTranslationButton_Click);
            myTabPage.Controls.Add(btnStartTranslation);

            // Stop Translation Button
            Button btnStopTranslation = new Button();
            btnStopTranslation.Name = $"btnStopView{connectionId}";
            btnStopTranslation.Location = new Point(158, 445);
            btnStopTranslation.Size = new Size(158, 30);
            btnStopTranslation.Text = "Отключить демонстрацию";
            btnStopTranslation.Enabled = false;
            btnStopTranslation.Click += new EventHandler(StopTranslationButton_Click);
            myTabPage.Controls.Add(btnStopTranslation);

            // Request Control Button
            Button btnRequestControl = new Button();
            btnRequestControl.Name = $"btnRequestControl{connectionId}";
            btnRequestControl.Location = new Point(316, 445);
            btnRequestControl.Size = new Size(158, 30);
            btnRequestControl.Text = "Запросить управление";
            btnRequestControl.Enabled = false;
            btnRequestControl.Click += new EventHandler(RequestControlButton_Click);
            myTabPage.Controls.Add(btnRequestControl);

            // Stop Control Button
            Button btnStopControl = new Button();
            btnStopControl.Name = $"btnStopControl{connectionId}";
            btnStopControl.Location = new Point(474, 445);
            btnStopControl.Size = new Size(158, 30);
            btnStopControl.Text = "Остановить управление";
            btnStopControl.Enabled = false;
            btnStopControl.Click += new EventHandler(StopControlButton_Click);
            myTabPage.Controls.Add(btnStopControl);

            // Delete Button
            Button btnDelecteConnection = new Button();
            btnDelecteConnection.Name = $"btnDelecteConnection{connectionId}";
            btnDelecteConnection.Location = new Point(632, 445);
            btnDelecteConnection.Size = new Size(158, 30);
            btnDelecteConnection.Text = "Удалить подключение";
            btnDelecteConnection.Click += new EventHandler(DeleteConnectionButton_Click);
            myTabPage.Controls.Add(btnDelecteConnection);

            // SendMsg
            Button btnSendMessage = new Button();
            btnSendMessage.Name = $"btnSendMsg{connectionId}";
            btnSendMessage.Location = new Point(800, 530);
            btnSendMessage.Size = new Size(150, 45);
            btnSendMessage.Text = "Отправить сообщение";
            btnSendMessage.Click += new EventHandler(SendMsgButton_Click);
            myTabPage.Controls.Add(btnSendMessage);

            // SendFile
            Button btnSendFile = new Button();
            btnSendFile.Name = $"btnSendFile{connectionId}";
            btnSendFile.Location = new Point(950, 530);
            btnSendFile.Size = new Size(150, 45);
            btnSendFile.Text = "Отправить файл";
            btnSendFile.Click += new EventHandler(SendFileButton_Click);
            myTabPage.Controls.Add(btnSendFile);
        }

        private void CreateTxts(ref TabPage myTabPage)
        {
            var pair = connectionsDictionary.Where(c => c.Value.ConnectionId == connectionId).FirstOrDefault();
            var user = pair.Value.User;

            if (user == null)
            {
                user = new User("Empty", "Empty", "Empty");
            }

            // Lab Name
            Label lblLabName = new Label();
            lblLabName.Name = $"lblbLabName{connectionId}";
            lblLabName.Location = new Point(0, 490);
            lblLabName.Size = new Size(158, 40);
            lblLabName.Text = "Лаборатория: ";
            myTabPage.Controls.Add(lblLabName);

            RichTextBox txtLabName = new RichTextBox();
            txtLabName.Name = $"txtLabName{connectionId}";
            txtLabName.Location = new Point(158, 490);
            txtLabName.Size = new Size(237, 40);
            txtLabName.Text = (!string.IsNullOrEmpty(user.LabName)) ? user.LabName : "Empty";
            txtLabName.ReadOnly = true;
            myTabPage.Controls.Add(txtLabName);

            // User Name
            Label lblUserName = new Label();
            lblUserName.Name = $"lblUserName{connectionId}";
            lblUserName.Location = new Point(405, 490);
            lblUserName.Size = new Size(148, 40);
            lblUserName.Text = "Инженер испытания: ";
            myTabPage.Controls.Add(lblUserName);

            RichTextBox txtUserName = new RichTextBox();
            txtUserName.Name = $"txtUserName{connectionId}";
            txtUserName.Location = new Point(553, 490);
            txtUserName.Size = new Size(237, 40);
            txtUserName.Text = (!string.IsNullOrEmpty(user.UserName)) ? user.UserName : "Empty";
            txtUserName.ReadOnly = true;
            myTabPage.Controls.Add(txtUserName);

            //Test Name
            Label lblTestName = new Label();
            lblTestName.Name = $"lblTestName{connectionId}";
            lblTestName.Location = new Point(0, 530);
            lblTestName.Size = new Size(158, 50);
            lblTestName.Text = "Испытание: ";
            myTabPage.Controls.Add(lblTestName);

            RichTextBox txtTestName = new RichTextBox();
            txtTestName.Name = $"txtTestName{connectionId}";
            txtTestName.Location = new Point(158, 530);
            //txtTestName.Location = new Point(0, 530);
            txtTestName.Size = new Size(632, 50);
            txtTestName.Text = (!string.IsNullOrEmpty(user.TestName)) ? user.TestName : "Empty";
            txtTestName.ReadOnly = true;
            myTabPage.Controls.Add(txtTestName);

            // Chat name
            Label lblChatName = new Label();
            lblChatName.Name = $"lblChatName{connectionId}";
            lblChatName.Location = new Point(800, 0);
            lblChatName.Size = new Size(300, 30);
            lblChatName.Text = $"Чат с : {pair.Value.ClientIP} - {txtUserName.Text}";
            myTabPage.Controls.Add(lblChatName);

            RichTextBox txtChat = new RichTextBox();
            txtChat.Name = $"txtChat{connectionId}";
            txtChat.Location = new Point(800, 30);
            txtChat.Size = new Size(300, 415);
            txtChat.ReadOnly = true;
            myTabPage.Controls.Add(txtChat);

            // Chat name
            Label ChatMsg = new Label();
            ChatMsg.Name = $"lblChatMsg{connectionId}";
            ChatMsg.Location = new Point(800, 445);
            ChatMsg.Size = new Size(300, 20);
            ChatMsg.Text = $"Новое сообщение:";
            myTabPage.Controls.Add(ChatMsg);

            RichTextBox txtChatMsg = new RichTextBox();
            txtChatMsg.Name = $"txtChatMsg{connectionId}";
            txtChatMsg.Location = new Point(800, 475);
            txtChatMsg.Size = new Size(300, 55);
            myTabPage.Controls.Add(txtChatMsg);
        }

        private delegate void RdpViewerCreation(ref TabPage myTabPage);

        private void CreateRdpViewerMethod(ref TabPage myTabPage)
        {
            AxRDPViewer axRDPViewer = new AxRDPViewer();
            axRDPViewer.Name = $"rdpViewer{connectionId}";
            axRDPViewer.Location = new Point(0, 0);
            axRDPViewer.Size = new Size(790, 445);
            myTabPage.Controls.Add(axRDPViewer);
        }

        private TabPage CreateNewTab(string clientIp, bool rdpSet, bool prohibitControl)
        {
            this.SuspendLayout();

            var myTabPage = InitTabPage(clientIp);
            CreateButtons(ref myTabPage, prohibitControl);
            CreateTxts(ref myTabPage);

            if (rdpSet)
            {
                this.Invoke(new RdpViewerCreation(CreateRdpViewerMethod), myTabPage);
            }

            tabControl1.Invoke(new Action(() => { tabControl1.TabPages.Add(myTabPage); }));
            this.ResumeLayout();

            return myTabPage;
        }

        private void DeleteConnectionButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var btnId = ParseControlId(btn.Name, "btnDelecteConnection");
            var ip = connectionsDictionary.Where(c => c.Value.ConnectionId == btnId).FirstOrDefault().Value.ClientIP;
            DeleteConnection(ip, true);
        }

        private void StopControlButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var btnId = ParseControlId(btn.Name, "btnStopControl");
            var currentConnection = connectionsDictionary.Where(c => c.Value.ConnectionId == btnId).FirstOrDefault().Value;
            currentConnection.State = Utils.Models.ConnectionState.Demonstration;

            if (btnId > 0)
            {
                var ip = string.Empty;

                try
                {
                    currentConnection.RdpViewer.RequestControl(CTRL_LEVEL.CTRL_LEVEL_VIEW);

                    foreach (var connection in connectionsDictionary)
                    {
                        var btns = connection.Value.TabPage.Controls.OfType<Button>().ToList();

                        if (connection.Value.ConnectionId != btnId)
                        {
                            btns[2].Enabled = !connection.Value.ProhibitDemo;
                            btns[3].Enabled = true;
                        }
                        else
                        {
                            ip = connection.Value.ClientIP;
                            btn.Enabled = false;
                            btns[0].Enabled = false;
                            btns[1].Enabled = true;
                            btns[2].Enabled = !connection.Value.ProhibitDemo;
                        }
                    }
                    UpdateGUI.UpdateControl(txtStatus, "Text", $"Управление клиентом {ip} остановлено.");
                }
                catch
                {
                    MessageBox.Show($"Не удалось запустить управление клиентом {ip}.");
                    UpdateGUI.UpdateControl(txtStatus, "Text", $"Не удалось запустить управление клиентом {ip}.");
                }
            }
        }

        private void RequestControlButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var btnId = ParseControlId(btn.Name, "btnRequestControl");
            var currentConnection = connectionsDictionary.Where(c => c.Value.ConnectionId == btnId).FirstOrDefault().Value;

            if (btnId > 0)
            {
                var ip = string.Empty;

                try
                {
                    currentConnection.RdpViewer.RequestControl(CTRL_LEVEL.CTRL_LEVEL_INTERACTIVE);
                    currentConnection.State = Utils.Models.ConnectionState.Control;

                    foreach (var connection in connectionsDictionary)
                    {
                        var btns = connection.Value.TabPage.Controls.OfType<Button>().ToList();

                        if (connection.Value.ConnectionId != btnId)
                        {
                            btns[2].Enabled = false;
                            btns[3].Enabled = false;
                        }
                        else
                        {
                            ip = connection.Value.ClientIP;
                            btn.Enabled = false;
                            btns[0].Enabled = false;
                            btns[1].Enabled = true;
                            btns[3].Enabled = true;
                        }
                    }

                    UpdateGUI.UpdateControl(txtStatus, "Text", $"Запущено управление клиентом {ip}.");
                }
                catch
                {
                    MessageBox.Show($"Не удалось запустить управление клиентом {ip}.");
                    UpdateGUI.UpdateControl(txtStatus, "Text", $"Не удалось запустить управление клиентом {ip}.");
                }
            }
        }

        private void StartTranslationButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var btnId = ParseControlId(btn.Name, "btnStartView");

            if (btnId > 0)
            {
                var ip = connectionsDictionary.Where(c => c.Value.ConnectionId == btnId).FirstOrDefault().Value.ClientIP;
                var connection = connectionsDictionary[ip];
                connection.State = Utils.Models.ConnectionState.Demonstration;
                var conStr = connectionsDictionary[ip].ConnectionString;

                connection.RdpViewer.Connect(connection.ConnectionString, connection.AuthData.Name, connection.AuthData.Password);

                btn.Enabled = false;
                connection.TabPage.Controls.OfType<Button>().ToList()[1].Enabled = true;
                connection.TabPage.Controls.OfType<Button>().ToList()[2].Enabled = !connection.ProhibitDemo;
                connection.TabPage.Controls.OfType<Button>().ToList()[3].Enabled = false;
                UpdateGUI.UpdateControl(txtStatus, "Text", "Трансляция запущена.");
            }
        }

        private void StopTranslationButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var btnId = ParseControlId(btn.Name, "btnStopView");

            if (btnId > 0)
            {
                var ip = connectionsDictionary.Where(c => c.Value.ConnectionId == btnId).FirstOrDefault().Value.ClientIP;
                var connection = connectionsDictionary[ip];
                connection.State = Utils.Models.ConnectionState.Off;

                try
                {
                    connection.RdpViewer.Disconnect();
                }
                catch { }

                connection.TabPage.Controls.OfType<Button>().ToList()[0].Enabled = true;
                connection.TabPage.Controls.OfType<Button>().ToList()[2].Enabled = false;

                btn.Enabled = false;

                UpdateGUI.UpdateControl(txtStatus, "Text", "Трансляция остановлена.");
            }
        }

        private void SendMsgButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var btnId = ParseControlId(btn.Name, "btnSendMsg");

            if (btnId > 0)
            {
                var con = connectionsDictionary.Where(c => c.Value.ConnectionId == btnId).FirstOrDefault();

                if (con.Value != null)
                {
                    var tempTxts = con.Value.TabPage.Controls.OfType<RichTextBox>().ToList();

                    if (tempTxts.Count > 0)
                    {
                        var txt = tempTxts.Where(t => t.Name == $"txtChatMsg{btnId}").FirstOrDefault(); //
                        var txChat = tempTxts.Where(t => t.Name == $"txtChat{btnId}").FirstOrDefault(); //

                        if (txt != null)
                        {
                            con.Value.MessageBroker.SendMessage(new SendChatMessage(txt.Text));

                            this.Invoke(new Action(() =>
                            {
                                txChat.Text += $"{DateTime.Now.ToString()} Server (me): \n{txt.Text}\n";
                                txt.Text = string.Empty;
                            }));
                        }
                    }
                }
            }
        }

        private void SendFileButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            var btnId = ParseControlId(btn.Name, "btnSendFile");

            if (btnId > 0)
            {
                var con = connectionsDictionary.Where(c => c.Value.ConnectionId == btnId).FirstOrDefault();

                if (con.Value != null)
                {
                    int size = -1;
                    var openFileDialog1 = new OpenFileDialog();
                    openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                    DialogResult result = openFileDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string file = openFileDialog1.FileName;
                        try
                        {
                            var cmd = con.Value.MessageBroker.PackFile(file);
                            con.Value.MessageBroker.SendMessage(cmd);

                            var tempTxts = con.Value.TabPage.Controls.OfType<RichTextBox>().ToList();

                            if (tempTxts.Count > 0)
                            {
                                var txChat = tempTxts.Where(t => t.Name == $"txtChat{btnId}").FirstOrDefault();
                                var tempStr = string.IsNullOrEmpty(con.Value.User.UserName) ? "" : con.Value.User.UserName;

                                if (txChat != null)
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                        txChat.Text += $"{DateTime.Now.ToString()} Server (me): \nФайл {file} отправлен клиенту {con.Value.ClientIP} {tempStr}\n\n";
                                    }));
                                }
                            }
                        }
                        catch (IOException)
                        {
                        }
                    }
                }
            }
        }

        private int ParseControlId(string btnName, string btnBaseName)
        {
            var idStr = Regex.Replace(btnName, btnBaseName, string.Empty);
            int id = 0;

            if (Int32.TryParse(idStr, out id))
            {
                return id;
            }

            return -1;
        }

        private void ConnectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connectionsDictionary.Count() > 0)
                StopListening();

            stop = true;

            this.Dispose();
            this.Close();
        }

        private void SaveFile(Connection con, FileExchangeCommand cmd)
        {
            string fileName = (string)cmd.GetData()[0];
            int len = (int)cmd.GetData()[1];
            byte[] content = (byte[])cmd.GetData()[2];

            char[] splitter = new char[] { '\\' };
            var dirs = fileName.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            var dirsLen = dirs.Count();
            string simpleFileName = dirs[dirsLen - 1];

            var name = string.IsNullOrEmpty(con.User.LabName) ? "" : con.User.LabName;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = $"Сохранить файл от клиента {con.ClientIP}  {name}  {simpleFileName}";

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = simpleFileName;

            var tempTxts = con.TabPage.Controls.OfType<RichTextBox>().ToList();
            var tmpTxt = tempTxts.Where(t => t.Name == $"txtChat{con.ConnectionId}").FirstOrDefault();
            var fullname = string.Empty;

            this.Invoke(new Action(() =>
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fullname = Path.GetFullPath(saveFileDialog1.FileName);

                    using (FileStream fs = new FileStream(fullname, FileMode.Create))
                    {
                        fs.Write(content, 0, len);
                    }
                }

                if (tempTxts.Count() > 0)
                {
                    tmpTxt.Text += $"{DateTime.Now.ToString()} Клиент {con.ClientIP} {con.User.LabName} {con.User.UserName} прислал новый файл.\n\n Сохранен в {fullname}\n\n";
                }
            }));
        }

        private void GetMessageFromClient(Connection con, SendChatMessage cmd)
        {
            if (con.ConnectionId > 0)
            {
                var key = $"txtChat{con.ConnectionId}";
                string msg = (string)cmd.GetData()[0];
                this.Invoke(new Action(() =>
                {
                    var txt = con.TabPage.Controls.Find(key, false).FirstOrDefault();

                    if (txt != null)
                    {
                        txt.Text += $"{DateTime.Now.ToString()} {con.ClientIP}: \n{msg} \n\n";
                    }
                }));
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var checkedItems = 0;
            var checkedIp = checkedListBox1.Items[e.Index].ToString();
            var chosenConnection = connectionsDictionary.Where(c => c.Value.ClientIP == checkedIp).FirstOrDefault().Value;

            if (checkedListBox1.CheckedItems.Count < 4)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    TieDisplayToConnection(chosenConnection);
                }
                else
                {
                    UntieDisplayToConnection(chosenConnection);
                }
            }
            else if (checkedListBox1.CheckedItems.Count == 4)
            {
                var ip = checkedListBox1.CheckedItems[0].ToString();
            }
        }
    }
}
