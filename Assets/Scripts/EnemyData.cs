using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    // Start is called before the first frame update
    public int hpMax;
    public int speed; 
    public int fireSpeed;
    public int damage;
    public GameObject projectile = null;
}
