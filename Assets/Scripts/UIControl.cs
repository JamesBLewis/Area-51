using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject SelectBox;
    bool toggle = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            GetComponent<mouseObj>().enabled = toggle;
            SelectBox.SetActive(!toggle);
            toggle = !toggle;
        }
    }

}
