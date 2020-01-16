using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    

    public bool gameOver = false;
    private bool playerWon = false;
    private bool loadMenu = false;
    public Text levelText;
    public GameObject pausedText;
    public GameObject winText;
    public Player player;
    public GameObject heart;
    public GameObject loserText;
    public GameObject blackCat;
    public GameObject redEyedCat;
    public GameObject moon;
    public GameObject moonLight;
    public GameObject lamp;
    public Transform hSpawnPoint;
    public Camera mainCamera;  
    public enum GameStates {running,waiting,paused}

    public GameStates gameStates;

    public bool noLight;
    public bool isPaused;
    private float  blackoutCounter = 0;
    private GameObject[] box;
    private bool firstCatHasSpawned = false;
    [SerializeField] [Range(0, 1)] private float duration;
    [SerializeField] private int gameLevel = 0;

    private GameObject nextBox;

    
    private string[] catPath = new string[100];

    void Start()
    {
        Time.timeScale = 1f;
        loserText.SetActive(false);
        pausedText.SetActive(false);
        winText.SetActive(false);
        SetPlayerLamp(false);
        SetGameState(GameStates.waiting);
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {

        PauseHandler();

       if(gameStates == GameStates.running && !noLight)
        {
          mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.black, duration);
          
        }

       if(gameLevel == 99 & playerWon == false)
        {
            playerWon = true;
            winText.SetActive(true);

        }

        blackoutCounter += Time.deltaTime;
        if (blackoutCounter > 10f)
            noLight = true;

        InputHandler();


        if (isPaused)
            if (Input.GetKeyUp(KeyCode.Q))
            {
                SceneManager.LoadScene(0);
                isPaused = false;
            }
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
        if (firstCatHasSpawned == false)
            firstCatHasSpawned = true;

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
        Instantiate(heart, hSpawnPoint.position, Quaternion.identity);

    }

    
    public void ProceedGame()
    {
        
        if(playerWon == false)
        {
            ComparePaths();
            if (gameOver == false)

            {
                LevelUp();
                player.ResetPath();
                Invoke("RandomizeBox", 1f);

            }
        }
     
       
       
        
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

            yield return new WaitForSeconds(1f);
        }

      
    }

    public void SetGameOver()
    {
        loserText.SetActive(true);
        gameOver = true;
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
        isPaused = false;
      
    }


    public void ComparePaths()
    {
        int index = 0;

        string[] playerPath = player.GetPath();

        for(int i = 0; i < 99; i++)


        {
            if (playerPath[i] != catPath[i])
            {
                Debug.Log(playerPath[i]);
                Debug.Log(catPath[i]);
                SetGameOver();


            }

            else if (playerPath[0] == null)
                SetGameOver();
           


        }

    }

    public void PauseHandler()

    {
        
        if(Input.GetKeyUp(KeyCode.Escape))

        {
            if (!isPaused)
            {
                
                pausedText.SetActive(true);
                Time.timeScale = 0;
                isPaused = !isPaused;

               
            }

            else
            {
                pausedText.SetActive(false);
                Time.timeScale = 1;
                isPaused = !isPaused;
            }



            

        }


        
        if(gameOver == true & loadMenu == false)
        {

            loadMenu = true;
            Invoke("BackToMenu", 2f);

        }


    }

    void BackToMenu()
    {

        SceneManager.LoadScene(0);

    }


}
