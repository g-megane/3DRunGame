using UnityEngine;

/// <summary>
/// シーン遷移の管理クラス
/// </summary>
public class MySceneManager : MonoBehaviour
{
    /// <summary>
    /// シーン遷移処理
    /// </summary>
    /// <param name="sceneName">次のシーン名</param>
    public static void changeScene(string sceneName)
    {
        // フェード中か？
        if (FadeImage.IsDuringFade) { return; }
        FadeImage.fadeOut(sceneName);
    }
}
