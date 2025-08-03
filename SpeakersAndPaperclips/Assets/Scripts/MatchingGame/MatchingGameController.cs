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
        private int _totalPairsLeftToWin = 0;
        
        private MatchingCardEntity _currentSelectedCard;
        
        public MatchingGameController(MatchingGameData gameData, MatchingBoardView gameBoard, Difficulty difficulty)
        {
            _gameBoard = gameBoard;
            _gameBoard.ClearBoard();
            _gameBoard.Initialize(gameData, difficulty);
            _gameBoard.OnCardSelected += OnCardSelected;
            
            _totalPairsLeftToWin = _gameBoard.NumberOfCards / 2;
            
        }

        private void OnCardSelected(MatchingCardEntity card)
        {
            card.SetIsInteractable(false);
            
            if (_currentSelectedCard == null) // is first card selected 
            {
                _currentSelectedCard = card;
            }
            else // is second card selected, will need to check if match by comparing matchID
            {
                if (_currentSelectedCard.MatchID == card.MatchID) // a match
                {
                    _totalPairsLeftToWin--;
                    if (_totalPairsLeftToWin <= 0)
                    {
                        
                    }
                } 
                else 
                {
                    _currentSelectedCard.FlipCardFaceDownWithDelay(1.5f);
                    _currentSelectedCard.SetIsInteractable(true);
                    
                    card.FlipCardFaceDownWithDelay(1.5f);
                    card.SetIsInteractable(true);
                }

                _currentSelectedCard = null;
            }
        }
   }
}