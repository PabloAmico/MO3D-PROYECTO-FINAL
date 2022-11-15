using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBox : MonoBehaviour
{
    public Selection_Box box;
    private bool _selecting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print("update");
        if (Input.GetMouseButtonDown(0))
        {
            //print("Click");
            this._selecting = true;
            box.BeginBox(Input.mousePosition);
        }

        if (_selecting)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                //Estamos arrastrando
                box.DragClick(Input.mousePosition);
            }
        }


        if (Input.GetMouseButtonUp(0))
        {

            box.EndClick();
            this._selecting = false;
        }
    }
}

