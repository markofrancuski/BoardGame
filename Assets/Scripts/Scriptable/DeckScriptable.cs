using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "Assets/Deck", order = 0)]
public class DeckScriptable : ScriptableObject
{
    [Header("Deck Basic Information")]
    public string DeckName = "Deck 1";
    public int MaxCardsInDeck;
    public int CurrentCardsInDeck => Cards.Count;

    [Header("Deck Minimum Requirements")]
    public int MinimumPawnCardsInDeck = 5;
    public int MinimumTerrainCardsInDeck = 5;
    public int MinimumSpellCardsInDeck = 5;

    [SerializeField]
    private Queue<CardBaseScriptable> Cards = new Queue<CardBaseScriptable>();
    [SerializeField]
    private List<CardBaseScriptable> _cardsList = new List<CardBaseScriptable>();

    public void AddCard(CardBaseScriptable cardToAdd)
    {
        if (Cards.Count >= MaxCardsInDeck)
        {
            Debug.LogWarning($"Cannot Add Card limit is hit!");
            return;
        }
        Cards.Enqueue(cardToAdd);
    }
    public void RemoveCard(CardBaseScriptable cardToRemove)
    {
        if(Cards.Count > 0)
        {
            CardBaseScriptable removedCard = Cards.Dequeue();
            Debug.LogWarning($"Card Removed from the Deck");
        }
        else
        {
            Debug.LogWarning($"Cannot Remove Card from the Deck");

        }
    }
    public CardBaseScriptable DrawCard()
    {
        CardBaseScriptable removedCard = null;
        if (Cards.Count > 0)
        {
            removedCard = Cards.Dequeue();
            Debug.LogWarning($"Card Removed from the Deck");
        }
        else
        {
            Debug.LogWarning($"Cannot Remove Card from the Deck");
        }
        return removedCard;
    }
    public void UpdateCardsInDeck(List<CardBaseScriptable> cards)
    {
        if(cards.Count > MaxCardsInDeck)
        {
            Debug.LogWarning($"Cannot Update Deck{DeckName}, max cards exceeded!");
            return;
        }
        _cardsList = cards;
    }

    public void ShuffleDeck(int guarrantedPawn = 1)
    {
        List<CardBaseScriptable> guarrantedPawns = new List<CardBaseScriptable>(guarrantedPawn);



    }

}
