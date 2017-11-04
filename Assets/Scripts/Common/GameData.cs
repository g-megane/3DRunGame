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

    public int coinCount = 0;

    public float distanceToGoal = 0.0f;
}
