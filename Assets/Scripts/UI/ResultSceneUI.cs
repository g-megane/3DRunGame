using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リザルトシーン用のUI制御クラス
/// </summary>
public class ResultSceneUI : MonoBehaviour
{
    /// <summary>
    /// ゲームデータを保有するScriptableObject
    /// </summary>
    [SerializeField]
    GameData gameData;

    /// <summary>
    /// 結果表示のテキスト
    /// </summary>
    [SerializeField]
    Text result;

    /// <summary>
    /// 今回のスコア表示テキスト
    /// </summary>
    [SerializeField]
    Text score;

    /// <summary>
    /// 1位のスコア表示テキスト
    /// </summary>
    [SerializeField]
    Text first;

    /// <summary>
    /// 2位のスコア表示テキスト
    /// </summary>
    [SerializeField]
    Text second;

    /// <summary>
    /// 3位のスコア表示テキスト
    /// </summary>
    [SerializeField]
    Text third;

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
        // ゲームをクリアしていない
        if (!gameData.IsClear) {
            result.text = "GameOver";
            gameData.resetCoinCount();
        }

        // ランキングデータのロード
        one   = PlayerPrefs.GetInt("One"  , 0);
        two   = PlayerPrefs.GetInt("Two"  , 0);
        three = PlayerPrefs.GetInt("Three", 0);

        // ランキングデータのソート
        int[] ranking = new int[]{ one, two, three, gameData.CoinCoint };
        Array.Sort<int>(ranking);
        // ソート後のデータの代入
        one   = ranking[3];
        two   = ranking[2];
        three = ranking[1];
        // ランキングデータの表示
        score.text  = string.Format("今回 {0:000}枚", gameData.CoinCoint);
        first.text  = string.Format("1位 {0:000}枚", one);
        second.text = string.Format("2位 {0:000}枚", two);
        third.text  = string.Format("3位 {0:000}枚", three);

        // 今回のスコアがランクインしていれば赤でハイライト
        if (gameData.CoinCoint == one) {
            first.color = Color.red;
        }
        else if (gameData.CoinCoint == two) {
            second.color = Color.red;
        }
        else if (gameData.CoinCoint == three) {
            third.color = Color.red;
        }
    }
    
    /// <summary>
    /// タイトルへ戻るボタンが押された時の処理
    /// </summary>
    public void onClickGoToTitle()
    {
        // 順位を保存
        PlayerPrefs.SetInt("One"  , one  );
        PlayerPrefs.SetInt("Two"  , two  );
        PlayerPrefs.SetInt("Three", three);
        PlayerPrefs.Save();
        // 取得コイン数をリセット
        gameData.resetCoinCount();
        // クリアフラグをリセット
        gameData.resetIsClear();

        MySceneManager.changeScene("Title");
    }
}
