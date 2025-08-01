using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Selector : MonoBehaviour
    {
        [SerializeField] private List<SelectableItem> _options;
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
            _currentOption = newOption;
        }

        public string GetCurrentOptionString()
        {
            return _currentOption.Value.ToString();
        }
    }
}