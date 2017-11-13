using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 操作説明シーンのUI制御クラス
/// </summary>
public class OperatingSceneUI : MonoBehaviour
{
    /// <summary>
    /// 操作説明に使用するImage
    /// </summary>
    [SerializeField]
    Image operatingImage;

    /// <summary>
    /// 操作説明のページ毎の画像
    /// </summary>
    [SerializeField]
    List<Sprite> operatingImageList = new List<Sprite>();

    /// <summary>
    ///  現在のページ番号
    /// </summary>
    int imageIndex = 0;

    /// <summary>
    /// 次のページに進むボタンの動作
    /// </summary>
    public void onClickNext()
    {
        imageIndex = Mathf.Clamp(++imageIndex, 0, 3);
        changeImage();
    }

    /// <summary>
    /// 前のページに戻るボタンの動作
    /// </summary>
    public void onClickBack()
    {
        imageIndex = Mathf.Clamp(--imageIndex, 0, 3);
        changeImage();
    }

    /// <summary>
    /// 操作説明画像の差し替え
    /// </summary>
    void changeImage()
    {
        // 次の説明画像がない場合
        if (imageIndex == operatingImageList.Count) {
            MySceneManager.changeScene("Title");
            return;
        }
        // 画像の差し替え
        operatingImage.sprite = operatingImageList[imageIndex];
    }
}
