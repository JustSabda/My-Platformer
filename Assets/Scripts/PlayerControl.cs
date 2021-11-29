using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

	[SerializeField] bool IsGrounded = false;

	private int isMoving = 0;
	public Animator anim;
	public SpriteRenderer sprite;
	public float moveSpeed = 2f;
	public Rigidbody2D rb;
	public float jumpForce;
	public bool Dead = false;
	GameObject Finish;
	
	

    private void Start()
    {
		Finish = GameObject.Find("Finish");
		Finish.SetActive(false);
		Data.score = 0;
	}
    private void Update()
	{
		if (!Dead)
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				MoveRight();
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				MoveLeft();
			}
			if (!Input.anyKey)
			{
				Idle();
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Jump();
			}
			if (rb.velocity.y < -0.1)
			{
				anim.SetBool("IsJump", false);
				anim.SetBool("IsFall", true);
			}
			Move();
		}
	}


	void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.CompareTag("Ground"))
        {
			IsGrounded = true;
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Gem"))
        {
			collision.gameObject.SetActive(false);
			Data.score += 1;
			if (Data.score == 12)
            {
				Finish.SetActive(true);
            }
			Destroy(collision.gameObject);
		}
        if (collision.transform.tag.Equals("Trap"))
        {
			anim.SetBool("IsRoll", false);
			anim.SetTrigger("IsDead");
			Dead = true;
            Destroy(this.gameObject, 0.6f);
			StartCoroutine(LoseCond());
			
		}
        if (collision.transform.tag.Equals("Finish"))
        {
			SceneManager.LoadScene("YouWin");
		}
    }
	public void MoveRight()
    {
		isMoving = 1;
	}
	public void MoveLeft()
	{
		isMoving = 2;
	}
	public void Idle()
    {
		anim.SetBool("IsRun", false);
		anim.SetBool("IsJump", false);
		anim.SetBool("IsFall", false);
		isMoving = 0;
	}
	public void Jump()
    {
		if (IsGrounded && !Dead)
		{
			anim.SetBool("IsJump", true);
			anim.SetBool("IsFall", false);
			rb.velocity = Vector3.up * jumpForce;
			IsGrounded = false;
		}
	}
    private void Move()
    {
        if (isMoving == 1)
        {
			transform.Translate(1 * Time.deltaTime * moveSpeed, 0, 0);
			sprite.flipX = false;
			anim.SetBool("IsRun", true);
			if (IsGrounded)
			{
				anim.SetBool("IsFall", false);
			}
		}
		if (isMoving == 2)
		{
			transform.Translate(-1 * Time.deltaTime * moveSpeed, 0, 0);
			sprite.flipX = true;
			anim.SetBool("IsRun", true);
			if (IsGrounded)
			{
				anim.SetBool("IsFall", false);
			}
		}
	}
	    IEnumerator LoseCond()
    {
        yield return new WaitForSeconds(0.58f);
        SceneManager.LoadScene("YouLose");
    }
}