using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class maxscoreShower : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Max Score: " + PlayerPrefs.GetFloat("maxScore");
    }
}
