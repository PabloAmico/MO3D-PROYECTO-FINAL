using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection_Box : MonoBehaviour
{
    public Rect _selectionRect; //Se utiliza para detectar si un objeto se encuentra dentro o fuera del rectangulo
    private RectTransform _rectTransform;   //se utiliza para visualizar adecuadamente el rectangulo en pantalla
    private Vector3 _mousePositionInit;

    private bool _selecting = false;

    private float _minSize;

    // Start is called before the first frame update
    void Start()
    {
        this._selectionRect.Set(0, 0, 0, 0); //seteo a 0 la posicion y el tamaño del rectandulo de seleccion
        this._rectTransform = GetComponent<RectTransform>(); //obtengo el rectransform del objeto
        this._rectTransform.gameObject.SetActive(false); //desactivo el objeto
        this._minSize = (Screen.width * 0.05f + Screen.height * 0.05f) / 2f; //medida minima del cuadrado a dibujar para obtener una seleccion valida.
    }

    public void BeginBox(Vector3 MousePos)  //esta funcion se ejecuta cuando realizo un click
    {
        this._mousePositionInit = MousePos;  //seteo la posicion inicial donde hice click
        this._selectionRect.Set(MousePos.x, MousePos.y, 0, 0);   //posiciono el rectangulo en la posicion donde realize el click, pero al ser el primer instante en el que presiono el click las dimensiones son 0
        this._rectTransform.gameObject.SetActive(true);  //Activo el rectangulo
        this._rectTransform.offsetMin = this._selectionRect.min;  //Se coloca el inicio del objeto en la posicion inicial
        this._rectTransform.offsetMax = this._selectionRect.max;  //se coloca el final del objeto en la posicion donde se encuentra el mouse
    }

    public bool isValidBox()
    {
        return this._selectionRect.size.magnitude > this._minSize;
    }

    public void DragClick(Vector3 MousePos)
    {
        //Arrastre hacia la izquierda
        if(MousePos.x < this._mousePositionInit.x)
        {
            this._selectionRect.xMin = MousePos.x;
            this._selectionRect.xMax = this._mousePositionInit.x;
        }
        //Arrastre hacia la derecha
        else
        {
            this._selectionRect.xMin = this._mousePositionInit.x;
            this._selectionRect.xMax = MousePos.x;
        }

        //Arrastre hacia abajo
        if(MousePos.y < this._mousePositionInit.y)
        {
            this._selectionRect.yMin = MousePos.y;
            this._selectionRect.yMax = this._mousePositionInit.y;
        }
        //Arrastre hacia arriba
        else
        {
            this._selectionRect.yMin = this._mousePositionInit.y; ;
            this._selectionRect.yMax = MousePos.y;
        }
        this._rectTransform.offsetMin = this._selectionRect.min; 
        this._rectTransform.offsetMax = this._selectionRect.max;
    }


    public void EndClick()
    {
        this._rectTransform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
