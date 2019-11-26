using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    public GameObject owner;
    Material m_material;
    private Color temp;
    float timer = 0;
    int ownerID;
    bool first = true;
    bool shaking = false;
    float shakeAmt = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_material = owner.GetComponent<Renderer>().material;
        temp = m_material.color;

        ActionEvent hit = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("TakeDamage", out hit))
        {
            hit.AddListener(Hitted);
        }

        ActionEvent heal = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("SelfHeal", out heal))
        {
            heal.AddListener(Recovered);
        }
    }
    private void Init()
    {
        ownerID = owner.GetComponent<Entity>().ID;
    }

    // Update is called once per frame
    void Update()
    {
        if(first)
        {
            ownerID = owner.GetComponent<Entity>().ID;
            first = false;
        }
        if (owner.GetComponent<Renderer>().material.color != temp)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                owner.GetComponent<Renderer>().material.color = temp;
                timer = 0;
            }
        }
        if(shaking)
        {
            Vector3 newPos;
            newPos.x = transform.position.x + (Random.Range(-0.5f,0.5f) * (Time.deltaTime * shakeAmt));
            newPos.y = transform.position.y;
            newPos.z = transform.position.z;

            transform.position = newPos;
        }
    }

    void Hitted(Entity e)
    {
        if (ownerID == e.target.GetComponent<Entity>().ID)
        {
            owner.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            StartCoroutine(Shake());
        }
    }

    void Recovered(Entity e)
    {
        if(ownerID == e.ID)
        {
            owner.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }
    }

    IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;

        if (shaking == false)
        {
            shaking = true;
        }

        yield return new WaitForSeconds(0.2f);

        shaking = false;
        transform.position = originalPos;
    }
}
