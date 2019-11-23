namespace gameCaro
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
            this.pnlChessBroard = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pgbO = new System.Windows.Forms.ProgressBar();
            this.pgbX = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnPC = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnPP = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.tmX = new System.Windows.Forms.Timer(this.components);
            this.tmO = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlChessBroard
            // 
            this.pnlChessBroard.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlChessBroard.Enabled = false;
            this.pnlChessBroard.Location = new System.Drawing.Point(290, 52);
            this.pnlChessBroard.Name = "pnlChessBroard";
            this.pnlChessBroard.Size = new System.Drawing.Size(660, 660);
            this.pnlChessBroard.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Aqua;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.optionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(962, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playerToolStripMenuItem,
            this.playerToolStripMenuItem1,
            this.newToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // playerToolStripMenuItem
            // 
            this.playerToolStripMenuItem.Name = "playerToolStripMenuItem";
            this.playerToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.playerToolStripMenuItem.Text = "2 Player";
            this.playerToolStripMenuItem.Click += new System.EventHandler(this.btnPP_Click);
            // 
            // playerToolStripMenuItem1
            // 
            this.playerToolStripMenuItem1.Name = "playerToolStripMenuItem1";
            this.playerToolStripMenuItem1.Size = new System.Drawing.Size(136, 26);
            this.playerToolStripMenuItem1.Text = "1 Player";
            this.playerToolStripMenuItem1.Click += new System.EventHandler(this.btnPC_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(136, 26);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.levelToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.optionToolStripMenuItem.Text = "Edit";
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.level1ToolStripMenuItem,
            this.level2ToolStripMenuItem,
            this.level3ToolStripMenuItem});
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.levelToolStripMenuItem.Text = "Level";
            // 
            // level1ToolStripMenuItem
            // 
            this.level1ToolStripMenuItem.Name = "level1ToolStripMenuItem";
            this.level1ToolStripMenuItem.Size = new System.Drawing.Size(130, 26);
            this.level1ToolStripMenuItem.Text = "Level 1";
            this.level1ToolStripMenuItem.Click += new System.EventHandler(this.level1ToolStripMenuItem_Click);
            // 
            // level2ToolStripMenuItem
            // 
            this.level2ToolStripMenuItem.Name = "level2ToolStripMenuItem";
            this.level2ToolStripMenuItem.Size = new System.Drawing.Size(130, 26);
            this.level2ToolStripMenuItem.Text = "Level 2";
            this.level2ToolStripMenuItem.Click += new System.EventHandler(this.level2ToolStripMenuItem_Click);
            // 
            // level3ToolStripMenuItem
            // 
            this.level3ToolStripMenuItem.Name = "level3ToolStripMenuItem";
            this.level3ToolStripMenuItem.Size = new System.Drawing.Size(130, 26);
            this.level3ToolStripMenuItem.Text = "Level 3";
            this.level3ToolStripMenuItem.Click += new System.EventHandler(this.level3ToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pgbO);
            this.groupBox1.Controls.Add(this.pgbX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 266);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 122);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TIME";
            // 
            // pgbO
            // 
            this.pgbO.Location = new System.Drawing.Point(111, 82);
            this.pgbO.Name = "pgbO";
            this.pgbO.Size = new System.Drawing.Size(122, 23);
            this.pgbO.TabIndex = 6;
            // 
            // pgbX
            // 
            this.pgbX.Location = new System.Drawing.Point(111, 33);
            this.pgbX.Name = "pgbX";
            this.pgbX.Size = new System.Drawing.Size(122, 23);
            this.pgbX.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Player O";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Player X";
            // 
            // btnNew
            // 
            this.btnNew.Enabled = false;
            this.btnNew.Location = new System.Drawing.Point(151, 656);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(109, 43);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnPC
            // 
            this.btnPC.Location = new System.Drawing.Point(22, 580);
            this.btnPC.Margin = new System.Windows.Forms.Padding(4);
            this.btnPC.Name = "btnPC";
            this.btnPC.Size = new System.Drawing.Size(239, 46);
            this.btnPC.TabIndex = 5;
            this.btnPC.Text = "1 Player";
            this.btnPC.UseVisualStyleBackColor = true;
            this.btnPC.Click += new System.EventHandler(this.btnPC_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(21, 656);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(109, 43);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "Quit";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnPP
            // 
            this.btnPP.Location = new System.Drawing.Point(22, 500);
            this.btnPP.Margin = new System.Windows.Forms.Padding(4);
            this.btnPP.Name = "btnPP";
            this.btnPP.Size = new System.Drawing.Size(239, 47);
            this.btnPP.TabIndex = 7;
            this.btnPP.Text = "2 Player";
            this.btnPP.UseVisualStyleBackColor = true;
            this.btnPP.Click += new System.EventHandler(this.btnPP_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Enabled = false;
            this.btnUndo.Location = new System.Drawing.Point(21, 416);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(4);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(109, 43);
            this.btnUndo.TabIndex = 9;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Enabled = false;
            this.btnRedo.Location = new System.Drawing.Point(151, 416);
            this.btnRedo.Margin = new System.Windows.Forms.Padding(4);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(109, 43);
            this.btnRedo.TabIndex = 10;
            this.btnRedo.Text = "Redo";
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // tmX
            // 
            this.tmX.Tick += new System.EventHandler(this.tmX_Tick);
            // 
            // tmO
            // 
            this.tmO.Tick += new System.EventHandler(this.tmO_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::gameCaro.Properties.Resources._1200px_Hcmute_svg___Copy;
            this.pictureBox1.Location = new System.Drawing.Point(12, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(255, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(962, 729);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnPC);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnPP);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnlChessBroard);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Game Caro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlChessBroard;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnPC;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnPP;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnRedo;
        private System.Windows.Forms.ProgressBar pgbO;
        private System.Windows.Forms.ProgressBar pgbX;
        private System.Windows.Forms.Timer tmX;
        private System.Windows.Forms.Timer tmO;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem level1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem level2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem level3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
    }
}

