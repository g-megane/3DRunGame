using UnityEngine;

/// <summary>
/// コインの制御クラス
/// </summary>
public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーと接触した
        if (other.tag == "Player") {
            GameManager.Instance.getCoin();
            gameObject.SetActive(false);
        }
    }
}
