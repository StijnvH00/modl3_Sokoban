﻿using Sokoban.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sokoban
{
    public class Parser 
    {

        private Maze _maze;
        private int _level;
        public Parser(int level)
        {
            _level = level;
        }

        public Maze ReadFile()
        {
            _maze = new Maze();

            String LevelPath = @"Doolhof\doolhof" + _level + ".txt";

            String[] readString = System.IO.File.ReadAllLines(LevelPath);

            int width = 0;
            int height = readString.Length;
            for(int i = 0; i < height; i++) // Pak de langste lengte die een lijn in het tekstbestand heeft
            {
                if(readString[i].Length > width)
                {
                    width = readString[i].Length;
                }
            }

            NonMoveableGameObject[,] levelArray = new NonMoveableGameObject[width, height];
            for(int y = 0; y < height; y++) // Full Multidimensional Array
            {
                for(int x = 0; x < width; x++)
                {
                    try
                    {
                        NonMoveableGameObject tempObject = null;
                        switch (readString[y].ElementAt(x))
                        {
                            case '.':
                                tempObject = new Floor();
                                break;
                            case '#':
                                tempObject = new Wall();
                                break;
                            case 'o':
                                tempObject = new Floor(); // Plaats crate hierin
                                tempObject.Crate = new Crate();
                                break;
                            case 'x':
                                tempObject = new Destination();
                                break;
                            case '@':
                                tempObject = new Floor();
                                tempObject.Player = new Player();
                                break;
                            default:
                                tempObject = new Empty();
                                break;

                        }
                        levelArray[x, y] = tempObject;
                    }
                    catch
                    {
                        NonMoveableGameObject tempObject = new Empty();
                        levelArray[x, y] = tempObject;
                    }
                }
            }
            _maze.InitMaze(levelArray, width, height);
            return _maze;
        }
    }
}