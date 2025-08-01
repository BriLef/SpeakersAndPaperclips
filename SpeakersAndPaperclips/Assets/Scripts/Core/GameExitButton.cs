using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class GameExitButton : MonoBehaviour, IPointerClickHandler
    {
        public System.Action OnExitPressed;
       
        public void OnPointerClick(PointerEventData eventData)
        {
            OnExitPressed?.Invoke();
        }
    }
}