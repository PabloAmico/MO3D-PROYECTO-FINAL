using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Button_Menu : MonoBehaviour
{

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Run_game(){
        SceneManager.LoadScene("SceneGame");
    }

    public void Run_Credits(){
        SceneManager.LoadScene("SceneCredits");
    }

    public void Run_Menu(){
        SceneManager.LoadScene("SceneMenu");
    }
}