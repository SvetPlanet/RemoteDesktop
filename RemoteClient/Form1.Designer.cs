namespace RemoteClient
{
    partial class FormClient
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.txtMyIp = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnResume = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDeleteSettings = new System.Windows.Forms.Button();
            this.txtTestName = new System.Windows.Forms.RichTextBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.txtHostPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHostIp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLabName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnMsgSend = new System.Windows.Forms.Button();
            this.txtChatMsg = new System.Windows.Forms.RichTextBox();
            this.txtChat = new System.Windows.Forms.RichTextBox();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(238, 190);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(217, 44);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Подключиться к серверу";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(474, 529);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage1.Controls.Add(this.checkedListBox1);
            this.tabPage1.Controls.Add(this.txtMyIp);
            this.tabPage1.Controls.Add(this.btnUpdate);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.btnResume);
            this.tabPage1.Controls.Add(this.btnPause);
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.txtConnectionString);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.btnConnect);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(466, 503);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Подключение";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(6, 29);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(449, 154);
            this.checkedListBox1.TabIndex = 20;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // txtMyIp
            // 
            this.txtMyIp.Location = new System.Drawing.Point(3, 477);
            this.txtMyIp.Name = "txtMyIp";
            this.txtMyIp.ReadOnly = true;
            this.txtMyIp.Size = new System.Drawing.Size(460, 20);
            this.txtMyIp.TabIndex = 4;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(6, 192);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(215, 41);
            this.btnUpdate.TabIndex = 19;
            this.btnUpdate.Text = "Обновить список запущенных приложений";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(218, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Выюрать приложение для демонстрации:";
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(235, 407);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(220, 41);
            this.btnResume.TabIndex = 16;
            this.btnResume.Text = "Возобновить трансляцию";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(9, 407);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(218, 41);
            this.btnPause.TabIndex = 15;
            this.btnPause.Text = "Приостановить трансляцию";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(238, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(141, 17);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Запретить управление";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 456);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(147, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "IP адрес моего устройства:";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString.Location = new System.Drawing.Point(6, 259);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.ReadOnly = true;
            this.txtConnectionString.Size = new System.Drawing.Size(449, 129);
            this.txtConnectionString.TabIndex = 3;
            this.txtConnectionString.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 243);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Строка подключения:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDeleteSettings);
            this.tabPage2.Controls.Add(this.txtTestName);
            this.tabPage2.Controls.Add(this.btnSaveSettings);
            this.tabPage2.Controls.Add(this.txtHostPort);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtHostIp);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtUserName);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.txtLabName);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(466, 503);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Настройки";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDeleteSettings
            // 
            this.btnDeleteSettings.Location = new System.Drawing.Point(278, 461);
            this.btnDeleteSettings.Name = "btnDeleteSettings";
            this.btnDeleteSettings.Size = new System.Drawing.Size(182, 36);
            this.btnDeleteSettings.TabIndex = 12;
            this.btnDeleteSettings.Text = "Сбросить";
            this.btnDeleteSettings.UseVisualStyleBackColor = true;
            this.btnDeleteSettings.Click += new System.EventHandler(this.btnDeleteSettings_Click);
            // 
            // txtTestName
            // 
            this.txtTestName.Location = new System.Drawing.Point(118, 85);
            this.txtTestName.Name = "txtTestName";
            this.txtTestName.Size = new System.Drawing.Size(339, 66);
            this.txtTestName.TabIndex = 11;
            this.txtTestName.Text = "";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(92, 461);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(177, 36);
            this.btnSaveSettings.TabIndex = 10;
            this.btnSaveSettings.Text = "Сохранить";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // txtHostPort
            // 
            this.txtHostPort.Location = new System.Drawing.Point(118, 243);
            this.txtHostPort.Name = "txtHostPort";
            this.txtHostPort.Size = new System.Drawing.Size(339, 20);
            this.txtHostPort.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Порт сервера";
            // 
            // txtHostIp
            // 
            this.txtHostIp.Location = new System.Drawing.Point(118, 205);
            this.txtHostIp.Name = "txtHostIp";
            this.txtHostIp.Size = new System.Drawing.Size(339, 20);
            this.txtHostIp.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "IP адрес сервера";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Испытание";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(118, 47);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(339, 20);
            this.txtUserName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Имя пользователя";
            // 
            // txtLabName
            // 
            this.txtLabName.Location = new System.Drawing.Point(118, 11);
            this.txtLabName.Name = "txtLabName";
            this.txtLabName.Size = new System.Drawing.Size(339, 20);
            this.txtLabName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Лаборатория";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.btnMsgSend);
            this.tabPage3.Controls.Add(this.txtChatMsg);
            this.tabPage3.Controls.Add(this.txtChat);
            this.tabPage3.Controls.Add(this.btnSendFile);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(466, 503);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Общение";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Чат:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 259);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Новое сообщение:";
            // 
            // btnMsgSend
            // 
            this.btnMsgSend.Location = new System.Drawing.Point(6, 455);
            this.btnMsgSend.Name = "btnMsgSend";
            this.btnMsgSend.Size = new System.Drawing.Size(218, 42);
            this.btnMsgSend.TabIndex = 3;
            this.btnMsgSend.Text = "Отправить сообщение";
            this.btnMsgSend.UseVisualStyleBackColor = true;
            this.btnMsgSend.Click += new System.EventHandler(this.btnMsgSend_Click);
            // 
            // txtChatMsg
            // 
            this.txtChatMsg.Location = new System.Drawing.Point(6, 278);
            this.txtChatMsg.Name = "txtChatMsg";
            this.txtChatMsg.Size = new System.Drawing.Size(454, 171);
            this.txtChatMsg.TabIndex = 2;
            this.txtChatMsg.Text = "";
            // 
            // txtChat
            // 
            this.txtChat.Location = new System.Drawing.Point(6, 25);
            this.txtChat.Name = "txtChat";
            this.txtChat.ReadOnly = true;
            this.txtChat.Size = new System.Drawing.Size(454, 229);
            this.txtChat.TabIndex = 1;
            this.txtChat.Text = "";
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(242, 455);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(218, 42);
            this.btnSendFile.TabIndex = 0;
            this.btnSendFile.Text = "Отправить файл";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(-1, 535);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(475, 20);
            this.txtStatus.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(473, 559);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtStatus);
            this.Name = "FormClient";
            this.Text = "Remote client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnDeleteSettings;
        private System.Windows.Forms.RichTextBox txtTestName;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.TextBox txtHostPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtHostIp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLabName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtConnectionString;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMyIp;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnMsgSend;
        private System.Windows.Forms.RichTextBox txtChatMsg;
        private System.Windows.Forms.RichTextBox txtChat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}

