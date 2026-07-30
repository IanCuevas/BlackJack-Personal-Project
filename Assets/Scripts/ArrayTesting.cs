using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

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
    List<string> valueStrings = new List<string> {"ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king"}; 

    List<string> suitStrings = new List<string> {"spades", "clubs", "hearts", "diamonds"}; 

    List<string> deckOfCards = new List<string> {};

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
                deckOfCards.Add($"{valueStrings[valueStringsIndex]} of {suitStrings[j]}");
            }
        }

        deckOfCards = deckOfCards.Randomize().ToList();
        foreach (var card in deckOfCards)
        {
            print(card);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
