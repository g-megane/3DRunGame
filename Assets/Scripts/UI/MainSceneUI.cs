using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// メインシーンのUIを制御するクラス
/// </summary>
public class MainSceneUI : MonoBehaviour
{
    /// <summary>
    /// ゲームデータを保有するScriptableObject
    /// </summary>
    [SerializeField, Header("GameDataのScriptableObject")]
    GameData gameData;

    /// <summary>
    /// コインの取得数を表示するText
    /// </summary>
    [SerializeField, Header("コインの取得数を表示するテキスト")]
    Text coinCount;

    /// <summary>
    /// ゴールまでの距離を表示するText
    /// </summary>
    [SerializeField, Header("ゴールまでの距離を表示するテキスト")]
    Text distanceToGoal;

    /// <summary>
    /// 経過時間を表示するText
    /// </summary>
    [SerializeField, Header("経過時間を表示するテキスト")]
    Text time;

    void Update()
    {
        updateDistanceToGoal();
        updateCoinCount();
        updateTime();
    }

    /// <summary>
    /// コイン取得数のテキストの更新
    /// </summary>
    void updateCoinCount()
    {
        coinCount.text = string.Format("Coin : {0:000}", gameData.CoinCoint);
    }

    /// <summary>
    /// 残り距離のテキストを更新
    /// </summary>
    void updateDistanceToGoal()
    {
        distanceToGoal.text = string.Format("残り{0:0000}m", GameManager.Instance.DistanceToGoal);
    }

    void updateTime()
    {
        time.text = string.Format("{0:00}:{1:00}", gameData.time.Minutes, gameData.time.Seconds);
    }
}
