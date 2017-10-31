using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームマネージャークラス(シングルトン)
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 唯一のインスタンス
    /// </summary>
    static GameManager instance;
    public static GameManager Instance {
        get { return instance; }
    }

    void Awake()
    {
        // シングルトンの処理
        if (Instance == null) {
            Instance = this;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// クリア時の処理
    /// </summary>
    public void gameCrear()
    {

    }

    /// <summary>
    /// ゲームオーバー時の処理
    /// </summary>
    public void gameOver()
    {

    }
}
