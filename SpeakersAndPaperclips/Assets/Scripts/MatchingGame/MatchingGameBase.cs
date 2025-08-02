using System;
using System.Collections.Generic;
using MatchingGame.Scripts.SOs;
using UnityEngine;
using Core;
using UnityEngine.UI;

namespace MatchingGame.Scripts
{
    public class MatchingGameBase : MonoBehaviour
    {
        [Header("Core Variables")] 
        [SerializeField] private MatchingGameData _gameData;
        [SerializeField] private GameDifficulty _difficulty;
        [SerializeField] private PlayButton _playButton;
        [SerializeField] private GameExitButton _exitButton;
        [SerializeField] private MatchingCardEntity _cardEntityPrefab;
        [SerializeField] private MatchingBoardView _gameBoard;
        private PageSelector _pageSelector;
        
        private MatchingGameController _gameController;
        
        private void Start()
        {
            _playButton.OnPlayPressed += StartGame;
            _exitButton.OnExitPressed += ExitGame;
            
            _pageSelector = GetComponent<PageSelector>();
        }

        private void StartGame()
        {
            _pageSelector.SelectPageWithString("Game");
    
            _gameController = new MatchingGameController(_gameData, _cardEntityPrefab, _gameBoard, _difficulty.GetDifficulty());
        }

        private void ExitGame()
        {
            _pageSelector.SelectPageWithString("Home");
        }

       
    }
}