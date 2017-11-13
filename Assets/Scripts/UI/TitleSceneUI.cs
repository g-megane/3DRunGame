using UnityEngine;

/// <summary>
/// タイトルシーン用のUI制御クラス
/// </summary>
public class TitleSceneUI : MonoBehaviour
{
    /// <summary>
    /// 画面サイズの固定
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(1600, 900, false, 60);
    }


    void Update()
    {
        // Rキーが押された
        if (Input.GetKeyDown(KeyCode.R)) {
            PlayerPrefs.DeleteAll(); // ランキングのリセット
        }
    }

    /// <summary>
    /// スタートボタンが押された時の動作
    /// </summary>
    public void onClickStart()
    {
        MySceneManager.changeScene("Main");
    }

    /// <summary>
    /// 操作説明ボタンが押された時の動作
    /// </summary>
    public void onClickOperating()
    {
        MySceneManager.changeScene("Operating");
    }
}
