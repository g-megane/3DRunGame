using UnityEngine;

/// <summary>
/// ゲームマネージャークラス(シングルトン)
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 唯一のインスタンス
    /// </summary>
    static GameManager instance;
    public static GameManager Instance {
        get { return instance; }
    }

    /// <summary>
    /// ゲームデータを保有するScriptableObject
    /// </summary>
    [SerializeField, Header("GameDataのScriptableObject")]
    GameData gameData;

    /// <summary>
    /// ゴールまでの距離
    /// </summary>
    float distanceToGoal = 0.0f;
    public float DistanceToGoal {
        get { return distanceToGoal; }
    }

    /// <summary>
    /// プレイヤーの参照
    /// </summary>
    GameObject player;

    /// <summary>
    /// ゴールオブジェクトの参照
    /// </summary>
    GameObject goal;

    void Awake()
    {
        // シングルトンの処理
        if (instance == null) {
            instance = this;
        }
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        goal   = GameObject.FindWithTag("Goal");
        gameData.countStart();
    }

    void Update()
    {
        checkDistanceToGoal();
    }

    /// <summary>
    /// ゴールまでの距離を計測
    /// </summary>
    void checkDistanceToGoal()
    {
        distanceToGoal = goal.transform.position.x - player.transform.position.x;
        distanceToGoal = Mathf.Clamp(distanceToGoal, 0.0f, 1000.0f);
    }

    public void getCoin()
    {
        gameData.addCoinCount();
    }
    
    /// <summary>
    /// クリア時の処理
    /// </summary>
    public void gameCrear()
    {
        gameData.countStop();
        MySceneManager.changeScene("Result");
    }

    /// <summary>
    /// ゲームオーバー時の処理
    /// </summary>
    public void gameOver()
    {

    }
}
