using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Deck : MonoBehaviour
{
    public GameObject canvas;
    public GameObject card;
    public GameObject text;
    public CardData[] data;
    private int handSize = 4;
    private List<Card> hand = null;
    private List<int> deckList = null;
    private List<int> playDeck = null;
    public static Deck instance = null;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        ShuffleDeck();
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeselectAll()
    {
        for(int i = 0; i < hand.Count; i++)
        {
            if(hand[i]!=null)
                hand[i].Deselect();
        }
    }

    public void DrawCard()
    {
        for(int i = 0; i < handSize; i++){
            if(hand[i] == null && playDeck.Count > 0){
                GameObject cardInst = Instantiate(card, new Vector3(12.5f+4.5f*(i/2),7.75f-6.25f*(i%2),0f), Quaternion.identity);
                if(data[playDeck[0]] is LineDamageCardData)
                    cardInst.AddComponent<LineDamageCard>();
                //else if(data[playDeck[0]] is LineDamageCardData)

                
                cardInst.transform.GetComponent<Card>().lookup = data[playDeck[0]];
                
                playDeck.RemoveAt(0);

                cardInst.transform.GetComponent<Card>().loc = i;
                cardInst.transform.GetComponent<Card>().canvas = canvas;
                cardInst.transform.GetComponent<Card>().text = text;
                hand[i] = cardInst.transform.GetComponent<Card>();
                cardInst.transform.GetComponent<Card>().generateCard();

                return;
            }
        }
    }

    public void RemoveCard(Card c)
    {
        hand[c.loc] = null;
        Destroy(c.gameObject);
    }

    public void ShuffleDeck()
    {
        deckList = new List<int>();
        for(int i = 0; i < 11; i++)
        {
            deckList.Add(Random.Range(0,data.Length));
        }
        playDeck = deckList;
        if (hand != null)
        {
            for(int i = 0; i < handSize; i++)
            {
                if(hand[i]!=null){
                    Destroy(hand[i].gameObject);
                    hand[i] = null;
                }
            }
        }

        hand = new List<Card>();
        for(int i = 0; i < handSize; i++){
            hand.Add(null);
        }
    }

    public List<Card> getHand()
    {
        return hand;
    }
}
