using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 床用のオブジェクトの制御
/// </summary>
public class Floor : MonoBehaviour
{
    bool canFloorDown = false;
    
    void Start()
    {

    }

    void Update()
    {
        if (!canFloorDown) { return; }
        transform.position += Vector3.down * 0.005f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            canFloorDown = true;
        }
    }
}
