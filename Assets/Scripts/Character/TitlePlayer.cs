using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePlayer : MonoBehaviour
{    
    /// <summary>
    /// Animatorの参照
    /// </summary>
    Animator animator;

    /// <summary>
    /// スタートが押されたか？
    /// </summary>
    bool isStart = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isStart) { return; }
        transform.localPosition += (Vector3.back + Vector3.right) * 0.1f;
    }

    public void onClickStart()
    {
        isStart = true;
        animator.SetBool("IsRun", true);
    }
}
