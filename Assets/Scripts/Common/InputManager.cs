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
    bool isLpadRightPrev = false;

    /// <summary>
    /// 左パッドを右に倒した瞬間を取得
    /// </summary>
    bool isLpadRight = false;
    public bool IsLpadRight {
        get { return isLpadRight; }
    }

    /// <summary>
    /// 前のフレームでパッドを倒していたか
    /// </summary>
    bool isLpadLeftPrev = false;

    /// <summary>
    /// 左パッドを右に倒した瞬間を取得
    /// </summary>
    bool isLpadLeft = false;
    public bool IsLpadLeft {
        get { return isLpadLeft; }
    }

    /// <summary>
    /// 前のフレームでパッドを倒していたか
    /// </summary>
    bool isLpadUpPrev = false;

    /// <summary>
    /// 左パッドを右に倒した瞬間を取得
    /// </summary>
    bool isLpadUp = false;
    public bool IsLpadUp {
        get { return isLpadUp; }
    }

    /// <summary>
    /// 前のフレームでパッドを倒していたか
    /// </summary>
    bool isLpadDownPrev = false;

    /// <summary>
    /// 左パッドを右に倒した瞬間を取得
    /// </summary>
    bool isLpadDown = false;
    public bool IsLpadDown {
        get { return isLpadDown; }
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

    /// <summary>
    /// 左パッドの状態をチェック
    /// </summary>
    void checkLeftPadState()
    {
        var isRight = getHorizontalAxis() > 0.5f  ? true : false;
        isLpadRight = isRight && !isLpadRightPrev ? true : false;

        var isLeft = getHorizontalAxis() < -0.5f ? true : false;
        isLpadLeft = isLeft && !isLpadLeftPrev   ? true : false;

        var isUp = getVerticalAxis() > 0.5f ? true : false;
        isLpadUp = isUp && !isLpadUpPrev    ? true : false;

        var isDown = getHorizontalAxis() < -0.5f ? true : false;
        isLpadDown = isDown && !isLpadDownPrev   ? true : false;

        // 前のフレームの状態を保存
        isLpadRightPrev = isRight;
        isLpadLeftPrev  = isLeft;
        isLpadUpPrev    = isUp;
        isLpadDownPrev  = isDown;
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
