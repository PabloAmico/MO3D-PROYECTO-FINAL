using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera_Menu : MonoBehaviour
{
    public GameObject _rocket;

    void Update()
    {
        gameObject.transform.LookAt(_rocket.transform);
        gameObject.transform.RotateAround(_rocket.transform.position, Vector3.up, 5f * Time.deltaTime); //Mueve la camara alrededor del cohete.
    }
}
