using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private Card selectedCard = null;
    private Transform player;
    public GameObject guide;
    private GameObject guideHolder;
    private void Start() {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        guideHolder = Instantiate(guide,new Vector3(0.5f,0.5f,0f), Quaternion.identity);
        guideHolder.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null && hit.collider.tag == "Card" && GameManager.instance.playersTurn)
            {
                selectedCard = hit.transform.GetComponent<Card>();
                selectedCard.Select();
                return;
            }

            if(canCast(mousePos2D)){
                selectedCard.castSpell(mousePos2D,player);
                postCast();
            }
            selectedCard = null;
            Deck.instance.DeselectAll();
        }


        if(Input.GetMouseButtonDown(1))
        {
            Deck.instance.DeselectAll();
            selectedCard = null;
        }


        if(selectedCard != null && mousePos.x > 0f && mousePos.x < 10.5f && mousePos.y > 0f && mousePos.y < 10.5f)
        {
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

            Vector3 dir = new Vector3(xDir,yDir,0f);

            guideHolder.transform.position = player.position + dir;
            guideHolder.SetActive(true);
        }
        else
        {
            guideHolder.SetActive(false);
        }
    }

    bool canCast(Vector2 mousePos)
    {
        if(selectedCard == null || mousePos.x < 0f || mousePos.x > 10.5f || mousePos.y < 0f || mousePos.y > 10.5f)
            return false;
        Player apCheck = player.GetComponent<Player>();
        if(!apCheck.useAp(selectedCard.cost))
            return false;
        return true;
    }

    void postCast()
    {
        AudioManager.instance.play(selectedCard.lookup.sfx);
        Debug.Log(selectedCard.lookup.cardName);
        Deck.instance.RemoveCard(selectedCard);
    }

}
