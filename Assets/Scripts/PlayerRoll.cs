using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    public bool isRoll = false;
    public PlayerControl PL;
    public Rigidbody2D rb;
    public Animator anim;
    public CapsuleCollider2D regularColl;
    public CircleCollider2D RollColl;
    public float RollSpeed = 5f;
    // Start is called before the first frame update
    private void Start()
    {
        RollColl.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Roll();
        }
        
    }
    private void prefromRoll()
    {
        isRoll = true;
        anim.SetBool("IsRoll", true);
        regularColl.enabled = false;
        RollColl.enabled = true;

        if (!PL.sprite.flipX)
        {
            rb.AddForce(Vector2.right * RollSpeed);
        }
        else
        {
            rb.AddForce(Vector2.left * RollSpeed);
        }
        
        StartCoroutine(stopRoll());
        
    }
    IEnumerator stopRoll()
    {
        yield return new WaitForSeconds(0.8f);
        anim.Play("PlayerIdle");
        anim.SetBool("IsRoll", false);
        regularColl.enabled = true;
        RollColl.enabled = false;
        isRoll = false;
    }
    void Roll()
    {
        if (PL.Dead == false)
        {
            prefromRoll();
        }
    }
}
