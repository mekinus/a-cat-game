using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    public Color pickedColor;

    public Transform playerSpawnPoint;
    private Color randomColor;
    [SerializeField] private bool isPicked;
    public string boxID;
    public bool isNextBoxToPick = false;
    public GameManager gm;
    public GameObject effect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(Color colorToPaint)
    {
        gameObject.GetComponent<SpriteRenderer>().color = colorToPaint;
    }

  
       

    public void SetPlayerPickedUpState(bool pickedUp)
    {
        isPicked = pickedUp;
    }

    void TeleportPlayer()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = playerSpawnPoint.position;
    }


    void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.tag == "Player") {
            Instantiate(effect, transform.position, Quaternion.identity);
            SetColor(pickedColor);
            SetPlayerPickedUpState(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PickBoxID(boxID);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddIndex();
            TeleportPlayer();
        }

        else
        {
            gm.SetGameOver();
        }

        
    }


    void OnCollisionExit2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            SetColor(Color.white);
            
        }


    }
}
