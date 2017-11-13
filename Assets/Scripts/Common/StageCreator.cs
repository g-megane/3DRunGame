using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ生成クラス
/// </summary>
public class StageCreator : MonoBehaviour
{
    /// <summary>
    /// ステージを構成するオブジェクトのリスト
    /// </summary>
    [SerializeField, Header("ステージに使用するオブジェクト")]
    List<GameObject> stageBlock = new List<GameObject>();

    [SerializeField, Header("ステージの空白部分に使用するオブジェクト")]
    GameObject space;

    /// <summary>
    /// ゴール用のオブジェクト
    /// </summary>
    [SerializeField, Header("ゴールに使用するオブジェクト")]
    GameObject goalObject;

    /// <summary>
    /// ゲームデータを保有するScriptableObject
    /// </summary>
    [SerializeField, Header("GameDataのScriptableObject")]
    GameData gameData;

    /// <summary>
    /// Playerの参照
    /// </summary>
    GameObject player;

    /// <summary>
    /// 生成位置
    /// </summary>
    Vector3 createPos;

    /// <summary>
    /// 連続してスペースを開けた数
    /// </summary>
    int continualSpaceCount = 0;

    /// <summary>
    /// 床の配置データ
    /// </summary>
    List<int> floorData = new List<int>();

    /// <summary>
    /// データのインデックス
    /// </summary>
    int dataIndex = 0;

    /// <summary>
    /// 床生成の終了位置
    /// </summary>
    float floorCreateFinishPos = 200.0f;

    void Awake()
    {
        player = GameObject.Find("Player");

        // ステージの生成
        createStage();
    }

    void Update()
    {
        // 床の終わりの50m手前までプレイヤーが来た
        if (createPos.x - 50.0f < player.transform.position.x) {
            createFloor();
        }
    }

    /// <summary>
    /// ステージの生成
    /// </summary>
    void createStage()
    {
        createFloorData();

        createFloor();

        createGoal();
    }

    /// <summary>
    /// 床データの生成
    /// </summary>
    void createFloorData()
    {
        // 生成位置の移動量X
        float xMoveValue = stageBlock[0].GetComponent<Renderer>().bounds.size.x;

        // 最初の3個の床はスペースを空けない
        for (int i = 0; i < 3; ++i) {
            floorData.Add(0);
            createPos.Set(createPos.x + xMoveValue, 0.0f, 0.0f);
        }

        // 980mまではランダムに生成
        for (int i = 0; createPos.x < gameData.GoalDistance - 50; ++i) {
            var ranNum = UnityEngine.Random.Range(1, stageBlock.Count + 1);

            // スペースを開ける
            if (ranNum == stageBlock.Count) {
                // 2マス連続でスペースが空いた場合 
                if (continualSpaceCount >= 1) {
                    --i;
                    continue;
                }
                floorData.Add(ranNum);
                ++continualSpaceCount;
            }
            // オブジェクトの生成
            else {
                floorData.Add(ranNum);
                continualSpaceCount = 0;
            }

            createPos.Set(createPos.x + xMoveValue, 0.0f, 0.0f);
        }

        // 最後の数mは床を配置
        for (int i = 0; createPos.x < gameData.GoalDistance + 50; ++i) {
            floorData.Add(0);
            createPos.Set(createPos.x + xMoveValue, 0.0f, 0.0f);
        }

        createPos = Vector3.zero;
    }

    /// <summary>
    /// 床の生成
    /// </summary>
    void createFloor()
    {
        float xMoveValue = stageBlock[0].GetComponent<Renderer>().bounds.size.x; // 生成位置の移動量X

        // 床の生成
        for (; createPos.x < floorCreateFinishPos; ++dataIndex) {
            // 床データの範囲外アクセスの監視
            if (floorData.Count <= dataIndex) { return; }
            var i = floorData[dataIndex];
            GameObject obj;

            // スペースの場合
            if (i == stageBlock.Count) {
                obj = ObjectPool.Instance.getGameObject(space, createPos, Quaternion.identity);

            }
            // スペース以外の場合
            else {
                obj = ObjectPool.Instance.getGameObject(stageBlock[i], createPos, Quaternion.identity);
            }
            createPos.Set(createPos.x + xMoveValue, 0.0f, 0.0f);
        }

        floorCreateFinishPos += 100.0f;
    }

    /// <summary>
    /// ゴールの自動配置
    /// </summary>
    void createGoal()
    {
        // ゴールの生成
        var goal = Instantiate(goalObject) as GameObject;
        goal.transform.position = new Vector3(gameData.GoalDistance, 2.0f, 0.0f);
    }
}
