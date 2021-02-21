using Enums.Card;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Deck", menuName = "Assets/Deck", order = 0)]
public class DeckScriptable : ScriptableObject
{
    [Header("Deck Basic Information")]
    public string DeckName = "Deck 1";
    public static int MaxCardsInDeck = 20;
    public int CurrentCardsInDeck => _cards.Count;

    [SerializeField]
    private List<CardBaseScriptable> _cards = new List<CardBaseScriptable>();
    public List<CardBaseScriptable> Cards => _cards;

    public void AddCard(CardBaseScriptable cardToAdd)
    {
        if (_cards.Count >= MaxCardsInDeck)
        {
            Debug.LogWarning($"Cannot Add Card limit is hit!");
            return;
        }
        _cards.Add(cardToAdd);
    }
    public void RemoveCard(CardBaseScriptable cardToRemove)
    {
        if (_cards.Count > 0)
        {
            _cards.Remove(cardToRemove);
            Debug.LogWarning($"Card Removed from the Deck");
        }
        else
        {
            Debug.LogWarning($"Cannot Remove Card from the Deck");
        }
    }
    public void UpdateCardsInDeck(List<CardBaseScriptable> cards)
    {
        if (cards.Count > MaxCardsInDeck)
        {
            Debug.LogWarning($"Cannot Update Deck{DeckName}, max cards exceeded!");
            return;
        }
        _cards = cards;
    }
    
    public List<CardBaseScriptable> GetAllCardsByType(CardType type)
    {
        if(_cards.Count <= 0)
        {
            Debug.LogWarning($"Cannot Get Cards with type:{type}, list is empty");
        }

        List<CardBaseScriptable> cards = new List<CardBaseScriptable>();

        foreach (CardBaseScriptable card in _cards)
        {
            if (card.CardType == type) cards.Add(card);
        }

        return cards;
    }
    
}