using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Cards", menuName = "ScriptableObjects/Cards", order = 1)]
public class Card_ScriptableObject : ScriptableObject
{
    public GameObject prefab;
    public Sprite img;
}