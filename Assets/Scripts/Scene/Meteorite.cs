using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public Vector3 _direction;
    public float _velocity;
    public Crater _crater = null;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        Move_Meteorite();
    }


    private void Move_Meteorite()
    {
        transform.position = Vector3.MoveTowards(transform.position, _direction, _velocity * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Destroy(gameObject,0.05f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            Instantiate(_crater,transform.position,Quaternion.identity);
            OnDestroy();
        }
    }
}
