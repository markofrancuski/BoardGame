using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "Assets/Deck", order = 0)]
public class DeckScriptable : ScriptableObject
{
    [SerializeField]
    private List<CardBaseScriptable> Cards = new List<CardBaseScriptable>();

    public int MaxCardsInDeck;
    public int CurrentCardsInDeck => Cards.Count;

    public void RemoveCard()
    {

    }

    public void AddCard(CardBaseScriptable cardToAdd)
    {

    }

}
