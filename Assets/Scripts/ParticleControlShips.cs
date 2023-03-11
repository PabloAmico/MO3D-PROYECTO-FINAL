using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControlShips : MonoBehaviour
{
    protected ParticleSystem[] _particleSystem;
    StatsUnits _statsUnits;
    public Material _material;
    
    void Start()
    {
        _statsUnits = GetComponent<StatsUnits>();
        _particleSystem = GetComponentsInChildren<ParticleSystem>(); //Obtengo las particulas de los hijos
        foreach (ParticleSystem Particle in _particleSystem) //Detengo las animaciones de las particulas
        {
            Particle.Stop();
        }
    }

    void Update()
    {
        if (_statsUnits._points_Life <= 0)
        {
            Kill_Unit();
        }
    }

//Metodo para instanciar las particulas cuando muere la nave.
    private void Kill_Unit()
    {
        if (!_statsUnits._is_Dead)
        {
            _statsUnits._is_Dead = true;
            _particleSystem[2].Play();  //Activo el fuego y la explosion.
        }
        if (!_particleSystem[2].isPlaying && !_particleSystem[1].isPlaying)
        {
            _particleSystem[1].Play();
            
        }
        
    }

//Metodo para activas las particulas
    public void Active_Particles()
    {
        if (_statsUnits._points_Life <= (_statsUnits._points_Life_Max * 0.6) && _statsUnits._points_Life > (_statsUnits._points_Life_Max * 0.4))
        {
            _particleSystem[0].Play();  //Activo el humo
        }

        if (_statsUnits._points_Life <= (_statsUnits._points_Life_Max * 0.4) && _statsUnits._points_Life > 0)
        {
            var Aux_em = _particleSystem[0].emission;
            Aux_em.enabled = true;
            Aux_em.rateOverTime = 30;


        }
    }
}
