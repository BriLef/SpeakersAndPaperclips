using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Selector : MonoBehaviour
    {
        [SerializeField] private List<SelectableItem> _options;
        [SerializeField] private float _scaleWhenSelected = 1.5f;
        private SelectableItem _currentOption;
        
        private void Start()
        {
            foreach (var option in _options)
            {
                option.OnSelect += OnOptionChange;
            }
            
            OnOptionChange(_options[0]);
        }

        private void OnOptionChange(SelectableItem newOption)
        {
            if(_currentOption != null)
                _currentOption.transform.localScale = Vector3.one;
            
            _currentOption = newOption;
            newOption.transform.localScale = Vector3.one * _scaleWhenSelected;
        }

        public string GetCurrentOptionString()
        {
            return _currentOption.Value.ToString();
        }
        
        public void IncrementSelection()
        {
            int newIndex = _options.IndexOf(_currentOption) + 1;
            int finalIndex = Math.Clamp(newIndex, 0, _options.Count - 1);
            
            OnOptionChange(_options[finalIndex]);
        }
    }
}