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
        public Action<MatchingCardEntity> OnCardSelected;
        
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private List<DifficultyGrids> _gridTypes;

        public int NumberOfCards { private set; get; }
        
        private List<MatchingCardEntity> _cards;
        private ICardFactory _factory;
       
        public void Initialize(MatchingGameData data, Difficulty difficulty)
        {
            NumberOfCards = GetCardCountAndUpdateGridLayout(difficulty);
            var deck = data.BuildDeck(NumberOfCards);
            
            _factory = gameObject.GetComponent<ICardFactory>();
            
            CreateCardsAndSetParent(deck, data.BackCardTexture, data.FaceCardTexture);
            
            HandleIntro();
        }

        private void CreateCardsAndSetParent(List<MatchingCard> deck, Texture2D backTexture, Texture2D faceTexture)
        {
            _cards = new List<MatchingCardEntity>(deck.Count);
            for (int i = 0; i < deck.Count; i++)
            {
                int index = i;                    
                var cardData = deck[i];              

                var args = new CardContext(
                    parent: _gridLayoutGroup.transform,
                    back: backTexture,
                    face: faceTexture,
                    value: cardData.Sprite,
                    matchId: cardData.MatchID,
                    onClick: card => OnCardClicked(card)
                );

                var card = _factory.Create(args);
                card.SetIsInteractable(false);
                
                _cards.Add(card);
            }
        }
        
        private void OnCardClicked(MatchingCardEntity card)
        {
            OnCardSelected?.Invoke(card);
            card.FlipCardFaceUp();
        }


        private int GetCardCountAndUpdateGridLayout(Difficulty difficulty)
        {
            int idx = _gridTypes.FindIndex(p => p.Difficulty == difficulty);
            Vector2 gridSize = _gridTypes[idx].GridSize;
            
            int numberOfCards = (int)(gridSize.x * gridSize.y);
            
            SetGridRowCount((int)gridSize.x);
            
            return numberOfCards;
        }

        private void SetGridRowCount(int rowCount)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            _gridLayoutGroup.constraintCount = rowCount;
        }

        public void ClearBoard()
        {
            if (_gridLayoutGroup != null)
            {
                foreach (var card in _gridLayoutGroup.GetComponentsInChildren<MatchingCardEntity>())
                {
                    Destroy(card.gameObject);
                }
            }
        }

        public void HandleIntro()
        {
            StartCoroutine(nameof(ShowThenHideCards));
        }

        public IEnumerator ShowThenHideCards()
        {
            yield return new WaitForSeconds(1.5f);
            foreach (var card in _cards)
            {
                card.FlipCardFaceUp();
                yield return new WaitForSeconds(0.05f);
            }
            
            yield return new WaitForSeconds(3);
            
            foreach (var card in _cards)
            {
                card.FlipCardFaceDown();
                yield return new WaitForSeconds(0.05f);
            }

            foreach (var card in _cards)
            {
                card.SetIsInteractable(true);
            }
        }
    }
}