using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class UICounterDisplay : MonoBehaviour
    {
        [SerializeField] private Text _textDisplay;
        private int _count = 0;

        private void Start()
        {
            _count = 0;
            SetCount(_count.ToString());
        }

        public void ResetCount()
        {
            _count = 0;
            SetCount(_count.ToString());
        }
        
        public void IncrementCount()
        {
            _count++;
            SetCount(_count.ToString());
        }

        private void SetCount(string count)
        {
            
            _textDisplay.text = count.ToString();
        }
    }
}