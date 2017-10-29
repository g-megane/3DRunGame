using UnityEngine;

/// <summary>
/// プレイヤーの制御クラス
/// </summary>
public class Player : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    Vector3 direction;

    float speed = 1.0f;

    const float JUMP_POWER = 7.0f;
    const float GRAVITY    = 10.0f;
    const string key_isRun  = "IsRun";
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
        jump();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void move()
    {
        // ジャンプ後の移動を可能にするため地面にいなくても移動を許可
        direction.x =  speed * Input.GetAxis("Horizontal");
        
        // 地面にいる
        if (controller.isGrounded) {
            // 向きの反転
            // 右向き
            if (direction.x > 0.1f) {
                animator.SetBool(key_isRun, true);
                speed += 0.1f;
                transform.rotation = Quaternion.AngleAxis(90, transform.up);
            }
            // 左向き
            else if (direction.x < -0.1f) {
                animator.SetBool(key_isRun, true);
                speed += 0.1f;
                transform.rotation = Quaternion.AngleAxis(-90, transform.up);
            }
            else {
                animator.SetBool(key_isRun, false);
                speed = 1.0f;
            }

            jump();
        }
        // 空中にいる
        else {
            direction.y -= GRAVITY * Time.deltaTime;
        }
                
        controller.Move(direction * Time.deltaTime);
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            direction.y = JUMP_POWER;
            // 移動していない場合はジャンプアニメーションを行わない
            if (speed == 1.0f) { return; }
            this.animator.SetBool(key_isJump, true);
        }
        else {
            this.animator.SetBool(key_isJump, false);
        }
    }
}
