using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Clase que muestra el porcentaje de progreso del cohete.
public class PercentageRocket : MonoBehaviour
{
    Text _text_Percentage;
    Rocket _Rocket;
    void Start()
    {
        _text_Percentage = GetComponent<Text>();
        _Rocket = FindObjectOfType<Rocket>();
    }

    void Update()
    {
        _text_Percentage.text = "" +_Rocket.Get_Percentage() + " %";
    }
}
