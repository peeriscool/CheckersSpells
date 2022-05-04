using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Cards", menuName = "ScriptableObjects/Cards", order = 1)]
public class DeckOfCardsScriptable : ScriptableObject
{
    public GameObject prefab;
    public Sprite img;
}