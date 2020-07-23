using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider collision)
    {
        Global.isTriggerHappend = true;
    }
    void OnTriggerExit(Collider collision)
    {
        Global.isTriggerHappend = false;
    }

}
