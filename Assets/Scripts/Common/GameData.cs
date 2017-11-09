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
    /// <summary>
    /// 取得コイン数を＋１
    /// </summary>
    public void addCoinCount()
    {
        ++coinCount;
    }
    /// <summary>
    /// 取得コイン数をリセット
    /// </summary>
    public void resetCoinCount()
    {
        coinCount = 0;
    }

    /// <summary>
    /// 経過時間を表す
    /// </summary>
    Stopwatch sw = new Stopwatch();
    public TimeSpan time {
        get { return sw.Elapsed; }
    }
    /// <summary>
    /// 経過時間の計測を開始
    /// </summary>
    public void countStart()
    {
        sw.Reset();
        sw.Start();
    }
    /// <summary>
    /// 経過時間の計測をストップ
    /// </summary>
    public void countStop()
    {
        sw.Stop();
    }
}
