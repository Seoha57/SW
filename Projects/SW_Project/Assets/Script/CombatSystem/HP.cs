using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public GameObject owner;
    public Text m_text;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        m_text.text = owner.GetComponent<Entity>().HP.ToString();
    }
}
