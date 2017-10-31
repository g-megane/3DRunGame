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
            //TODO: ゴール時の処理
            Debug.Log("Goal");
        }
    }
}
