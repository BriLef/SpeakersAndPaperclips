using System;
using System.Collections.Generic;
using Core;
using MatchingGame.Scripts.SOs;
using UnityEngine;

namespace MatchingGame.Scripts
{
   public class MatchingGameController
   {
        public Action OnMatchFound;
        public Action OnIncorrectTry;
        public Action OnGameWon;

        [SerializeField] private float _delayToFlipCards = 0.5f;
        private MatchingBoardView _gameBoard;
        private int _totalPairsLeftToWin = 0;
        private int _turnsTaken = 0;
        private Difficulty _currentDifficulty;
        private MatchingCardEntity _currentSelectedCard;
        
        public MatchingGameController(MatchingGameData gameData, MatchingBoardView gameBoard, Difficulty difficulty)
        {
            _gameBoard = gameBoard;
            _gameBoard.ClearBoard();
            _currentDifficulty = difficulty;

            MatchingGameSaveSystem.GameBoardSaveData saveData = null;
            
            if (MatchingGameSaveSystem.DoesSaveExists(_currentDifficulty.ToString()))//Save exists
            {
                saveData = MatchingGameSaveSystem.LoadGame(_currentDifficulty.ToString());
            }
            
            _gameBoard.Initialize(gameData, _currentDifficulty, saveData);
            _totalPairsLeftToWin = _gameBoard.NumberOfCards / 2;
            _gameBoard.OnCardSelected += OnCardSelected;
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
                    OnMatchFound?.Invoke();
                    _totalPairsLeftToWin--;
                    _turnsTaken++;
                    _currentSelectedCard.HideCardWithDelay(0.5f);
                    _currentSelectedCard.IsMatched = true;
                    
                    card.HideCardWithDelay(0.5f);
                    card.IsMatched = true;
                    
                    if (_totalPairsLeftToWin <= 0)
                    {
                        OnGameWon?.Invoke();
                    }
                } 
                else 
                {
                    _currentSelectedCard.FlipCardFaceDownWithDelay(_delayToFlipCards);
                    _currentSelectedCard.SetIsInteractable(true);
                    _turnsTaken++;
                    
                    card.FlipCardFaceDownWithDelay(_delayToFlipCards);
                    card.SetIsInteractable(true);
                    OnIncorrectTry?.Invoke();
                }

                _currentSelectedCard = null;
            }
        }

        public void DestroySelf()
        {
            _gameBoard.OnCardSelected -= OnCardSelected;
        }
        
        public void SaveBoard()
        {
            MatchingGameSaveSystem.SaveGame(_gameBoard, _currentDifficulty.ToString(), _totalPairsLeftToWin, _turnsTaken);
        }
   }
}