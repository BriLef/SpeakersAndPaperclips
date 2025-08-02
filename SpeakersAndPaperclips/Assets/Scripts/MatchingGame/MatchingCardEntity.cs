using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MatchingCardEntity : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _backImage;
    [SerializeField] private Image _faceImage;
    
    public int MatchID { get; private set; }
    public bool IsMatched { get; private set; }
    
    private bool _isFaceUp;
    
    public void Initialize(int matchId, Texture2D backTexture, Texture2D faceTexture)
    {
        if (!_backImage || !_faceImage)
        {
            Debug.LogError("No backImage or frontImage is set in inspector for card entity prefab.");
            return;
        }
        MatchID = matchId;
        _backImage.sprite = SpriteUtil.FromTexture(backTexture);
        _faceImage.sprite = SpriteUtil.FromTexture(faceTexture);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
