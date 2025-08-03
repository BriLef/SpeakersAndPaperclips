using System;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MatchingCardEntity : MonoBehaviour, IPointerClickHandler
{
    public Action<MatchingCardEntity> OnCardClick;
    
    [SerializeField] private Image _backImage;
    [SerializeField] private Image _faceImage;
    [SerializeField] private Image _valueImage;
    public Texture2D ValueTexture;
    
    public bool IsMatched;
    public int MatchID { get; private set; }
    public bool _isInteractable { get; private set; }
    
    public void Initialize(int matchId, Texture2D backTexture, Texture2D faceTexture, Texture2D valueTexture)
    {
        if (!_backImage || !_faceImage)
        {
            Debug.LogError("No backImage or frontImage is set in inspector for card entity prefab.");
            return;
        }
        
        MatchID = matchId;
        ValueTexture = valueTexture;
        _backImage.sprite = SpriteUtil.FromTexture(backTexture);
        _faceImage.sprite = SpriteUtil.FromTexture(faceTexture);
        _valueImage.sprite = SpriteUtil.FromTexture(valueTexture);
    }

    public void FlipCardFaceUp()
    {
        _backImage.gameObject.SetActive(false);
        _faceImage.gameObject.SetActive(true);
        _valueImage.gameObject.SetActive(true);
    }

    public void FlipCardFaceDown()
    {
        _backImage.gameObject.SetActive(true);
        _faceImage.gameObject.SetActive(false);
        _valueImage.gameObject.SetActive(false);
    }

    public void SetIsInteractable(bool isInteractable)
    {
        _isInteractable = isInteractable;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isInteractable)
        {
            CancelInvoke();
            OnCardClick?.Invoke(this);
        }
            
    }

    public void FlipCardFaceDownWithDelay(float delay)
    {
        Invoke(nameof(FlipCardFaceDown), delay);
    }

    public void HideCardWithDelay(float delay)
    {
        Invoke(nameof(HideCard), delay);
    }

    public void HideCard()
    {
        _backImage.gameObject.SetActive(false);
        _faceImage.gameObject.SetActive(false);
        _valueImage.gameObject.SetActive(false);
    }

    public void DestroySelf()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
}
