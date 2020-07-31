using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_bool : MonoBehaviour
{
    int ColliderID;
    int Space_a = 9;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider collision)
    {
        ColliderID = (int)float.Parse(this.name);
        Global.ColliderState[ColliderID % Space_a, (ColliderID / Space_a) % Space_a, ColliderID / (Space_a * Space_a)] = true;
        //Debug.Log("[" + ColliderID % Space_a + "," + (ColliderID / Space_a) % Space_a + "," + ColliderID / (Space_a * Space_a) + "]" + Global.ColliderState[ColliderID % Space_a, (ColliderID / Space_a) % Space_a, ColliderID / (Space_a * Space_a)]);

    }
    void OnTriggerExit(Collider collision)
    {
        ColliderID = (int)float.Parse(this.name);
        Global.ColliderState[ColliderID % Space_a, (ColliderID / Space_a) % Space_a, ColliderID / (Space_a * Space_a)] = false;
        //Debug.Log("[" + ColliderID % Space_a + "," + (ColliderID / Space_a) % Space_a + "," + ColliderID / (Space_a * Space_a) + "]" + Global.ColliderState[ColliderID % Space_a, (ColliderID / Space_a) % Space_a, ColliderID / (Space_a * Space_a)]);
    }
}
