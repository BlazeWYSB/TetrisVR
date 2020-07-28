using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Global : MonoBehaviour
{
    public static bool isTriggerHappend;
    //这个bool分别是长宽高
    public static bool[,,] ColliderState = new bool[8, 8, 8];
}


public class Game_maintimelineContoller : MonoBehaviour
{
    public GameObject ColliderFather;
    GameObject ColliderTry;
    GameObject ActiveObject;
    GameObject NextObject;
    GameObject PreviousObject;
    GameObject L_block_perfab;
    GameObject T_block_perfab;
    GameObject I_block_perfab;
    GameObject Zeta_block_perfab;
    GameObject Trans_block_perfab;
    Vector3 start_position;
    Quaternion start_rotation;

    void ColliderTest()
    {
        int Space_a = 8;
        GameObject ColliderTry = Resources.Load("Prebs/ColliderTry") as GameObject;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (Global.ColliderState[i, j, k] == true)
                    {
                        Instantiate(ColliderTry, new Vector3(i+10, 0.5f + k, j), new Quaternion(90, 0, 0, 0));
                    }
                }
            }
        }
    }

    void ColliderCreater()
    {   
        //长宽和高，便于修改
        int Space_a = 8;
        GameObject ColliderPreb = Resources.Load("Prebs/ColliderPrefabs") as GameObject;
        GameObject ColliderInst;
        for (int i = 0; i < 512; i++)
        {   
            //这里vector3的参数分别是长高宽
            ColliderInst=Instantiate(ColliderPreb, new Vector3(i % Space_a, 0.5f + i / (Space_a * Space_a), (i / Space_a) % Space_a), new Quaternion(90, 0, 0, 0), ColliderFather.transform);
            ColliderInst.name = "" + i;
        }
    }

// Start is called before the first frame update
    void Start()
    {
        ColliderCreater();
        start_position = new Vector3(4f, 10.5f, 4f);
        start_rotation = new Quaternion(90, 0, 0, 0);
        Global.isTriggerHappend = false;
        L_block_perfab = Resources.Load("Prebs/L_Object") as GameObject;
        T_block_perfab = Resources.Load("Prebs/T_Object") as GameObject;
        I_block_perfab = Resources.Load("Prebs/I_Object") as GameObject;
        Zeta_block_perfab = Resources.Load("Prebs/Zeta_Object") as GameObject;
        PreviousObject = Resources.Load("Prebs/Plane") as GameObject;
        NextObject = ramdom_block();
        ActiveObject = Instantiate(ramdom_block(), start_position, start_rotation);
        InvokeRepeating("Block_Drop", 1f, 1f);
    }


    //随机选取方块函数
    GameObject ramdom_block()
    {
        int tmp = Random.Range(0, 115);
        if (tmp <= 28)
            return L_block_perfab;
        else if (tmp <= 57)
            return T_block_perfab;
        else if (tmp <= 86)
            return I_block_perfab;
        else
            return Zeta_block_perfab;
    }

    //小键盘移动方块
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
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ActiveObject.transform.RotateAround(ActiveObject.transform.Find("Cube").localPosition, Vector3.up, 90);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ActiveObject.transform.RotateAround(ActiveObject.transform.Find("Cube").localPosition, Vector3.right, 90);
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
    {
        ColliderTest();
        if (Global.isTriggerHappend == false) 
            ActiveObject.transform.Translate(Vector3.up);
        if (Global.isTriggerHappend == true)
        {
            Debug.Log("碰撞发生");
            NextObject = ramdom_block();
            foreach (Transform child in ActiveObject.transform)
            {
                Destroy(child.GetComponent<Rigidbody>());
                child.GetComponent<MeshCollider>().isTrigger = true;
            }
            PreviousObject = ActiveObject;
            ActiveObject = Instantiate(NextObject, start_position, start_rotation);
            Global.isTriggerHappend = false;


            foreach (Transform child in PreviousObject.transform)
            {
               child.gameObject.AddComponent<ActiveObjectrigger>();
            }
        }
    }
}
