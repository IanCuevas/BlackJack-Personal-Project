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
    private Label cardText;
    private Button randomCard;

    int numberOfCards = 51;

    //Card Generating Lists
    List<string> valueStrings = new List<string> {"Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"}; 

    List<string> suitStrings = new List<string> {"Spades", "Clubs", "Hearts", "Diamonds"}; 

    [SerializeField] List<string> deckOfCards = new List<string> {};

    [SerializeField] List<string> discardedCards = new List<string> {};


    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        cardText = root.Q<Label>("Card-Name");
        randomCard = root.Q<Button>("Randomizer");

        if (randomCard != null)
        {
            randomCard.clicked += RandomCardPick;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < valueStrings.Count; i++)
        {
            int valueStringsIndex = i;

            for(int j = 0; j < suitStrings.Count; j++)
            {
                if(j == suitStrings.Count)
                {
                    j = 0;
                }
                deckOfCards.Add($"{valueStrings[valueStringsIndex]} Of {suitStrings[j]}");
            }
        }

        deckOfCards = deckOfCards.Randomize().ToList();
    }

    public void RandomCardPick()
    {
        if(deckOfCards.Count == 0)
        {
            deckOfCards.AddRange(discardedCards);
            discardedCards.Clear();
            numberOfCards = 51;
        }

        int randomNumber = UnityEngine.Random.Range(0, numberOfCards);

        cardText.text = deckOfCards[randomNumber];

        discardedCards.Add(deckOfCards[randomNumber]);
        deckOfCards.Remove(deckOfCards[randomNumber]);
        
        numberOfCards--;
        print(numberOfCards);
    }
}
