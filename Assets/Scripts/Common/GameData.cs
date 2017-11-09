using UnityEngine;
using System;
using System.Diagnostics;

/// <summary>
/// ゲームデータをまとめたScriptableObject
/// </summary>
[CreateAssetMenu]
public class GameData : ScriptableObject
{
    /// <summary>
    /// ゴールまでの距離
    /// </summary>
    [SerializeField, Header("ゴールまでの距離"), Range(0, 1000)]
    float goalDistance = 1000;
    public float GoalDistance {
        get { return goalDistance; }
    }

    /// <summary>
    /// 取得コイン数
    /// </summary>
    [SerializeField, Header("取得コイン数")]
    int coinCount = 0;
    public int CoinCoint {
        get { return coinCount; }
    }
    public void addCoinCount()
    {
        ++coinCount;
    }

    /// <summary>
    /// 経過時間を表す
    /// </summary>
    Stopwatch sw = new Stopwatch();
    public TimeSpan time {
        get { return sw.Elapsed; }
    }
    public void countStart()
    {
        sw.Reset();
        sw.Start();
    }
    public void countStop()
    {
        sw.Stop();
    }

    void OnEnable()
    {
        coinCount = 0;
    }
}
