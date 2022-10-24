using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public TextMeshProUGUI score;
    public GameObject winTextObject;
    public TextMeshProUGUI winText;
    public GameObject loseTextObject;
    public TextMeshProUGUI livesText;
    public GameObject stage2textObject;

    public AudioSource musicSource;
    public AudioClip winning;
    private int scoreValue = 0;
    private int lives;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        SetScoreText();
        winTextObject.SetActive(false);

        lives = 3;
        SetLivesText();
        loseTextObject.SetActive(false);

        stage2textObject.SetActive(false);
    }

    void SetScoreText()
    {
        if (scoreValue == 8)
        {
            winTextObject.SetActive(true);
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            loseTextObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
                {
                    lives = 3;
                    SetLivesText();
                }
        }

        if ((scoreValue == 8) && (collision.collider.tag == "Coin"))
        {
            winTextObject.SetActive(true);
        }

        if(collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            Destroy(collision.collider.gameObject);

            SetLivesText();
        }

        if ((lives == 0) && (collision.collider.tag == "Enemy"))
        {
            speed = 0;
        }

        if ((scoreValue == 4) && (collision.collider.tag == "Coin"))
        {
            transform.position = new Vector3(81f, 2f);
            stage2textObject.SetActive(true);
        }

        if ((scoreValue == 8) && (collision.collider.tag == "Coin"))
        {
            musicSource.clip = winning;
            musicSource.Play();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        { 
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
