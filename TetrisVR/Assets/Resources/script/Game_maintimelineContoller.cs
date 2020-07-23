using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Global : MonoBehaviour
{
    public static bool isTriggerHappend;
}



public class Game_maintimelineContoller : MonoBehaviour
{
    GameObject ActiveObject;
    GameObject NextObject;
    GameObject PreviousObject;
    GameObject L_block_perfab;
    GameObject T_block_perfab;
    GameObject I_block_perfab;
    GameObject Trans_block_perfab;
    Vector3 start_position;
    Quaternion start_rotation;
    

    // Start is called before the first frame update
    void Start()
    {
        start_position = new Vector3(0f, 10.5f, 0f);
        start_rotation = new Quaternion(90, 0, 0, 0);
        Global.isTriggerHappend = false;
        L_block_perfab = Resources.Load("Prebs/L") as GameObject;
        T_block_perfab = Resources.Load("Prebs/T") as GameObject;
        I_block_perfab = Resources.Load("Prebs/I") as GameObject;
        PreviousObject = Resources.Load("Prebs/Plane") as GameObject;
        Trans_block_perfab = (GameObject)Resources.Load("Prebs/Transform");
        NextObject = ramdom_block();
        ActiveObject = Instantiate(ramdom_block(), start_position, start_rotation);
        //Debug.Log("???");
        InvokeRepeating("Block_Drop", 1f, 0.8f);
    }

    GameObject ramdom_block()
    {
        int tmp = Random.Range(0, 101);
        if (tmp <= 28)
            return L_block_perfab;
        else if (tmp <= 57)
            return T_block_perfab;
        else if (tmp <= 86)
            return I_block_perfab;
        else
            return Trans_block_perfab;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            ActiveObject.transform.Translate(Vector3.forward * -1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ActiveObject.transform.Translate(Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ActiveObject.transform.Translate(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            ActiveObject.transform.Translate(Vector3.right);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        Global.isTriggerHappend = true;
    }
    void OnTriggerExit(Collider collision)
    {
        Global.isTriggerHappend = false;
    }

    // Update is called once per frame
    void Block_Drop()
    {   if (Global.isTriggerHappend == false) 
            ActiveObject.transform.Translate(Vector3.up);
        if (Global.isTriggerHappend == true)
        {
            Debug.Log("碰撞发生");
            NextObject = ramdom_block();
            Destroy(ActiveObject.GetComponent<Rigidbody>());
            BoxCollider[] boxcs = ActiveObject.GetComponents<BoxCollider>();
            for (int i = 0; i < boxcs.Length; i++)
            {
                boxcs[i].isTrigger = true;
            }
            PreviousObject = ActiveObject;
            ActiveObject = Instantiate(NextObject, start_position, start_rotation);
            PreviousObject.AddComponent<ActiveObjectrigger>();
            Global.isTriggerHappend = false;
        }
    }
}
