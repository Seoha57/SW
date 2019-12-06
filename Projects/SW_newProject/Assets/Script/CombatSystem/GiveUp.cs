using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiveUp : MonoBehaviour
{
    public Button m_button;
    public Image m_background;
    public Text m_text;
    // Start is called before the first frame update
    void Start()
    {
        m_button.gameObject.SetActive(false);
        m_background.gameObject.SetActive(false);
        m_text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!m_background.gameObject.activeSelf)
            {
                m_background.gameObject.SetActive(true);
                m_button.gameObject.SetActive(true);
                m_text.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                m_background.gameObject.SetActive(false);
                m_button.gameObject.SetActive(false);
                m_text.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}
