using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameCaro
{
    public class ChessBoardManager
    {
        #region Properties
        public enum ENDGAME
        {
            Draw,
            PlayerX,
            PlayerO,
        }

        private Panel chessBoard;
        private Form1 form;
        private int currentPlayer;
        private List<List<Button>> matrix;
        private ENDGAME isEndgame;
        private int maxGame;
        private Stack<Point> stkUndo;
        private Stack<Point> stkRedo;
        private int gameMode;
        private bool ready;
        private bool symbolPlayerIsX;
        private Computer computer;

        public Panel ChessBoard
        {
            get
            {
                return chessBoard;
            }

            set
            {
                chessBoard = value;
            }
        }

        public int CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }

            set
            {
                currentPlayer = value;
            }
        }

        public List<List<Button>> Matrix
        {
            get
            {
                return matrix;
            }

            set
            {
                matrix = value;
            }
        }

        public ENDGAME IsEndGame
        {
            get
            {
                return isEndgame;
            }

            set
            {
                this.isEndgame = value;
            }
        }

        public int MaxGame
        {
            get
            {
                return maxGame;
            }

            set
            {
                maxGame = value;
            }
        }

        public Stack<Point> StkUndo
        {
            get
            {
                return stkUndo;
            }

            set
            {
                stkUndo = value;
            }
        }

        public Stack<Point> StkRedo
        {
            get
            {
                return stkRedo;
            }

            set
            {
                stkRedo = value;
            }
        }

        public int GameMode
        {
            get
            {
                return gameMode;
            }

            set
            {
                gameMode = value;
            }
        }

        public Form1 Form
        {
            get
            {
                return form;
            }

            set
            {
                form = value;
            }
        }

        public bool Ready
        {
            get
            {
                return ready;
            }

            set
            {
                ready = value;
            }
        }

        public bool SymbolPlayerIsX
        {
            get
            {
                return symbolPlayerIsX;
            }

            set
            {
                symbolPlayerIsX = value;
            }
        }

        public Computer Computer
        {
            get
            {
                return computer;
            }

            set
            {
                computer = value;
            }
        }

        #endregion

        #region Initialize
        public ChessBoardManager(Panel chessBoard, Form1 form)
        {
            this.ChessBoard = chessBoard;
            this.CurrentPlayer = 1;
            this.Form = form;
            this.Computer = new Computer(this);
        }
        #endregion

        #region Methods
        public void DrawChessBoard()
        {
            Ready = true;
            this.StkUndo = new Stack<Point>();
            this.stkRedo = new Stack<Point>();
            ChessBoard.Controls.Clear();
            CurrentPlayer = 2;
            ChangePlayer();
            Matrix = new List<List<Button>>();
            Button oldButton = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int i = 0; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < Cons.CHESS_BOARD_WIDTH; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.CHESS_WIDTH,
                        Height = Cons.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        Font = new Font(oldButton.Font.FontFamily, 11, FontStyle.Bold),
                        Tag = i.ToString()
                    };

                    btn.Click += btn_Click;

                    this.ChessBoard.Controls.Add(btn);
                    Matrix[i].Add(btn);

                    oldButton.Location = btn.Location;
                    oldButton.Width = btn.Width;
                    oldButton.Height = btn.Height;
                }
                oldButton.Location = new Point(0, oldButton.Location.Y + Cons.CHESS_HEIGHT);
                oldButton.Width = 0;
                oldButton.Height = 0;
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (!Ready)
                return;
            Button btn = sender as Button;
            if (btn.Text != "")
                return;
            DrawChess(GetChessPoint(btn), CurrentPlayer);
            this.StkUndo.Push(GetChessPoint(btn));
            this.StkRedo = new Stack<Point>();

            if(isEndGame(btn))
            {
                IsEndGame = CurrentPlayer == 1 ? ENDGAME.PlayerX : ENDGAME.PlayerO;
                Form.EndGame(Convert.ToInt32(IsEndGame));
            }
            else
            {
                ChangePlayer();
                if (GameMode == 2)
                {
                    Ready = false;
                    new Thread(
                        () =>
                        {
                            ComputerMove(Computer.SearchPosition());
                        }
                        )
                    { IsBackground = true }.Start();
                    //ComputerMove(Computer.SearchPosition());
                }      
            }    
        }

        public void DrawChess(Point point, int value)
        {
            Button btn = matrix[point.Y][point.X];
            switch (value)
            {
                case 1:
                    {
                        if (SymbolPlayerIsX)
                        {
                            btn.Text = "X";
                            btn.ForeColor = Color.Red;
                        }
                        else
                        {
                            btn.Text = "O";
                            btn.ForeColor = Color.Blue;
                        }
                        break;
                    }
                case 2:
                    {
                        if (!SymbolPlayerIsX)
                        {
                            btn.Text = "X";
                            btn.ForeColor = Color.Red;
                        }
                        else
                        {
                            btn.Text = "O";
                            btn.ForeColor = Color.Blue;
                        }
                        break;
                    }
                default:
                    {
                        btn.Text = "";
                        break;
                    }
            }
        }
        
        private void ChangePlayer()
        {
            CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
            Form.resetTime(CurrentPlayer);
        }

        public bool checkChess(Point point, int value)
        {
            Button btn = matrix[point.Y][point.X];
            switch (value)
            {
                case 0:
                    {
                        if (btn.Text != "")
                            return false;
                        return true;
                    }
                case 1:
                    {
                        string symbolPlayer = (SymbolPlayerIsX) ? "X" : "O";
                        if (btn.Text.Contains(symbolPlayer))
                            return true;
                        return false;
                    }
                case 2:
                    {
                        string symbolComputer = (SymbolPlayerIsX) ? "O" : "X";
                        if (btn.Text.Contains(symbolComputer))
                            return true;
                        return false;
                    }
                default: return false;
            }
        }

        #region checkEndGame


        public bool isEndGame(Button btn)
        {
            return isEndHorizontal(btn) || isEndVertical(btn) || isEndPrimary(btn) || isEndSub(btn);
        }

        //kiểm tra hàng ngang
        private bool isEndHorizontal(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countLeft = 0;
            for (int i = point.X; i >= 0; i--) 
            {
                if (Matrix[point.Y][i].Text.Equals(btn.Text))
                    countLeft++;
                else break;
            }

            int countRight = 0;
            for (int i = point.X + 1; i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                if (Matrix[point.Y][i].Text.Equals(btn.Text))
                    countRight++;
                else break;
            }
            if (countLeft + countRight < MaxGame)
                return false;
            //kiểm tra biên trái
            if (point.X - countLeft <= 0)
                return true;
            //kiểm tra biên phải
            if (point.X + countRight == Cons.CHESS_BOARD_WIDTH - 1)
                return true;
            //kiểm tra chặn 2 đầu
            if (Matrix[point.Y][point.X - countLeft].Text.Equals("") || Matrix[point.Y][point.X + countRight + 1].Text.Equals("")) 
                return true;
            return false;
        }

        //kiểm tra hàng dọc
        private bool isEndVertical(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].Text.Equals(btn.Text))
                    countTop++;
                else break;
            }

            int countBottom = 0;
            for (int i = point.Y + 1; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[i][point.X].Text.Equals(btn.Text))
                    countBottom++;
                else break;
            }
            if (countTop + countBottom < MaxGame)
                return false;
            //kiểm tra biên trên
            if (point.Y - countTop <= 0)
                return true;
            //kiểm tra biên dưới
            if (point.Y + countBottom == Cons.CHESS_BOARD_HEIGHT - 1)
                return true;
            //kiểm tra chặn 2 đầu
            if (Matrix[point.Y - countTop][point.X].Text.Equals("") || Matrix[point.Y + countBottom + 1][point.X].Text.Equals(""))
                return true;
            return false;
        }

        //kiểm tra chéo chính
        private bool isEndPrimary(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.Y - i < 0 || point.X - i < 0)
                    break;
                if (Matrix[point.Y - i][point.X - i].Text.Equals(btn.Text))
                    countTop++;
                else break;
            }

            int countBottom = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_HEIGHT - point.X; i++)
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X + i >= Cons.CHESS_BOARD_WIDTH)
                    break;
                if (Matrix[point.Y + i][point.X + i].Text.Equals(btn.Text))
                    countBottom++;
                else break;
            }
            if (countTop + countBottom < MaxGame)
                return false;
            //kiểm tra biên trái
            if (point.X - countTop <= 0)
                return true;
            //kiểm tra biên phải
            if (point.X + countBottom == Cons.CHESS_BOARD_WIDTH - 1)
                return true;
            //kiểm tra biên trên
            if (point.Y - countTop <= 0)
                return true;
            //kiểm tra biên dưới
            if (point.Y + countBottom == Cons.CHESS_BOARD_HEIGHT - 1)
                return true;
            //kiểm tra chặn 2 đầu
            if (Matrix[point.Y - countTop][point.X - countTop].Text.Equals("") || Matrix[point.Y + countBottom + 1][point.X + countBottom + 1].Text.Equals(""))
                return true;
            return false;
        }

        //kiểm tra chéo phụ
        private bool isEndSub(Button btn)
        {
            Point point = GetChessPoint(btn);

            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.Y - i < 0 || point.X + i >= Cons.CHESS_BOARD_WIDTH)
                    break;
                if (Matrix[point.Y - i][point.X + i].Text.Equals(btn.Text))
                    countTop++;
                else break;
            }

            int countBottom = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_HEIGHT - point.X; i++)
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X - i < 0)
                    break;
                if (Matrix[point.Y + i][point.X - i].Text.Equals(btn.Text))
                    countBottom++;
                else break;
            }
            if (countTop + countBottom < MaxGame)
                return false;
            //kiểm tra biên trái
            if (point.X - countBottom <= 0)
                return true;
            //kiểm tra biên phải
            if (point.X + countTop == Cons.CHESS_BOARD_WIDTH - 1)
                return true;
            //kiểm tra biên trên
            if (point.Y - countTop <= 0)
                return true;
            //kiểm tra biên dưới
            if (point.Y + countBottom == Cons.CHESS_BOARD_HEIGHT - 1)
                return true;
            //kiểm tra chặn 2 đầu
            if (Matrix[point.Y - countTop][point.X + countTop].Text.Equals("") || Matrix[point.Y + countBottom + 1][point.X - countBottom - 1].Text.Equals(""))
                return true;
            return false;
        }

        private Point GetChessPoint(Button btn)
        {
            int column = Convert.ToInt32(btn.Tag);
            int row = Matrix[column].IndexOf(btn);
            Point point = new Point(row, column);
            return point;
        }

        #endregion

        #region undo_redo

        public void Undo()
        {
            if (stkUndo.Count == 0)
                return;
            Point undoPoint = StkUndo.Pop();
            StkRedo.Push(undoPoint);
            Button btn = Matrix[undoPoint.Y][undoPoint.X];
            btn.Text = "";
            if (Ready)
                ChangePlayer();
            else
                Form.resetTime(CurrentPlayer);
        }

        public void Redo()
        {
            if (StkRedo.Count == 0)
                return;

            Point undoPoint = StkRedo.Pop();
            StkUndo.Push(undoPoint);
            Button btn = Matrix[undoPoint.Y][undoPoint.X];
            DrawChess(GetChessPoint(btn), CurrentPlayer);
            if (isEndGame(btn))
            {
                IsEndGame = CurrentPlayer == 1 ? ENDGAME.PlayerX : ENDGAME.PlayerO;
                Form.EndGame(Convert.ToInt32(IsEndGame));
            }
            else
                ChangePlayer();

        }

        #endregion

        #region ComputerMove
        private void ComputerMove(Point point)
        {
            if (Matrix[point.Y][point.X].Text != "")
                return;
            DrawChess(point, CurrentPlayer);
            this.StkUndo.Push(point);
            this.StkRedo = new Stack<Point>();

            if (isEndGame(Matrix[point.Y][point.X]))
            {
                IsEndGame = CurrentPlayer == 1 ? ENDGAME.PlayerX : ENDGAME.PlayerO;
                Form.EndGame(Convert.ToInt32(IsEndGame));
            }
            else
            {
                Ready = true;
                ChangePlayer();
            }   
        }
        #endregion

        #endregion

    }
}
