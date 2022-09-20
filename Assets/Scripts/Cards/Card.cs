using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Card : MonoBehaviour
{
    //look up info
    public CardData lookup = null;

    //card info for all
    public int cost;


    //location in hand tracking
    public int loc = 0;
    //private bool isSelected = false;

    //card at generation
    public GameObject text;
    public GameObject canvas;

    private GameObject instanceText = null;
    private GameObject instanceCost = null;
    private GameObject instanceName = null;
    protected virtual void Start()
    {
        cost = lookup.cost;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Select()
    {
        Deck.instance.DeselectAll();
        //isSelected = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(180f/255f,180f/255f,1800f/255f);
    }

    public void Deselect()
    {
        //isSelected = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(120f/255f,120f/255f,120f/255f);
    }

    public void generateCard()
    {
        //description
        instanceText = Instantiate(text, gameObject.transform.position+new Vector3(0f,-.5f,0f),Quaternion.identity) as GameObject;
        instanceText.transform.SetParent(canvas.transform);
        instanceText.GetComponent<TMP_Text>().text = lookup.cardText;
        
        //cost
        instanceCost = Instantiate(text, gameObject.transform.position + new Vector3(0f,.5f,0f),Quaternion.identity) as GameObject;
        instanceCost.transform.SetParent(canvas.transform);
        instanceCost.GetComponent<TMP_Text>().text = lookup.cost.ToString();

        //name
        instanceName = Instantiate(text, gameObject.transform.position+new Vector3(.5f,.5f,0f),Quaternion.identity) as GameObject;
        instanceName.transform.SetParent(canvas.transform);
        instanceName.GetComponent<TMP_Text>().text = lookup.cardName;
        
    }

    public abstract void castSpell(Vector2 mousePos, Transform player);

    private void OnDestroy() {
        if(instanceText!=null)
            Destroy(instanceText);
        if(instanceCost!=null)
            Destroy(instanceCost);
        if(instanceName!=null)
            Destroy(instanceName);

    }
}
