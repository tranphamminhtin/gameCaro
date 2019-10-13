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
        ChessBoardManager boardState; // trang thai cua ban co
        EvalBoard move;
        //int playerFlag = 2; // danh dau la computer player
        //private int _x, _y; // tọa độ nước đi

        public static int MAXDEPTH; // độ sâu tối đa

        private int[] AScore = { 0, 4, 27, 256, 1458 };// Mảng điểm tấn công
        private int[] DScore = { 0, 2, 9, 99, 769 };  // Mảng điểm phòng thủ

        //public int[] AScore = { 0, 9, 54, 169, 1458 };  // Mang diem tan cong
        //public int[] DScore = {0, 3, 27, 99, 729};// Mang diem phong ngu
        //public bool cWin = false;
        //public Point goPoint;

        public EvalBoard Move
        {
            get
            {
                return move;
            }

            set
            {
                Move = value;
            }
        }
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
        private int evalChessBoard(bool isComputer)
        {
            for (int row = 0; row < Cons.CHESS_BOARD_WIDTH; row++) //hàng
            {
                for (int col = 0; col < Cons.CHESS_BOARD_HEIGHT; col++) //cột
                {
                    if (boardState.checkChess(new Point(row, col), 0))  //kiểm tra vị trí đang xét là trống
                        evalPosition(isComputer, new Point(row, col));  //đánh giá điểm cho vị trí đó
                }
            }
            eBoard.MaxPos();
            return eBoard.evaluationBoard;
        }

        //đánh giá điểm cho vị trí đang xet
        private void evalPosition(bool isComputer, Point point)
        {
            // tính điểm tấn công và phòng thủ
            int AttScore = evalHorizontal(isComputer, point, true) + evalVertical(isComputer, point, true)
                                            + evalPrimary(isComputer, point, true) + evalSub(isComputer, point, true);

            int DefScore = evalHorizontal(isComputer, point, false) + evalVertical(isComputer, point, false)
                                            + evalPrimary(isComputer, point, false) + evalSub(isComputer, point, false);
            eBoard.EBoard[point.X, point.Y] = Math.Max(AttScore, DefScore);
        }

        //Tính điểm hàng ngang
        private int evalHorizontal(bool isComputer, Point point, bool isAttack)
        {
            int score;
            int value = (!isComputer) ? 1 : 2;
            // tính điểm tấn công
            if (isAttack)
            {
                score = 0;
            }
            // tính điểm phòng thủ
            else
            {
                score = 1;
            }
            return score;
        }
        //Tính điểm hàng dọc
        private int evalVertical(bool isComputer, Point point, bool isAttack)
        {
            int score;
            int value = (!isComputer) ? 1 : 2;
            // tính điểm tấn công
            if (isAttack)
            {
                score = 0;
            }
            // tính điểm phòng thủ
            else
            {
                score = 1;
            }
            return score;
        }
        //Tính điểm chéo chính
        private int evalPrimary(bool isComputer, Point point, bool isAttack)
        {
            int score;
            int value = (!isComputer) ? 1 : 2;
            // tính điểm tấn công
            if (isAttack)
            {
                score = 0;
            }
            // tính điểm phòng thủ
            else
            {
                score = 1;
            }
            return score;
        }
        //Tính điểm chéo phụ
        private int evalSub(bool isComputer, Point point, bool isAttack)
        {
            int score;
            int value = (!isComputer) ? 1 : 2;
            // tính điểm tấn công
            if (isAttack)
            {
                score = 0;
            }
            // tính điểm phòng thủ
            else
            {
                score = 1;
            }
            return score;
        }
        #endregion

        #region Alphabeta pruning
        // thuật toán alpha-beta
        private int alphaBetaPruning(int alpha, int beta, int depth, bool isComputer)
        {
            if (depth >= MAXDEPTH)      // kiểm tra là node lá
                return evalChessBoard(isComputer);  // return điểm node lá, hàm evalChessBoard là hàm tính điểm, node lá nên là máy
            int bestVal;    // value tạm thời của node
            evalChessBoard(isComputer);     // tính điểm tạm thời
            List<Point> list = ListPosition();  // lấy ra danh sách các point có thể xét đến để đánh thử
            // Trường hợp MAX
            if (isComputer)  
            {
                bestVal = int.MinValue;     // gán tạm thời value của node là MinValue
                foreach (Point point in list)   // xét danh sách các nút đánh thử - đây sẽ là các node con
                {
                    boardState.DrawChess(point, 2);     // đánh thử

                    // nếu như nước đánh này máy thắng
                    if (boardState.isEndGame(boardState.Matrix[point.Y][point.X]))
                    {
                        boardState.DrawChess(point, 0);     // xóa trạng thái đánh thử
                        bestVal = int.MaxValue;     // khi đó node này sẽ mang giá trị MaxValue và return giá trị này
                        // gán điểm cho trường hợp để lấy nước đi
                        if (depth == 0)
                            Move.setPosition(point.X, point.Y, bestVal);
                        return bestVal;
                    }
                    // lấy giá trị max của các node con
                    bestVal = Math.Max(bestVal, alphaBetaPruning(alpha, beta, depth + 1, !isComputer));
                    boardState.DrawChess(point, 0); // xóa trạng thái đánh thử
                    if (bestVal >= beta)    // pruning (cắt trường hợp, nếu node này mang giá trị cao hơn beta thì cắt)
                    {
                        // ở đây có thể hiểu là beta là giá trị node cha - MIN
                        // gán điểm cho trường hợp để lấy nước đi
                        if (depth == 0)
                            Move.setPosition(point.X, point.Y, bestVal);
                        return bestVal;
                    }
                    alpha = Math.Max(alpha, bestVal);
                    // gán điểm cho trường hợp để lấy nước đi
                    if (depth == 0)
                        Move.setPosition(point.X, point.Y, alpha);
                }
                return bestVal;
            }
            // Trường hợp MIN - ngược lại với MAX
            else
            {
                bestVal = int.MaxValue;
                foreach (Point point in list)
                {
                    boardState.DrawChess(point, 1);
                    if (boardState.isEndGame(boardState.Matrix[point.Y][point.X]))
                    {
                        boardState.DrawChess(point, 0);
                        bestVal = int.MinValue;
                        return bestVal;
                    }
                    bestVal = Math.Min(bestVal, alphaBetaPruning(alpha, beta, depth + 1, !isComputer));
                    boardState.DrawChess(point, 0);
                    if (bestVal <= alpha)
                    {
                        return bestVal;
                    }
                    beta = Math.Min(beta, bestVal);
                }
                return bestVal;
            }
        }

        private List<Point> ListPosition()
        {
            List<Point> list = new List<Point>();
            for(int i=0; i<Cons.CHESS_BOARD_WIDTH; i++)
            {
                Point p = (Point)eBoard.MaxPos();
                if (p == null)
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
            Point point = (Point)Move.MaxPos();
            int max = Move.evaluationBoard; // lấy max điểm của trạng thái
            // lấy list Point có điểm
            List<Point> list = new List<Point>();
            for (int i = 0; i < Cons.CHESS_BOARD_WIDTH * Cons.CHESS_BOARD_HEIGHT; i++) 
            {
                Point p = (Point)eBoard.MaxPos();
                // nếu hết các trường hợp hoặc là bé hơn 9% so với max thì break
                if (p == null || eBoard.evaluationBoard < (max-max*0.09))
                    break;
                list.Add(p);
                eBoard.setPosition(p.X, p.Y, 0);
            }
            Random r = new Random();
            point = list[r.Next(0, list.Count)];
            return point;
        }

        public Point SearchPosition()
        {
            Move.resetBoard();
            eBoard.resetBoard();
            alphaBetaPruning(int.MinValue, int.MaxValue, 0, true);
            Point p = new Point();
            p = randomBestPositions();
            return p;
        }
        #endregion
        #endregion
    }
}
