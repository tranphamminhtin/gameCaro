using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCaro
{
    public class EvalBoard
    {
        // class đánh giá điểm của bàn cờ
        public int[,] EBoard;
        public int evaluationBoard = 0;
        public EvalBoard()
        {
            EBoard = new int[Cons.CHESS_BOARD_HEIGHT, Cons.CHESS_BOARD_WIDTH];
            // ResetBoard();
        }

        public void resetBoard()
        {
            for (int r = 0; r < Cons.CHESS_BOARD_HEIGHT; r++)
                for (int c = 0; c < Cons.CHESS_BOARD_WIDTH; c++)
                    EBoard[r, c] = 0;
        }

        public void setPosition(int x, int y, int diem)
        {
            EBoard[x, y] = diem;
        }

        // hàm lấy ví trí có điểm max
        public Point MaxPos()
        {
            int Max = 0; // diem max 
            Point p = new Point();
            for (int i = 0; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Cons.CHESS_BOARD_WIDTH; j++)
                {
                    if (EBoard[i, j] > Max)
                    {
                        p.X = i;
                        p.Y = j;
                        Max = EBoard[i, j];
                    }
                }
            }
            if (Max == 0)
            {
                return new Point(-1, -1);
            }
            evaluationBoard = Max;
            return p;
        }


        public void pr(int[,] a)
        {
            for (int i = 0; i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                for (int j = 0; j < Cons.CHESS_BOARD_HEIGHT; j++)
                {
                    Console.WriteLine(a[i, j] + "\t");
                }
                Console.WriteLine("");
            }
        }
    }
}
