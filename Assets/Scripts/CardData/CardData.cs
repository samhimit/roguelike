using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CardData : ScriptableObject
{
    public string cardName = "New Card";
    public string cardText = "Does a thing";
    public Texture2D cardIcon = null;
    public int cost = 1;
    public AudioClip sfx = null;
}
