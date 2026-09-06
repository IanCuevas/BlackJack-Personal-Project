using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "CardValue", menuName = "Scriptable Objects/CardValue")]
public class CardValue : ScriptableObject
{
    public Sprite cardSprite;
    public int cardValue;
}
