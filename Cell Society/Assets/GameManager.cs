using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject moon;
    public GameObject moonLight;
    public GameObject lamp;
    public Camera mainCamera;  
    public enum GameStates {running,waiting,paused}

    public GameStates gameStates;

    public bool noLight;
    private float  blackoutCounter = 0;
    [SerializeField] [Range(0, 1)] private float duration;
    void Start()
    {
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


    IEnumerator StartGame ()
    {
 
      yield return new WaitForSeconds(5f);
        TurnMoonLightOff();
        SetGameState(GameStates.running);
        yield return new WaitForSeconds(2f);
        SetPlayerLamp(true);
      
    }


    
}
