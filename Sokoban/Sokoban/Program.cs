﻿using Sokoban.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Program
    {
        internal Game Game
        {
            get => default(Game);
            set
            {
            }
        }

        static void Main(string[] args)
        {
            Game g = new Game();
        }
    }
}
