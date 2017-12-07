using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris_V2
{
    class State
    {
        public int board { get; set; }
        public double w_up { get; set; }
        public double w_down { get; set; }
        public double w_left { get; set; }
        public double w_right { get; set; }
        public int reward { get; set; }
        public int last_key { get; set; }


        int count = new int();
        int keyboard = 0;
        uint couleur = new uint();
        int pos_x = 841;
        int pos_y = 400;
        int height = 20;
        int width = 10;
        int size = 19;
        int[] raw_board = new int[200];

        public int State_Board()
        {
            //Console.WriteLine("INIT BOARD");
            for (int i = pos_x; i < (pos_x + width * size); i += size)
            {
                for (int j = pos_y; j < (pos_y + height * size); j += size)
                {
                    couleur = Win32.GetPixelColor(i, j);
                    //Console.WriteLine(couleur);
                    if (couleur == 0)
                        raw_board[count] = 0;
                    else
                        raw_board[count] = 1;
                    keyboard += raw_board[count];
                }

            }
            return keyboard;
        }

        public void Key_pressing(State that)
        {
            int tot = (int)that.w_right * 100 + (int)that.w_left * 100 + (int)that.w_up * 100 + (int)that.w_down * 100;
            Random rnd = new Random();
            int key = rnd.Next(0, tot);
            if (key <= (int)that.w_right * 100)
                key = 1;
            else if (key <= (int)that.w_right * 100 + (int)that.w_left * 100)
                key = 2;
            else if (key <= (int)that.w_right * 100 + (int)that.w_left * 100 + (int)that.w_up * 100)
                key = 3;
            else if (key <= (int)that.w_right * 100 + (int)that.w_left * 100 + (int)that.w_up * 100 + (int)that.w_down * 100)
                key = 4;
            else
                key = 10;

            that.last_key = key;
            switch (key)
            {
                case 1:
                    Console.WriteLine("right");
                    SendKeys.SendWait("{RIGHT}");
                    break;
                case 2:
                    Console.WriteLine("left");
                    SendKeys.SendWait("{LEFT}");
                    break;
                case 3:
                    Console.WriteLine("up");
                    SendKeys.SendWait("{UP}");
                    break;
                case 4:
                    Console.WriteLine("down");
                    SendKeys.SendWait("{DOWN}");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }


        public void Calcul_Reward(State that, int score)
        {
            reward = 0;
            int is_reward = 0;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int temp = raw_board[i + j];
                    if (is_reward == 10)
                        that.reward += 100;
                    switch (that.last_key)
                    {
                        case 1:
                            that.w_right += 0.01;
                            break;
                        case 2:
                            that.w_left += 0.01;
                            break;
                        case 3:
                            that.w_up += 0.01;
                            break;
                        case 4:
                            that.w_down += 0.01;
                            break;
                    }
                    is_reward = 0;
                }
            }
        }
    }
}
