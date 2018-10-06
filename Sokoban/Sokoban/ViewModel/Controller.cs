﻿using Sokoban.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Sokoban
{
    public class Controller
    {
        private OutputView _outputview;
        private InputView _inputView;
        private bool _finished;
        public Maze Maze { get => _maze; set => _maze = value; }
        private Maze _maze;
        private int countDestination;

        public Controller()
        {
            _finished = false;
            _outputview = new OutputView();
            _inputView = new InputView();
        }

        public int AskLevel()
        {
            return _inputView.ReadLevel();
        }

        public void DrawMaze()
        {
            _finished = _outputview.PrintMaze(_maze.First);
        }

        public void GameLoop()
        {
            while (!_finished)
            {
                WaitForInput();
                DrawMaze();
            }
            _outputview.FinishedGame();
        }

        public void WaitForInput()
        {
            var keySwitch = Console.ReadKey().Key;
            System.Console.WriteLine("move");
            switch (keySwitch)
            {
                case ConsoleKey.UpArrow:
                    if (CheckCollision(1))
                    {
                        _maze.TileWithPlayer.Up.Player = _maze.TileWithPlayer.Player;
                        _maze.TileWithPlayer.Player = null;
                        _maze.TileWithPlayer = _maze.TileWithPlayer.Up;
                        _maze.UpdatePlayerPosition();
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (CheckCollision(2))
                    {
                        _maze.TileWithPlayer.Right.Player = _maze.TileWithPlayer.Player;
                        _maze.TileWithPlayer.Player = null;
                        _maze.TileWithPlayer = _maze.TileWithPlayer.Right;
                        _maze.UpdatePlayerPosition();
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (CheckCollision(3))
                    {
                        _maze.TileWithPlayer.Down.Player = _maze.TileWithPlayer.Player;
                        _maze.TileWithPlayer.Player = null;
                        _maze.TileWithPlayer = _maze.TileWithPlayer.Down;
                        _maze.UpdatePlayerPosition();
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (CheckCollision(4))
                    {
                        _maze.TileWithPlayer.Left.Player = _maze.TileWithPlayer.Player;
                        _maze.TileWithPlayer.Player = null;
                        _maze.TileWithPlayer = _maze.TileWithPlayer.Left;
                        _maze.UpdatePlayerPosition();
                    }
                    break;
                case ConsoleKey.S:
                    Console.WriteLine("SHUTDOWN");
                    System.Threading.Thread.Sleep(1000);
                    System.Environment.Exit(1);
                    break;
                default:
                    break;
            }
        }


        public bool CheckCollision(int dir) // 1: up, 2: right, 3: down, 4: left
        {
            INonMoveableGameObject tileOfDir = GetTileByDir(dir, _maze.TileWithPlayer);
            if (tileOfDir.GetType().Name == "Wall")
            {
                return false;
            }
            if (tileOfDir.Crate != null)
            {
                if(GetTileByDir(dir, tileOfDir).GetType().Name != "Wall" && GetTileByDir(dir, tileOfDir).Crate == null) {
                    MoveCrate(tileOfDir, dir);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public void MoveCrate(INonMoveableGameObject crateTile, int dir)
        {
            switch (dir)
            {
                case 1:
                    crateTile.Up.Crate = crateTile.Crate;
                    Console.WriteLine(crateTile.Up.IsActive);
                    crateTile.Crate = null;
                    break;
                case 2:
                    crateTile.Right.Crate = crateTile.Crate;
                    crateTile.Crate = null;
                    break;
                case 3:
                    crateTile.Down.Crate = crateTile.Crate;
                    crateTile.Crate = null;
                    break;
                case 4:
                    crateTile.Left.Crate = crateTile.Crate;
                    crateTile.Crate = null;
                    break;
            }
        }

        public INonMoveableGameObject GetTileByDir(int dir, INonMoveableGameObject current)
        {
            INonMoveableGameObject tileOfDir = null;
            switch (dir)
            {
                case 1:
                    tileOfDir = current.Up;
                    break;
                case 2:
                    tileOfDir = current.Right;
                    break;
                case 3:
                    tileOfDir = current.Down;
                    break;
                case 4:
                    tileOfDir = current.Left;
                    break;
            }
            return tileOfDir;
        } 
    }
}