using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneWin : MonoBehaviour
{
    public Text _Message;
    public Button _Button;
    private SaveOptions _SaveOptions;
    // Start is called before the first frame update
    void Start()
    {
        _SaveOptions = GetComponent<SaveOptions>();
        if (!_SaveOptions.Get_English_Options())
        {
            _Message.text = "¡FELICITACIONES COMANDANTE!\r\nHEMOS ESCAPADO DEL PLANETA GRACIAS A USTED.\r\nLA SEGURIDAD DE ESA NAVE ERA DE SUMA IMPORTANCIA PARA NOSOTROS. EN SU INTERIOR SE ENCUENTRA UN ARMA DE DESTRUCCION CAPAZ DE DOBLEGAR A TODAS LAS ESPECIES DEL UNIVERSO.\r\nAHORA NUESTRO ASCENSO A LA CIMA DE LA PIRAMIDE ES INEVITABLE Y EXTINGUIREMOS A TODOS LOS QUE SE NOS OPONGAN. \r\nA PARTIR DE HOY COMIENZA UNA EPOCA DE PAZ PARA NOSOTROS, AUNQUE NO PUEDO AFIRMAR LO MISMO PARA LAS DEMAS ESPECIES.\r\n\r\nNUEVAMENTE COMANDANTE, GRACIAS POR SU SERVICIO.";
            _Button.GetComponentInChildren<Text>().text = "VOLVER A JUGAR";
        }
        else
        {
            _Message.text = "CONGRATULATIONS, COMMANDER! \r\nWE HAVE ESCAPED THE PLANET THANKS TO YOU. \r\nTHE SAFETY OF THAT SHIP WAS UTTERLY IMPORTANT TO US. \r\nTHERE IS A WEAPON INSIDE IT, WHICH IS CAPABLE OF MAKING ALL THE SPECIES OF THE UNIVERSE BEND TO US. \r\nNOW OUR RISE TO THE TOP OF THE PYRAMID IS INEVITABLE AND WE WILL EXTINGUISH ALL WHO DARE OPPOSE US. \r\nFROM THIS MOMENT, A TIME OF PEACE BEGINS FOR US, ALTHOUGH I CANNOT SAY THE SAME FOR OTHER SPECIES. \r\n\r\nAGAIN, COMMANDER, THANK YOU FOR YOUR SERVICE. ";
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
