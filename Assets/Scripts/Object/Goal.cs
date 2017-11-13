using UnityEngine;

/// <summary>
/// ゴールオブジェクトの制御クラス
/// </summary>
public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーと接触した場合
        if (other.tag == "Player") {
            GameManager.Instance.gameCrear();
        }
    }
}
