using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Todas las naves tiene una faccion. Se dividen en naves del jugador y enemigas.
public class Faction : MonoBehaviour
{
    public string _name;    //Nombre de la nave.
    public Material _material_Player;   //Material que se coloca para que las naves tengan el color del jugador.
    public Material _material_Enemy;    //Material que se coloca para que las naves tengan el color del enemigo.
    public bool _playable_Unit = false; //Si la nave es jugable esta variable es verdad.
    private bool _player_Unit = false;  
    public int _layer_Material = 2; //posicion en el array de materiales que tengo que cambiar para ser nave del enemigo o del jugador.

//Metodo que GetSet para el atributo _playable_Unit.
    public bool Is_PlayerUnit
    { get
        {
            return this._player_Unit;
        }
        set
        {
            this._player_Unit = value;
        }
    }

    private void Awake()
    {
        Is_PlayerUnit = this._playable_Unit;
    }
    
    void Start()
    {
        //Inicializo la unidad, con las configuraciones necesarias.
        Init_Unit();
    }

//Metodo para inicializar el color de la unidad cambiando el material.
    void Init_Unit()
    {
        var Renderer = this.GetComponent<MeshRenderer>();   //Obtengo la malla.
        Material[] materials = Renderer.sharedMaterials;    //Obtengo todos los materiales aplicados a la nave.
        
        if (Is_PlayerUnit)  //Si es una unidad del jugador le asigno el material del jugador.
        {
            materials[_layer_Material] = this._material_Player;
            Renderer.sharedMaterials = materials;
        }
        else    //Sino le asigno el material del enemigo.
        { 
            materials[_layer_Material] = this._material_Enemy;
            Renderer.sharedMaterials = materials;
        }
    }

//Este metodo no se utiliza. Ya que no se llego a crear una nave que cambia la faccion del enemigo (WOLOLOLOOO!).
    public void Change_Faction()    //cambia la faccion.
    {
        var Renderer = this.GetComponent<MeshRenderer>();
        Material[] materials = Renderer.sharedMaterials;

        if (Is_PlayerUnit)
        {
            Is_PlayerUnit=false;
            materials[2] = this._material_Player;
            Renderer.sharedMaterials = materials;
        }
        else
        {
            Is_PlayerUnit=true;
            materials[2] = this._material_Enemy;
            Renderer.sharedMaterials = materials;
        }
    }

}
