using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : BaseBullets
{
    protected override void Set_Texture()
    {
        if (_ship_Shooter.GetComponent<Faction>().Is_PlayerUnit)
        {
            gameObject.GetComponent<MeshRenderer>().material = _material_Bullet_Player;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = _material_Bullet_Enemy;
        }
    }


}
