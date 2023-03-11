using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUnit : MonoBehaviour
{
    private Plane _ground_Plane;    //El plano
    
    public Selection_Box _box;  //Caja de seleccion
    private bool _selecting = false;    

    public List<Unit> _units = new List<Unit>();    //Lista con las unidades creadas
    public List<Unit> _units_Selected = new List<Unit>();   //Lista con las unidades seleccionadas
    public List<Unit> _units_Total = new List<Unit>();  //Lista con las unidades totales

    private SFX_Radio _radio;   //radio con las voces de las naves.

    public int _max_Units = 200;

    public GameObject _cross = null;    //Cruz que se crea en la posicion donde iran las naves.
 

   
    void Start()
    {
       _radio = FindObjectOfType<SFX_Radio>();
        this._ground_Plane.SetNormalAndPosition(Vector3.up, Vector3.zero); //plano en la posicionado en la posicion zero y la normal apuntando hacia arriba
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))    //Si presiono el click izquierdo
        {
            this._selecting = true; //estoy seleccionando
            this._box.BeginBox(Input.mousePosition);    //Le paso la posicion de inicio a la caja.
        }

        if (this._selecting)
        {
            //Si se movio el mouse
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                //Arrastre
                this._box.DragClick(Input.mousePosition);



                foreach (Unit u in _units)  //recorro la lista con todas las unidades creadas
                {
                    if (u.gameObject.GetComponent<Faction>().Is_PlayerUnit) //Si es una unidad jugable
                    {
                        Vector2 Coord_Screen = Camera.main.WorldToScreenPoint(u.transform.position);    //Obtengo su posicion dentro de la camara

                        if (this._box._selectionRect.Contains(Coord_Screen))    //Si la unidad se encuentra dentro de la caja de sellecion
                        {
                            if (!u.Is_Selected) //Y la unidad no se encuentra seleccionada
                            {
                                u.Is_Selected = true;   //La unidad se selecciona
                                u.GetComponentInChildren<ShowSelectShip>().Set_Show(true);  //Y activo el sprite que se encuentra debajo de la nave para mostrar que se encuentra seleccionada.
                                this._units_Selected.Add(u);    //Agrego esta unidad a la lista de unidades seleccionadas
                            }
                        }
                        else
                        {
                            if (u.Is_Selected)  //si la unidad esta seleccionada y no se encuentra dentro de la caja de seleccion
                            {
                                u.Is_Selected = false;  //La deselecciono
                                u.GetComponentInChildren<ShowSelectShip>().Set_Show(false); //Desactivo el sprite que muestra a la tropa como seleccionada
                                this._units_Selected.Remove(u); //La remuevo de la lista de tripas seleccionadas
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))  //Si dejo de presionas el click izquierdo
        {
            
            this._selecting = false;    //La seleccion se vuelve falsa.
            this._box.EndClick();   //Y aviso a la caja que solte el click izquierdo.

            if (!this._box.isValidBox())    //Si la caja no es valida.
            {
                //Limpiar la seleccion anterior
                for(int i = 0; i< this._units_Selected.Count; i++)
                {
                    this._units_Selected[i].Is_Selected = false;
                    this._units_Selected[i].GetComponentInChildren<ShowSelectShip>().Set_Show(false);
                    this._units_Selected[i]._select_Attack = false;
                   
                }
                this._units_Selected.Clear();   //Limpio la lista de unidades seleccionadas.

                //lo que sigue a continuacion sirve para seleccionar una sola unidad con el click.

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //Creo un raycas en la posicion donde levante el mouse.
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, 200))  //Si el raycast colisiono con algo
                {
                    GameObject ObjectHit = hit.collider.gameObject;
                    Unit u  = ObjectHit.GetComponent<Unit>();

                    if(u != null && u.gameObject.GetComponent<Faction>().Is_PlayerUnit) //Y ese algo es una unidad del jugador
                    {
                        //Se selecciona esta unidad.
                        u.Is_Selected = true;
                        u.GetComponentInChildren<ShowSelectShip>().Set_Show(true);
                        this._units_Selected.Add(u);
                    }
                }
            }
        }

         if (Input.GetMouseButtonDown(1))   //Si presiono el click derecho.
         {
             //Posicion de destino
             Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);   //Creo un rayo en la posicion donde hice click
             float Distance;


             _ground_Plane.Raycast(Ray, out Distance);  //Si hice click sobre el suelo
             Vector3 Point = Ray.GetPoint(Distance);    //obtengo el punto
            _cross.transform.position = new Vector3(Point.x, Point.y + 0.02f, Point.z); //posiciono la cruz en el piso
             
            RaycastHit Hit = CastRay(); //Si selecciono una unidad para atacar

           
            
            foreach (Unit u in _units_Selected) //Recorro la lista de unidades seleccionadas.
            {
                if (Hit.collider != null)
                {
                    
                    if (Hit.collider.gameObject.CompareTag("Ship Enemy"))   //Si hice click derecho sobre un enemigo, lo ataco.
                    {
                        
                        //asigno a las unidades seleccionadas que ataquen al enemigo seleccionado
                        u.GetComponent<StatsUnits>()._unit_Objective = Hit.collider.gameObject.GetComponent<StatsUnits>();
                        Point = Hit.collider.transform.position;
                        u._select_Attack = true;
                       
                    }
                    else    //Sino, si hice click sobre el plano le digo a las tropas seleccionadas que no tienen objetivo apra atacar.
                    {
                        u._select_Attack = false;
                        u.gameObject.GetComponent<StatsUnits>()._unit_Objective = null;
                        
                    }
                }
                int Random_Radio = Random.Range(0, 2);
                if(Random_Radio == 0)   //Activo la radio.
                {
                    _radio.Run();
                }
                else
                {
                    _radio.Roger_That();
                }

                u.OnMove(Point);    //Y le digo a la unidad que se mueva hasta el punto.
            }

           


         }
    }


//Metodo que se utiliza para saber si el jugador hizo click derecho sobre una unidad enemiga.
    private RaycastHit CastRay()
    {
        Vector3 screenMousePositionFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePositionNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePositionFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePositionNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}
