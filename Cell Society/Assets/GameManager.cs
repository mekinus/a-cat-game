using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text levelText;
    public GameObject loserText;
    public GameObject blackCat;
    public GameObject redEyedCat;
    public GameObject moon;
    public GameObject moonLight;
    public GameObject lamp;
    public Camera mainCamera;  
    public enum GameStates {running,waiting,paused}

    public GameStates gameStates;

    public bool noLight;
    private float  blackoutCounter = 0;
    private GameObject[] box;
    [SerializeField] [Range(0, 1)] private float duration;
    [SerializeField] private int gameLevel = 0;

    private GameObject nextBox;

    
    [SerializeField] private string[] catPath = new string[100];
    void Start()
    {
        loserText.SetActive(false);
        SetPlayerLamp(false);
        SetGameState(GameStates.waiting);
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
       if(gameStates == GameStates.running && !noLight)
        {
          mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.black, duration);
          
        }


        blackoutCounter += Time.deltaTime;
        if (blackoutCounter > 10f)
            noLight = true;

        InputHandler();
    }

    void TurnMoonLightOff ()
    {
        if(moonLight.GetComponent<Light>() != null)
        moonLight.GetComponent<Light>().enabled = false;
    }


    public void SetGameState(GameStates states)
    {
        gameStates = states;
    }

    void SetPlayerLamp (bool on)
    {
        if (on)
            lamp.GetComponent<Light>().enabled = true;
        else
            lamp.GetComponent<Light>().enabled = false;

    }

    void RandomizeBox ()
    {
        int index = Random.Range(0, box.Length);
        Transform boxPosition = box[index].gameObject.transform;
        nextBox = box[index];
        // Instantiate(redEyedCat,boxPosition.transform.position,Quaternion.identity);
        AddBoxIDToPath(nextBox.GetComponent<Box>().boxID);
        
        foreach(GameObject boxItem in box)
        {
            if(box != null)
            boxItem.GetComponent<Box>().isNextBoxToPick = false;
        }

        nextBox.GetComponent<Box>().isNextBoxToPick = true;

        
        StartCoroutine("ShowCatPath");


    }


    void GetBoxList ()
    {
        box = GameObject.FindGameObjectsWithTag("Box");
    }

    void AddBoxIDToPath(string id)
    {
        catPath[gameLevel] = id;
    }


    void LevelUp()
    {
        gameLevel++;
        levelText.text = gameLevel.ToString();

    }

    
    public void ProceedGame()
    {
        
        LevelUp();

        Invoke("RandomizeBox", 1f);
       
        
    }

   void InputHandler()
    {
       if(Input.GetKeyUp(KeyCode.H))

        {
            ProceedGame();
        }

    }

    IEnumerator ShowCatPath()
    {


      

        foreach(string bID in catPath)
        {

            
            if(bID == "left")
            {
                Vector2 catSpawnPoint = GameObject.Find("left-box").transform.position;
                Instantiate(blackCat, catSpawnPoint, Quaternion.identity);
                GameObject.Find("left-box").GetComponent<Box>().SetColor(Color.white);
            }

           else if (bID == "right")
            {
                Vector2 catSpawnPoint = GameObject.Find("right-box").transform.position;
                Instantiate(blackCat, catSpawnPoint, Quaternion.identity);
                GameObject.Find("right-box").GetComponent<Box>().SetColor(Color.white);
            }


            else if (bID == "down")
            {
                Vector2 catSpawnPoint = GameObject.Find("down-box").transform.position;
                Instantiate(blackCat, catSpawnPoint, Quaternion.identity);
                GameObject.Find("down-box").GetComponent<Box>().SetColor(Color.white);
            }

            yield return new WaitForSeconds(2f);
        }

      
    }

    public void SetGameOver()
    {
        loserText.SetActive(true);
    }

   
    IEnumerator StartGame ()
    {
 
      yield return new WaitForSeconds(5f);
        TurnMoonLightOff();
        SetGameState(GameStates.running);
        yield return new WaitForSeconds(2f);
        SetPlayerLamp(true);
        GetBoxList();
        RandomizeBox();
      
    }

    


    
}
