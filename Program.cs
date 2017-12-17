using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LajiEngine
{
    class Program
    {

        //涉及到读入的需要多线程?
        static void Initialize()
        {
            string opening = line;
            if (opening == "ucci")
            {
                Console.WriteLine("Laji Engine");
                Console.WriteLine("中国象棋");
                Console.WriteLine("垃圾引擎");
                Console.WriteLine("Author: Wang Wenxi");
                Console.WriteLine("2017.9");
                Console.WriteLine("All rights reserved. 233333");
                Console.WriteLine("ucci ok");
                init = true;
            }
        }
        static int a;
        static int b;
        static int[] Read_Position()
        {
            if (line.Contains(",") || line.Contains("，"))
            {
                try
                {
                    int i = line.IndexOf(",");
                    a = (int)char.GetNumericValue(line[i - 1]);
                    b = (int)char.GetNumericValue(line[i + 1]);
                }
                catch { }
            }

            int[] c = { a, b };
            return c;
        }
        static void Display_Board()
        {
            if (line == "display b")
            {
                for (int i = 9; i > -1; i--)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (board[j, i] != null)
                        {
                            Console.Write(Decode_Character(board[j, i]));
                        }
                        else
                        {
                            Console.Write("〇");
                        }
                    }
                    Console.WriteLine(" "+ i);
                }
                Console.WriteLine("a b c d e f g h i");
            }
        }
        static string Decode_Character(Piece pc)
        {
            string c = "";
            Type t = pc.GetType();
            if (t == typeof(Rook))
            {
                if (!pc.side) c = "車";
                else c = "俥";
            }
            if (t == typeof(Knight))
            {
                if (!pc.side) c = "馬";
                else c = "傌";
            }
            if (t == typeof(Bishop))
            {
                if (!pc.side) c = "象";
                else c = "相";
            }
            if (t == typeof(Advisor))
            {
                if (!pc.side) c = "士";
                else c = "仕";
            }
            if (t == typeof(General))
            {
                if (!pc.side) c = "將";
                else c = "帥";
            }
            if (t == typeof(Cannon))
            {
                if (!pc.side) c = "砲";
                else c = "炮";
            }
            if (t == typeof(Pawn))
            {
                if (!pc.side) c = "卒";
                else c = "兵";
            }
            return c;
        }
        static void Display_Terri(int[] rd)

        {
            if (line == "display t")
            {
                var a = Board.GetAllSeeTo(board, true);
                for (int i = 9; i > -1; i--)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        int[] c = { j, i };
                        if (Board.ContainIt(a, c))
                        {
                            Console.Write("×");
                        }
                        else
                        {
                            Console.Write("〇");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
        static void Display_TerriM(int[] rd)

        {
            if (line == "display m")
            {
                var a = Board.GetAllPossiMove(board, true);
                for (int i = 9; i > -1; i--)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        int[] c = { j, i };
                        if (Board.ContainIt(a, c))
                        {
                            Console.Write("×");
                        }
                        else
                        {
                            Console.Write("〇");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
        static void Display_Cap(int[] rd)
        {
            if (line == "display c")
            {
                var a = Board.GetAllPossiCap(board, true);
                for (int i = 9; i > -1; i--)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        int[] c = { j,i };
                        if (Board.ContainIt(a, c))
                        {
                            Console.Write("×");
                        }
                        else
                        {
                            Console.Write("〇");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
        static void Display_Checkmate()
        {
            if(line=="display checkmate red")
            {
                Console.WriteLine("查询中");
                if(Board.IsCheckmate(board, true))
                {
                    Console.WriteLine("红方危险。");
                }
                else
                {
                    Console.WriteLine("红方安全。");
                }
            }
            if(line=="display checkmate black")
            {
                Console.WriteLine("查询中");
                if (Board.IsCheckmate(board, false))
                {
                    Console.WriteLine("黑方危险。");
                }
                else
                {
                    Console.WriteLine("黑方安全。");
                }
            }
        }
        static void Display_Worth()
        {
            if(init&& line=="display worth")
            {
                for (int i = 9; i > -1; i--)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (board[j,i]!=null)
                        {
                            Console.Write(board[j, i].Worth+ " "); 
                        }
                        else
                        {
                            Console.Write("〇");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        static void Go()
        {
            if(line.Contains("go depth")||line=="go")
            {
                string t = Search.Search_Surface(board, now_side);
                if (t!="nobestmove")
                {
                    string tohint = "bestmove ";
                    tohint += t;
                    Console.WriteLine(tohint);
                }
                else
                {
                    Console.WriteLine(Search.Search_Surface(board, now_side)+"Nowside:"+now_side.ToString());
                }
            }
        }

        static void Status()
        {
            if (line == "status")
            {
                Console.WriteLine("Side="+now_side);
            }
        }
        /// <summary>
        ///status: 0=null 1=red 2=black妈的怎么从这里调用的。。。
        /// </summary>
        /// <param name="indx"></param>
        /// <returns></returns>
        public static int CheckGrid(int[] indx)
        {
            int status = 0;
            if (board[indx[0], indx[1]] != null)
            {
                if (GetPiece(indx).side == false)
                {
                    status = 2;
                }
                if (GetPiece(indx).side == true)
                {
                    status = 1;
                }
            }
            else
            {
                status = 0;
            }


            return status;
        }
        public static Piece GetPiece(int[] indx)
        {
            if (board[indx[0],indx[1]]!=null)
            {
                return board[indx[0], indx[1]];
            }
            else
            {
                Console.WriteLine("。。。");

                return new ErrorPiece();
            }
        }
        static void Quit()
        {
            if (line == "exit" || line == "quit")
            {
                Environment.Exit(0);
            }
        }
        static string line;
        static Piece[,] board = new Piece[9, 10];
        static bool now_side;
        static bool init = false;
        static void Main(string[] args)///主程序
        {
            while (true)
            {
                line = Console.ReadLine();
                Initialize();
                Go();
                Display_Terri(Read_Position());
                Display_Cap(Read_Position());
                Display_TerriM(Read_Position());
                Display_Checkmate();
                Display_Worth();
                Fen();
                Display_Board();
                Quit();
                Status();
            }
        }










        //有关数据和数组



        //接受并分析传来的信息
        static Piece DescribePC(char zi)
        {
            Piece pc = new Piece();
            switch (zi)
            {
                case 'r':
                    pc = new Rook(); pc.side = false;
                    break;
                case 'R':
                    pc = new Rook(); pc.side = true;
                    break;
                case 'n':
                    pc = new Knight(); pc.side = false;
                    break;
                case 'N':
                    pc = new Knight(); pc.side = true;
                    break;
                case 'b':
                    pc = new Bishop(); pc.side = false;
                    break;
                case 'B':
                    pc = new Bishop(); pc.side = true;
                    break;
                case 'a':
                    pc = new Advisor(); pc.side = false;
                    break;
                case 'A':
                    pc = new Advisor(); pc.side = true;
                    break;
                case 'k':
                    pc = new General(); pc.side = false;
                    break;
                case 'K':
                    pc = new General(); pc.side = true;
                    break;
                case 'c':
                    pc = new Cannon(); pc.side = false;
                    break;
                case 'C':
                    pc = new Cannon(); pc.side = true;
                    break;
                case 'p':
                    pc = new Pawn(); pc.side = false;
                    break;
                case 'P':
                    pc = new Pawn(); pc.side = true;
                    break;
                default:
                    break;
            }
            return pc;
        }

        static void Fen()
        {
            string fen;
            if (init)
            {
                fen = line;
                if (fen.Contains("position fen "))
                {
                    int t = fen.IndexOf("position fen ");
                    fen = fen.Substring(t + "position fen ".Length);
                    char[] fenc = fen.ToCharArray();
                    board = new Piece[9, 10];

                    int rdIndx = 0;
                    for (int i = 9; i > -1; i--)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (fenc.Length > rdIndx)
                            {
                                if (fenc[rdIndx] == '/')
                                {
                                    //这里一行结束，需要在这一次循环中把下一个棋子也读出来
                                    rdIndx++;
                                }
                                if ((int)char.GetNumericValue(fenc[rdIndx]) == -1)
                                {
                                    //这里有一个棋子
                                    board[j, i] = DescribePC(fenc[rdIndx]);
                                    rdIndx++;
                                }
                                else
                                {
                                    //这里有一个数字
                                    if (fenc[rdIndx] == '1')
                                    {
                                        //就这一个格是空的
                                        rdIndx++;
                                    }
                                    else
                                    {
                                        //这个格是空的，后面的几个也是
                                        //rdIndx不能动
                                        fenc[rdIndx]--;
                                    }
                                }
                            }
                        }
                    }
                    //读完了FEN的部分
                    rdIndx++;
                    try
                    {
                        if (fenc[rdIndx] == 'w' || fenc[rdIndx] == 'r')
                        {
                            now_side = true;
                        }
                        else if (fenc[rdIndx] == 'b')
                        {
                            now_side = false;
                        }
                        else
                        {
                            Console.Write("FEN代码有问题：");
                            Console.WriteLine(fenc[rdIndx]);
                        }
                    }
                    catch (Exception)
                    {
                        Console.Write("FEN代码有大问题：");
                        //throw;
                    }//读现在轮到哪一边


                }
                //后面处理moves的部分，需要生成走棋的方法先
                if (fen.Contains("moves "))
                {
                    int t = fen.IndexOf("moves ");
                    fen = fen.Substring(t + "moves ".Length);
                    char[] fenm = fen.ToCharArray();
                    for (int rdIndx = 0; rdIndx < fenm.Length; rdIndx = rdIndx + 5)
                    {
                        char[] onemove = { fenm[rdIndx], fenm[rdIndx + 1], fenm[rdIndx + 2], fenm[rdIndx + 3] };
                        board = Board.Board_After_Move(board, Board.Moves_to_PosArray(onemove, false), Board.Moves_to_PosArray(onemove, true));
                        now_side = !now_side;
                    }
                }
            }
        }



    }
}
