using System;
using System.Collections.Generic;
using MatchingGame.Scripts.SOs;
using UnityEngine;
using Core;

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

            GetCardCountWithDifficulty(_difficulty.GetDifficulty());
            
            _gameController = new MatchingGameController(_gameData, 5);
        }

        private void ExitGame()
        {
            _pageSelector.SelectPageWithString("Home");
        }

        private int GetCardCountWithDifficulty(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.VeryEasy:
                    return 4;
                case Difficulty.Easy:
                    return 8;
                case Difficulty.Medium:
                    return 14;
                case Difficulty.Hard:
                    return 22;
                case Difficulty.VeryHard:
                    return 32;
            }

            return 4;
        }
    }
}