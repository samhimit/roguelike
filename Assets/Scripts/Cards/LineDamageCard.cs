using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDamageCard : Card
{
    private int damage;
    private int range;
    private bool pierce;

    // Start is called before the first frame update
    protected override void Start() {
        LineDamageCardData LineLookup = base.lookup as LineDamageCardData;
        damage = LineLookup.damage;
        range = LineLookup.range;
        pierce = LineLookup.pierce;
        base.Start();
    }


    public override void castSpell(Vector2 mousePos, Transform player){
        int xDir = 0;
        int yDir = 0;

        if(Mathf.Abs(mousePos.x-player.position.x)>Mathf.Abs(mousePos.y-player.position.y))
        {
            xDir = player.position.x > mousePos.x ? -1 : 1;
        }
        else
        {
            yDir = player.position.y > mousePos.y ? -1 : 1;
        }
        for(int i = 1; i <= range; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(player.position.x+i*xDir,player.position.y+i*yDir), Vector2.zero);
            if(hit.collider != null)
            {
                MovingObject selectedEnemy = hit.transform.GetComponent<MovingObject>();
                selectedEnemy.takeDamage(damage);
            }
        }
    }
}
