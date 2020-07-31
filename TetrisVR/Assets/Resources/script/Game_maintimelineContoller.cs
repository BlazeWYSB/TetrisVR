using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Global : MonoBehaviour
{
    //这个bool分别是长宽高
    public static bool[,,] ColliderState = new bool[9, 9, 9];
}


public class Game_maintimelineContoller : MonoBehaviour
{
    public RawImage black_screen;
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
    bool isTriggerHappend = false;

    int Space_a = 9;

    Vector3[] currentcube= new Vector3[4];
    Vector3[] nextcube = new Vector3[4];
    Vector3[] previecube = new Vector3[4];

    //获取当前移动物体
    void GetCurrentVector()
    {
        for (int i = 0; i < ActiveObject.transform.childCount; i++)
        {
            var child = ActiveObject.transform.GetChild(i);
            currentcube[i].x = child.transform.position.x;
            currentcube[i].y = child.transform.position.z;
            currentcube[i].z = child.transform.position.y-0.5f;
            previecube[i] = currentcube[i];
          //  Debug.Log("子物体名称:" + child.name);
        }
        while (FastpreviewTest())
        {
            for (int i = 0; i < 4; i++)
            {
                previecube[i].z = previecube[i].z - 1;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            previecube[i].z = previecube[i].z + 1;
        }
    }

   

    //纯debug用脚本
    void ColliderTest()
    {
       
        GameObject[] tmp;
        GameObject ColliderTry = Resources.Load("Prebs/ColliderTry") as GameObject;
        tmp = GameObject.FindGameObjectsWithTag("destory");
        for (int m = 0; m < tmp.Length; m++)
        {

            Destroy(tmp[m]); 
        }


        for (int i = 0; i < 4; i++)
        {
            
            Instantiate(ColliderTry, new Vector3(previecube[i].x, 0.5f + previecube[i].z, previecube[i].y), new Quaternion(90, 0, 0, 0));
        }


        for (int i = 0; i < Space_a; i++)
        {
            for (int j = 0; j < Space_a; j++)
            {
                for (int k = 0; k < Space_a; k++)
                {
                    if (Global.ColliderState[i, j, k] == true)
                    {
                        Instantiate(ColliderTry, new Vector3(i+10, 0.5f + k, j), new Quaternion(90, 0, 0, 0));
                    }
                }
            }
        }
    }

    //生成碰撞体
    void ColliderCreater()
    {   
        //长宽和高，便于修改
        GameObject ColliderPreb = Resources.Load("Prebs/ColliderPrefabs") as GameObject;
        GameObject ColliderInst;
        for (int i = 0; i < Space_a*Space_a*Space_a; i++)
        {   
            //这里vector3的参数分别是长高宽
            ColliderInst=Instantiate(ColliderPreb, new Vector3(i % Space_a, 0.5f + i / (Space_a * Space_a), (i / Space_a) % Space_a), new Quaternion(90, 0, 0, 0), ColliderFather.transform);
            ColliderInst.name = "" + i;
        }
    }
    
    //汪写的脚本
    IEnumerator fade_in(){
        float temp_alpha = black_screen.GetComponent<RawImage>().color.a;
        int times=2000;
        while(temp_alpha >= 0f && (times--) > 0){
            temp_alpha = black_screen.GetComponent<RawImage>().color.a;
            black_screen.GetComponent<RawImage>().color = new Color(0,0,0,temp_alpha - 0.006f);
            yield return 0;
        }
    }

    //随机选取方块函数
    GameObject ramdom_block(GameObject a)
    {
        int tmp = Random.Range(0, 5);
        if (tmp == 0 && a != L_block_perfab)
            return L_block_perfab;
        else if (tmp == 1 && a != T_block_perfab)
            return T_block_perfab;
        else if (tmp == 2 && a != I_block_perfab)
            return I_block_perfab;
        else if (tmp == 3 && a != Zeta_block_perfab)
            return Zeta_block_perfab;
        else
            return ramdom_block(a);
    }

    bool FastpreviewTest()
    {
        for (int ii = 0; ii < 4; ii++)
        {
            if (previecube[ii].z < -0.3f)
            {
                return false;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (previecube[i].z < Space_a)
            {
                if (Global.ColliderState[(int)previecube[i].x, (int)previecube[i].y, (int)previecube[i].z])
                {
                    return false;
                }
            }
        }
        return true;
    }

    //检测是否合法的语法，包括游玩空间和是否触碰
    bool isInPlayerSpace()
    {
        for (int ii = 0; ii < 4; ii++)
        {
            if (nextcube[ii].x >= Space_a || nextcube[ii].x < -0.3f)
            {
                return false;
            }
            if (nextcube[ii].y >= Space_a || nextcube[ii].y < -0.3f)
            {
                return false;
            }
            if (nextcube[ii].z < -0.3f)
            {
                return false;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (nextcube[i].z < Space_a)
            {
                if (Global.ColliderState[(int)nextcube[i].x, (int)nextcube[i].y, (int)nextcube[i].z])
                {
                    return false;
                }
            }
        }
        return true;
    }

    Vector3 V3RotateAround(Vector3 origin, Vector3 point, Vector3 axis)
    {
        Quaternion q = Quaternion.AngleAxis(90, axis);// 旋转系数
        Vector3 o = origin - point;// 旋转中心到源点的偏移向量
        o = q * o;// 旋转偏移向量，得到旋转中心到目标点的偏移向量
        return point + o;// 返回目标点
    }


    //移动旋转脚本
    //
    //
    void MoveForward()
    {
        for (int i = 0; i < 4; i++)
        {
            nextcube[i].x = currentcube[i].x;
            nextcube[i].y = currentcube[i].y - 1;
            nextcube[i].z = currentcube[i].z;
        }
        if (isInPlayerSpace())
        {
            ActiveObject.transform.position -= Vector3.forward;
        }
    }
    void MoveBack()
    {
        for (int i = 0; i < 4; i++)
        {
            nextcube[i].x = currentcube[i].x;
            nextcube[i].y = currentcube[i].y + 1;
            nextcube[i].z = currentcube[i].z;
        }
        if (isInPlayerSpace())
        {
                ActiveObject.transform.position -= Vector3.back;
        }
    }
    void MoveRight()
    {
        for (int i = 0; i < 4; i++)
        {
            nextcube[i].x = currentcube[i].x + 1;
            nextcube[i].y = currentcube[i].y;
            nextcube[i].z = currentcube[i].z;
        }
        if (isInPlayerSpace())
        {
            ActiveObject.transform.position += Vector3.right;
        }
    }

    void MoveLeft()
    {
        for (int i = 0; i < 4; i++)
        {
            nextcube[i].x = currentcube[i].x - 1;
            nextcube[i].y = currentcube[i].y;
            nextcube[i].z = currentcube[i].z;
        }
        if (isInPlayerSpace())
        {
            ActiveObject.transform.position += Vector3.left;
        }
    }

    void Drop()
    {
        for (int i = 0; i < 4; i++)
        {
            nextcube[i].x = currentcube[i].x;
            nextcube[i].y = currentcube[i].y;
            nextcube[i].z = currentcube[i].z - 1;
        }
        if (!isInPlayerSpace())
        {
            isTriggerTrue();
        }
        else
        {
            isTriggerFalse();
        }
    }

    void Confirm()
    {   
         for (int i = 0; i < ActiveObject.transform.childCount; i++)
        {
            var child = ActiveObject.transform.GetChild(i);
            child.transform.position= new Vector3(previecube[i].x, 0.5f + previecube[i].z, previecube[i].y);
            
          //  Debug.Log("子物体名称:" + child.name);
        }
       
    }

    void RotateRight()
    {
        for (int i = 0; i < 4; i++)
        {
            nextcube[i] = currentcube[i];
        }
        nextcube[0] = V3RotateAround(nextcube[0], nextcube[1], Vector3.back);
        nextcube[2] = V3RotateAround(nextcube[2], nextcube[1], Vector3.back);
        nextcube[3] = V3RotateAround(nextcube[3], nextcube[1], Vector3.back);
        if (isInPlayerSpace())
        {
            ActiveObject.transform.RotateAround(ActiveObject.transform.localPosition, Vector3.up, 90);
        }
    }

    void RotateUp()
    {
        for (int i = 0; i < 4; i++)
        {
            nextcube[i] = currentcube[i];
        }
        nextcube[0] = V3RotateAround(nextcube[0], nextcube[1], Vector3.left);
        nextcube[2] = V3RotateAround(nextcube[2], nextcube[1], Vector3.left);
        nextcube[3] = V3RotateAround(nextcube[3], nextcube[1], Vector3.left);
        if (isInPlayerSpace())
        {
            ActiveObject.transform.RotateAround(ActiveObject.transform.localPosition, Vector3.right, 90);
        }
    }
    //初始化脚本
    void Start()
    {
        StartCoroutine("fade_in");
        ColliderCreater();
        start_position = new Vector3(4f, 10.5f, 4f);
        start_rotation = new Quaternion(90, 0, 0, 0);
        L_block_perfab = Resources.Load("Prebs/L_Object") as GameObject;
        T_block_perfab = Resources.Load("Prebs/T_Object") as GameObject;
        I_block_perfab = Resources.Load("Prebs/I_Object") as GameObject;
        Zeta_block_perfab = Resources.Load("Prebs/Zeta_Object") as GameObject;
        PreviousObject = Resources.Load("Prebs/Plane") as GameObject;
        NextObject = ramdom_block(PreviousObject);
        ActiveObject = Instantiate(ramdom_block(PreviousObject), start_position, start_rotation);
        InvokeRepeating("Block_Drop", 2f, 1.5f);
    }

    
   
    //小键盘移动方块
    private void Update()
    {

        
        ColliderTest();
        //防止乱按跳帧bug
        GetCurrentVector();
        //movescript
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.K))
        {
            MoveForward();
        }
        if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.I))
        {
            MoveBack();
        }
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.J))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.L))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.U))
        {
            RotateRight();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.O))
        {
            RotateUp();
        }
        if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Space))
        {
            Confirm();//极速下落
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("opening");
        }
        //movescript
    }



    // Update is called once per frame
    void Block_Drop()
    {
        
        Debug.Log(isInPlayerSpace());
        Drop();
    }

    void isTriggerTrue()
    {
        Debug.Log("碰撞发生");
        NextObject = ramdom_block(NextObject);

        for (int i = 0; i < ActiveObject.transform.childCount; i++)
        {
            var child = ActiveObject.transform.GetChild(i);
            child.GetComponent<MeshCollider>().enabled = true;
        }
        PreviousObject = ActiveObject;
        ActiveObject = Instantiate(NextObject, start_position, start_rotation);
        isTriggerHappend = false;
    }
    void isTriggerFalse()
    {
        ActiveObject.transform.position -= Vector3.up;
    }
}
