using System.Collections.Generic;
using UnityEngine;
using MEC;
using Random = UnityEngine.Random;
using System;
using UnityEngine.Events;
using Enums;
using System.Linq;

[CreateAssetMenu(fileName = "Deck", menuName = "Assets/Deck", order = 0)]
public class DeckScriptable : ScriptableObject
{
    [Header("Deck Basic Information")]
    public string DeckName = "Deck 1";
    public static int MaxCardsInDeck = 20;
    public int CurrentCardsInDeck => _cards.Count;

    [SerializeField]
    private List<CardBaseScriptable> _cards = new List<CardBaseScriptable>();

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
    
    public List<CardBaseScriptable> ShuffleDeck()
    {
        if (_cards.Count <= 0)
        {
            Debug.LogWarning($"Cannot shuffle deck, list is empty");
            return null;
        }

        int maxCards = _cards.Count;

        CardBaseScriptable[] shuffledCards = new CardBaseScriptable[maxCards];

        for (int i = 0; i < maxCards; i++)
        {
            shuffledCards[i] = _cards[i];
        }

        int iteration = Random.Range(50, 101);

        while (iteration > 0)
        {
            int firstIndex = Random.Range(0, maxCards);

            int secondIndex = Random.Range(0, maxCards);
            while(secondIndex == firstIndex)
            {
                secondIndex = Random.Range(0, maxCards);
            }

            CardBaseScriptable firstCard = shuffledCards[firstIndex];
            CardBaseScriptable secondCard = shuffledCards[secondIndex];

            // Swap two cards
            shuffledCards[firstIndex] = secondCard;
            shuffledCards[secondIndex] = firstCard;

            iteration -= 1;
        }

        return shuffledCards.ToList();
    }

}