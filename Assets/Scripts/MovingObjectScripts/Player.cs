using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MovingObject
{
    public TMP_Text healthDisplay;
    public TMP_Text apDisplay;
    public int ap;
    public AudioClip cantMoveSound;
    public GameObject DamageCover;
    // Start is called before the first frame update
    protected override void Start()
    {
        hp = 12;
        ap = 1;
        healthDisplay.text = "hp: " + hp;
        apDisplay.text = "ap: " + ap;
        DamageCover.SetActive(false);
        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        if(!GameManager.instance.playersTurn) return;

        int hor = 0;
        int ver = 0;

        hor = (int)(Input.GetAxisRaw("Horizontal"));
        ver = (int)(Input.GetAxisRaw("Vertical"));
        if(hor != 0 || ver != 0)
        {
            AttemptMove<MovingObject> (hor,ver);
        }
    }

    protected override void AttemptMove<T> (int xDir, int yDir)
    {
        GameManager.instance.playersTurn = false;
        base.AttemptMove<T> (xDir,yDir);
        RaycastHit2D hit;
        if(Move(xDir,yDir, out hit))
        {
            //call if move sucessful
            ap++;
            apDisplay.text = "ap: " + ap;
            Deck.instance.DrawCard();
        }
        else
        {
            instance.clip = cantMoveSound;
            instance.Play();
        }
    }

    protected override void OnCantMove <T> (T component)
    {
        
        return;
    }

    public override void takeDamage(int damage)
    {
        StartCoroutine(damageCover());
        base.takeDamage(damage);
        healthDisplay.text = "hp: " + hp;
        instance.clip = hitSound;
        instance.Play();
    }

    IEnumerator damageCover()
    {
        DamageCover.SetActive(true);
        yield return new WaitForSeconds(.2f);
        DamageCover.SetActive(false);
    }

    protected override void OnZeroHp()
    {
        enabled = false;
        GameManager.instance.GameOver();
    }

    public bool useAp(int use)
    {
        if(ap < use)
            return false;
        ap -= use;
        apDisplay.text = "ap: " + ap;
        return true;
    }
}
