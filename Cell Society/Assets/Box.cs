using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    public Color pickedColor;
   
    
    private Color randomColor;
    [SerializeField] private bool isPicked;
    public string boxID;
    public bool isNextBoxToPick = false;
    public GameManager gm;
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


    void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.tag == "Player" && isNextBoxToPick) {
            SetColor(pickedColor);
            SetPlayerPickedUpState(true);
        }

        else
        {
            gm.SetGameOver();
        }

        
    }
}
