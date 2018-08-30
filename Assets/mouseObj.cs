using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseObj : MonoBehaviour {

    public GameObject ObjToPlace;
    Vector2 mousePos;
    public Text money;
    int CurrentFunds = 1000000; 
	
	// Update is called once per frame
	void Update () {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        if(Input.GetMouseButton(0) && mousePos[1] <= 1){
           
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("base");
            //GameObject closest = null;
            float distance = Mathf.Infinity;
            foreach (GameObject go in gos)
            {
                Vector2 diff = go.transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    //closest = go;
                    distance = curDistance;
                    Debug.Log(distance);
                }
            }

            if (distance > 0 && CurrentFunds >= 150) {
                Instantiate(ObjToPlace, transform.position, Quaternion.identity);
                CurrentFunds = CurrentFunds - 150;
                money.text = "Budget:\n" + CurrentFunds.ToString("C");
            }
        }
	}
}
