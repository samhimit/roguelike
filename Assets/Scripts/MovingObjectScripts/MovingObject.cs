using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.1f;
    public LayerMask blockingLayer;
    protected int hp;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;  
    public AudioClip moveSound;
    public AudioSource instance;
    public AudioClip hitSound;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    //returns true if able to move, takes x and y direction, outputs a raycast to detect collision
    protected bool Move (int xDir, int yDir, out RaycastHit2D hit) {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        boxCollider.enabled = false;

        hit = Physics2D.Linecast(start, end, blockingLayer);

        boxCollider.enabled = true;

        if(hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            instance.clip = moveSound;
            instance.Play();
            return true;
        }
        return false;
    }

    protected IEnumerator SmoothMovement (Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while(sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime );
            rb2D.MovePosition (newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

    protected virtual void AttemptMove <T> (int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir,yDir, out hit);

        if(hit.transform == null)
            return;
        
        T hitComponent = hit.transform.GetComponent<T>();
        if(!canMove && hitComponent != null){
            OnCantMove (hitComponent);
        }
    }

    protected abstract void OnCantMove <T> (T component)
        where T : Component;

    protected abstract void OnZeroHp ();

    public virtual void takeDamage(int damage)
    {
        hp -= damage;
        instance.clip = hitSound;
        instance.Play();
        if(hp<=0)
            OnZeroHp();
    }
}