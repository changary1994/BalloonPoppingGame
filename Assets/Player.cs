using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movement;
    [SerializeField] float movement2;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] int speed;
    [SerializeField] bool isFacingRight = true;
    [SerializeField]
    private float fireRate = .5f;
    private float canFire = 0f;
    [SerializeField] private GameObject pin;

    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        speed = 15;
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Vertical");
        movement2 = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.LeftControl) && Time.time > canFire)
        {
            ShootPin();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * 2;
        }
    }

    //called potentially multiple times per frame
    //used for physics & movement
    void FixedUpdate()
    { 
        rigid.velocity = new Vector2(movement * speed, rigid.velocity.y);
        if (movement2 < 0 && isFacingRight || movement2 > 0 && !isFacingRight)
            Flip();

        rigid.velocity = new Vector2(movement2 * speed, rigid.velocity.x);

    }

    void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }
    void ShootPin()
    {
        canFire = Time.time + fireRate;
        Instantiate(pin, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // if (collision.gameObject.tag == "Ground") ;
        
    }

}
