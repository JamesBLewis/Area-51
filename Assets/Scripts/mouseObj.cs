using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class mouseObj : MonoBehaviour {
    Sprite[] Ladder;
    public Sprite[] TransformSprites = new Sprite[16];
    public GameObject[] ObjectCatalogue;
    public GameObject SelectBox;
    Dropdown ObjectDropdown;
    Vector2 mousePos;
    public Text money;
    int CurrentFunds = 1000000;
    List<List<int>> Objects = new List<List<int>>();
    List<GameObject> ObjectRef = new List<GameObject>();
    public Material vectorMat;
    // Object prefabs



    void Start()
	{
        ObjectDropdown = SelectBox.GetComponent<Dropdown>();
	}

	void Update () {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
        if(Input.GetMouseButton(0) && mousePos[1] <= 0){

            if (CanPlace((int)transform.position.x, (int)transform.position.y)) {

                GameObject NewObj = Instantiate(ObjectCatalogue[ObjectDropdown.value - 1], transform.position, Quaternion.identity);
                ObjectRef.Add(NewObj);
                // need object index in array! //
                SpriteRenderer BockSprite = NewObj.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

                // avoiding checking game object transform as this needs to exacute very rapidly.
                Objects.Add(new List<int>(new int[] { (int)transform.position.x, (int)transform.position.y }));

                // check what size of sprite we want to place.
                if (ObjectDropdown.value - 1 <= 1)
                {
                    BockSprite.sprite = TransformSprites[WhatToPlace((int)transform.position.x, (int)transform.position.y, true)];
                    BockSprite.material = vectorMat;
                    CurrentFunds = CurrentFunds - 1500;
                } else {
                    // larger than one square
                    CurrentFunds = CurrentFunds - 5000;
                    Debug.Log("more than 1 square sized block");
                }

                money.text = "Budget:\n" + CurrentFunds.ToString("C");
            }
        }
	}
    // can object be placed at square clicked by mouse?
    bool CanPlace (int xCo, int yCo) {
        foreach (var obj in Objects) {

            if (obj[0] == xCo && obj[1] == yCo)
            {
                return (false);
            }
        }
        return (true);
    }

    int WhatToPlace(int xCo,int yCo, bool FixAdjacent = false)
    {

        bool[] ajacentObj = { false, false, false, false };
        bool[,] objectArrangement = new bool[16, 4] {
            {false,false,false,false},
            {false,false,false,true},
            {false,false,true,false},
            {false,false,true,true},
            {false,true,false,false},
            {false,true,false,true},
            {false,true,true,false},
            {false,true,true,true},
            {true,false,false,false},
            {true,false,false,true},
            {true,false,true,false},
            {true,false,true,true},
            {true,true,false,false},
            {true,true,false,true},
            {true,true,true,false},
            {true,true,true,true}

        };

        // is there anything above this object?
        if (!CanPlace(xCo, yCo + 1))
        {
            ajacentObj[0] = true;
            if (FixAdjacent)
            {
                FixSprite(xCo, yCo + 1);
            }

        }

        // is there anything bellow this object?
        if (!CanPlace(xCo, yCo - 1))
        {
            ajacentObj[1] = true;
            if (FixAdjacent)
            {
                FixSprite(xCo, yCo - 1);
            }
        }

            // is there anything to the left of this object?
        if (!CanPlace(xCo - 1, yCo))
        {
            ajacentObj[2] = true;

            if (FixAdjacent)
            {
                FixSprite(xCo - 1, yCo);
            }
        }

        // is there anything to the right of this object?
        if (!CanPlace(xCo + 1, yCo))
        {
            ajacentObj[3] = true;

            if (FixAdjacent)
            {
                FixSprite(xCo + 1, yCo);
            }
        }
        for (int id = 0; id < 16; id++)
        {
            for (int BoolVal = 0; BoolVal < 4; BoolVal++) {
                if (objectArrangement[id,BoolVal] != ajacentObj[BoolVal]) {
                    break;
                }
                if (BoolVal == 3) {
                    return (id);
                }
            }
        }
        Debug.LogError("no object config found!");
        return (0);
    }

    void FixSprite(int xCo, int yCo) {
        for (int i = 0; i < Objects.Count; i++)
        {
            if (Objects[i][0] == xCo && Objects[i][1] == yCo) {
                if (ObjectRef[i].CompareTag("1x1"))
                {
                    SpriteRenderer BockSprite = ObjectRef[i].GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                    int placed = WhatToPlace(xCo, yCo, false);
                    BockSprite.sprite = TransformSprites[placed];
                    Debug.Log("another sprite at "+ xCo +","+ yCo + " was changed to " + placed);
                } 
            }
        }
    }
}
