using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGameOver : MonoBehaviour
{
    public Button _Button;
    public Text _Message;
    private SaveOptions _SaveOptions;
    // Start is called before the first frame update
    void Start()
    {
        _SaveOptions = GetComponent<SaveOptions>();
        if (!_SaveOptions.Get_English_Options())
        {
            _Message.text = "HAS PERDIDO";
            _Button.GetComponentInChildren<Text>().text = "HAS PERDIDO";
        }
        else
        {
            _Message.text = "GAME OVER";
            _Button.GetComponentInChildren<Text>().text = "PLAY AGAIN";
        }


    }
    public void Run_game()
    {
        SceneManager.LoadScene("SceneGame");
    }

    public void Run_Menu()
    {
        SceneManager.LoadScene("SceneMenu");
    }

}
