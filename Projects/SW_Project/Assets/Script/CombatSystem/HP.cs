using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public GameObject owner;
    public Text m_text;

    // Update is called once per frame
    void Update()
    {
        m_text.text = owner.GetComponent<Entity>().HP.ToString();
        if (!owner.activeSelf)
        {
            m_text.gameObject.SetActive(false);
        }
    }
}
