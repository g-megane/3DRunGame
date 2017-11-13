using UnityEngine;

/// <summary>
/// タイトル用プレイヤーの制御クラス
/// </summary>
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
        // スタートが押されていない
        if (!isStart) { return; }
        transform.localPosition += (Vector3.back + Vector3.right) * 0.1f;
    }

    /// <summary>
    /// スタートボタンが押された時の動作
    /// </summary>
    public void onClickStart()
    {
        isStart = true;
        animator.SetBool("IsRun", true);
    }
}
