using UnityEngine;

/// <summary>
/// インプットのラッパークラス(シングルトン)
/// </summary>
public class InputManager : MonoBehaviour
{
    /// <summary>
    /// 唯一のインスタンス
    /// </summary>
    static InputManager instance;
    public static InputManager Instance {
        get { return instance; }
    }
    
    /// <summary>
    /// 前のフレームでパッドを倒していたか
    /// </summary>
    bool isLpadRightDownPrev = false;

    /// <summary>
    /// 左パッドを右に倒した瞬間を取得
    /// </summary>
    bool isLpadRight = false;
    public bool IsRightDown {
        get { return isLpadRight; }
    }

    void Awake()
    {
        // シングルトンの処理
        if (instance == null) {
            instance = this;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        checkLeftPadState();
    }

    void checkLeftPadState()
    {
        var isRight = getHorizontalAxis() > 0.0f ? true : false;
        isLpadRight = isRight && !isLpadRightDownPrev ? true : false;

        // 前のフレームの状態を保存
        isLpadRightDownPrev = isRight;
    }

    /// <summary>
    /// 水平方向の左パッドの入力値
    /// </summary>
    /// <returns>入力値（1.0～-1.0）</returns>
    public float getHorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }

    /// <summary>
    /// 垂直方向の左パッドの入力値
    /// </summary>
    /// <returns>入力値（1.0～-1.0）</returns>
    public float getVerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }
}
