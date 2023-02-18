using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public float _border = 5f;
    private Vector2 _screen_Border;
    private float _mousePositionX;
    public float _move_Speed = 20f;
    private Vector3 _move;

    public Vector2 _move_Limits;

    public float _zoom_Speed = 20f;
    public float _rotaton_Speed = 20f;

    public float MinY = 20f;
    public float MaxY = 120f;

    Rocket _rocket;

    // Start is called before the first frame update
    void Start()
    {
        _rocket = FindObjectOfType<Rocket>();
       
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 Pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - _border)
        {
            Pos.z += _move_Speed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= _border)
        {
            Pos.z -= _move_Speed * Time.deltaTime;
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - _border)
        {
            Pos.x += _move_Speed * Time.deltaTime;
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= _border)
        {
            Pos.x -= _move_Speed * Time.deltaTime;
        }

       


        float Zoom = Input.GetAxis("Mouse ScrollWheel");

        Pos.y -= Zoom * _zoom_Speed * 100f * Time.deltaTime; 

        Pos.x = Mathf.Clamp(Pos.x, -_move_Limits.x, _move_Limits.x);
        Pos.y = Mathf.Clamp(Pos.y, MinY,MaxY);
        Pos.z = Mathf.Clamp(Pos.z, -_move_Limits.y, _move_Limits.y);

        transform.position = Pos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //print("Press");
            transform.position = new Vector3(_rocket.transform.position.x, Pos.y, _rocket.transform.position.z - 5f);
        }
    }
}
