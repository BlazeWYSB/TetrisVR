using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_maintimelineContoller : MonoBehaviour
{
    public GameObject ActiveObject;
    public GameObject NextObject;
    GameObject L_block_perfab;
    GameObject T_block_perfab;
    GameObject I_block_perfab;
    GameObject Trans_block_perfab;
    Vector3 start_position;
    Quaternion start_rotation;
    // Start is called before the first frame update
    void Start()
    {
        start_position = new Vector3(0, 11, 0);
        start_rotation = new Quaternion(90, 0, 0, 0);
        L_block_perfab = Resources.Load("Prebs/L") as GameObject;
        T_block_perfab = Resources.Load("Prebs/T") as GameObject;
        I_block_perfab = Resources.Load("Prebs/I") as GameObject;
        Trans_block_perfab = (GameObject)Resources.Load("Prebs/Transform");
        ActiveObject = Instantiate(Trans_block_perfab, start_position, start_rotation);
        //Debug.Log("???");
        InvokeRepeating("Block_Drop", 1f, 1.5f);
    }

    // Update is called once per frame
    void Block_Drop()
    {
        ActiveObject.transform.Translate(Vector3.up);
    }
}
