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
        
        private MatchingGameController _gameController;
        
        private void Start()
        {
            _playButton.OnPlayPressed += StartGame;
        }

        private void StartGame()
        {
            _gameController = new MatchingGameController();
            //TODO: get number of cards based on difficulty.
            _gameController.Initialize(_gameData, 5);
        }
    }
}