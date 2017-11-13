using UnityEngine;

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
    /// クリアしたか？
    /// </summary>
    bool isClear = false;
    public bool IsClear {
        get { return isClear; }
    }

    public void gameClear()
    {
        isClear = true;
    }

    public void resetIsClear()
    {
        isClear = false;
    }
}
