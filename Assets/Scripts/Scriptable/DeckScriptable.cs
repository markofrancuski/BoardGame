using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "Assets/Deck", order = 0)]
public class DeckScriptable : ScriptableObject
{

    public string DeckName = "Deck 1";
    public int MaxCardsInDeck;
    public int CurrentCardsInDeck => Cards.Count;

    [SerializeField]
    private List<CardBaseScriptable> Cards = new List<CardBaseScriptable>();
    public void RemoveCard(CardBaseScriptable cardToRemove)
    {
        if (Cards.Remove(cardToRemove))
        {
            Debug.LogWarning($"Card Removed from the Deck");
        }
        else
        {
            Debug.LogWarning($"Cannot Remove Card from the Deck");
        }
    }

    public void AddCard(CardBaseScriptable cardToAdd)
    {

    }

    public void UpdateCards(List<CardBaseScriptable> cards)
    {
        if(cards.Count > MaxCardsInDeck)
        {
            Debug.LogWarning($"Cannot Update Deck{DeckName}, max cards exceeded!");
            return;
        }
        Cards = cards;
    }

}
