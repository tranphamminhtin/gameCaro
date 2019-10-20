using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCaro
{
    public class Computer
    {
        #region Properties
        EvalBoard eBoard; // bảng điểm của bàn cờ
        ChessBoardManager boardState; // bàn cờ hiện tại
        private EvalBoard move; // bảng điểm các nước đi đánh, dùng để random nước đi

        public static int MAXDEPTH; // độ sâu tối đa, dùng set level

        // các mảng điểm dùng để tính toán điểm số
        private int[] AScore1 = { 0, 4, 27, 256, 1458 }; // Mảng điểm tấn công
        private int[] DScore1 = { 0, 2, 9, 99, 769 };    // Mảng điểm phòng thủ

        public EvalBoard Move
        {
            get
            {
                return move;
            }

            set
            {
                move = value;
            }
        }

        //public int[] AScore = { 0, 9, 54, 169, 1458 };    // Mang diem tan cong
        //public int[] DScore = {0, 3, 27, 99, 729};        // Mang diem phong ngu

        public int[] AScore = { 0, 3, 24, 192, 1536, 12288, 98304 };  // Mang diem tan cong
        public int[] DScore = { 0, 1, 9, 81, 729, 6561, 59049 };      // Mang diem phong ngu
        #endregion

        #region Initialize
        public Computer(ChessBoardManager board)
        {
            this.boardState = board;
            this.eBoard = new EvalBoard();
            this.Move = new EvalBoard();
        }
        #endregion

        #region Method
        #region evalScore
        private void evalNodeLeaf(int value)
        {
            eBoard.resetBoard();
            int countCom, countPlayer;
            // hàng ngang
            for (int row = 0; row < Cons.CHESS_BOARD_WIDTH; row++) //hàng
            {
                for (int col = 0; col < Cons.CHESS_BOARD_HEIGHT - 4; col++) //cột
                {
                    countCom = countPlayer = 0; // biến đếm số lượng của máy và người
                    for (int i = 0; i < 5; i++)
                    {
                        // đếm số quân cờ của máy và người trong phạm vi 5 con
                        if (boardState.checkChess(new Point(row, col + i), 2))
                            countCom++;
                        if (boardState.checkChess(new Point(row, col + i), 1))
                            countPlayer++;
                    }
                    // trong vòng 5 ô k có quân địch
                    if (countCom * countPlayer == 0 && countCom != countPlayer)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (boardState.checkChess(new Point(row, col + i), 0))  // ô chưa đánh trong 5 ô
                            {
                                if (countPlayer == 0)   // nếu trong 5 ô k có quân người
                                {
                                    if (value == 1) // mà lượt đánh là của người
                                        eBoard.EBoard[row, col + i] += DScore[countCom];
                                    else  // lượt đánh của máy
                                        eBoard.EBoard[row, col + i] += AScore[countCom];
                                }
                                if (countCom == 0)  // nếu trong 5 ô k có quân máy
                                {
                                    if (value == 2) // lượt đánh của máy
                                        eBoard.EBoard[row, col + i] += DScore[countPlayer];
                                    else  // lượt đánh của người
                                        eBoard.EBoard[row, col + i] += AScore[countPlayer];
                                }
                                if (countPlayer == 4 || countCom == 4) // nhân 2 điểm khi có 1 bên gần thắng
                                    eBoard.EBoard[row, col + i] *= 2;
                            }
                        }
                    }
                }
            }
            //dọc - tương tự ngang
            for (int row = 0; row < Cons.CHESS_BOARD_WIDTH - 4; row++)  //hàng
            {
                for (int col = 0; col < Cons.CHESS_BOARD_HEIGHT; col++) //cột
                {
                    countCom = countPlayer = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (boardState.checkChess(new Point(row + i, col), 2))
                            countCom++;
                        if (boardState.checkChess(new Point(row + i, col), 1))
                            countPlayer++;
                    }
                    if (countCom * countPlayer == 0 && countCom != countPlayer)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (boardState.checkChess(new Point(row + i, col), 0))
                            {
                                if (countPlayer == 0)
                                {
                                    if (value == 1)
                                        eBoard.EBoard[row + i, col] += DScore[countCom];
                                    else
                                        eBoard.EBoard[row + i, col] += AScore[countCom];
                                }
                                if (countCom == 0)
                                {
                                    if (value == 2)
                                        eBoard.EBoard[row + i, col] += DScore[countPlayer];
                                    else
                                        eBoard.EBoard[row + i, col] += AScore[countPlayer];
                                }
                                if (countPlayer == 4 || countCom == 4)
                                    eBoard.EBoard[row + i, col] *= 2;
                            }
                        }
                    }
                }
            }
            //chéo chính - tương tự ngang
            for (int row = 0; row < Cons.CHESS_BOARD_WIDTH - 4; row++)  //hàng
            {
                for (int col = 0; col < Cons.CHESS_BOARD_HEIGHT - 4; col++) //cột
                {
                    countCom = countPlayer = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (boardState.checkChess(new Point(row + i, col + i), 2))
                            countCom++;
                        if (boardState.checkChess(new Point(row + i, col + i), 1))
                            countPlayer++;
                    }
                    if (countCom * countPlayer == 0 && countCom != countPlayer)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (boardState.checkChess(new Point(row + i, col + i), 0))
                            {
                                if (countPlayer == 0)
                                {
                                    if (value == 1)
                                        eBoard.EBoard[row + i, col + i] += DScore[countCom];
                                    else
                                        eBoard.EBoard[row + i, col + i] += AScore[countCom];
                                }
                                if (countCom == 0)
                                {
                                    if (value == 2)
                                        eBoard.EBoard[row + i, col + i] += DScore[countPlayer];
                                    else
                                        eBoard.EBoard[row + i, col + i] += AScore[countPlayer];
                                }
                                if (countPlayer == 4 || countCom == 4)
                                    eBoard.EBoard[row + i, col + i] *= 2;
                            }
                        }
                    }
                }
            }
            //chéo phụ - tương tự ngang
            for (int row = 4; row < Cons.CHESS_BOARD_WIDTH; row++)  //hàng
            {
                for (int col = 0; col < Cons.CHESS_BOARD_HEIGHT - 4; col++) //cột
                {
                    countCom = countPlayer = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (boardState.checkChess(new Point(row - i, col + i), 2))
                            countCom++;
                        if (boardState.checkChess(new Point(row - i, col + i), 1))
                            countPlayer++;
                    }
                    if (countCom * countPlayer == 0 && countCom != countPlayer)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (boardState.checkChess(new Point(row - i, col + i), 0))
                            {
                                if (countPlayer == 0)
                                {
                                    if (value == 1)
                                        eBoard.EBoard[row - i, col + i] += DScore[countCom];
                                    else
                                        eBoard.EBoard[row - i, col + i] += AScore[countCom];
                                }
                                if (countCom == 0)
                                {
                                    if (value == 2)
                                        eBoard.EBoard[row - i, col + i] += DScore[countPlayer];
                                    else
                                        eBoard.EBoard[row - i, col + i] += AScore[countPlayer];
                                }
                                if (countPlayer == 4 || countCom == 4)
                                    eBoard.EBoard[row - i, col + i] *= 2;
                            }
                        }
                    }
                }
            }
            //eBoard.MaxPos();
            //return eBoard.evaluationBoard;
        }

        //private void evalPosition()
        //{

        //}

        //private void evalHorizontal(int row, int col)
        //{
        //    for (int row = 0; row < Cons.CHESS_BOARD_WIDTH; row++) //hàng
        //    {
        //        for (int col = 0; col < Cons.CHESS_BOARD_HEIGHT - 4; col++) //cột
        //        {
        //            countCom = countPlayer = 0; // biến đếm số lượng của máy và người
        //            for (int i = 0; i < 5; i++)
        //            {
        //                đếm số quân cờ của máy và người trong phạm vi 5 con
        //                if (boardState.checkChess(new Point(row, col + i), 2))
        //                    countCom++;
        //                if (boardState.checkChess(new Point(row, col + i), 1))
        //                    countPlayer++;
        //            }
        //            trong vòng 5 ô k có quân địch
        //            if (countCom * countPlayer == 0 && countCom != countPlayer)
        //            {
        //                for (int i = 0; i < 5; i++)
        //                {
        //                    if (boardState.checkChess(new Point(row, col + i), 0))  // ô chưa đánh trong 5 ô
        //                    {
        //                        if (countPlayer == 0)   // nếu trong 5 ô k có quân người
        //                        {
        //                            if (value == 1) // mà lượt đánh là của người
        //                                eBoard.EBoard[row, col + i] += DScore[countCom];
        //                            else  // lượt đánh của máy
        //                                eBoard.EBoard[row, col + i] += AScore[countCom];
        //                        }
        //                        if (countCom == 0)  // nếu trong 5 ô k có quân máy
        //                        {
        //                            if (value == 2) // lượt đánh của máy
        //                                eBoard.EBoard[row, col + i] += DScore[countPlayer];
        //                            else  // lượt đánh của người
        //                                eBoard.EBoard[row, col + i] += AScore[countPlayer];
        //                        }
        //                        if (countPlayer == 4 || countCom == 4) // nhân 2 điểm khi có 1 bên gần thắng
        //                            eBoard.EBoard[row, col + i] *= 2;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private void evalVertical(int row, int col)
        //{
        //    for (int row = 0; row < Cons.CHESS_BOARD_WIDTH - 4; row++)  //hàng
        //    {
        //        for (int col = 0; col < Cons.CHESS_BOARD_HEIGHT; col++) //cột
        //        {
        //            countCom = countPlayer = 0;
        //            for (int i = 0; i < 5; i++)
        //            {
        //                if (boardState.checkChess(new Point(row + i, col), 2))
        //                    countCom++;
        //                if (boardState.checkChess(new Point(row + i, col), 1))
        //                    countPlayer++;
        //            }
        //            if (countCom * countPlayer == 0 && countCom != countPlayer)
        //            {
        //                for (int i = 0; i < 5; i++)
        //                {
        //                    if (boardState.checkChess(new Point(row + i, col), 0))
        //                    {
        //                        if (countPlayer == 0)
        //                        {
        //                            if (value == 1)
        //                                eBoard.EBoard[row + i, col] += DScore[countCom];
        //                            else
        //                                eBoard.EBoard[row + i, col] += AScore[countCom];
        //                        }
        //                        if (countCom == 0)
        //                        {
        //                            if (value == 2)
        //                                eBoard.EBoard[row + i, col] += DScore[countPlayer];
        //                            else
        //                                eBoard.EBoard[row + i, col] += AScore[countPlayer];
        //                        }
        //                        if (countPlayer == 4 || countCom == 4)
        //                            eBoard.EBoard[row + i, col] *= 2;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private void evalPrimary(int row, int col)
        //{
        //    for (int row = 0; row < Cons.CHESS_BOARD_WIDTH - 4; row++)  //hàng
        //    {
        //        for (int col = 0; col < Cons.CHESS_BOARD_HEIGHT - 4; col++) //cột
        //        {
        //            countCom = countPlayer = 0;
        //            for (int i = 0; i < 5; i++)
        //            {
        //                if (boardState.checkChess(new Point(row + i, col + i), 2))
        //                    countCom++;
        //                if (boardState.checkChess(new Point(row + i, col + i), 1))
        //                    countPlayer++;
        //            }
        //            if (countCom * countPlayer == 0 && countCom != countPlayer)
        //            {
        //                for (int i = 0; i < 5; i++)
        //                {
        //                    if (boardState.checkChess(new Point(row + i, col + i), 0))
        //                    {
        //                        if (countPlayer == 0)
        //                        {
        //                            if (value == 1)
        //                                eBoard.EBoard[row + i, col + i] += DScore[countCom];
        //                            else
        //                                eBoard.EBoard[row + i, col + i] += AScore[countCom];
        //                        }
        //                        if (countCom == 0)
        //                        {
        //                            if (value == 2)
        //                                eBoard.EBoard[row + i, col + i] += DScore[countPlayer];
        //                            else
        //                                eBoard.EBoard[row + i, col + i] += AScore[countPlayer];
        //                        }
        //                        if (countPlayer == 4 || countCom == 4)
        //                            eBoard.EBoard[row + i, col + i] *= 2;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private void evalSub(int row, int col)
        //{
        //    for (int row = 4; row < Cons.CHESS_BOARD_WIDTH; row++)  //hàng
        //    {
        //        for (int col = 0; col < Cons.CHESS_BOARD_HEIGHT - 4; col++) //cột
        //        {
        //            countCom = countPlayer = 0;
        //            for (int i = 0; i < 5; i++)
        //            {
        //                if (boardState.checkChess(new Point(row - i, col + i), 2))
        //                    countCom++;
        //                if (boardState.checkChess(new Point(row - i, col + i), 1))
        //                    countPlayer++;
        //            }
        //            if (countCom * countPlayer == 0 && countCom != countPlayer)
        //            {
        //                for (int i = 0; i < 5; i++)
        //                {
        //                    if (boardState.checkChess(new Point(row - i, col + i), 0))
        //                    {
        //                        if (countPlayer == 0)
        //                        {
        //                            if (value == 1)
        //                                eBoard.EBoard[row - i, col + i] += DScore[countCom];
        //                            else
        //                                eBoard.EBoard[row - i, col + i] += AScore[countCom];
        //                        }
        //                        if (countCom == 0)
        //                        {
        //                            if (value == 2)
        //                                eBoard.EBoard[row - i, col + i] += DScore[countPlayer];
        //                            else
        //                                eBoard.EBoard[row - i, col + i] += AScore[countPlayer];
        //                        }
        //                        if (countPlayer == 4 || countCom == 4)
        //                            eBoard.EBoard[row - i, col + i] *= 2;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        private void evalChessBoard(bool isComputer)
        {
            eBoard.resetBoard();
            for (int row = 0; row < Cons.CHESS_BOARD_WIDTH; row++) //hàng
            {
                for (int col = 0; col < Cons.CHESS_BOARD_HEIGHT; col++) //cột
                {
                    if (boardState.checkChess(new Point(row, col), 0))  //kiểm tra vị trí đang xét là trống
                        evalPosition(isComputer, new Point(row, col));  //đánh giá điểm cho vị trí đó
                }
            }
        }

        // đánh giá điểm cho vị trí đang xet

        private void evalPosition(bool isComputer, Point point)
        {
            int value = (!isComputer) ? 1 : 2;
            // tính điểm tấn công và phòng thủ
            int AttScore = evalHorizontalAttack(value, point) + evalVerticalAttack(value, point)
                                            + evalPrimaryAttack(value, point) + evalSubAttack(value, point);

            int DefScore = evalHorizontalDefence(value, point) + evalVerticalDefence(value, point)
                                            + evalPrimaryDefence(value, point) + evalSubDefence(value, point);
            //eBoard.EBoard[point.X, point.Y] = Math.Max(AttScore, DefScore);
            eBoard.EBoard[point.X, point.Y] = AttScore + DefScore;
        }

        // Tính điểm hàng ngang
        private int evalHorizontalAttack(int value, Point point)
        {
            int score = 0;
            int countEnemy = 0, countOur = 0;
            for (int i = 1; i < 6 && point.X + i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                if (boardState.checkChess(new Point(point.X + i, point.Y), value))
                    countOur++;
                else
                {
                    if (boardState.checkChess(new Point(point.X + i, point.Y), 0))
                        break;
                    else
                    {
                        countEnemy++;
                        break;
                    }
                }
            }
            for (int i = 1; i < 6 && point.X - i >= 0; i++)
            {
                if (boardState.checkChess(new Point(point.X - i, point.Y), value))
                    countOur++;
                else
                {
                    if (boardState.checkChess(new Point(point.X - i, point.Y), 0))
                        break;
                    else
                    {
                        countEnemy++;
                        break;
                    }
                }
            }
            if (countEnemy == 2)
                return 0;
            if (countOur == 0)
                return 0;
            score -= DScore[countEnemy + 1];
            score += AScore[countOur];
            return score;
        }

        private int evalHorizontalDefence(int value, Point point)
        {
            int score = 0;
            int countEnemy = 0, countOur = 0;
            for (int i = 1; i < 6 && point.X + i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                if (boardState.checkChess(new Point(point.X + i, point.Y), value))
                {
                    countOur++;
                    break;
                }
                else
                {
                    if (boardState.checkChess(new Point(point.X + i, point.Y), 0))
                        break;
                    else
                    {
                        countEnemy++;
                    }
                }
            }
            for (int i = 1; i < 6 && point.X - i >= 0; i++)
            {
                if (boardState.checkChess(new Point(point.X - i, point.Y), value))
                {
                    countOur++;
                    break;
                }
                else
                {
                    if (boardState.checkChess(new Point(point.X - i, point.Y), 0))
                        break;
                    else
                    {
                        countEnemy++;
                    }
                }
            }
            if (countOur == 2)
                return 0;
            score += DScore[countEnemy];
            return score;
        }
        // Tính điểm hàng dọc
        private int evalVerticalAttack(int value, Point point)
        {
            int score = 0;
            int countEnemy = 0, countOur = 0;
            for (int i = 1; i < 6 && point.Y + i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (boardState.checkChess(new Point(point.X, point.Y + i), value))
                    countOur++;
                else
                {
                    if (boardState.checkChess(new Point(point.X, point.Y + i), 0))
                        break;
                    else
                    {
                        countEnemy++;
                        break;
                    }
                }
            }
            for (int i = 1; i < 6 && point.Y - i >= 0; i++)
            {
                if (boardState.checkChess(new Point(point.X, point.Y - i), value))
                    countOur++;
                else
                {
                    if (boardState.checkChess(new Point(point.X, point.Y - i), 0))
                        break;
                    else
                    {
                        countEnemy++;
                        break;
                    }
                }
            }
            if (countEnemy == 2)
                return 0;
            if (countOur == 0)
                return 0;
            score -= DScore[countEnemy + 1];
            score += AScore[countOur];
            return score;
        }

        private int evalVerticalDefence(int value, Point point)
        {
            int score = 0;
            int countEnemy = 0, countOur = 0;
            for (int i = 1; i < 6 && point.Y + i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (boardState.checkChess(new Point(point.X, point.Y + i), value))
                {
                    countOur++;
                    break;
                }
                else
                {
                    if (boardState.checkChess(new Point(point.X, point.Y + i), 0))
                        break;
                    else
                    {
                        countEnemy++;
                    }
                }
            }
            for (int i = 1; i < 6 && point.Y - i >= 0; i++)
            {
                if (boardState.checkChess(new Point(point.X, point.Y - i), value))
                {
                    countOur++;
                    break;
                }
                else
                {
                    if (boardState.checkChess(new Point(point.X, point.Y - i), 0))
                        break;
                    else
                    {
                        countEnemy++;
                    }
                }
            }
            if (countOur == 2)
                return 0;
            score += DScore[countEnemy];
            return score;
        }
        // Tính điểm chéo chính
        private int evalPrimaryAttack(int value, Point point)
        {
            int score = 0;
            int countEnemy = 0, countOur = 0;
            for (int i = 1; i < 6 && point.X + i < Cons.CHESS_BOARD_WIDTH && point.Y + i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (boardState.checkChess(new Point(point.X + i, point.Y + i), value))
                    countOur++;
                else
                {
                    if (boardState.checkChess(new Point(point.X + i, point.Y + i), 0))
                        break;
                    else
                    {
                        countEnemy++;
                        break;
                    }
                }
            }
            for (int i = 1; i < 6 && point.X - i >= 0 && point.Y - i >= 0; i++)
            {
                if (boardState.checkChess(new Point(point.X - i, point.Y - i), value))
                    countOur++;
                else
                {
                    if (boardState.checkChess(new Point(point.X - i, point.Y - i), 0))
                        break;
                    else
                    {
                        countEnemy++;
                        break;
                    }
                }
            }
            if (countEnemy == 2)
                return 0;
            if (countOur == 0)
                return 0;
            score -= DScore[countEnemy + 1];
            score += AScore[countOur];
            return score;
        }

        private int evalPrimaryDefence(int value, Point point)
        {
            int score = 0;
            int countEnemy = 0, countOur = 0;
            for (int i = 1; i < 6 && point.X + i < Cons.CHESS_BOARD_WIDTH && point.Y + i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (boardState.checkChess(new Point(point.X + i, point.Y + i), value))
                {
                    countOur++;
                    break;
                }
                else
                {
                    if (boardState.checkChess(new Point(point.X + i, point.Y + i), 0))
                        break;
                    else
                    {
                        countEnemy++;
                    }
                }
            }
            for (int i = 1; i < 6 && point.X - i >= 0 && point.Y - i >= 0; i++)
            {
                if (boardState.checkChess(new Point(point.X - i, point.Y - i), value))
                {
                    countOur++;
                    break;
                }
                else
                {
                    if (boardState.checkChess(new Point(point.X - i, point.Y - i), 0))
                        break;
                    else
                    {
                        countEnemy++;
                    }
                }
            }
            if (countOur == 2)
                return 0;
            score += DScore[countEnemy];
            return score;
        }
        // Tính điểm chéo phụ
        private int evalSubAttack(int value, Point point)
        {
            int score = 0;
            int countEnemy = 0, countOur = 0;
            for (int i = 1; i < 6 && point.X + i < Cons.CHESS_BOARD_WIDTH && point.Y - i >= 0; i++)
            {
                if (boardState.checkChess(new Point(point.X + i, point.Y - i), value))
                    countOur++;
                else
                {
                    if (boardState.checkChess(new Point(point.X + i, point.Y - i), 0))
                        break;
                    else
                    {
                        countEnemy++;
                        break;
                    }
                }
            }
            for (int i = 1; i < 6 && point.X - i >= 0 && point.Y + i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (boardState.checkChess(new Point(point.X - i, point.Y + i), value))
                    countOur++;
                else
                {
                    if (boardState.checkChess(new Point(point.X - i, point.Y + i), 0))
                        break;
                    else
                    {
                        countEnemy++;
                        break;
                    }
                }
            }
            if (countEnemy == 2)
                return 0;
            if (countOur == 0)
                return 0;
            score -= DScore[countEnemy + 1];
            score += AScore[countOur];
            return score;
        }

        private int evalSubDefence(int value, Point point)
        {
            int score = 0;
            int countEnemy = 0, countOur = 0;
            for (int i = 1; i < 6 && point.X + i < Cons.CHESS_BOARD_WIDTH && point.Y - i >= 0; i++)
            {
                if (boardState.checkChess(new Point(point.X + i, point.Y - i), value))
                {
                    countOur++;
                    break;
                }
                else
                {
                    if (boardState.checkChess(new Point(point.X + i, point.Y - i), 0))
                        break;
                    else
                    {
                        countEnemy++;
                    }
                }
            }
            for (int i = 1; i < 6 && point.X - i >= 0 && point.Y + i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (boardState.checkChess(new Point(point.X - i, point.Y + i), value))
                {
                    countOur++;
                    break;
                }
                else
                {
                    if (boardState.checkChess(new Point(point.X - i, point.Y + i), 0))
                        break;
                    else
                    {
                        countEnemy++;
                    }
                }
            }
            if (countOur == 2)
                return 0;
            score += DScore[countEnemy];
            return score;
        }
        #endregion

        #region Alphabeta pruning
        //thuật toán alpha-beta
        //private int alphaBetaPruning(int alpha, int beta, int depth, bool isComputer)
        //{
        //    if (depth >= MAXDEPTH)      // kiểm tra là node lá
        //        return evalNodeLeaf(isComputer ? 2 : 1);  // return điểm node lá, hàm evalChessBoard là hàm tính điểm, node lá nên là máy
        //    int bestVal;    // value tạm thời của node
        //    evalChessBoard(isComputer);     // tính điểm tạm thời
        //    List<Point> list = ListPosition();  // lấy ra danh sách các point có thể xét đến để đánh thử
        //    // Trường hợp MAX
        //    if (isComputer)
        //    {
        //        Console.WriteLine(depth);
        //        bestVal = int.MinValue;     // gán tạm thời value của node là MinValue
        //        foreach (Point point in list)   // xét danh sách các nút đánh thử - đây sẽ là các node con
        //        {
        //            boardState.DrawChess(point, 2);     // đánh thử
        //            // nếu như nước đánh này máy thắng
        //            if (boardState.isEndGame(boardState.Matrix[point.Y][point.X]))
        //            {
        //                boardState.DrawChess(point, 0);     // xóa trạng thái đánh thử
        //                bestVal = int.MaxValue;     // khi đó node này sẽ mang giá trị MaxValue và return giá trị này
        //                // gán điểm cho trường hợp để lấy nước đi
        //                if (depth == 0)
        //                    Move.setPosition(point.X, point.Y, bestVal);
        //                return bestVal;
        //            }
        //            // lấy giá trị max của các node con
        //            bestVal = Math.Max(bestVal, alphaBetaPruning(alpha, beta, depth + 1, !isComputer));
        //            boardState.DrawChess(point, 0); // xóa trạng thái đánh thử
        //            alpha = Math.Max(alpha, bestVal);
        //            if (alpha >= beta)    // pruning (cắt trường hợp, nếu node này mang giá trị cao hơn beta thì cắt)
        //            {
        //                // ở đây có thể hiểu là beta là giá trị node cha - MIN                    
        //                return bestVal;
        //            }
        //            // gán điểm cho trường hợp để lấy nước đi
        //            if (depth == 0)
        //            {
        //                Move.setPosition(point.X, point.Y, alpha);
        //            }
        //        }
        //        return bestVal;
        //    }
        //    // Trường hợp MIN - ngược lại với MAX
        //    else
        //    {
        //        bestVal = int.MaxValue;
        //        foreach (Point point in list)
        //        {
        //            boardState.DrawChess(point, 1);
        //            if (boardState.isEndGame(boardState.Matrix[point.Y][point.X]))
        //            {
        //                boardState.DrawChess(point, 0);
        //                bestVal = int.MinValue;
        //                return bestVal;
        //            }
        //            bestVal = Math.Min(bestVal, alphaBetaPruning(alpha, beta, depth + 1, !isComputer));
        //            boardState.DrawChess(point, 0);
        //            beta = Math.Min(beta, bestVal);
        //            if (beta <= alpha)
        //            {
        //                return bestVal;
        //            }
        //        }
        //        return bestVal;
        //    }
        //}

        private int alphaBetaPruning(int alpha, int beta, int depth, bool isComputer)
        {
            if (depth >= MAXDEPTH)      // kiểm tra là node lá
            {
                eBoard.MaxPos();
                return eBoard.evaluationBoard;  // return điểm node lá, hàm evalChessBoard là hàm tính điểm, node lá nên là máy
            }
            int bestVal;    // value tạm thời của node
            evalNodeLeaf(isComputer ? 2 : 1);    // version 1
            //evalChessBoard(isComputer);     // tính điểm tạm thời, version 2
            List<Point> list = ListPosition();  // lấy ra danh sách các point có thể xét đến để đánh thử
            // Trường hợp MAX
            if (!isComputer)
            {
                bestVal = int.MinValue;     // gán tạm thời value của node là MinValue
                foreach (Point point in list)   // xét danh sách các nút đánh thử - đây sẽ là các node con
                {
                    boardState.DrawChess(point, 1);     // đánh thử
                    // nếu như nước đánh này máy thắng
                    if (boardState.isEndGame(boardState.Matrix[point.Y][point.X]))
                    {
                        boardState.DrawChess(point, 0);     // xóa trạng thái đánh thử
                        bestVal = 1;     // khi đó node này sẽ mang giá trị MaxValue và return giá trị này
                        return bestVal;
                    }
                    // lấy giá trị max của các node con
                    bestVal = Math.Max(bestVal, alphaBetaPruning(alpha, beta, depth + 1, !isComputer));
                    boardState.DrawChess(point, 0); // xóa trạng thái đánh thử
                    alpha = Math.Max(alpha, bestVal);
                    if (alpha >= beta)    // pruning (cắt trường hợp, nếu node này mang giá trị cao hơn beta thì cắt)
                    {
                        // ở đây có thể hiểu là beta là giá trị node cha - MIN                    
                        return bestVal;
                    }

                }
                return bestVal;
            }
            // Trường hợp MIN - ngược lại với MAX
            else
            {
                bestVal = int.MaxValue;
                foreach (Point point in list)
                {
                    boardState.DrawChess(point, 2);
                    if (boardState.isEndGame(boardState.Matrix[point.Y][point.X]))
                    {
                        boardState.DrawChess(point, 0);     // xóa trạng thái đánh thử
                        bestVal = int.MaxValue;     // khi đó node này sẽ mang giá trị MaxValue và return giá trị này
                        // gán điểm cho trường hợp để lấy nước đi
                        if (depth == 0)
                            Move.setPosition(point.X, point.Y, bestVal);
                        return bestVal;
                    }
                    bestVal = Math.Min(bestVal, alphaBetaPruning(alpha, beta, depth + 1, !isComputer));
                    boardState.DrawChess(point, 0);
                    beta = Math.Min(beta, bestVal);
                    if (beta <= alpha)
                    {
                        return bestVal;
                    }
                    // gán điểm cho trường hợp để lấy nước đi
                    if (depth == 0)
                    {
                        Console.WriteLine(point + " " + beta);
                        Move.setPosition(point.X, point.Y, beta);
                    }
                }
                return bestVal;
            }
        }

        private List<Point> ListPosition()
        {
            List<Point> list = new List<Point>();
            for (int i = 0; i < 6; i++)
            {
                Point p = eBoard.MaxPos();
                if (p.X == -1 && p.Y == -1)
                    break;
                list.Add(p);
                eBoard.setPosition(p.X, p.Y, 0);
            }
            return list;
        }

        #endregion

        #region Move
        private Point randomBestPositions()
        {
            Point point = Move.MaxPos();
            int max = Move.evaluationBoard; // lấy max điểm của trạng thái
            if (max == 0)
                return new Point(0, 0);
            // lấy list Point có điểm
            List<Point> list = new List<Point>();
            list.Add(point);
            Move.setPosition(point.X, point.Y, 0);
            for (int i = 0; i < Cons.CHESS_BOARD_WIDTH * Cons.CHESS_BOARD_HEIGHT; i++)
            {
                Point p = Move.MaxPos();
                // nếu hết các trường hợp hoặc khác max thì break
                if (p.X == -1 || (Move.evaluationBoard != max))
                    break;
                list.Add(p);
                Move.setPosition(p.X, p.Y, 0);    // set cho điểm ô đã lấy = 0
            }
            Random r = new Random();
            // random 0-list.count để lấy point
            //Console.WriteLine(list.Count);
            return list[r.Next(0, list.Count)];
        }

        public Point SearchPosition()
        {
            alphaBetaPruning(int.MinValue, int.MaxValue, 0, true);
            Point p = new Point();
            p = randomBestPositions();
            eBoard.resetBoard();
            Move.resetBoard();
            return p;
        }
        #endregion
        #endregion
    }
}
