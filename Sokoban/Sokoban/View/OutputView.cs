﻿using Sokoban.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sokoban
{
    public class OutputView
    {
        public OutputView()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("| Welkom bij Sokoban!          |   Doel van het spel     |");
            Console.WriteLine("| Betekenis van de symbolen    |                         |");
            Console.WriteLine("|                              |   Duw met de truck      |");
            Console.WriteLine("| Spatie: Outerspace           |   De krat(ten)          |");
            Console.WriteLine("|      #: Muur                 |   Naar de bestemming    |");
            Console.WriteLine("|      .: Vloer                |                         |");
            Console.WriteLine("|      O: Krat                 |                         |");
            Console.WriteLine("|      0: Krat op bestemming   |                         |");
            Console.WriteLine("|      x: Bestemming           |                         |");
            Console.WriteLine("|      @: Speler               |                         |");
            Console.WriteLine("----------------------------------------------------------");
        }

        public void PrintMaze(INonMoveableGameObject first)
        {
            Console.Clear();

            INonMoveableGameObject current = first;
            while (current.Down != null) // Loop down the list
            {
                while (current.Right != null) // Loop to the last item
                {
                    printSymbol(current);
                    current = current.Right;
                }
                printSymbol(current);
                while (current.Left != null) // Loop back to begin
                {
                    current = current.Left;
                }
                System.Console.WriteLine("");
                current = current.Down;
            }
            while (current.Right != null) // Loop to the last item
            {
                printSymbol(current);
                current = current.Right;
            }
            printSymbol(current);
        }

        public void printSymbol(INonMoveableGameObject current)
        {
            if(current.Player != null)
            {
                System.Console.Write(GetSymbol("Player"));
            }
            else
            {
                System.Console.Write(GetSymbol(current.GetType().Name));
            }
        }
        public char GetSymbol(String className)
        {
            char symbol = ' ';
            switch (className)
            {
                case "Floor":
                    symbol = '.';
                    break;
                case "Wall":
                    symbol = '#';
                    break;
                case "Crate":
                    symbol = 'o';
                    break;
                case "Player":
                    symbol = '@';
                    break;
                case "Destination":
                    symbol = 'x';
                    break;
            }
            return symbol;
        }
    }
}