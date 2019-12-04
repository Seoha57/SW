﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    public ParticleSystem[] particleLauncher;
    // Start is called before the first frame update
    void Start()
    {
        ActionEvent SelfHeal = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("SelfHeal", out SelfHeal))
        {
            SelfHeal.AddListener(SelfRecoveryParticle);
        }

        ActionEvent fire = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("UseFire", out fire))
        {
            fire.AddListener(FireballEffect);
        }

        ActionEvent thunder = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("UseThunder", out thunder))
        {
            thunder.AddListener(ThunderParticle);
        }

        ActionEvent attack = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("UseAttack", out attack))
        {
            attack.AddListener(PhysicalParticle);
        }

        ActionEvent arrowShot = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("UseArrowShot", out arrowShot))
        {
            arrowShot.AddListener(PhysicalParticle);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FireballEffect(Entity e)
    {
        Vector3 pos = e.target.GetComponent<Transform>().position;
        particleLauncher[0].GetComponent<Transform>().position = new Vector3(pos.x, pos.y - 0.5f, pos.z);
        particleLauncher[1].GetComponent<Transform>().position = new Vector3(pos.x, pos.y - 0.5f, pos.z);

        particleLauncher[0].Emit(1);
        particleLauncher[1].Emit(10);
    }

    void SelfRecoveryParticle(Entity e)
    {
        Vector3 pos = e.transform.position;
        particleLauncher[2].GetComponent<Transform>().position = new Vector3(pos.x, pos.y - 1f, pos.z);
        particleLauncher[3].GetComponent<Transform>().position = new Vector3(pos.x, pos.y - 1f, pos.z);
        particleLauncher[2].Emit(1);
        particleLauncher[3].Emit(10);
    }

    void ThunderParticle(Entity e)
    {
        Vector3 pos = e.target.GetComponent<Transform>().position;
        particleLauncher[4].GetComponent<Transform>().position = new Vector3(pos.x, pos.y + 3f, pos.z);
        particleLauncher[5].GetComponent<Transform>().position = new Vector3(pos.x, pos.y + 3f, pos.z);

        particleLauncher[4].Emit(1);
        particleLauncher[5].Emit(10);
    }

    void PhysicalParticle(Entity e)
    {
        if(e.ID == 0)
        {
            Vector3 pos = e.target.GetComponent<Transform>().position;
            particleLauncher[6].GetComponent<Transform>().position = new Vector3(pos.x, pos.y - 0.5f, pos.z);
            particleLauncher[7].GetComponent<Transform>().position = new Vector3(pos.x, pos.y - 0.5f, pos.z);

            particleLauncher[6].Emit(1);
            particleLauncher[7].Emit(10);
        }
    }
    void ArrowShotParticle(Entity e)
    {
        Vector3 pos = e.target.GetComponent<Transform>().position;
        particleLauncher[8].GetComponent<Transform>().position = new Vector3(pos.x, pos.y - 0.5f, pos.z);
        particleLauncher[9].GetComponent<Transform>().position = new Vector3(pos.x, pos.y - 0.5f, pos.z);

        particleLauncher[8].Emit(1);
        particleLauncher[9].Emit(10);
    }
}
