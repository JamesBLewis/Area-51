using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clock : MonoBehaviour {

    public Text Timetext;

	 void FixedUpdate()
    {
        string min;
        string sec;
        float timer = Time.fixedTime;

        if ((int)timer / 60 < 10) {
             min = "0" + ((int)timer / 60).ToString();
        }
        else 
        {
             min = ((int)timer / 60).ToString();
        }

        if (Mathf.RoundToInt(timer % 60) < 10)
        {
             sec = "0" + Mathf.RoundToInt(timer % 60).ToString();
        }
        else
        {
             sec = Mathf.RoundToInt(timer % 60).ToString();
        }

        Timetext.text = min + ":" + sec;

	}
}
