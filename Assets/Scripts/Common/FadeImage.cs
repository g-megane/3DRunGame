using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// フェードに使用するイメージ
/// </summary>
public class FadeImage : MonoBehaviour
{
    /// <summary>
    /// フェード中か？
    /// </summary>
    public static bool IsDuringFade {
        get;
        private set;
    }

    /// <summary>
    /// 次のシーンの名前
    /// </summary>
    static string nextScene = "";
    
    /// <summary>
    /// 現在のフェードの状態
    /// </summary>
    static FadeState state = FadeState.FadeIn;
    
    /// <summary>
    /// フェードの状態を示す列挙体
    /// </summary>
    enum FadeState
    {
        FadeIn,
        FadeOut,
        NotFade,
    }

    /// <summary>
    /// フェードの状態と処理を対応させたDictionary
    /// </summary>
    Dictionary<FadeState, Action> FadeActions = new Dictionary<FadeState, Action>();

    void Start ()
    {
        state        = FadeState.FadeIn;
        IsDuringFade = true;
        GetComponent<Image>().raycastTarget = true;
        GetComponent<Image>().color = Color.black;

        var color    = Color.black;
        GetComponent<Image>().color = color;

        FadeActions[FadeState.FadeIn] = () => {
            color.a -= Time.deltaTime;
            gameObject.GetComponent<Image>().color = color;

            if (color.a >= 0.0f) { return; }

            color.a      = 0.0f;
            state        = FadeState.NotFade;
            IsDuringFade = false;
            gameObject.GetComponent<Image>().raycastTarget = false;
        };

        FadeActions[FadeState.FadeOut] = () => {
            gameObject.GetComponent<Image>().raycastTarget = true;
            color.a += Time.deltaTime;
            gameObject.GetComponent<Image>().color = color;

            if (color.a <= 1.0f) { return; }

            color.a      = 1.0f;
            state        = FadeState.FadeIn;
            IsDuringFade = false;
            SceneManager.LoadScene(nextScene);
        };

        FadeActions[FadeState.NotFade] = () => {

        };
    }

    void Update ()
    {
        FadeActions[state]();
    }
    /// <summary>
    /// 外部からフェードアウト＋シーン遷移を行わせる
    /// </summary>
    /// <param name="sceneName">次のシーンの名前</param>
    public static void fadeOut(string sceneName)
    {
        IsDuringFade = true;
        state        = FadeState.FadeOut;
        nextScene    = sceneName;
    }
}
