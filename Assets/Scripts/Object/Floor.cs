using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 床用のオブジェクトの制御
/// </summary>
public class Floor : MonoBehaviour
{
    /// <summary>
    /// 床が落下可能か？
    /// </summary>
    bool canFloorDown = false;

    void OnEnable()
    {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        // 床を落下させる事ができない
        if (!canFloorDown) { return; }
        transform.position += Vector3.down * 0.005f;

        // -5.0fより小さくなった場合
        if (transform.position.y < -5.0f) {
            canFloorDown = false;
            ObjectPool.Instance.releaseGameObject(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // プレイヤーと接触した
        if (other.tag == "Player") {
            canFloorDown = true;
        }
    }
}
