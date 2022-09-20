using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Enemy
{
    // Start is called before the first frame update
    // private void OnEnable() {
    //     RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.zero);
    //     if(hit.collider!=null && hit.collider.tag == "Player")
    //     {
    //         Player hitComponent = hit.transform.GetComponent<Player>();
    //         OnCantMove(hitComponent);
    //         return;
    //     }
    //     if(hit.collider!=null && hit.collider.tag == "Enemy")
    //     {
    //         OnZeroHp();
    //         return;
    //     }
    // }
    
    public override void MoveEnemy()
    {
        base.takeDamage(1);
        base.MoveEnemy();
    }

    protected override void OnCantMove <T> (T component)
    {
        base.OnCantMove(component);
        OnZeroHp();
        return;
    }
}
