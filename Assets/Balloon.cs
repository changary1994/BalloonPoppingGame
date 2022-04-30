using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Balloon : MonoBehaviour
{
    [SerializeField] float speed = .5f;
    //[SerializeField] RigidBody2D rigid;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] AudioSource audio;
    [SerializeField] GameObject controller;
    [SerializeField] private GameObject player;
    private Vector3 scaleChange;
    [SerializeField]
    private float maxSize = 3;
    private float minSize = 0;
    [SerializeField]
    private float growRate = 0.01f;
    [SerializeField] int level;
    private bool shrinkDifficulty = false;
    private int maxDistance = 15;
    private Vector2 desiredVelocity;
    private Vector2 steeringVelocity;
    Vector2 currentVelocity = new Vector2(.3f, 0);
    Vector2 direction = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex + 1;
        if (audio == null)
            audio = gameObject.AddComponent<AudioSource>();
        //if (rigid == null)
        //    rigid = GetComponent<Rigidbody2D>();
        //speed = 7;
        speed = speed * level;
        controller = GameObject.Find("GameController");
        player = GameObject.Find("Player");
        InvokeRepeating("SizeGrow", 3.0f, 0.05f);
        shrinkDifficulty = (PlayerPrefs.GetInt("ShrinkToggle") == 1 ? true : false);
        if (shrinkDifficulty != true)
        {
            scaleChange = new Vector3(+growRate, +growRate, +growRate);
        }
        else
        {
            scaleChange = new Vector3(-growRate, -growRate, -growRate);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localScale.x > maxSize || this.transform.localScale.x < minSize)
        {
            AudioSource.PlayClipAtPoint(audio.clip, transform.position);
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        if (Time.timeSinceLevelLoad > 10)
        {
            AudioSource.PlayClipAtPoint(audio.clip, transform.position);
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    //called potentially multiple times per frame
    //used for physics & movement
    void FixedUpdate()
    {
        //rigid.velocity = new Vector2(movement * speed, rigid.velocity.y);
        // if (movement2 < 0 && isFacingRight || movement2 > 0 && !isFacingRight)
        //     Flip();
        if (transform.position.x >= 55.7)
            Flip();
        if (transform.position.x <= -13.3)
            Flip();
        if (transform.position.y >= 27 || transform.position.y <= -7)
            transform.position = new Vector2(Random.Range(-13, 55), Random.Range(9, 26));
        if ((SceneManager.GetActiveScene().buildIndex) == 3)
        {
            Flee();
        }
        else
        {
            transform.Translate(direction * speed);
        }
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    void SizeGrow()
    {
        this.transform.localScale += scaleChange;
    }

    public void Flee()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < maxDistance)
        {
            desiredVelocity = (transform.position - player.transform.position);
            steeringVelocity = desiredVelocity - currentVelocity;
            currentVelocity += steeringVelocity;
            transform.Translate(currentVelocity * Time.deltaTime);
            if (speed < 0 && isFacingRight || speed > 0 && !isFacingRight)
                Flip();
        }
        else
        {
            transform.Translate(direction * speed);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pin")
        {
            AudioSource.PlayClipAtPoint(audio.clip, transform.position);
            controller.GetComponent<ScoreKeeper>().AddPoints();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
     


        }
    }
}
