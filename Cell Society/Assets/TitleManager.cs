using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    public Animator billAnim;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        billAnim.Play("transition");
        Invoke("LoadMainLevel", 2f);

    }

    private void LoadMainLevel()
    {
            SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        billAnim.Play("transition");
        Application.Quit();
    }


}

