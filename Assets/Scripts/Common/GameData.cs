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
    public void addCoinCount()
    {
        ++coinCount;
    }

    public float distanceToGoal = 0.0f;

    void OnEnable()
    {
        coinCount = 0;
    }
}
