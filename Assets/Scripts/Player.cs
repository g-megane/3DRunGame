using UnityEngine;

/// <summary>
/// プレイヤーの制御クラス
/// </summary>
public class Player : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    Vector3 direction;

    float speed                = 0.0f; // 移動速度
    float inputHorizontalValue = 0.0f; // 水平方向の入力の値
    bool  canSecondJump        = true; // 2段ジャンプ可能か？

    /// <summary>
    /// 定数
    /// </summary>
    const float  JUMP_POWER =  7.0f;    // ジャンプ力
    const float  GRAVITY    = 10.0f;    // 重力
    const string key_isRun  = "IsRun";  // Runアニメーションへの遷移フラグ名
    const string key_isJump = "IsJump"; // Jumpアニメーションへの遷移フラグ名

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator   = GetComponent<Animator>();
        direction  = Vector3.zero;
    }

    void Update()
    {
        move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void move()
    {
        // 方向転換
        changeOfDirection();
        // 入力を移動量に変換
        direction.x = speed * inputHorizontalValue;

        // 地面にいる
        if (controller.isGrounded) {
            // 移動があるか？
            if (inputHorizontalValue != 0.0f) {
                animator.SetBool(key_isRun, true);
                animator.speed = Mathf.Clamp(Mathf.Abs(inputHorizontalValue), 0.5f, 1.0f); // 入力量でアニメーションスピードを変更
                speed += 0.1f;
                speed = Mathf.Clamp(speed, 0.0f, 6.0f);
            }
            // 静止
            else {
                animator.speed = 1.0f;
                animator.SetBool(key_isRun, false);
                speed = 0.0f;
            }

            firstJump();
        }
        // 空中にいる
        else {
            // 重力
            direction.y -= GRAVITY * Time.deltaTime;
            // 2段ジャンプ
            secondJump();
        }

        // 最終的な移動量を反映
        controller.Move(direction * Time.deltaTime);
    }

    /// <summary>
    /// 方向転換（空中でも可）
    /// </summary>
    void changeOfDirection()
    {
        // 今回のフレームの水平方向の入力量を保存
        inputHorizontalValue = Input.GetAxis("Horizontal");
 
        // 右
        if (inputHorizontalValue > 0.0f) {
            transform.rotation = Quaternion.AngleAxis(90, transform.up);
        }
        // 左
        else if (inputHorizontalValue < 0.0f) {
            transform.rotation = Quaternion.AngleAxis(-90, transform.up);
        }
    }

    /// <summary>
    /// 1段目のジャンプ
    /// </summary>
    void firstJump()
    {
        jumpAction(true);
    }

    /// <summary>
    /// 2段目のジャンプ
    /// </summary>
    void secondJump()
    {
        if (!canSecondJump) {
            this.animator.SetBool(key_isJump, false);
            return;
        }
        jumpAction(false);
    }

    /// <summary>
    /// ジャンプの動作
    /// </summary>
    /// <param name="_canSecondJump">2段目のジャンプ可能かどうかのフラグ</param>
    void jumpAction(bool _canSecondJump)
    {
        if (Input.GetButtonDown("Jump")) {
            canSecondJump = _canSecondJump;
            direction.y   = JUMP_POWER;
            // 移動していない場合はJumpアニメーションを行わない
            if (speed == 0.0f) { return; }
            this.animator.SetBool(key_isJump, true);
        }
        else {
            this.animator.SetBool(key_isJump, false);
        }
    }
}
