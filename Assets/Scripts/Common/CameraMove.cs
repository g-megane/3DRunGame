using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの制御クラス
/// </summary>
public class CameraMove : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの参照
    /// </summary>
    GameObject player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // X軸だけプレイヤーを追従
        Vector3 newPosition = transform.position;
        newPosition.x = player.transform.position.x;

        transform.position = newPosition;
    }
}
