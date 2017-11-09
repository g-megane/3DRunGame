using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルトシーン用のUI制御クラス
/// </summary>
public class ResultSceneUI : MonoBehaviour
{
    [SerializeField]
    GameData gameData;

    /// <summary>
    /// 1位のスコア
    /// </summary>
    int one;

    /// <summary>
    /// 2位のスコア
    /// </summary>
    int two;

    /// <summary>
    /// 3位のスコア
    /// </summary>
    int three;

    void Start()
    {
        one = PlayerPrefs.GetInt("One", 0);
        two = PlayerPrefs.GetInt("Two", 0);
        three = PlayerPrefs.GetInt("Three", 0);

        int[] ranking = new int[]{ one, two, three, gameData.CoinCoint };

        Array.Sort<int>(ranking);

        one   = ranking[3];
        two   = ranking[2];
        three = ranking[1];
    }

    /// <summary>
    /// タイトルへ戻るボタンが押された時の処理
    /// </summary>
    public void onClickGoToTitle()
    {
        PlayerPrefs.SetInt("One", one);
        PlayerPrefs.SetInt("Two", two);
        PlayerPrefs.SetInt("Three", three);

        MySceneManager.changeScene("Title");
    }
}
