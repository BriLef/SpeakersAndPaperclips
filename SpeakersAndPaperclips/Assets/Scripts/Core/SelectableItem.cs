using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core
{
    public class SelectableItem : MonoBehaviour, IPointerClickHandler
    {
        public System.Action<SelectableItem> OnSelect;
        
        [SerializeField] private string _valueWhenSelected;
        public string Value => _valueWhenSelected;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSelect?.Invoke(this);
        }
    }
}