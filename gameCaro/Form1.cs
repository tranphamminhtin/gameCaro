using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameCaro
{
    public partial class Form1 : Form
    {
        ChessBoardManager ChessBoard;

        public Form1()
        {
            InitializeComponent();

            ChessBoard = new ChessBoardManager(this.pnlChessBroard, this);
            ChessBoard.MaxGame = 5;
            ChessBoard.GameMode = 1;
            ChessBoard.Ready = false;
            ChessBoard.SymbolPlayerIsX = true;
            pgbO.Step = pgbX.Step = Cons.TM_STEP;
            pgbO.Maximum = pgbX.Maximum = Cons.TM_MAX;
            pgbO.Value = pgbX.Value = 0;
            tmO.Interval = tmX.Interval = Cons.TM_INTERVAL;
        }

        public void resetTime(int currentPlayer)
        {
            if (currentPlayer == 1)
            {
                pgbO.Value = pgbX.Value = 0;
                tmX.Start();
                tmO.Stop();
            }
            else
            {
                pgbO.Value = pgbX.Value = 0;
                tmO.Start();
                tmX.Stop();
            }
        }      

        public void EndGame(int endgame)
        {
            tmX.Stop();
            tmO.Stop();
            ChessBoard.Ready = false;
            //kiểm tra hết nước đi thì hòa
            //if (1)
            //{

            //}


            switch (endgame)
            {
                case 0:
                    MessageBox.Show("Hòa!");
                    break;
                case 1:
                    MessageBox.Show("Player X win!");
                    break;
                case 2:
                    MessageBox.Show("Player O win!");
                    break;
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if(ChessBoard.GameMode==1)
                ChessBoard.Undo();
            else
            {
                ChessBoard.Undo();
                ChessBoard.Undo();
            }
            ChessBoard.Ready = true;
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (ChessBoard.GameMode == 1)
                ChessBoard.Redo();
            else
            {
                ChessBoard.Redo();
                ChessBoard.Redo();
            }
        }

        private void tmX_Tick(object sender, EventArgs e)
        {
            pgbX.PerformStep();
            if (pgbX.Value >= pgbX.Maximum)
            {
                EndGame(2);      //player O win
            }
        }

        private void tmO_Tick(object sender, EventArgs e)
        {
            pgbO.PerformStep();
            if(pgbO.Value >= pgbO.Maximum)
            {
                EndGame(1);      //player X win      
            }             
        }

        private void btnPP_Click(object sender, EventArgs e)
        {
            ChessBoard.GameMode = 1;
            ChessBoard.Ready = true;
            btnUndo.Enabled = true;
            btnRedo.Enabled = true;
            btnNew.Enabled = true;
            ChessBoard.DrawChessBoard();
            pnlChessBroard.Enabled = true;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ChessBoard.DrawChessBoard();
            pnlChessBroard.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát khỏi game ? ? ?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
                e.Cancel = true;
        }

        private void btnPC_Click(object sender, EventArgs e)
        {
            ChessBoard.GameMode = 2;
            Computer.MAXDEPTH = 5;
            ChessBoard.Ready = true;
            btnUndo.Enabled = true;
            btnRedo.Enabled = true;
            btnNew.Enabled = true;
            ChessBoard.DrawChessBoard();
            pnlChessBroard.Enabled = true;
        }
    }
}
