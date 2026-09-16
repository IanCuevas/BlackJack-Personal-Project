using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UIElements;
using System.Collections;

// Randomizer Code
// Analyze code later
public static class CollectionExtensions
{
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        System.Random rnd = new System.Random();
        return source.OrderBy(_ => rnd.Next());
    }
}

public class CardDeck : MonoBehaviour
{
    // List/Arrays
    [SerializeField] List<CardValue> cardDeck = new List<CardValue>();

    [SerializeField] List<CardValue> currentCards = new List<CardValue>();

    [SerializeField] List<CardValue> discardedDeck = new List<CardValue>();

    [SerializeField] CardValue[] suitOfDiamonds, suitOfHearts;

    [SerializeField] List<CardValue[]> cardSuits = new List<CardValue[]> { };

    //UI Document

    Button dealCard, discardCards;

    List<Image> givenCards = new List<Image> { };

    float delay = 1.0f;

    int shoeDeck = 4;

    int playerValue, dealerValue;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement playerHand = root.Q<VisualElement>("Player-Hand");
        VisualElement dealerHand = root.Q<VisualElement>("Dealer-Hand");
        VisualElement buttons = root.Q<VisualElement>("Buttons");

        givenCards.Add(playerHand.Q<Image>("Player-Card-One"));
        givenCards.Add(dealerHand.Q<Image>("Dealer-Card-One"));
        givenCards.Add(playerHand.Q<Image>("Player-Card-Two"));
        givenCards.Add(dealerHand.Q<Image>("Dealer-Card-Two"));

        dealCard = buttons.Q<Button>("Play");
        discardCards = buttons.Q<Button>("Discard");

        if (dealCard != null)
        {
            dealCard.clicked += DealCards;
        }

        if (discardCards != null)
        {
            discardCards.clicked += RemoveCards;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardSuits.Add(suitOfDiamonds);
        cardSuits.Add(suitOfHearts);

        for (int i = 0; i < shoeDeck; i++)
        {
            foreach (var suit in cardSuits)
            {
                foreach (var card in suit)
                {
                    cardDeck.Add(card);
                }
            }
        }

        cardDeck = cardDeck.Randomize().ToList();
    }

    private void FixedUpdate()
    {
        if (cardDeck.Count == 0)
        {
            cardDeck.AddRange(discardedDeck);
            discardedDeck.Clear();
        }
    }

    public void DealCards()
    {
        StartCoroutine(SetCardValue(0));
    }

    public void RemoveCards()
    {
        StartCoroutine(RemoveCardValues(0));
    }

    IEnumerator SetCardValue(int indexCard)
    {
        foreach (Image card in givenCards)
        {
            card.sprite = cardDeck[indexCard].cardSprite;

            print(cardDeck[indexCard].cardValue);

            currentCards.Add(cardDeck[indexCard]);
            cardDeck.Remove(cardDeck[indexCard]);

            yield return new WaitForSeconds(delay);
        }

        playerValue = currentCards[0].cardValue + currentCards[2].cardValue;
        dealerValue = currentCards[1].cardValue + currentCards[3].cardValue;

        print($"Player Value: { playerValue }");
        print($"Dealer Value: { dealerValue }");

        if (playerValue > dealerValue)
        {
            print("Player Wins!");
        }
        else
        {
            print("Dealer Wins :(");
        }

    }

    IEnumerator RemoveCardValues(int indexCard)
    {
        foreach (Image card in givenCards)
        {
            discardedDeck.Add(currentCards[indexCard]); 
            currentCards.Remove(currentCards[indexCard]);

            card.sprite = null;

            yield return new WaitForSeconds(delay);
        }
    }

}
