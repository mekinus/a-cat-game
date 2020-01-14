using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] public Vector2 jumpForce;
    public GameManager manager;
    public Transform groundCheck;
    public bool onGround;
    public LayerMask whatIsGround;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.gameStates == GameManager.GameStates.running)
        MovementHandler();
    }

    void MovementHandler()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        gameObject.transform.Translate(x, 0, 0);

        anim.SetFloat("speed",x);

        if (anim.GetFloat("speed") < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        if (anim.GetFloat("speed") > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        if (Physics2D.Raycast(groundCheck.position,Vector2.down,0.05f,whatIsGround))
        {
            onGround = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) & onGround) 
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(jumpForce);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            onGround = false;

        }

    }
}
