using System;
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

    /// <summary>
    /// ゴール用のオブジェクト
    /// </summary>
    [SerializeField, Header("ゴールに使用するオブジェクト")]
    GameObject goalObject;

    [SerializeField, Header("コインオブジェクト")]
    GameObject coin;

    /// <summary>
    /// 生成位置
    /// </summary>
    Vector3 createPos;

    /// <summary>
    /// スペースを開けた数
    /// </summary>
    int continualSpaceCount = 0;

    void Awake()
    {
        // ステージの生成
        createStage();

        // ステージ生成ご自らを破棄
        Destroy(gameObject);
    }

    /// <summary>
    /// ステージの生成
    /// </summary>
    void createStage()
    {
        createFloor();

        createCoin();

        createGoal();
    }

    void createFloor()
    {
        float xMoveValue = stageBlock[0].GetComponent<Renderer>().bounds.size.x; // 生成位置の移動量X
        //float yMoveValue = stageBlock[0].GetComponent<Renderer>().bounds.size.y;  // 生成位置の移動量Y

        // 最初の10個の床はスペースを空けない
        for (int i = 0; i < 10; ++i) {
            var ranNum = UnityEngine.Random.Range(0, stageBlock.Count);
            var obj = Instantiate(stageBlock[ranNum]) as GameObject;
            obj.transform.position = createPos;
            createPos.Set(createPos.x + xMoveValue, 0.0f, 0.0f);
        }

        // 980mまではランダムに生成
        for (int i = 0; createPos.x < 980; ++i) {
            var ranNum = UnityEngine.Random.Range(0, stageBlock.Count + 1);

            // スペースを開ける
            if (ranNum == stageBlock.Count) {
                ++continualSpaceCount;
                if (continualSpaceCount >= 2) {
                    continualSpaceCount = 0;
                    --i;
                    continue;
                }
            }
            // オブジェクトの生成
            else {
                var obj = Instantiate(stageBlock[ranNum]) as GameObject;
                obj.transform.position = createPos;
            }
            createPos.Set(createPos.x + xMoveValue, 0.0f, 0.0f);
        }

        // 最後の数mは床を配置
        for (int i = 0; createPos.x < 1050; ++i) {
            var ranNum = UnityEngine.Random.Range(0, stageBlock.Count);
            var obj = Instantiate(stageBlock[ranNum]) as GameObject;
            obj.transform.position = createPos;
            createPos.Set(createPos.x + xMoveValue, 0.0f, 0.0f);
        }
    }

    void createCoin()
    {
        int[] usedCoinPosX = new int[100];

        // コインの配置
        for (int i = 0; i < 100; ++i) {
            var coinObj = Instantiate(coin) as GameObject;
            var x = UnityEngine.Random.Range(5, 990);
            var y = UnityEngine.Random.Range(2, 6);
            coin.transform.position = new Vector3(x, y, 0.0f);
        }
    }

    void createGoal()
    {
        // ゴールの生成
        var goal = Instantiate(goalObject) as GameObject;
        goal.transform.position = new Vector3(1000.0f, 2.0f, 0.0f);
    }
}
