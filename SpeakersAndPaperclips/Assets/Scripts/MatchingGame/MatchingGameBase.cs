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
        [SerializeField] private MatchingBoardView _gameBoard;
        
        [Header("Buttons")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _endScreenExitButton;

        
        [Header("Text")]
        [SerializeField] private UICounterDisplay turnCountTextDisplay;
        [SerializeField] private UICounterDisplay MatchesFoundTextDisplay;

        [Header("Audio")] 
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _musicTrack;
        [SerializeField] private AudioClip _correctPairSound;
        [SerializeField] private AudioClip _incorrectPairSound;
        [SerializeField] private AudioClip _gameWonSound;
        
        private PageSelector _pageSelector;
        private MatchingGameController _gameController;
        
        private void Start()
        {
            _playButton.onClick.AddListener(StartGame);
            
            _pageSelector = GetComponent<PageSelector>();
            
            _audioSource.clip = _musicTrack;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        private void StartGame()
        {
            _pageSelector.SelectPageWithString("Game");

            _gameController = new MatchingGameController(_gameData, _gameBoard, _difficulty.GetDifficulty());
           
            _gameController.OnGameWon += GameWon;
            
            ResetCounters();
            
            _exitButton.onClick.AddListener(()=>
            {
                _gameController.OnGameWon -= GameWon;
                ExitGame();
            });
            
            _playButton.onClick.RemoveAllListeners();
            _endScreenExitButton.onClick.RemoveAllListeners();
            _nextButton.onClick.RemoveAllListeners();
            
            _gameController.OnIncorrectTry += turnCountTextDisplay.IncrementCount;
            _gameController.OnMatchFound += turnCountTextDisplay.IncrementCount;
            
            _gameController.OnMatchFound += MatchesFoundTextDisplay.IncrementCount;
            
            _gameController.OnGameWon += () => { _audioSource.PlayOneShot(_gameWonSound);  };
            _gameController.OnMatchFound += () => { _audioSource.PlayOneShot(_correctPairSound); };
            _gameController.OnIncorrectTry += () => { _audioSource.PlayOneShot(_incorrectPairSound); };
        }

        private void ExitGame(bool destroyGame = false)
        {
            _pageSelector.SelectPageWithString("Home"); 
            
            _playButton.onClick.AddListener(StartGame);
            
            _exitButton.onClick.RemoveAllListeners();
            _endScreenExitButton.onClick.RemoveAllListeners();
            _nextButton.onClick.RemoveAllListeners();
            
            DestroyGame();
        }

        private void GameWon()
        {
            _pageSelector.SelectPageWithString("Completed");
            
            _endScreenExitButton.onClick.AddListener(()=>
            {
                _pageSelector.SelectPageWithString("Home");    
                _playButton.onClick.AddListener(StartGame);
            });
            
            _nextButton.onClick.AddListener(NextGamePressed);
            
            _playButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            
            DestroyGame();
        }
        
        private void NextGamePressed()
        {
            _difficulty.IncreaseDifficulty();
            
            StartGame();
        }

        private void DestroyGame()
        {
            _gameController.OnGameWon -= GameWon;
            _gameController.OnIncorrectTry -= turnCountTextDisplay.IncrementCount;
            _gameController.OnMatchFound -= turnCountTextDisplay.IncrementCount;
            
            _gameController.OnMatchFound -= MatchesFoundTextDisplay.IncrementCount;
            
            _gameBoard.ClearBoard();
            
            _gameController.DestroySelf();
            _gameController = null;
        }
        private void ResetCounters()
        {
            turnCountTextDisplay.ResetCount();
            MatchesFoundTextDisplay.ResetCount();
        }
    }
}