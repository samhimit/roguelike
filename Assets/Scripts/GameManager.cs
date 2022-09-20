using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    //board manager as a private maybe
    public float turnDelay = .55f;
    [HideInInspector] public bool playersTurn = true;
    public GameObject ImageCover;
    private bool enemiesActing = false;
    private List<Enemy> enemies;
    private boardManager board;
    private int level = 1;
    private Transform enemyHolder;
    public AudioClip gameOverSound;
    public AudioClip nextLevelSound;
    //

    // Start is called before the first frame update
    void Awake()
    {
        playersTurn = false;
        if(instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        board = GetComponent<boardManager>();
        enemyHolder = new GameObject ("Enemy").transform;
        //need to also grab a board script to initiate enemies
        InitGame();
    }

    void InitGame()
    {
        //call functions to set up the game
        playersTurn = false;
        ImageCover.SetActive(true);
        enemies.Clear();
        //call board setup
        board.setUpBoard(level);
        ImageCover.SetActive(false);
        playersTurn = true;
        
    }
    // Update is called once per frame
    void Update()
    {
        if(playersTurn || enemiesActing)
            return;
        StartCoroutine(MoveEnemies());
    }
    public int AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
        script.transform.SetParent(enemyHolder);
        for(int i = enemies.Count-1; i >= 0; i--)
        {
            enemies[i].id = i;
        }
        return enemies.Count-1;
    }

    public void RemoveEnemyFromList(int id)
    {
        if(id < enemies.Count)
            enemies.RemoveAt(id);
        for(int i = enemies.Count-1; i >= 0; i--)
        {
            enemies[i].id = i;
        }
        if(enemies.Count == 0)
        {
            AudioManager.instance.play(nextLevelSound);
            level++;
            Deck.instance.ShuffleDeck();
            Debug.Log("nextlevel");
            InitGame();
        }
    }

    IEnumerator MoveEnemies()
    {
        enemiesActing = true;
        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for(int i = enemies.Count-1; i >= 0; --i)
        {
            enemies[i].MoveEnemy();
            if(i < enemies.Count)
            {
                yield return new WaitForSeconds(enemies[i].moveTime);
                enemies[i].fireProjectile();
            }
        }

        playersTurn = true;
        enemiesActing = false;
    }

    public void GameOver()
    {
        AudioManager.instance.play(gameOverSound);
        ImageCover.SetActive(true);
        enabled = false;
    }
}
