using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LineDamageCard", menuName = "LineDamageCardData", order = 1)]
public class LineDamageCardData : CardData
{
    public int damage = 1;
    public int range = 1;
    public bool pierce = true;
}
