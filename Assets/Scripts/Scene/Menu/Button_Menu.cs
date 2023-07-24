using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Button_Menu : MonoBehaviour
{
    public Dropdown _dropdown;
    public SaveOptions _saveOptions;
    public Button _Play;
    public Button _Credits;

    private void Awake()
    {
        if (_saveOptions.Get_English_Options())
        {
            _dropdown.value = 1;
            Buttons_English();
            _saveOptions.Set_English(true);
            print("ingles " + _saveOptions.Get_English_Options());
        }
        else
        {
            _dropdown.value = 0;
            Buttons_Spanish();
            _saveOptions.Set_English(false);
            print("español " + _saveOptions.Get_English_Options());

        }
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

    public void Change_Lenguage()
    {
        if(_dropdown.value == 1)
        {
            _saveOptions.Set_English(true);
            Buttons_English();
        }
        else
        {
            _saveOptions.Set_English(false);
            Buttons_Spanish();
        }
    }

    public void Buttons_English()
    {
        _Play.GetComponentInChildren<Text>().text = "START";
        _Credits.GetComponentInChildren<Text>().text = "CREDITS";
    }

    public void Buttons_Spanish()
    {
        _Play.GetComponentInChildren<Text>().text = "JUGAR";
        _Credits.GetComponentInChildren<Text>().text = "CREDITOS";
    }
}
