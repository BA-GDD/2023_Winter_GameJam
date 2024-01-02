using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public override void Shoot()
    {
        if (CanShoot())
        {


            base.Skill();
        }
    }

    public override void Skill()
    {
        if (CanUseSkill())
        {


            base.Skill();
        }
    }
}
