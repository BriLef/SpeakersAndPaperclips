using System;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Scripts.SOs
{
    [Serializable]
    public struct MatchingCardData
    {
        public Texture2D Sprite;
    }
    
    [CreateAssetMenu(fileName = "NewMatchingGameData", menuName = "Prototype/GameData/MatchingGame", order = 1)]
    public class MatchingGameData : ScriptableObject
    {
        public string ThemeName = "New Theme";
        public Texture2D DefaultCardTexture;
        public List<Texture2D> PossibleMatches;
    }
}