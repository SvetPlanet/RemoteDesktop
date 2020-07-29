using AxRDPCOMAPILib;

namespace Host
{
    partial class ViewerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewerForm));
            this.btnStarter = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAll = new System.Windows.Forms.TabPage();
            this.lbl4 = new System.Windows.Forms.Label();
            this.rdpDisplay4 = new AxRDPCOMAPILib.AxRDPViewer();
            this.rdpDisplay3 = new AxRDPCOMAPILib.AxRDPViewer();
            this.rdpDisplay2 = new AxRDPCOMAPILib.AxRDPViewer();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.rdpDisplay1 = new AxRDPCOMAPILib.AxRDPViewer();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMyIp = new System.Windows.Forms.TextBox();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPageAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdpDisplay4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdpDisplay3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdpDisplay2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdpDisplay1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStarter
            // 
            this.btnStarter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStarter.Location = new System.Drawing.Point(930, 9);
            this.btnStarter.Name = "btnStarter";
            this.btnStarter.Size = new System.Drawing.Size(162, 29);
            this.btnStarter.TabIndex = 0;
            this.btnStarter.Text = "Запустить прослушивание";
            this.btnStarter.UseVisualStyleBackColor = true;
            this.btnStarter.Click += new System.EventHandler(this.btnStarter_Click);
            // 
            // txtPort
            // 
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPort.Location = new System.Drawing.Point(476, 15);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(448, 20);
            this.txtPort.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(331, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Прослушивать через порт";
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(2, 640);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(1100, 20);
            this.txtStatus.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageAll);
            this.tabControl1.Location = new System.Drawing.Point(2, 41);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1100, 600);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPageAll
            // 
            this.tabPageAll.Controls.Add(this.lbl1);
            this.tabPageAll.Controls.Add(this.lbl2);
            this.tabPageAll.Controls.Add(this.lbl3);
            this.tabPageAll.Controls.Add(this.lbl4);
            this.tabPageAll.Controls.Add(this.rdpDisplay4);
            this.tabPageAll.Controls.Add(this.rdpDisplay3);
            this.tabPageAll.Controls.Add(this.rdpDisplay2);
            this.tabPageAll.Controls.Add(this.checkedListBox1);
            this.tabPageAll.Controls.Add(this.rdpDisplay1);
            this.tabPageAll.Location = new System.Drawing.Point(4, 22);
            this.tabPageAll.Name = "tabPageAll";
            this.tabPageAll.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAll.Size = new System.Drawing.Size(1092, 574);
            this.tabPageAll.TabIndex = 0;
            this.tabPageAll.Text = "Мониторинг";
            this.tabPageAll.UseVisualStyleBackColor = true;
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Location = new System.Drawing.Point(0, 0);
            this.lbl4.MinimumSize = new System.Drawing.Size(421, 15);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(421, 15);
            this.lbl4.TabIndex = 11;
            // 
            // rdpDisplay4
            // 
            this.rdpDisplay4.Enabled = true;
            this.rdpDisplay4.Location = new System.Drawing.Point(0, 15);
            this.rdpDisplay4.MinimumSize = new System.Drawing.Size(423, 272);
            this.rdpDisplay4.Name = "rdpDisplay4";
            this.rdpDisplay4.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("rdpDisplay4.OcxState")));
            this.rdpDisplay4.Size = new System.Drawing.Size(423, 272);
            this.rdpDisplay4.TabIndex = 10;
            // 
            // rdpDisplay3
            // 
            this.rdpDisplay3.Enabled = true;
            this.rdpDisplay3.Location = new System.Drawing.Point(423, 15);
            this.rdpDisplay3.MinimumSize = new System.Drawing.Size(423, 272);
            this.rdpDisplay3.Name = "rdpDisplay3";
            this.rdpDisplay3.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("rdpDisplay3.OcxState")));
            this.rdpDisplay3.Size = new System.Drawing.Size(423, 272);
            this.rdpDisplay3.TabIndex = 9;
            // 
            // rdpDisplay2
            // 
            this.rdpDisplay2.Enabled = true;
            this.rdpDisplay2.Location = new System.Drawing.Point(0, 305);
            this.rdpDisplay2.MinimumSize = new System.Drawing.Size(423, 269);
            this.rdpDisplay2.Name = "rdpDisplay2";
            this.rdpDisplay2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("rdpDisplay2.OcxState")));
            this.rdpDisplay2.Size = new System.Drawing.Size(423, 269);
            this.rdpDisplay2.TabIndex = 8;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(846, 15);
            this.checkedListBox1.MinimumSize = new System.Drawing.Size(242, 560);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(242, 559);
            this.checkedListBox1.TabIndex = 7;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // rdpDisplay1
            // 
            this.rdpDisplay1.Enabled = true;
            this.rdpDisplay1.Location = new System.Drawing.Point(423, 305);
            this.rdpDisplay1.MinimumSize = new System.Drawing.Size(423, 269);
            this.rdpDisplay1.Name = "rdpDisplay1";
            this.rdpDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("rdpDisplay1.OcxState")));
            this.rdpDisplay1.Size = new System.Drawing.Size(423, 269);
            this.rdpDisplay1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Мой IP:";
            // 
            // txtMyIp
            // 
            this.txtMyIp.Location = new System.Drawing.Point(62, 14);
            this.txtMyIp.Name = "txtMyIp";
            this.txtMyIp.ReadOnly = true;
            this.txtMyIp.Size = new System.Drawing.Size(263, 20);
            this.txtMyIp.TabIndex = 7;
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(424, 0);
            this.lbl3.MinimumSize = new System.Drawing.Size(421, 15);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(421, 15);
            this.lbl3.TabIndex = 12;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(2, 290);
            this.lbl2.MinimumSize = new System.Drawing.Size(420, 15);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(420, 15);
            this.lbl2.TabIndex = 13;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(424, 287);
            this.lbl1.MinimumSize = new System.Drawing.Size(420, 15);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(420, 15);
            this.lbl1.TabIndex = 14;
            // 
            // ViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 662);
            this.Controls.Add(this.txtMyIp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.btnStarter);
            this.MinimumSize = new System.Drawing.Size(1115, 700);
            this.Name = "ViewerForm";
            this.Text = "Viewer From";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectionForm_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPageAll.ResumeLayout(false);
            this.tabPageAll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdpDisplay4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdpDisplay3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdpDisplay2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdpDisplay1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStarter;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMyIp;
        private System.Windows.Forms.TabPage tabPageAll;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private AxRDPViewer rdpDisplay1;
        private AxRDPViewer rdpDisplay4;
        private AxRDPViewer rdpDisplay3;
        private AxRDPViewer rdpDisplay2;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl1;
    }
}

