using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingAdvanced
{
    class Program
    {
        public static int[] Maps = new int[100];
        public static string[] players = new string[2];
        public static int[] playerPos = new int[2];
        public static bool[] Flag = new bool[2];

        static void Main(string[] args)
        {
            HeadGame(); //画游戏头
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("请输入玩家A名称：");
            players[0] = Console.ReadLine();
            while (true)
            {
                if (players[0] == "")
                {
                    Console.WriteLine("名称不得为空，请重新输入");
                    players[0] = Console.ReadLine();
                }
                else break;
            }
            Console.WriteLine("请输入玩家B名称：");
            players[1] = Console.ReadLine();
            while (true)
            {
                if (players[1] == "")
                {
                    Console.WriteLine("名称不得为空，请重新输入");
                    players[1] = Console.ReadLine();
                }
                else if (players[1] == players[0])
                {
                    Console.WriteLine("你输入的名称和玩家A名称相同！请重新输入");
                    players[1] = Console.ReadLine();
                }
                else break;
            }
            //玩家输入后，我们首先应该清屏
            Console.Clear();
            HeadGame();
            Console.WriteLine("{0}的士兵用A表示", players[0]);
            Console.WriteLine("{0}的士兵用B表示", players[1]);
            InitMaps(); //初始化地图
            DrawMaps(); //画地图

            while (playerPos[0] < 99 && playerPos[1] < 99)
            {
                if (Flag[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    Flag[0] = false;
                }

                if (playerPos[0] >= 99)
                {
                    Console.WriteLine("玩家{0}无耻的赢了玩家{1}", players[0], players[1]);
                    Win();
                    break;
                }
                if (Flag[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    Flag[1] = false;
                }
                if (playerPos[1] >= 99)
                {
                    Console.WriteLine("玩家{0}无耻的赢了玩家{1}", players[0], players[1]);
                    Win();
                    break;
                }
            }



            Console.ReadKey();

        }
        /// <summary>
        /// 画游戏头
        /// </summary>
        public static void HeadGame()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**********************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("**********************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**********************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("***.Net飞行棋高阶版***");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**********************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("**********************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**********************");

        }
        /// <summary>
        /// 初始化地图
        /// </summary>
        public static void InitMaps()
        {
            //我用0表示普通,显示给用户就是 □
            //....1...幸运轮盘,显示组用户就◎ 
            //....2...地雷,显示给用户就是 ☆
            //....3...暂停,显示给用户就是 ▲
            //....4...时空隧道,显示组用户就 卐
            int[] luckyturn = { 6, 23, 40, 55, 69, 83 };//幸运轮盘◎
            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//地雷☆
            int[] pause = { 9, 27, 60, 93 };//暂停▲
            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };//时空隧道卐

            for (int i = 0; i < luckyturn.Length; i++)
            {
                Maps[luckyturn[i]] = 1;
            }

            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = 2;
            }

            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 3;
            }

            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Maps[timeTunnel[i]] = 4;
            }
        }

        /// <summary>
        /// 画地图
        /// </summary>
        public static void DrawMaps()
        {
            #region 第一道横
            for (int i = 0; i < 30; i++)
            {

                Console.Write(CSS(i));
            }

            #endregion
            Console.WriteLine();
            #region 第一道竖
            for (int i = 30; i < 35; i++)
            {
                for (int j = 0; j <= 28; j++)
                {
                    Console.Write("  ");
                }
                Console.Write(CSS(i));
                Console.WriteLine();
            }


            #endregion
            #region 第二道横
            for (int i = 64; i >= 35; i--)
            {
                Console.Write(CSS(i));
            }
            #endregion
            Console.WriteLine();
            #region 第二道竖
            for (int i = 65; i < 70; i++)
            {
                Console.WriteLine(CSS(i));
            }
            #endregion
            #region 第三道横
            for (int i = 70; i <= 99; i++)
            {
                Console.Write(CSS(i));
            }
            #endregion
            Console.WriteLine();
        }
        /// <summary>
        /// 样式
        /// </summary>
        /// <param name="i">传来的i值</param>
        /// <returns>返回样式</returns>
        public static string CSS(int i)
        {
            string str = "";
            //playerPos[0] == i这句话的精髓是使得在地图内
            if (playerPos[0] == playerPos[1] && playerPos[0] == i)
            {
                str = "<>";
            }
            else if (playerPos[0] == i)
            {
                str = "Ａ";
            }
            else if (playerPos[1] == i)
            {
                str = "Ｂ";
            }
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        str = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        str = "◎";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        str = "☆";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        str = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        str = "卐";
                        break;
                }
            }
            return str;
        }


        public static void PlayGame(int im)
        {
            Random r = new Random();
            int num = r.Next(1, 7);
            Console.WriteLine("请{0}按任意键掷骰子", players[im]);
            Console.ReadKey(true);
            Console.WriteLine("{0}掷出了{1},并按任意键开始行动", players[im], num);
            Console.ReadKey(true);
            playerPos[im] += num;
            ChangePos();
            /*
            Console.WriteLine("{0}行动完了", players[im]);
            Console.ReadKey(true);
            */
            //玩家A有可能踩到玩家B，地雷， 时空隧道，幸运轮盘，暂停，方块

            if (playerPos[im] == playerPos[1 - im])
            {
                Console.WriteLine("{0}踩到了{1}, {2}向后退6格", players[im], players[1 - im], players[1 - im]);
                playerPos[1 - im] -= 6;
                Console.ReadKey(true);

            }
            else if (Maps[playerPos[im]] == 0)
            {
                Console.WriteLine("{0}踩到的是方块, 安全！", players[im]);
                Console.ReadKey(true);

            }
            else if (Maps[playerPos[im]] == 1)
            {
                Console.WriteLine("{0}踩到幸运轮盘, 请选择1--交换位置, 2--轰炸对手", players[im]);
                string input = Console.ReadLine();
                while (true)
                {
                    if (input == "1")
                    {
                        Console.WriteLine("{0}选择和对手交换位置", players[im]);
                        Console.ReadKey(true);
                        int temp = playerPos[im];
                        playerPos[im] = playerPos[1 - im];
                        playerPos[1 - im] = temp;
                        Console.WriteLine("交换位置成功！!！请按任意键继续！！！");
                        Console.ReadKey(true);
                        break;
                    }
                    else if (input == "2")
                    {
                        Random r1 = new Random();
                        int n1 = r1.Next(1, 11);
                        Console.WriteLine("{0}选择轰炸对手, 对手向后退{1}步", players[im], n1);
                        playerPos[1 - im] -= n1;
                        Console.ReadKey(true);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("输入错误！请输入1--交换位置, 2--轰炸对手");
                        input = Console.ReadLine();
                    }
                }
            }
            else if (Maps[playerPos[im]] == 2)
            {
                Random r2 = new Random();
                int n2 = r2.Next(1, 6);
                Console.WriteLine("{0}踩到地雷, 向后退{1}步", players[im], n2);
                playerPos[im] -= n2;
                Console.ReadKey(true);


            }
            else if (Maps[playerPos[im]] == 3)
            {
                Console.WriteLine("{0}踩到暂停, 暂停一回合", players[im]);
                Flag[im] = true;
                Console.ReadKey(true);


            }
            else if (Maps[playerPos[im]] == 4)
            {
                Random r3 = new Random();
                int n3 = r3.Next(1, 11);
                Console.WriteLine("{0}踩到时空隧道, 向前前进{1}步", players[im], n3);
                playerPos[im] += n3;
                Console.ReadKey(true);

            }

            ChangePos();
            Console.Clear();
            DrawMaps(); //画地图

        }


        public static void ChangePos()
        {
            if (playerPos[0] < 0)
            {
                playerPos[0] = 0;
            }
            if (playerPos[0] > 99)
            {
                playerPos[0] = 99;
            }
            if (playerPos[1] < 0)
            {
                playerPos[1] = 0;
            }
            if (playerPos[1] > 99)
            {
                playerPos[1] = 99;
            }
        }
        public static void Win()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                          ◆                      ");
            Console.WriteLine("                    ■                  ◆               ■        ■");
            Console.WriteLine("      ■■■■  ■  ■                ◆■         ■    ■        ■");
            Console.WriteLine("      ■    ■  ■  ■              ◆  ■         ■    ■        ■");
            Console.WriteLine("      ■    ■ ■■■■■■       ■■■■■■■   ■    ■        ■");
            Console.WriteLine("      ■■■■ ■   ■                ●■●       ■    ■        ■");
            Console.WriteLine("      ■    ■      ■               ● ■ ●      ■    ■        ■");
            Console.WriteLine("      ■    ■ ■■■■■■         ●  ■  ●     ■    ■        ■");
            Console.WriteLine("      ■■■■      ■             ●   ■   ■    ■    ■        ■");
            Console.WriteLine("      ■    ■      ■            ■    ■         ■    ■        ■");
            Console.WriteLine("      ■    ■      ■                  ■               ■        ■ ");
            Console.WriteLine("     ■     ■      ■                  ■           ●  ■          ");
            Console.WriteLine("    ■    ■■ ■■■■■■             ■              ●         ●");
            Console.ResetColor();
        }
    }
}
