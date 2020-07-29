namespace RemoteClient
{
    using System;
    using System.Net;
    using System.Windows.Forms;
    using Utils;
    using Utils.Models;
    using Utils.Commands;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public partial class FormClient : Form
    {
        private Client _client;
        private bool desktopOption = true;
        private string chosenApp = "";
        private bool firstRun = true;
        private Settings settings;
        private User user;
        private AuthorizationData authData;
        public delegate void ChangeSettings();
        public event ChangeSettings ChangeSettingsEvent;
        public delegate void ChangeUserData(string s, string s1, string s2);
        public event ChangeUserData ChangeUserDataEvent;
        public FormClient()
        {
            InitializeComponent();
            settings = new Settings();
            user = new User(
                Properties.Settings.Default.UserName,
                Properties.Settings.Default.LabName,
                Properties.Settings.Default.TestName);
            authData = new AuthorizationData
            {
                Name = null,
                GroupName = Environment.UserName,
                Password = "",
                MaxClients = 2,
                ProhibitDemonstration = Properties.Settings.Default.ProhibitDemonstration
            };
            ChangeSettingsEvent += settings.UpdateSettings;
            ChangeUserDataEvent += user.UpdateUserData;
            InitSettingsAndData();
            _client = new Client(settings, user, authData, Listening, true);
            txtMyIp.Text = _client.MyIP;
            btnResume.Enabled = false;
            btnPause.Enabled = false;
            btnSendFile.Enabled = false;
            btnConnect.Enabled = false;
            btnMsgSend.Enabled = false;
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text.StartsWith("Подключиться"))
            {
                if (firstRun)
                {
                    firstRun = false;
                }
                try
                {
                    if (!_client.Settings.IsEmpty())
                    {
                        if (checkedListBox1.CheckedItems.Count > 0)
                        {
                            _client.ConnectTcp();
                            _client.CreateSession(desktopOption, chosenApp);
                            _client.CreateConnectionString();
                            _client.SendConnectionCommand();
                            txtConnectionString.Text = _client.ConnectionString;
                            UpdateGUI.UpdateControl(btnMsgSend, "Enabled", true);
                            UpdateGUI.UpdateControl(btnSendFile, "Enabled", true);
                            UpdateGUI.UpdateControl(btnPause, "Enabled", true);
                            UpdateGUI.UpdateControl(btnResume, "Enabled", true);
                            _client.MessageListener.Start();
                            UpdateGUI.UpdateControl(txtStatus, "Text", "Подключение к серверу выполнено успешно.");
                            UpdateGUI.UpdateControl(btnConnect, "Text", "Отключиться от сервера");
                        }
                        else
                        {
                            MessageBox.Show("Опция демонстрации не выбрана.\n");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Настройки подключения не заданы!\n");
                    }
                }
                catch (Exception ex)
                {
                    _client.TcpClient = null;
                    _client.MessageBroker = null;
                    _client.Connected = false;
                    MessageBox.Show("Не удалось подключиться к севреру.\n");
                    UpdateGUI.UpdateControl(txtStatus, "Text", "Не удалось подключиться к севреру.");
                }
            }
            else
            {
                if (_client.TcpClient != null)
                {
                    _client.Disconnect(true);
                    _client.Dispose();
                    this.Dispose();
                    this.Close();
                }
            }
        }
        private void Listening()
        {
            while (_client.ListenOn)
            {
                var cmd = _client.MessageBroker.ReceiveMessage();
                if (cmd as DisconnectServerCommand != null && cmd != null)
                {
                    if (_client.TcpClient != null && _client.RdpSession != null)
                    {
                        _client.Dispose();
                    }
                    MessageBox.Show("Сервер отключился");
                    this.Invoke(new Action(() =>
                    {
                        this.Dispose();
                        this.Close();
                    }));
                }
                else if (cmd as SendChatMessage != null && cmd != null)
                {
                    string msg = (string)cmd.GetData()[0];
                    this.Invoke(new Action(() =>
                    {
                        txtChat.Text += $"{DateTime.Now.ToString()} Server\n: {msg} \n\n";
                    }));
                }
                else if (cmd as FileExchangeCommand != null)
                {
                    SaveFile((FileExchangeCommand)cmd);
                }
            }
        }
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            DataValidation();
            Properties.Settings.Default.UserName = txtUserName.Text;
            Properties.Settings.Default.LabName = txtLabName.Text;
            Properties.Settings.Default.TestName = txtTestName.Text;
            Properties.Settings.Default.HostIp = txtHostIp.Text;
            Properties.Settings.Default.ProhibitDemonstration = checkBox1.Checked;
            Properties.Settings.Default.Save();
            ChangeSettingsEvent();
            ChangeUserDataEvent(txtUserName.Text, txtLabName.Text, txtTestName.Text);
            MessageBox.Show("Настройки сохранены");
        }
        private void btnDeleteSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.UserName = String.Empty;
            txtUserName.Text = String.Empty;
            Properties.Settings.Default.LabName = String.Empty;
            txtLabName.Text = String.Empty;
            Properties.Settings.Default.TestName = String.Empty;
            txtTestName.Text = String.Empty;
            Properties.Settings.Default.HostIp = String.Empty;
            txtHostIp.Text = String.Empty;
            Properties.Settings.Default.HostPort = 0;
            txtHostPort.Text = String.Empty;
            Properties.Settings.Default.ProhibitDemonstration = false;
            checkBox1.Checked = false;
        }
        private void DataValidation()
        {
            if (String.IsNullOrEmpty(txtHostIp.Text) || String.IsNullOrEmpty(txtHostPort.Text))
            {
                MessageBox.Show("Введите порт и IP адрес сервера.");
                return;
            }
            if (IPAddress.TryParse(txtHostIp.Text, out var ip) && Regex.IsMatch(txtHostIp.Text, @"^([0-9]{1,3}[\.]){3}[0-9]{1,3}$"))
            {
                Properties.Settings.Default.HostIp = txtHostIp.Text;
            }
            else
            {
                txtHostIp.Text = String.Empty;
                MessageBox.Show("Некорректный IP адрес.");
            }
            if (int.TryParse(txtHostPort.Text, out var port))
            {
                Properties.Settings.Default.HostPort = port;
            }
            else
            {
                txtHostPort.Text = String.Empty;
                MessageBox.Show("Некорректный порт.");
            }
        }
        private void InitSettingsAndData()
        {
            txtUserName.Text = user.UserName;
            txtLabName.Text = user.LabName;
            txtTestName.Text = user.TestName;
            txtHostIp.Text = settings.HostIp;
            txtHostPort.Text = settings.HostPort.ToString();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_client != null && _client.TcpClient != null)
            {
                try
                {
                    _client.Disconnect(true);
                    _client.Dispose();
                }
                catch { }
            }
            this.Dispose();
            this.Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ProhibitDemonstration = checkBox1.Checked;
            _client.AuthData.ProhibitDemonstration = checkBox1.Checked;

            if (_client.Connected)
            {
                _client.MessageBroker.SendMessage(new ProhibitControlCommand(checkBox1.Checked));
            }
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text.StartsWith("Отключиться"))
            {
                _client.RdpSession.Pause();
                btnPause.Enabled = false;
                btnResume.Enabled = true;
            }
        }
        private void btnResume_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text.StartsWith("Отключиться"))
            {
                _client.RdpSession.Resume();
                btnResume.Enabled = false;
                btnPause.Enabled = true;
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FillCheckBoxList();
            UpdateGUI.UpdateControl(btnConnect, "Enabled", true);
        }
        private void FillCheckBoxList()
        {
            checkedListBox1.Items.Clear();
            Process[] processes = Process.GetProcesses();
            checkedListBox1.Items.Add("Показывать все", false);
            foreach (Process pro in processes)
            {
                if (pro.MainWindowTitle != string.Empty)
                {
                    checkedListBox1.Items.Add(pro.ProcessName, false);
                }
            }
            var n = checkedListBox1.Items.Count;
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text.StartsWith("Отключиться") && _client.Connected)
            {
                int size = -1;
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string file = openFileDialog1.FileName;
                    try
                    {
                        var cmd = _client.MessageBroker.PackFile(file);
                        _client.MessageBroker.SendMessage(cmd);
                        txtChat.Text += $"Файл {file} отправлен на сервер.\n";
                    }
                    catch (IOException)
                    {
                    }
                }
                else
                {
                    MessageBox.Show("Отсутствет подключение к серверу");
                }
            }
        }
        private bool ItemChosen()
        {
            return checkedListBox1.CheckedItems.Count == 1 ? true : false;
        }
        private void btnMsgSend_Click(object sender, EventArgs e)
        {
            var msg = txtChatMsg.Text;
            if (!string.IsNullOrEmpty(txtChatMsg.Text))
            {
                _client.MessageBroker.SendMessage(new SendChatMessage(msg));

                this.Invoke(new Action(() =>
                {
                    txtChat.Text += $"{DateTime.Now.ToString()} Me \n: {msg} \n\n";
                    txtChatMsg.Text = string.Empty;
                }));
            }
        }
        private void SaveFile(FileExchangeCommand cmd)
        {
            string fileName = (string)cmd.GetData()[0];
            int len = (int)cmd.GetData()[1];
            byte[] content = (byte[])cmd.GetData()[2];
            char[] splitter = new char[] { '\\' };
            var dirs = fileName.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            var dirsLen = dirs.Count();
            string simpleFileName = dirs[dirsLen - 1];
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = $"Сохранить файл от сервера.";
            saveFileDialog1.FileName = simpleFileName;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;
            this.Invoke(new Action(() =>
            {
                var newFileName = string.Empty;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    newFileName = Path.GetFullPath(saveFileDialog1.FileName);
                    using (FileStream fs = new FileStream(newFileName, FileMode.Create))
                    {
                        fs.Write(content, 0, len);
                    }
                }
                txtChat.Text += $"{DateTime.Now.ToString()} Диспетчер прислал новый файл.\n Сохранен в {newFileName}\n\n";
            }));
        }
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var checkedItems = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (i != e.Index) checkedListBox1.SetItemChecked(i, false);
                if (i == e.Index)
                {
                    if (e.NewValue == CheckState.Checked)
                        checkedItems++;
                }
            }
            chosenApp = checkedListBox1.Items[e.Index].ToString();
            if (chosenApp != "Показывать все")
            {
                desktopOption = false;
            }
            else
            {
                desktopOption = true;
            }
            if (checkedItems > 0 )
            {
                if (_client.Connected)
                {
                    if (desktopOption)
                    {
                        _client.RdpSession.ApplicationFilter.Enabled = false;
                    }
                    else
                    {
                        _client.RdpSession.ApplicationFilter.Enabled = true;
                        _client.SetAppForSharing(chosenApp);
                    }
                }
            }
        }   
    }
}
