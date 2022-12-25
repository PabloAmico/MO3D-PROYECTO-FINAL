using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUnit : MonoBehaviour
{
    private Plane _ground_Plane;
    
    public Selection_Box _box;
    private bool _selecting = false;

    public List<Unit> _units = new List<Unit>();
    public List<Unit> _units_Selected = new List<Unit>();
    public List<Unit> _units_Total = new List<Unit>();

    private SFX_Radio _radio;

    public int _max_Units = 0;
    // Start is called before the first frame update

   
    void Start()
    {
       _radio = FindObjectOfType<SFX_Radio>();
        this._ground_Plane.SetNormalAndPosition(Vector3.up, Vector3.zero); //plano en la posicionado en la posicion zero y la normal apuntando hacia arriba
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this._selecting = true;
            this._box.BeginBox(Input.mousePosition);
        }

        if (this._selecting)
        {
            //Si se movio el mouse
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                //Arrastre
                this._box.DragClick(Input.mousePosition);



                foreach (Unit u in _units)
                {
                    if (u.gameObject.GetComponent<Faction>().Is_PlayerUnit)
                    {
                        Vector2 Coord_Screen = Camera.main.WorldToScreenPoint(u.transform.position);

                        if (this._box._selectionRect.Contains(Coord_Screen))
                        {
                            if (!u.Is_Selected)
                            {
                                u.Is_Selected = true;
                                u.GetComponentInChildren<ShowSelectShip>().Set_Show(true);
                                this._units_Selected.Add(u);
                            }
                        }
                        else
                        {
                            if (u.Is_Selected)
                            {
                                u.Is_Selected = false;
                                u.GetComponentInChildren<ShowSelectShip>().Set_Show(false);
                                this._units_Selected.Remove(u);
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            
            this._selecting = false;
            this._box.EndClick();

            if (!this._box.isValidBox())
            {
                //Limpiar la seleccion anterior
                for(int i = 0; i< this._units_Selected.Count; i++)
                {
                    this._units_Selected[i].Is_Selected = false;
                    this._units_Selected[i].GetComponentInChildren<ShowSelectShip>().Set_Show(false);
                    this._units_Selected[i]._select_Attack = false;
                   
                }
                this._units_Selected.Clear();

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, 200))
                {
                    GameObject ObjectHit = hit.collider.gameObject;
                    Unit u  = ObjectHit.GetComponent<Unit>();

                    if(u != null && u.gameObject.GetComponent<Faction>().Is_PlayerUnit)
                    {
                        u.Is_Selected = true;
                        u.GetComponentInChildren<ShowSelectShip>().Set_Show(true);
                        this._units_Selected.Add(u);
                    }
                }
            }
        }

         if (Input.GetMouseButtonDown(1))
         {
             //Posicion de destino
             Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             float Distance;


             _ground_Plane.Raycast(Ray, out Distance);
             Vector3 Point = Ray.GetPoint(Distance);
             
            RaycastHit Hit = CastRay(); //Si selecciono una unidad para atacar

           
            
            foreach (Unit u in _units_Selected)
            {
                if (Hit.collider != null)
                {
                    
                    if (Hit.collider.gameObject.CompareTag("Ship Enemy"))
                    {
                        
                        //asigno a las unidades seleccionadas que ataquen al enemigo seleccionado
                        u.GetComponent<StatsUnits>()._unit_Objective = Hit.collider.gameObject.GetComponent<StatsUnits>();
                        //Point = u.transform.position;
                        Point = Hit.collider.transform.position;
                        u._select_Attack = true;
                       
                    }
                    else
                    {
                        u._select_Attack = false;
                        u.gameObject.GetComponent<StatsUnits>()._unit_Objective = null;
                        //print(u.gameObject.GetComponent<StatsUnits>()._unit_Objective);
                    }
                }
                int Random_Radio = Random.Range(0, 2);
                if(Random_Radio == 0)
                {
                    _radio.Run();
                }
                else
                {
                    _radio.Roger_That();
                }
                print("POINT " + Point);
                u.OnMove(Point);
            }

            // this._agent.SetDestination(Point);


         }
    }

    private RaycastHit CastRay()
    {
        //print("RAY");
        Vector3 screenMousePositionFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePositionNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePositionFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePositionNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}
