namespace Ogame12
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.conn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.loginLink = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.uni = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.generateLink = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.metal = new System.Windows.Forms.TextBox();
            this.kristal = new System.Windows.Forms.TextBox();
            this.deut = new System.Windows.Forms.TextBox();
            this.state = new System.Windows.Forms.Label();
            this.stop = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.msg = new System.Windows.Forms.Label();
            this.atck = new System.Windows.Forms.Label();
            this.energj = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.metalH = new System.Windows.Forms.TextBox();
            this.KrisH = new System.Windows.Forms.TextBox();
            this.DeutH = new System.Windows.Forms.TextBox();
            this.err = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.Toggle = new System.Windows.Forms.Label();
            this.mesajCheckBox = new System.Windows.Forms.CheckBox();
            this.saldiriCheckBox = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // conn
            // 
            this.conn.Location = new System.Drawing.Point(375, 157);
            this.conn.Name = "conn";
            this.conn.Size = new System.Drawing.Size(92, 45);
            this.conn.TabIndex = 0;
            this.conn.Text = "Bağlan";
            this.conn.UseVisualStyleBackColor = true;
            this.conn.Click += new System.EventHandler(this.conn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Link";
            // 
            // loginLink
            // 
            this.loginLink.Location = new System.Drawing.Point(16, 30);
            this.loginLink.Name = "loginLink";
            this.loginLink.Size = new System.Drawing.Size(482, 20);
            this.loginLink.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Id";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(16, 127);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(100, 20);
            this.name.TabIndex = 6;
            // 
            // uni
            // 
            this.uni.Location = new System.Drawing.Point(16, 80);
            this.uni.Name = "uni";
            this.uni.Size = new System.Drawing.Size(100, 20);
            this.uni.TabIndex = 4;
            this.uni.Text = "uni113.tr.ogame.org";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Server";
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(16, 168);
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.Size = new System.Drawing.Size(100, 20);
            this.pass.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Pass";
            // 
            // generateLink
            // 
            this.generateLink.Location = new System.Drawing.Point(134, 168);
            this.generateLink.Name = "generateLink";
            this.generateLink.Size = new System.Drawing.Size(78, 23);
            this.generateLink.TabIndex = 9;
            this.generateLink.Text = "Linki Oluştur";
            this.generateLink.UseVisualStyleBackColor = true;
            this.generateLink.Click += new System.EventHandler(this.generateLink_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Metal";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(271, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Kristal";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(372, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Deut";
            // 
            // metal
            // 
            this.metal.Location = new System.Drawing.Point(134, 80);
            this.metal.Name = "metal";
            this.metal.ReadOnly = true;
            this.metal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.metal.Size = new System.Drawing.Size(100, 20);
            this.metal.TabIndex = 13;
            this.metal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // kristal
            // 
            this.kristal.Location = new System.Drawing.Point(240, 80);
            this.kristal.Name = "kristal";
            this.kristal.ReadOnly = true;
            this.kristal.Size = new System.Drawing.Size(100, 20);
            this.kristal.TabIndex = 14;
            this.kristal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // deut
            // 
            this.deut.Location = new System.Drawing.Point(346, 80);
            this.deut.Name = "deut";
            this.deut.ReadOnly = true;
            this.deut.Size = new System.Drawing.Size(100, 20);
            this.deut.TabIndex = 15;
            this.deut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // state
            // 
            this.state.AutoSize = true;
            this.state.Location = new System.Drawing.Point(473, 157);
            this.state.Name = "state";
            this.state.Size = new System.Drawing.Size(57, 13);
            this.state.TabIndex = 17;
            this.state.Text = "Bağlı Değil";
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(473, 176);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(75, 24);
            this.stop.TabIndex = 18;
            this.stop.Text = "Durdur";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(481, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "Sayfayı Aç";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // msg
            // 
            this.msg.AutoSize = true;
            this.msg.Location = new System.Drawing.Point(246, 157);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(81, 13);
            this.msg.TabIndex = 21;
            this.msg.Text = "Yeni Mesaj Yok";
            // 
            // atck
            // 
            this.atck.AutoSize = true;
            this.atck.Location = new System.Drawing.Point(246, 177);
            this.atck.Name = "atck";
            this.atck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.atck.Size = new System.Drawing.Size(57, 13);
            this.atck.TabIndex = 22;
            this.atck.Text = "Saldırı Yok";
            // 
            // energj
            // 
            this.energj.Location = new System.Drawing.Point(452, 80);
            this.energj.Name = "energj";
            this.energj.ReadOnly = true;
            this.energj.Size = new System.Drawing.Size(100, 20);
            this.energj.TabIndex = 24;
            this.energj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(478, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Enerji";
            // 
            // metalH
            // 
            this.metalH.Location = new System.Drawing.Point(134, 103);
            this.metalH.Name = "metalH";
            this.metalH.Size = new System.Drawing.Size(100, 20);
            this.metalH.TabIndex = 25;
            // 
            // KrisH
            // 
            this.KrisH.Location = new System.Drawing.Point(240, 103);
            this.KrisH.Name = "KrisH";
            this.KrisH.Size = new System.Drawing.Size(100, 20);
            this.KrisH.TabIndex = 26;
            // 
            // DeutH
            // 
            this.DeutH.Location = new System.Drawing.Point(346, 103);
            this.DeutH.Name = "DeutH";
            this.DeutH.Size = new System.Drawing.Size(100, 20);
            this.DeutH.TabIndex = 27;
            // 
            // err
            // 
            this.err.AutoSize = true;
            this.err.Location = new System.Drawing.Point(261, 199);
            this.err.Name = "err";
            this.err.Size = new System.Drawing.Size(0, 13);
            this.err.TabIndex = 29;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(453, 103);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 30;
            this.button2.Text = "Hedef Aç Kapa";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Toggle
            // 
            this.Toggle.AutoSize = true;
            this.Toggle.Location = new System.Drawing.Point(513, 129);
            this.Toggle.Name = "Toggle";
            this.Toggle.Size = new System.Drawing.Size(36, 13);
            this.Toggle.TabIndex = 31;
            this.Toggle.Text = "Kapalı";
            // 
            // mesajCheckBox
            // 
            this.mesajCheckBox.AutoSize = true;
            this.mesajCheckBox.Checked = true;
            this.mesajCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mesajCheckBox.Location = new System.Drawing.Point(225, 157);
            this.mesajCheckBox.Name = "mesajCheckBox";
            this.mesajCheckBox.Size = new System.Drawing.Size(15, 14);
            this.mesajCheckBox.TabIndex = 32;
            this.mesajCheckBox.UseVisualStyleBackColor = true;
            // 
            // saldiriCheckBox
            // 
            this.saldiriCheckBox.AutoSize = true;
            this.saldiriCheckBox.Checked = true;
            this.saldiriCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saldiriCheckBox.Location = new System.Drawing.Point(225, 177);
            this.saldiriCheckBox.Name = "saldiriCheckBox";
            this.saldiriCheckBox.Size = new System.Drawing.Size(15, 14);
            this.saldiriCheckBox.TabIndex = 33;
            this.saldiriCheckBox.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(284, 194);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 34;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(564, 222);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.saldiriCheckBox);
            this.Controls.Add(this.mesajCheckBox);
            this.Controls.Add(this.Toggle);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.err);
            this.Controls.Add(this.DeutH);
            this.Controls.Add(this.KrisH);
            this.Controls.Add(this.metalH);
            this.Controls.Add(this.energj);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.atck);
            this.Controls.Add(this.msg);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.state);
            this.Controls.Add(this.deut);
            this.Controls.Add(this.kristal);
            this.Controls.Add(this.metal);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.generateLink);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.uni);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loginLink);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.conn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(580, 260);
            this.MinimumSize = new System.Drawing.Size(580, 260);
            this.Name = "Form1";
            this.Text = "Ogame Helper by Mert";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button conn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox loginLink;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox uni;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button generateLink;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox metal;
        private System.Windows.Forms.TextBox kristal;
        private System.Windows.Forms.TextBox deut;
        private System.Windows.Forms.Label state;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label msg;
        private System.Windows.Forms.Label atck;
        private System.Windows.Forms.TextBox energj;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox metalH;
        private System.Windows.Forms.TextBox KrisH;
        private System.Windows.Forms.TextBox DeutH;
        private System.Windows.Forms.Label err;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label Toggle;
        private System.Windows.Forms.CheckBox mesajCheckBox;
        private System.Windows.Forms.CheckBox saldiriCheckBox;
        private System.Windows.Forms.Button button3;
    }
}

