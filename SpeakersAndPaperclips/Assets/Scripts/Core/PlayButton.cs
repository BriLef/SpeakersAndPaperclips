using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayButton : MonoBehaviour, IPointerClickHandler
{
    public System.Action OnPlayPressed;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPlayPressed?.Invoke();
    }
}
