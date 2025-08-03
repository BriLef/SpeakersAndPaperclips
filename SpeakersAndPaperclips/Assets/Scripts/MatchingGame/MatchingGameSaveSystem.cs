using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace MatchingGame.Scripts
{
    public static class MatchingGameSaveSystem
    {
        [System.Serializable]
        public class MatchingCardSaveData
        {
            public int matchID;
            public bool isMatched;
            public Texture2D texture;
        }

        [System.Serializable]
        public class GameBoardSaveData
        {
            public Difficulty GameDifficulty;
            public List<MatchingCardSaveData> Cards;
            public int TotalPairsLeftToWin;
            public int TurnsTaken; // NEW

            public GameBoardSaveData(List<MatchingCardSaveData> cards, int pairsLeft, int turnsTaken)
            {
                Cards = cards;
                TotalPairsLeftToWin = pairsLeft;
                TurnsTaken = turnsTaken;
            }
        }

        public static void SaveGame(MatchingBoardView board, string difficulty, int pairsLeft, int turnsTaken)
        {
            List<MatchingCardSaveData> allCardData = new List<MatchingCardSaveData>();

            var cards = board.Cards;

            foreach (var card in cards)
            {
                MatchingCardSaveData cardSaveData = new MatchingCardSaveData();
                cardSaveData.matchID = card.MatchID;
                cardSaveData.isMatched = card.IsMatched;
                cardSaveData.texture = card.ValueTexture;

                allCardData.Add(cardSaveData);
            }

            GameBoardSaveData saveData = new GameBoardSaveData(allCardData, pairsLeft, turnsTaken);

            string json = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(difficulty.ToString(), json);
            PlayerPrefs.Save();
        }

        public static GameBoardSaveData LoadGame(string difficulty)
        {
            string saveKey = difficulty.ToString();
            if (PlayerPrefs.HasKey(saveKey))
            {
                string json = PlayerPrefs.GetString(saveKey);
                GameBoardSaveData saveData = JsonUtility.FromJson<GameBoardSaveData>(json);

                return saveData;
            }

            return null;
        }

        public static void DeleteSave(string difficulty)
        {
            if(PlayerPrefs.HasKey(difficulty))
                PlayerPrefs.DeleteKey(difficulty);
        }
        
        public static bool DoesSaveExists(string difficulty)
        {
            return PlayerPrefs.HasKey(difficulty);
        }
}
}