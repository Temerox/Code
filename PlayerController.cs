using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

    
    public float speed;
    public TextMeshProUGUI countText;
    //public GameObject winTextObject;
    public MenuController menuController;
    public Transform respawnPoint;

    public TextMeshProUGUI enemyHitsText;
    public int startingLives;

    private float movementX;
    private float movementY;
    private Rigidbody rb;
    private int count;
    private AudioSource pop;

    private AudioSource oof;

    private int lives;

    //jump features

     public float jumpForce;

     private bool canJump;

     //enemy hits

     public int enemyHits;

    
    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        pop = audios[0];
        oof = audios[1];
        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();

        
        // Set the count to zero 
        count = 0;

        enemyHits = 0;


        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        //winTextObject.SetActive(false);

        //jump
        canJump = true;

    }

    private void Update()
    {
        if(transform.position.y <= -1)
        {
            Respawn();
        }

        if(enemyHits == 3)
        {
            EndGame();
        }

    
        enemyHitsText.text = "Lives Left: " + (3 - enemyHits).ToString();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        //if the GameObject you intersect has the tag 'Pick Up' assigned to it..
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            
            count = count + 1;

            
            SetCountText();

            pop.Play();
            
        }

        if (other.gameObject.CompareTag("RedBox"))
        {
            other.gameObject.SetActive(false);

            enemyHits = enemyHits + 1;

            oof.Play();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            enemyHits++;
            if (enemyHits == 3)
            {
                EndGame();
                return;
            }
            Respawn();
        }

        

         
        if(collision.gameObject.CompareTag("ground"))
        {
            canJump = true;
        }
    }

    void Respawn()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        transform.position = respawnPoint.position;
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;
    }

    void OnJump()
    {
        if(canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12)
        {
            // Set the text value of your 'winText'
            //winTextObject.SetActive(true);
            menuController.WinGame();
        }
    }

    void EndGame()
    {
        menuController.LoseGame();
        gameObject.SetActive(false);
        Time.timeScale = 0.5f;

    }
}