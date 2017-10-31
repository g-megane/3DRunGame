using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コインの制御クラス
/// </summary>
public class Coin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            gameObject.SetActive(false);
        }
    }
}
