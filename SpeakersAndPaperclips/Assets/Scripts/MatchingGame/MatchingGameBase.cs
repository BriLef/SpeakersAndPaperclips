using System;
using System.Collections.Generic;
using MatchingGame.Scripts.SOs;
using UnityEngine;
using Core;
using UnityEngine.UI;

namespace MatchingGame.Scripts
{
    [Serializable]
    public struct GameTypes
    {
        public Difficulty Difficulty;
        public Vector2 GridSize;
    }
    public class MatchingGameBase : MonoBehaviour
    {
        [Header("Core Variables")] 
        [SerializeField] private MatchingGameData _gameData;
        [SerializeField] private GameDifficulty _difficulty;
        [SerializeField] private List<GameTypes> _gameTypesList;
        [SerializeField] private PlayButton _playButton;
        [SerializeField] private GameExitButton _exitButton;
        [SerializeField] private GridLayoutGroup _gridLayout;
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

            int cardCount = GetCardCountWithDifficulty(_difficulty.GetDifficulty());
            
            _gameController = new MatchingGameController(_gameData, cardCount);
        }

        private void ExitGame()
        {
            _pageSelector.SelectPageWithString("Home");
        }

        private int GetCardCountWithDifficulty(Difficulty difficulty)
        {
            
            int idx = _gameTypesList.FindIndex(p => p.Difficulty == difficulty);
            Vector2 gridSize = _gameTypesList[idx].GridSize;
            
            int numberOfCards = (int)(gridSize.x * gridSize.y);

            return numberOfCards;
        }
    }
}