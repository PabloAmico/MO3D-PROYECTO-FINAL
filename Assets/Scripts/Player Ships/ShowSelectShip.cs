using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSelectShip : MonoBehaviour
{

    public void Set_Show(bool Show)
    {
       
        GetComponent<SpriteRenderer>().enabled = Show;
       
    }
}
