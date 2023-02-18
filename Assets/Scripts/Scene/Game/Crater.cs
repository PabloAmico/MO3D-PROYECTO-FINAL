using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crater : MonoBehaviour
{
    private float _time_enabled = 0.2f;
    BoxCollider _collider = null;
    void Start()
    {
        _collider = gameObject.GetComponent<BoxCollider>();
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
            _collider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" || other.tag == "Ship Enemy"){
            other.GetComponent<StatsUnits>()._points_Life -= 100;
        }
    }
}
