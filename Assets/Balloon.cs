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
    private Vector3 scaleChange;
    [SerializeField]
    private float maxSize = 3;
    private float minSize = 0;
    [SerializeField]
    private float growRate = 0.01f;
    [SerializeField] int level;
    private bool shrinkDifficulty = false;
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

    }

    //called potentially multiple times per frame
    //used for physics & movement
    void FixedUpdate()
    {
        //rigid.velocity = new Vector2(movement * speed, rigid.velocity.y);
        // if (movement2 < 0 && isFacingRight || movement2 > 0 && !isFacingRight)
        //     Flip();
        Vector2 direction = new Vector2(1, 0);
        transform.Translate(direction * speed);
        if (transform.position.x >= 55.7)
            Flip();
        if (transform.position.x <= -13.3)
            Flip();
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
