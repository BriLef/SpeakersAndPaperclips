using System;
using System.Collections;
using System.Collections.Generic;
using MatchingGame.Scripts.SOs;
using UnityEngine;
using UnityEngine.UI;
using Core;

namespace MatchingGame.Scripts
{
    [Serializable]
    public struct DifficultyGrids
    {
        public Difficulty Difficulty;
        public Vector2 GridSize;
    }
    
    public class MatchingBoardView : MonoBehaviour
    {
        [SerializeField] private List<DifficultyGrids> _gridTypes;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        private ICardFactory _factory;
        
        public void Initialize(MatchingGameData data, MatchingCardEntity cardPrefab, Difficulty difficulty)
        {
            int numberOfCards = GetCardCountWithDifficulty(difficulty);
            var deck = data.BuildDeck(numberOfCards);
            
            _factory = gameObject.AddComponent<UICardFactory>();
            
           
        }
        
        
        private int GetCardCountWithDifficulty(Difficulty difficulty)
        {
            int idx = _gridTypes.FindIndex(p => p.Difficulty == difficulty);
            Vector2 gridSize = _gridTypes[idx].GridSize;
            
            int numberOfCards = (int)(gridSize.x * gridSize.y);

            return numberOfCards;
        }
        
        /*[SerializeField] private MatchingGameData data;
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private int totalCards = 12;
        [SerializeField] private float mismatchHideDelay = 0.5f;

        // Assign either UICardFactory or PooledCardFactory in the Inspector
        [SerializeField] private MonoBehaviour cardFactoryBehaviour;
        private ICardFactory _factory;

        private MatchingGameController _game;
        private List<MatchingCardEntity> _cards;

        void Awake() => _factory = (ICardFactory) cardFactoryBehaviour;

        void Start()
        {
            var deck = data.BuildDeck(Mathf.Max(2, totalCards + totalCards % 2));
            var idByFace = new Dictionary<Texture2D, int>();
            var matchIds = new int[deck.Count];
            int nextId = 0;
            for (int i = 0; i < deck.Count; i++)
            {
                var face = deck[i];
                if (!idByFace.TryGetValue(face, out var id)) idByFace[face] = id = nextId++;
                matchIds[i] = id;
            }

            _game = new MatchingGame(matchIds);
            _game.PairMismatched += OnPairMismatched;
            _game.PairMatched += (a, b) =>
            {
                _cards[a].SetMatched();
                _cards[b].SetMatched();
            };
            _game.CardFlippedUp += i => _cards[i].FlipUp(this);
            _game.CardFlippedDown += i => _cards[i].FlipDown(this);

            _cards = new List<MatchingCardEntity>(deck.Count);
            for (int i = 0; i < deck.Count; i++)
            {
                int idx = i; // capture
                var ctx = new CardSpawnContext(
                    parent: grid.transform,
                    back: data.BackCardTexture,
                    face: deck[i],
                    matchId: matchIds[i],
                    onClicked: _ => _game.Select(idx));

                var card = _factory.Create(ctx);
                _cards.Add(card);
            }
        }

        private void OnPairMismatched(int a, int b)
        {
            SetInteractable(false);
            StartCoroutine(Delay());

            System.Collections.IEnumerator Delay()
            {
                yield return new WaitForSecondsRealtime(mismatchHideDelay);
                _game.FlipDownUnmatched();
                SetInteractable(true);
            }
        }

        private void SetInteractable(bool v)
        {
            foreach (var c in _cards)
                if (!c.IsMatched)
                    c.SetInteractable(v);
        }

        public void ClearBoard()
        {
            if (_cards == null) return;
            foreach (var c in _cards) _factory.Release(c);
            _cards.Clear();
        }*/
    }
}