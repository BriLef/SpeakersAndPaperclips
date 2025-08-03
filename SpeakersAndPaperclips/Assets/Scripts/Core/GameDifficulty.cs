using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
   public enum Difficulty
    {
        VeryEasy,
        Easy,
        Medium,
        Hard,
        VeryHard
    }

    public class GameDifficulty : MonoBehaviour
    {
        [SerializeField] private Selector _selector;

        public void IncreaseDifficulty()
        {
            _selector.IncrementSelection();
        }
        
        public Difficulty GetDifficulty()
        {
            try
            {
                Difficulty diff;
                if (Enum.TryParse(_selector.GetCurrentOptionString(), out diff))
                {
                    return diff;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            return Difficulty.VeryEasy; // as a fallback incase the string could not be parsed to an enum.
        }
    }
}