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
    }

    /// <summary>
    /// ステージの生成
    /// </summary>
    void createStage()
    {
        //TODO: 関数に分ける
        float xMoveValue = stageBlock[0].GetComponent<Renderer>().bounds.size.x; // 生成位置の移動量X
        //float yMoveValue = stageBlock[0].GetComponent<Renderer>().bounds.size.y;  // 生成位置の移動量Y

        // 最初の10個の床はスペースを空けない
        for (int i = 0; i < 10; ++i) {
            var ranNum = Random.Range(0, stageBlock.Count);
            var obj = Instantiate(stageBlock[ranNum]);
            obj.transform.position = createPos;
            createPos.Set(createPos.x + xMoveValue, 0.0f, 0.0f);
        }

        for (int i = 0; createPos.x < 1050; ++i) {
            var ranNum = Random.Range(0, stageBlock.Count + 1);

            // スペースを開ける
            if (ranNum == stageBlock.Count) {
                ++continualSpaceCount;
                if (continualSpaceCount >= 2) {
                    continualSpaceCount = 0;
                    --i;
                } 
            }
            // オブジェクトの生成
            else {
                var obj = Instantiate(stageBlock[ranNum]) as GameObject;
                obj.transform.position = createPos;
            }
            createPos.Set(createPos.x + xMoveValue, 0.0f, 0.0f);
        }

        // コインの配置
        for (int i = 0; i < 100; ++i) {
            var coinObj = Instantiate(coin) as GameObject;
            coin.transform.position = new Vector3(Random.Range(5, 990), Random.Range(2, 6), 0.0f);
        }

        var goal = Instantiate(goalObject) as GameObject;
        goal.transform.position = new Vector3(1000.0f, 2.0f, 0.0f);
    }
}
