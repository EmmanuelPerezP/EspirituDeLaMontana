using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D body;
    float jumpSpeed;
    public LayerMask tileLayer;
    public AudioSource walkSound;
    public AudioSource jumpSound;
    public AudioSource deathSound;
    public float maxSpeed = 5f;
    public float speed = 2f;
    public float distance = 6f;
    public float jumpPower = 6.5f;
    public bool hidden = false;
    public bool crouched = false;

    private bool facingRight = false;
    private bool jump;
    private Animator anim;
    private IEnumerator coroutine;



    // Use this for initialization
    void Start()
    {

        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {

        // Move the object forward along its z axis 1 unit/second.
        // transform.Translate(Vector3.forward * 3 * Time.deltaTime);
        bool jumpDown = Input.GetButtonDown("Jump");
        float horizontal = Input.GetAxis("Horizontal");
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.UpArrow)) && IsGrounded())
        {
            jump = true;
            anim.SetBool("jump", true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("jump", false);
            jumpSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && IsGrounded())
        {
            crouched = true;
            anim.SetBool("crouch", true);

        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && IsGrounded())
        {
            crouched = false;
            anim.SetBool("crouch", false);
        }


    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        body.AddForce(Vector2.right * speed * h);

        float limitedSpeed = Mathf.Clamp(body.velocity.x, -maxSpeed, maxSpeed);
        body.velocity = new Vector2(limitedSpeed, body.velocity.y);

        if (h > 0.1f)
        {
            if (!walkSound.isPlaying)
            {
                walkSound.Play();
            }
            if (jump)
            {
                walkSound.Stop();
            }
            anim.SetBool("running", true);
            if (facingRight)
            {
                walkSound.Play();
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                facingRight = false;
            }

        }
        if (h < -0.1f)
        {
            if (!walkSound.isPlaying)
            {
                walkSound.Play();
            }
            if (jump)
            {
                walkSound.Stop();
            }
            anim.SetBool("running", true);
            if (!facingRight)
            {
                walkSound.Play();

                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                facingRight = true;
            }
        }

        if (h == 0)
        {
            anim.SetBool("running", false);
            walkSound.Stop();

        }

        if (jump)
        {
            body.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }
    }



    public void Dead()
    {
        deathSound.Play();
        anim.SetTrigger("death");

        coroutine = CallTriggerWatch();
        StartCoroutine(coroutine);

    }

    IEnumerator CallTriggerWatch()
    {
        yield return new WaitForSeconds(3f);
        Application.LoadLevel(Application.loadedLevel);
    }


    bool IsGrounded()
    {

        Vector2 position = transform.position;
        Vector2 position2 = transform.position;
        position2.x -= 1f;

        //position.x =+ 
        Vector2 direction = new Vector2(0, -4.0f); //Vector2.down;
        Vector2 direction2 = new Vector2(-1f, -4.0f); //Vector2.down;

        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, tileLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(position, direction2, distance, tileLayer);

        if (hit.collider != null || hit2.collider != null)
        {
            return true;
        }

        return false;
    }

    public void Caminar()
    {
        walkSound.Play();
    }
}
