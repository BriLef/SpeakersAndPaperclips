using System;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Scripts.SOs
{
    [Serializable]
    public readonly struct MatchingCard
    {
        public readonly Texture2D Sprite;
        public readonly int MatchID;

        public MatchingCard(Texture2D sprite, int matchID)
        {
            Sprite = sprite;
            MatchID = matchID;
        }
    }
    
    [CreateAssetMenu(fileName = "NewMatchingGameData", menuName = "Prototype/GameData/MatchingGame", order = 1)]
    public class MatchingGameData : ScriptableObject
    {
        public string ThemeName = "New Theme";
        public Texture2D BackCardTexture;
        public Texture2D FaceCardTexture;
        public List<Texture2D> PossibleMatches;

        public List<MatchingCard> BuildDeck(int totalCards)
        {
            if (totalCards % 2 != 0)
                throw new ArgumentException("totalCards must be even");

            var deck = new List<MatchingCard>(totalCards);

            var faces = (PossibleMatches == null || PossibleMatches.Count == 0)
                ? new List<Texture2D> { FaceCardTexture }
                : PossibleMatches;

            int pairsNeeded = totalCards / 2;

            for (int p = 0; p < pairsNeeded; p++)
            {
                var face = faces[p % faces.Count];
                int id = p;                   // stable id per pair slot
                deck.Add(new MatchingCard(face, id));
                deck.Add(new MatchingCard(face, id));
            }
            
            for (int i = deck.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                (deck[i], deck[j]) = (deck[j], deck[i]);
            }

            return deck;
        }
    }
}