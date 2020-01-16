using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private bool isPressed;
    public Player player;
    private Animator anim;
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isPressed)
        {
            if(collision.gameObject.tag == "Player")

            {
                anim.SetBool("isPressed", true);
                isPressed = true;
                gm.ProceedGame();
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")

        {
            isPressed = false;
            anim.SetBool("isPressed", false);
        }
       
            
        
    }
}
