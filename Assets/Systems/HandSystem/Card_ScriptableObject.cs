using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Cards", menuName = "ScriptableObjects/Deck", order = 1)]
public class DeckOfCardsScriptable : ScriptableObject
{
    public Array prefabs;
    public int length;
    public int shuffleSeed;
}