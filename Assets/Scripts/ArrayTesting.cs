using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UIElements;

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

public class ArrayTesting : MonoBehaviour
{
    //UI
    private Button dealCards;

    //Card Generating Lists

    int shoeDeck = 4;

    List<string> valueStrings = new List<string> {"Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"}; 

    List<string> suitStrings = new List<string> {"Spades", "Clubs", "Hearts", "Diamonds"};

    [SerializeField] List<string> deckOfCards = new List<string> {};

    [SerializeField] List<string> discardedCards = new List<string> {};

    //Card Dealing

    [SerializeField] List<Label> cardLabels = new List<Label> {};


    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        cardLabels.Add(root.Q<Label>("Player-Card-One"));
        cardLabels.Add(root.Q<Label>("Dealer-Card-One"));
        cardLabels.Add(root.Q<Label>("Player-Card-Two"));
        cardLabels.Add(root.Q<Label>("Dealer-Card-Two"));

        dealCards = root.Q<Button>("Randomizer");

        if (dealCards != null)
        {
            dealCards.clicked += DealCards;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int b = 0; b < shoeDeck; b++)
        {
            for(int i = 0; i < valueStrings.Count; i++)
            {
                int valueStringsIndex = i;
                if(i == valueStrings.Count)
                {
                    i = 0;
                }

                for(int j = 0; j < suitStrings.Count; j++)
                {
                    if(j == suitStrings.Count)
                    {
                        j = 0;
                    }
                    deckOfCards.Add($"{valueStrings[valueStringsIndex]} Of {suitStrings[j]}");
                }
            }
        }

        deckOfCards = deckOfCards.Randomize().ToList();
    }

    public void DealCards()
    {
        if(deckOfCards.Count == 0)
        {
            deckOfCards.AddRange(discardedCards);
            discardedCards.Clear();
        }

        int currentIndexCard = deckOfCards.Count - deckOfCards.Count;

        foreach(Label card in cardLabels)
        {
            card.text = deckOfCards[currentIndexCard];

            discardedCards.Add(deckOfCards[currentIndexCard]);
            deckOfCards.Remove(deckOfCards[currentIndexCard]);
        }
        
        print(deckOfCards.Count);
    }
}
