using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHit : MonoBehaviour
{
    public bool _move;
    public float _max_Height = 0f;
    public int _damage = 0;
    public CanvasHit _canvasParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        On_Move();
    }

    public void Set_Text(int text, string name, CanvasHit parent)
    {
        _canvasParent = parent;
        _damage += text;
        gameObject.GetComponent<Text>().text = _damage.ToString();
        print(text + " " + name);
    }

    private void On_Move()
    {
        if( _move)
        {
            //print("MOVING!");
            //print(gameObject.GetComponent<Text>().text + " " + gameObject.GetComponentInParent<CanvasHit>().Name);
            
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            transform.rotation = gameObject.GetComponentInParent<Transform>().rotation;
            if(transform.position.y > _max_Height)
            {
                _canvasParent.Destroy_Text();
                Destroy(gameObject,0.5f);
            }
        }
    }
}
