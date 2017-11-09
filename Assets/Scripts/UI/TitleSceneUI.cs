using UnityEngine;

/// <summary>
/// タイトルシーン用のUI制御クラス
/// </summary>
public class TitleSceneUI : MonoBehaviour
{
    public void onClickStart()
    {
        MySceneManager.changeScene("Main");
    }
}
