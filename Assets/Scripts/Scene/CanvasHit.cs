using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHit : MonoBehaviour
{
    public TextHit _text_Prefab;
    public string Name;
    //private bool _text_enabled = false;
    private TextHit _text_Instantiate = null;
    // Start is called before the first frame update
    void Start()
    {
        Name = gameObject.GetComponentInParent<StatsUnits>().name;
    }

    // Update is called once per frame
    void Update()
    {
        UI_Look_Camera();
    }

    public void Create_Text(int life)
    {
        if (_text_Instantiate == null)
        {
            try
            {
                _text_Instantiate = Instantiate(_text_Prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                _text_Instantiate.transform.parent = gameObject.transform;
                // aux.GetComponent<Text>().text = "HOLA";

                _text_Instantiate.Set_Text(life, gameObject.GetComponentInParent<StatsUnits>().name, gameObject.GetComponent<CanvasHit>());

                _text_Instantiate._max_Height = gameObject.GetComponent<RectTransform>().anchorMax.y + transform.position.y;
                _text_Instantiate._move = true;
            }
            catch { }
        }
        else
        {
            _text_Instantiate.Set_Text(life, gameObject.GetComponentInParent<StatsUnits>().name, gameObject.GetComponent<CanvasHit>());
        }
        
    }

    public void Destroy_Text()
    {
        _text_Instantiate = null;
    }

    private void UI_Look_Camera()
    {
        transform.forward = Camera.main.transform.forward;
        
    }
}
