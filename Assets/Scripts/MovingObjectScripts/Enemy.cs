using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    // Start is called before the first frame update
    public EnemyData lookup;
    private Transform target;
    private int currTurn;
    private int fireTurn;
    public int id;
    public AudioClip deathSound;
    public AudioClip attackSound;
    public AudioClip projectileSound;
    protected override void Start()
    {
        
        id = GameManager.instance.AddEnemyToList(this);
        target = GameObject.FindGameObjectWithTag ("Player").transform;
        currTurn = lookup.speed;
        fireTurn = lookup.fireSpeed;
        hp = lookup.hpMax;
        base.Start();
    }

    // Update is called once per frame
    protected override void AttemptMove <T> (int xDir,int yDir)
    {
        if(currTurn > 0)
        {
            currTurn--;
            return;
        }

        base.AttemptMove<T>(xDir,yDir);
        currTurn = lookup.speed;
    }

    public virtual void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;
        findPlayer(out xDir, out yDir);
        AttemptMove<Player>(xDir,yDir);
    }

    protected override void OnCantMove <T> (T component)
    {
        Player target = component as Player;
        target.takeDamage(lookup.damage);
        instance.clip = attackSound;
        instance.Play();
        return;
    }

    protected override void OnZeroHp()
    {
        GameManager.instance.RemoveEnemyFromList(id);
        instance.clip = deathSound;
        instance.Play();
        Destroy(gameObject, moveTime);
    }

    public virtual void fireProjectile()
    {
        if(lookup.projectile == null)
            return;
        if(fireTurn > 0)
        {
            fireTurn--;
            return;
        }
        int xDir = 0;
        int yDir = 0;
        findPlayer(out xDir, out yDir);
        GameObject shotProjectile = Instantiate(lookup.projectile, new Vector3(transform.position.x+xDir, transform.position.y+yDir , 0f), Quaternion.identity);
        fireTurn = lookup.fireSpeed;
        instance.clip = projectileSound;
        instance.Play();

    }

    private void findPlayer(out int xDir, out int yDir)
    {
        xDir = 0;
        yDir = 0;

        if(Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
            yDir = target.position.y > transform.position.y ? 1 : -1;
        else
            xDir = target.position.x > transform.position.x ? 1 : -1;
    }
}
