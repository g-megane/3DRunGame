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
    
    /// <summary>
    /// プレイヤーとのオフセット
    /// </summary>
    Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        offset.x = transform.position.x - player.transform.position.x;
    }

    void Update()
    {
        // X軸だけプレイヤーを追従
        Vector3 newPosition = transform.position;
        newPosition.x = player.transform.position.x + offset.x;

        transform.position = newPosition;
    }
}
