namespace Translate
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gösterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sözlükToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çeviriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dİlSeçimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neydenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.almancaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ingilizceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.türkçeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neyeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.almancaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ingilizceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.türkçeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gizleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hakkındaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kapatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.richTextBox1.Location = new System.Drawing.Point(13, 26);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(302, 89);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.richTextBox2.Location = new System.Drawing.Point(12, 127);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBox2.Size = new System.Drawing.Size(381, 81);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(320, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 56);
            this.button1.TabIndex = 2;
            this.button1.Text = "Çeviri Yap";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gösterToolStripMenuItem,
            this.dİlSeçimiToolStripMenuItem,
            this.gizleToolStripMenuItem,
            this.hakkındaToolStripMenuItem,
            this.kapatToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 136);
            // 
            // gösterToolStripMenuItem
            // 
            this.gösterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sözlükToolStripMenuItem,
            this.çeviriToolStripMenuItem});
            this.gösterToolStripMenuItem.Name = "gösterToolStripMenuItem";
            this.gösterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gösterToolStripMenuItem.Text = "Göster";
            this.gösterToolStripMenuItem.Click += new System.EventHandler(this.gösterToolStripMenuItem_Click);
            // 
            // sözlükToolStripMenuItem
            // 
            this.sözlükToolStripMenuItem.Name = "sözlükToolStripMenuItem";
            this.sözlükToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.sözlükToolStripMenuItem.Text = "Sözlük";
            this.sözlükToolStripMenuItem.Click += new System.EventHandler(this.sözlükToolStripMenuItem_Click);
            // 
            // çeviriToolStripMenuItem
            // 
            this.çeviriToolStripMenuItem.Name = "çeviriToolStripMenuItem";
            this.çeviriToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.çeviriToolStripMenuItem.Text = "Çeviri";
            this.çeviriToolStripMenuItem.Click += new System.EventHandler(this.çeviriToolStripMenuItem_Click);
            // 
            // dİlSeçimiToolStripMenuItem
            // 
            this.dİlSeçimiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neydenToolStripMenuItem,
            this.neyeToolStripMenuItem});
            this.dİlSeçimiToolStripMenuItem.Name = "dİlSeçimiToolStripMenuItem";
            this.dİlSeçimiToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dİlSeçimiToolStripMenuItem.Text = "Dil Seçimi";
            // 
            // neydenToolStripMenuItem
            // 
            this.neydenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.almancaToolStripMenuItem,
            this.ingilizceToolStripMenuItem,
            this.türkçeToolStripMenuItem});
            this.neydenToolStripMenuItem.Name = "neydenToolStripMenuItem";
            this.neydenToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.neydenToolStripMenuItem.Text = "Neyden?";
            // 
            // almancaToolStripMenuItem
            // 
            this.almancaToolStripMenuItem.Name = "almancaToolStripMenuItem";
            this.almancaToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.almancaToolStripMenuItem.Text = "Almanca";
            this.almancaToolStripMenuItem.Click += new System.EventHandler(this.almancaToolStripMenuItem_Click);
            // 
            // ingilizceToolStripMenuItem
            // 
            this.ingilizceToolStripMenuItem.Name = "ingilizceToolStripMenuItem";
            this.ingilizceToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.ingilizceToolStripMenuItem.Text = "İngilizce";
            this.ingilizceToolStripMenuItem.Click += new System.EventHandler(this.ingilizceToolStripMenuItem_Click);
            // 
            // türkçeToolStripMenuItem
            // 
            this.türkçeToolStripMenuItem.Name = "türkçeToolStripMenuItem";
            this.türkçeToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.türkçeToolStripMenuItem.Text = "Türkçe";
            this.türkçeToolStripMenuItem.Click += new System.EventHandler(this.türkçeToolStripMenuItem_Click);
            // 
            // neyeToolStripMenuItem
            // 
            this.neyeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.almancaToolStripMenuItem1,
            this.ingilizceToolStripMenuItem1,
            this.türkçeToolStripMenuItem1});
            this.neyeToolStripMenuItem.Name = "neyeToolStripMenuItem";
            this.neyeToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.neyeToolStripMenuItem.Text = "Neye?";
            // 
            // almancaToolStripMenuItem1
            // 
            this.almancaToolStripMenuItem1.Name = "almancaToolStripMenuItem1";
            this.almancaToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
            this.almancaToolStripMenuItem1.Text = "Almanca";
            this.almancaToolStripMenuItem1.Click += new System.EventHandler(this.almancaToolStripMenuItem1_Click);
            // 
            // ingilizceToolStripMenuItem1
            // 
            this.ingilizceToolStripMenuItem1.Name = "ingilizceToolStripMenuItem1";
            this.ingilizceToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
            this.ingilizceToolStripMenuItem1.Text = "İngilizce";
            this.ingilizceToolStripMenuItem1.Click += new System.EventHandler(this.ingilizceToolStripMenuItem1_Click);
            // 
            // türkçeToolStripMenuItem1
            // 
            this.türkçeToolStripMenuItem1.Name = "türkçeToolStripMenuItem1";
            this.türkçeToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
            this.türkçeToolStripMenuItem1.Text = "Türkçe";
            this.türkçeToolStripMenuItem1.Click += new System.EventHandler(this.türkçeToolStripMenuItem1_Click);
            // 
            // gizleToolStripMenuItem
            // 
            this.gizleToolStripMenuItem.Name = "gizleToolStripMenuItem";
            this.gizleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gizleToolStripMenuItem.Text = "Gizle";
            this.gizleToolStripMenuItem.Click += new System.EventHandler(this.gizleToolStripMenuItem_Click);
            // 
            // hakkındaToolStripMenuItem
            // 
            this.hakkındaToolStripMenuItem.Name = "hakkındaToolStripMenuItem";
            this.hakkındaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hakkındaToolStripMenuItem.Text = "Hakkında";
            this.hakkındaToolStripMenuItem.Click += new System.EventHandler(this.hakkındaToolStripMenuItem_Click);
            // 
            // kapatToolStripMenuItem
            // 
            this.kapatToolStripMenuItem.Name = "kapatToolStripMenuItem";
            this.kapatToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.kapatToolStripMenuItem.Text = "Kapat";
            this.kapatToolStripMenuItem.Click += new System.EventHandler(this.kapatToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(321, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Gizle";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Assagidan Seciniz";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(317, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Assagidan Seciniz";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "- - - - - >";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 220);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Çeviri";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gösterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gizleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kapatToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem sözlükToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem çeviriToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem dİlSeçimiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neydenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem almancaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ingilizceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem türkçeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neyeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem almancaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ingilizceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem türkçeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hakkındaToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}

