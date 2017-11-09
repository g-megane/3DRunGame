using System;
using System.Collections;
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
    /// 2段目のジャンプのエフェクト
    /// </summary>
    [SerializeField]
    GameObject jumpEffect;

    /// <summary>
    /// インプットマネージャークラスの参照
    /// </summary>
    InputManager input;

    /// <summary>
    /// 速度
    /// </summary>
    float speed = 1.0f;

    /// <summary>
    /// 水平方向の入力の値
    /// </summary>
    float inputValue = 0.0f;
    
    /// <summary>
    /// 2段目のジャンプが可能か？
    /// </summary>
    bool  canSecondJump = false; 

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
        input      = InputManager.Instance;
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

        // 地面にいる
        if (controller.isGrounded) {
            // 移動があるか？
            if (input.getHorizontalAxis() != 0.0f) {
                animator.SetBool(key_isRun, true);
                animator.speed = Mathf.Clamp(speed / 10.0f, 0.5f, 1.0f); // 移動速度でアニメーションスピードを変更
                speed += 0.05f;
                speed = Mathf.Clamp(speed, 0.0f, 10.0f);
            }
            // 静止
            else {
                if (speed == 0.0f) {
                    animator.SetBool(key_isRun, false);
                }
                animator.speed = 1.0f;
                speed = Mathf.Clamp(speed -= 1.0f, 0.0f, 10.0f);
            }

            firstJump();
        }
        // 空中にいる
        else {
            airAction();
        }

        // 入力を移動量に変換
        direction.x = speed * inputValue;
        // 最終的な移動量を反映
        controller.Move(direction * Time.deltaTime);
    }

    /// <summary>
    /// 方向転換（空中でも可）
    /// </summary>
    void changeOfDirection()
    {
        // 今回のフレームの水平方向の入力量を保存
        //inputValue = Input.GetAxis("Horizontal");
 
        // 右にパッドが倒されている
        if (input.getHorizontalAxis() > 0.0f) {
            inputValue = Input.GetAxis("Horizontal");            
            transform.rotation = Quaternion.AngleAxis(90, transform.up);
        }
        // 左にパッドが倒されている
        else if (input.getHorizontalAxis() < 0.0f) {
            inputValue = Input.GetAxis("Horizontal");
            transform.rotation = Quaternion.AngleAxis(-90, transform.up);
        }
        
        // 向きが反転した
        if (input.IsLpadRight || input.IsLpadLeft) {
            speed = 2.0f;
        }
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

        if (Input.GetAxis("Horizontal") == 0.0f) {
            speed = Mathf.Clamp(speed -= 0.1f, 0.0f, 10.0f);
        }

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
            direction.y   = JUMP_POWER;
            canSecondJump = _canSecondJump;
            if (!canSecondJump) {
                var obj = ObjectPool.Instance.getGameObject(jumpEffect, transform.position, Quaternion.identity);
                StartCoroutine(delayReleaseMethod(obj.GetComponent<ParticleSystem>().main.duration, obj,
                    (x) => {
                        ObjectPool.Instance.releaseGameObject(x);
                    }));
            }
        }
    }

    IEnumerator delayReleaseMethod(float waitTime, GameObject obj, Action<GameObject> action)
    {
        yield return new WaitForSeconds(waitTime);

        action(obj);
    }

    /// <summary>
    /// 落下した際に位置を14m前に戻す
    /// </summary>
    void resetPosition()
    {
        speed = 0.0f;
        transform.position = new Vector3(gameObject.transform.position.x - 14.0f, 1.0f, 0.0f);
    }
}
