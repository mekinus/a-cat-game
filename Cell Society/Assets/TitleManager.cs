using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    public GameObject manualBoard;
    public Animator billAnim;
    private bool manualIsDisplayed;
    // Start is called before the first frame update
    void Start()
    {
        manualIsDisplayed = false;
        manualBoard.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    public void StartGame()
    {
        billAnim.Play("transition");
        Invoke("LoadMainLevel", 2f);

    }

    private void LoadMainLevel()
    {
            SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        billAnim.Play("transition");
        Application.Quit();
    }



    public void InputHandler()
    {
        if(Input.GetKeyUp(KeyCode.Escape) & manualIsDisplayed == false)

        {
            manualIsDisplayed = true;
            manualBoard.SetActive(true);

        }

        else if (Input.GetKeyUp(KeyCode.Escape) & manualIsDisplayed == true)

        {
            manualIsDisplayed = false;
            manualBoard.SetActive(false);

        }
    }
}

