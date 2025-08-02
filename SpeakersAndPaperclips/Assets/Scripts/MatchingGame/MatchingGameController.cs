using System;
using System.Collections.Generic;
using Core;
using MatchingGame.Scripts.SOs;
using UnityEngine;

namespace MatchingGame.Scripts
{
   public class MatchingGameController
   {
        private MatchingBoardView _gameBoard;
        private int TotalPairs = 0;
        public MatchingGameController(MatchingGameData gameData, MatchingCardEntity cardPrefab, MatchingBoardView gameBoard, Difficulty difficulty)
        {
            _gameBoard = gameBoard;
            
            _gameBoard.Initialize(gameData, cardPrefab, difficulty);
        }
   }
}