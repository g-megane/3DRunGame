using System.Collections;
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
    [SerializeField, Header("ステージに使用するオブジェクト（）")]
    List<GameObject> stageBlock = new List<GameObject>();

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
        float xMoveValue = stageBlock[0].GetComponent<Renderer>().bounds.size.x; // 生成位置の移動量X
        //float yMoveValue = stageBlock[0].GetComponent<Renderer>().bounds.size.y;  // 生成位置の移動量Y

        for (int i = 0; i < 100; ++i) {
            var ranNum = Random.Range(0, stageBlock.Count + 1);

            // スペースを開ける
            if (ranNum == stageBlock.Count) {
                ++continualSpaceCount;
                if (continualSpaceCount >= 2) {
                    continualSpaceCount = 0;
                    --i;
                } 
                xMoveValue = 10;
            }
            else {
                var obj = Instantiate(stageBlock[ranNum]);
                obj.transform.position = createPos;
            }
            createPos.Set(createPos.x + xMoveValue, 0.0f, 0.0f);
        }

        //createPos = Vector3.zero;
        //float xMoveValue = stageBlock[0].GetComponent<Renderer>().bounds.size.x;　// 生成位置の移動量X
        //float yMoveValue = stageBlock[0].GetComponent<Renderer>().bounds.size.y;  // 生成位置の移動量Y

        //for (int i = stageData.Count - 1; i >= 0; --i) {
        //    for (int j = 0; j < stageData[i].Length; ++j) {
        //        var index = int.Parse(stageData[i][j]);

        //        // スペースの場合
        //        if (index == 0) {
        //            createPos.Set(createPos.x + xMoveValue, createPos.y, 0.0f);
        //        }
        //        // スペース以外の場合
        //        else {
        //            var obj = Instantiate(stageBlock[index]);
        //            obj.transform.position = createPos;
        //            createPos.Set(createPos.x, createPos.y, 0.0f);
        //            createPos.Set(createPos.x + xMoveValue, createPos.y, 0.0f);
        //        }
        //    }
        //    createPos.Set(0.0f, createPos.y + yMoveValue, 0.0f); 
        //}
    }
}
