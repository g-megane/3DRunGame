using UnityEngine;

/// <summary>
/// タイトルシーン用のUI制御クラス
/// </summary>
public class TitleSceneUI : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            PlayerPrefs.DeleteAll();    
        }
    }

    public void onClickStart()
    {
        MySceneManager.changeScene("Main");
    }
}
