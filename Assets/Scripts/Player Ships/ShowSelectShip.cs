using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSelectShip : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void Set_Show(bool Show)
    {
       
        GetComponent<SpriteRenderer>().enabled = Show;
       
    }
}
