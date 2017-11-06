using UnityEngine;

/// <summary>
/// プレイヤーの制御クラス
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// キャラクターコントローラーの参照
    /// </summary>
    CharacterController controller;

    /// <summary>
    /// アニメーターの参照
    /// </summary>
    Animator animator;

    /// <summary>
    /// 速度
    /// </summary>
    float speed = 1.0f;

    /// <summary>
    /// 水平方向の入力の値
    /// </summary>
    float inputValue = 0.0f;

    /// <summary>
    /// 1フレーム前の入力値
    /// </summary>
    float inputValuePrev = 0.0f;
    
    /// <summary>
    /// 2段目のジャンプが可能か？
    /// </summary>
    bool  canSecondJump = true; 

    /// <summary>
    /// 1フレームあたりの移動量ベクトル
    /// </summary>
    Vector3 direction;

    /// <summary>
    /// ジャンプ力
    /// </summary>
    const float  JUMP_POWER = 6.0f;

    /// <summary>
    /// 重力
    /// </summary>
    const float  GRAVITY = 12.0f;

    /// <summary>
    /// Runアニメーションへの遷移フラグ
    /// </summary>
    const string key_isRun = "IsRun"; 

    /// <summary>
    /// Jumpアニメーションへの遷移フラグ
    /// </summary>
    const string key_isJump = "IsJump";

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
        direction.x = speed * inputValue;

        // 地面にいる
        if (controller.isGrounded) {
            // 移動があるか？
            if (inputValue != 0.0f) {
                animator.SetBool(key_isRun, true);
                animator.speed = Mathf.Clamp(speed / 10.0f, 0.5f, 1.0f); // 入力量でアニメーションスピードを変更
                speed += 0.1f;
                speed = Mathf.Clamp(speed, 0.0f, 10.0f);
            }
            // 静止
            else {
                animator.SetBool(key_isRun, false);
                animator.speed = 1.0f;
                speed = 1.0f;
            }

            firstJump();
        }
        // 空中にいる
        else {
            airAction();
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
        inputValue = Input.GetAxis("Horizontal");
 
        // 右
        if (inputValue > 0.0f) {
            transform.rotation = Quaternion.AngleAxis(90, transform.up);
        }
        // 左
        else if (inputValue < 0.0f) {
            transform.rotation = Quaternion.AngleAxis(-90, transform.up);
        }
        
        // 前回の入力値よりも今回の入力値が小さい場合
        if (Mathf.Abs(inputValue) < Mathf.Abs(inputValuePrev)) {
            //speed *= Mathf.Clamp(Mathf.Abs(inputValue), 0.7f, 1.0f);
            speed -= 0.1f;
        }

        inputValuePrev = inputValue;
    }

    /// <summary>
    /// 空中行動
    /// </summary>
    void airAction()
    {
        // 重力
        direction.y -= GRAVITY * Time.deltaTime;
        // 2段ジャンプ
        secondJump();

        // 落下した
        if (transform.position.y < -5.0f) {
            resetPosition();
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
        // 2段ジャンプできない
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
            // 速度が遅い場合Jumpアニメーションを行わない
            //if (speed < 2.0f) { return; }
            //this.animator.SetBool(key_isJump, true);
        }
        else {
            //this.animator.SetBool(key_isJump, false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void resetPosition()
    {
        //TODO: 仮実装（スタート位置に戻す）
        //transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        speed = 1.0f;
        transform.position = new Vector3(gameObject.transform.position.x - 15.0f, 1.0f, 0.0f);
    }
}
