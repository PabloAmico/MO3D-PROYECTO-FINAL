using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crater : MonoBehaviour
{
    private float _time_enabled = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        _time_enabled -= Time.deltaTime;
        if(_time_enabled <= 0)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
