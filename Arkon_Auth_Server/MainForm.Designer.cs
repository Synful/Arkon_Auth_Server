namespace Server {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.serverBTN = new System.Windows.Forms.ToolStripButton();
            this.ConsoleData = new System.Windows.Forms.RichTextBox();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.clientsTab = new System.Windows.Forms.TabPage();
            this.clientsData = new System.Windows.Forms.DataGridView();
            this.u_username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.u_lic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.u_ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.u_port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConsolePage = new System.Windows.Forms.TabPage();
            this.VarsPage = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MaxClients = new System.Windows.Forms.TextBox();
            this.CurrentVer = new System.Windows.Forms.TextBox();
            this.listener_bw = new System.ComponentModel.BackgroundWorker();
            this.clientOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.send_test_btn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.clientsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientsData)).BeginInit();
            this.ConsolePage.SuspendLayout();
            this.VarsPage.SuspendLayout();
            this.clientOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverBTN});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(691, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // serverBTN
            // 
            this.serverBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.serverBTN.Image = ((System.Drawing.Image)(resources.GetObject("serverBTN.Image")));
            this.serverBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.serverBTN.Name = "serverBTN";
            this.serverBTN.Size = new System.Drawing.Size(70, 22);
            this.serverBTN.Text = "Start Server";
            this.serverBTN.Click += new System.EventHandler(this.serverBTN_Click);
            // 
            // ConsoleData
            // 
            this.ConsoleData.BackColor = System.Drawing.SystemColors.Control;
            this.ConsoleData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConsoleData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleData.Location = new System.Drawing.Point(3, 3);
            this.ConsoleData.Name = "ConsoleData";
            this.ConsoleData.ReadOnly = true;
            this.ConsoleData.Size = new System.Drawing.Size(677, 348);
            this.ConsoleData.TabIndex = 8;
            this.ConsoleData.Text = "";
            this.ConsoleData.TextChanged += new System.EventHandler(this.Console_TextChanged);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.clientsTab);
            this.TabControl.Controls.Add(this.ConsolePage);
            this.TabControl.Controls.Add(this.VarsPage);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 25);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(691, 380);
            this.TabControl.TabIndex = 9;
            // 
            // clientsTab
            // 
            this.clientsTab.Controls.Add(this.clientsData);
            this.clientsTab.Location = new System.Drawing.Point(4, 22);
            this.clientsTab.Name = "clientsTab";
            this.clientsTab.Padding = new System.Windows.Forms.Padding(3);
            this.clientsTab.Size = new System.Drawing.Size(683, 354);
            this.clientsTab.TabIndex = 2;
            this.clientsTab.Text = "Clients";
            this.clientsTab.UseVisualStyleBackColor = true;
            // 
            // clientsData
            // 
            this.clientsData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.clientsData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientsData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.u_username,
            this.u_lic,
            this.u_ip,
            this.u_port});
            this.clientsData.ContextMenuStrip = this.clientOptions;
            this.clientsData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientsData.Location = new System.Drawing.Point(3, 3);
            this.clientsData.MultiSelect = false;
            this.clientsData.Name = "clientsData";
            this.clientsData.ReadOnly = true;
            this.clientsData.RowHeadersVisible = false;
            this.clientsData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.clientsData.Size = new System.Drawing.Size(677, 348);
            this.clientsData.TabIndex = 1;
            this.clientsData.SelectionChanged += new System.EventHandler(this.clientsData_SelectionChanged);
            // 
            // u_username
            // 
            this.u_username.DataPropertyName = "Username";
            this.u_username.HeaderText = "Username";
            this.u_username.Name = "u_username";
            this.u_username.ReadOnly = true;
            // 
            // u_lic
            // 
            this.u_lic.DataPropertyName = "License";
            this.u_lic.HeaderText = "License";
            this.u_lic.Name = "u_lic";
            this.u_lic.ReadOnly = true;
            // 
            // u_ip
            // 
            this.u_ip.DataPropertyName = "IP";
            this.u_ip.HeaderText = "IP";
            this.u_ip.Name = "u_ip";
            this.u_ip.ReadOnly = true;
            // 
            // u_port
            // 
            this.u_port.DataPropertyName = "Port";
            this.u_port.HeaderText = "Port";
            this.u_port.Name = "u_port";
            this.u_port.ReadOnly = true;
            // 
            // ConsolePage
            // 
            this.ConsolePage.Controls.Add(this.ConsoleData);
            this.ConsolePage.Location = new System.Drawing.Point(4, 22);
            this.ConsolePage.Name = "ConsolePage";
            this.ConsolePage.Padding = new System.Windows.Forms.Padding(3);
            this.ConsolePage.Size = new System.Drawing.Size(683, 354);
            this.ConsolePage.TabIndex = 0;
            this.ConsolePage.Text = "Console";
            this.ConsolePage.UseVisualStyleBackColor = true;
            // 
            // VarsPage
            // 
            this.VarsPage.Controls.Add(this.label2);
            this.VarsPage.Controls.Add(this.label1);
            this.VarsPage.Controls.Add(this.MaxClients);
            this.VarsPage.Controls.Add(this.CurrentVer);
            this.VarsPage.Location = new System.Drawing.Point(4, 22);
            this.VarsPage.Name = "VarsPage";
            this.VarsPage.Padding = new System.Windows.Forms.Padding(3);
            this.VarsPage.Size = new System.Drawing.Size(683, 354);
            this.VarsPage.TabIndex = 1;
            this.VarsPage.Text = "Vars";
            this.VarsPage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Max Client Amount:\r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Current Version:";
            // 
            // MaxClients
            // 
            this.MaxClients.Location = new System.Drawing.Point(6, 58);
            this.MaxClients.Name = "MaxClients";
            this.MaxClients.Size = new System.Drawing.Size(100, 20);
            this.MaxClients.TabIndex = 1;
            // 
            // CurrentVer
            // 
            this.CurrentVer.Location = new System.Drawing.Point(6, 19);
            this.CurrentVer.Name = "CurrentVer";
            this.CurrentVer.Size = new System.Drawing.Size(100, 20);
            this.CurrentVer.TabIndex = 0;
            // 
            // listener_bw
            // 
            this.listener_bw.WorkerSupportsCancellation = true;
            this.listener_bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.listener_bw_DoWork);
            // 
            // clientOptions
            // 
            this.clientOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.send_test_btn});
            this.clientOptions.Name = "clientOptions";
            this.clientOptions.ShowImageMargin = false;
            this.clientOptions.Size = new System.Drawing.Size(99, 26);
            // 
            // send_test_btn
            // 
            this.send_test_btn.Name = "send_test_btn";
            this.send_test_btn.Size = new System.Drawing.Size(155, 22);
            this.send_test_btn.Text = "Send Test";
            this.send_test_btn.Click += new System.EventHandler(this.send_test_btn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 405);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server | Idle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.clientsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clientsData)).EndInit();
            this.ConsolePage.ResumeLayout(false);
            this.VarsPage.ResumeLayout(false);
            this.VarsPage.PerformLayout();
            this.clientOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton serverBTN;
        private System.Windows.Forms.RichTextBox ConsoleData;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage ConsolePage;
        private System.Windows.Forms.TabPage VarsPage;
        private System.Windows.Forms.TextBox CurrentVer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MaxClients;
        private System.Windows.Forms.TabPage clientsTab;
        private System.Windows.Forms.DataGridView clientsData;
        private System.ComponentModel.BackgroundWorker listener_bw;
        private System.Windows.Forms.ContextMenuStrip clientOptions;
        private System.Windows.Forms.DataGridViewTextBoxColumn u_username;
        private System.Windows.Forms.DataGridViewTextBoxColumn u_lic;
        private System.Windows.Forms.DataGridViewTextBoxColumn u_ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn u_port;
        private System.Windows.Forms.ToolStripMenuItem send_test_btn;
    }
}

