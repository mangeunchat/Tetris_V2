using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace Tetris_V2
{
    class Program
    {
        List<State> goty = new List<State>();
        int score = 0;

        public void change_state()
        {
            bool connu = false;
            State this_state = new State();
            this_state.board = this_state.State_Board();
            foreach (State astat in goty)
            {
                if (this_state.State_Board() == astat.board)
                {
                    this_state.board = astat.board;
                    this_state.w_up = astat.w_up;
                    this_state.w_down = astat.w_down;
                    this_state.w_left = astat.w_left;
                    this_state.w_right = astat.w_right;
                    connu = true;
                }
            }
            if (connu == false)
            {
                this_state.board = this_state.State_Board();
                this_state.w_up = 0.25;
                this_state.w_down = 0.25;
                this_state.w_left = 0.25;
                this_state.w_right = 0.25;
            }
            this_state.Key_pressing(this_state);
            this_state.Calcul_Reward(this_state, score); // + change_weights()
            System.Diagnostics.Debug.Write(score);
            if (connu == false)
            {
                goty.Add(new State() { board = this_state.board, last_key = this_state.last_key, reward = this_state.reward, w_right = this_state.w_right, w_left = this_state.w_left, w_up = this_state.w_up, w_down = this_state.w_down });
            }
            else
            {
                foreach (State astat in goty)
                {
                    if (this_state.State_Board() == astat.board)
                    {
                        astat.w_up = this_state.w_up;
                        astat.w_down = this_state.w_down;
                        astat.w_left = this_state.w_left;
                        astat.w_right = this_state.w_right;
                    }
                }
            }
           
        }

        static void Main(string[] args)
        {
            Program Player_1 = new Program();
            System.Threading.Thread.Sleep(2000);
            for (int i = 0; i < 10000; i++)
            {
                //if (Keyboard.IsKeyDown(Key.Q))
                //{
                //    Environment.Exit(1);
                //}
                Player_1.change_state();
                System.Threading.Thread.Sleep(100);
            }

        }
    }
}
