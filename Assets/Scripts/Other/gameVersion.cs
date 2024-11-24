using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameVersion : MonoBehaviour
{
    [SerializeField] TMP_Text m_gameVersion;
    // Start is called before the first frame update
    void Start()
    {
        m_gameVersion.text = "version: " + Application.version;
    }
}
