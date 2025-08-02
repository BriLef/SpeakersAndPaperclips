using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICardFactory : MonoBehaviour, ICardFactory
{
    [SerializeField] private MatchingCardEntity _cardPrefab;
    
    public MatchingCardEntity Create(CardContext ctx)
    {
        var card = Instantiate(_cardPrefab, ctx.Parent);
        
        return card;
    }
}
