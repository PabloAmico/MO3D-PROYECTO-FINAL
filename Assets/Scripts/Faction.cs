using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
    public string _name;
    public Material _material_Player;
    public Material _material_Enemy;
    public bool _playable_Unit = false;
    private bool _player_Unit = false;
    public int _layer_Material = 2;
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
    // Start is called before the first frame update
    void Start()
    {
        //Seteo la faccion de la unidad.
       

        //Inicializo la unidad, con las configuraciones necesarias.
        Init_Unit();
    }

    // Update is called once per frame
    void Update()
    {
        //Init_Unit();
    }

    void Init_Unit()
    {
        var Renderer = this.GetComponent<MeshRenderer>();
        Material[] materials = Renderer.sharedMaterials;
        
        if (Is_PlayerUnit)
        {
            materials[_layer_Material] = this._material_Player;
            Renderer.sharedMaterials = materials;
        }
        else
        { 
            materials[_layer_Material] = this._material_Enemy;
            Renderer.sharedMaterials = materials;
        }
    }

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
