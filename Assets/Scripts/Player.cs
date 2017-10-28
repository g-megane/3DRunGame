using System;
using System.Collections;
using System.Collections.Generic;
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
    float jumpPower = 3.0f;
    float gravity = 10.0f;
    float moveX = 0.0f;
    float moveY = 0.0f;
    private const string key_isRun  = "IsRun";
    private const string key_isJump = "IsJump";
    

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
        // 地面にいる
        if (controller.isGrounded) {
            direction.x =  speed * Input.GetAxis("Horizontal");

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
            direction.y -= gravity * Time.deltaTime;
        }
                
        controller.Move(direction * Time.deltaTime);
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    void jump()
    {
        //TODO: 移動中とその場でのジャンプを変える
        if (Input.GetKeyDown(KeyCode.Space)) {
            direction.y = jumpPower;
            this.animator.SetBool(key_isJump, true);
        }
        else {
            this.animator.SetBool(key_isJump, false);
        }
    }
}
