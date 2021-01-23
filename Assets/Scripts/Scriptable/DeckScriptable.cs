using System.Collections.Generic;
using UnityEngine;
using MEC;
using Random = UnityEngine.Random;
using System;
using UnityEngine.Events;
using Enums;

[CreateAssetMenu(fileName = "Deck", menuName = "Assets/Deck", order = 0)]
public class DeckScriptable : ScriptableObject
{
    [Header("Deck Basic Information")]
    public string DeckName = "Deck 1";
    public int MaxCardsInDeck;
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
    
    /// TODO: Rework this so it doesn't shuffle the initial list and therefore impacts the cards in deck
    public List<CardBaseScriptable> ShuffleDeck()
    {
        List<CardBaseScriptable> shuffledCards = _cards;

        if (shuffledCards.Count <= 0)
        {
            Debug.LogWarning($"Shuffling Deck didn't failed but the list is empty");
            return shuffledCards;
        }

        List<int> shuffledIndexes = new List<int>();

        bool numberOfCardsAreEven = shuffledCards.Count % 2 == 0 ? true : false;

        while (shuffledIndexes.Count != shuffledCards.Count)
        {
            int firstIndex = 0;
            while(shuffledIndexes.Contains(firstIndex))
            {
                firstIndex = Random.Range(0, shuffledCards.Count);
            }

            int secondIndex = 0;
            while (shuffledIndexes.Contains(secondIndex) && secondIndex != firstIndex)
            {
                secondIndex = Random.Range(0, shuffledCards.Count);
            }
            
            // Get cards to be swiped.
            CardBaseScriptable firstCard = shuffledCards[firstIndex];
            CardBaseScriptable secondCard = shuffledCards[secondIndex];
            // Swap two cards.
            shuffledCards[firstIndex] = secondCard;
            shuffledCards[secondIndex] = firstCard;

            // Add indexes.
            shuffledIndexes.Add(firstIndex);
            shuffledIndexes.Add(secondIndex);

            // Check even number and if its done.
            if(!numberOfCardsAreEven && shuffledIndexes.Count + 1 == _cards.Count)
            {
                break;
            }
        }

        return shuffledCards;
    }

    #region COROUTINES

    #endregion COROUTINES
}


