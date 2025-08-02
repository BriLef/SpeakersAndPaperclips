using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct CardContext
{
    public readonly Transform Parent;
    public readonly Texture2D Back;
    public readonly Texture2D Face;
    public readonly int MatchId;
    public readonly System.Action<MatchingCardEntity> OnClick;

    public CardContext(Transform parent, Texture2D back, Texture2D face, int matchId,
        System.Action<MatchingCardEntity> onClick)
    {
        Parent = parent; 
        Back = back; 
        Face = face; 
        MatchId = matchId; 
        OnClick = onClick;
    }
}

public interface ICardFactory 
{
    MatchingCardEntity Create(CardContext ctx);
}
