using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountText : MonoBehaviour
{
    static Text coinCount;

    void Start()
    {
        coinCount = GetComponent<Text>();
    }

    public static void textUpate()
    {
        coinCount.text = string.Format("Coin : {0:000}", GameManager.Instance.CoinCount);
    }
}
