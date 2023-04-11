using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rbPlayer;
    private SpriteRenderer sr;
    public float jumpForce;
    public bool inFloor = true;
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

       MovePlayer();
        JumpPlayer();
    }

    void MovePlayer()
    {

        float horizontalMoviment = Input.GetAxisRaw("Horizontal");
        //Debug.Log(horizontalMoviment);
        //transform.position += new Vector3(horizontalMoviment * Time.deltaTime * speed, 0, 0);
        rbPlayer.velocity = new Vector2(horizontalMoviment * speed, rbPlayer.velocity.y);

        if(horizontalMoviment > 0)
        {
            playerAnim.SetBool("Walking", true);
            sr.flipX = false;
        }
        else if (horizontalMoviment < 0)
        {
            playerAnim.SetBool("Walking", true);
            sr.flipX = true;
        }
        else
        {
            playerAnim.SetBool("Walking", false);
        }
    }

    void JumpPlayer()
    {
        if (Input.GetButtonDown("Jump") && inFloor)
        {
            playerAnim.SetBool("Jump", true);
            rbPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            inFloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            playerAnim.SetBool("Jump", false);
            inFloor = true;
        }
    }
}
