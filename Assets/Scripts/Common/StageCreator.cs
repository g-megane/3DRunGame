using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ生成クラス
/// </summary>
public class StageCreator : MonoBehaviour
{
    /// <summary>
    /// ステージの情報を記述したcsvのファイル名
    /// </summary>
    [SerializeField, Header("ステージ名(csvのファイル名)")]
    List<string> stageNameList = new List<string>();

    /// <summary>
    /// ステージを構成するオブジェクトのリスト
    /// </summary>
    [SerializeField, Header("ステージに使用するオブジェクト")]
    List<GameObject> stageBlock = new List<GameObject>();

    /// <summary>
    /// 生成位置
    /// </summary>
    Vector3 createPos;

    void Start()
    {
        // csvの読み込み
        var stageData = ExcelImporter.Instance.importCSV(stageNameList[GameManager.StageNum - 1]);
        // ステージの生成
        createStage(stageData);
    }

    /// <summary>
    /// ステージの生成
    /// </summary>
    /// <param name="stageData"></param>
    void createStage(List<string[]> stageData)
    {
        createPos = Vector3.zero;
        float xMoveValue = stageBlock[0].GetComponent<Renderer>().bounds.size.x;　// 生成位置の移動量X
        float yMoveValue = stageBlock[0].GetComponent<Renderer>().bounds.size.y;  // 生成位置の移動量Y

        for (int i = stageData.Count - 1; i >= 0; --i) {
            for (int j = 0; j < stageData[i].Length; ++j) {
                var index = int.Parse(stageData[i][j]);

                // スペースの場合
                if (index == stageBlock.Count) {
                    createPos.Set(createPos.x + xMoveValue, createPos.y, 0.0f);
                }
                // スペース以外の場合
                else {
                    var obj = Instantiate(stageBlock[index]);
                    obj.transform.position = createPos;
                    createPos.Set(createPos.x, createPos.y, 0.0f);
                    createPos.Set(createPos.x + xMoveValue, createPos.y, 0.0f);
                }
            }
            createPos.Set(0.0f, createPos.y + yMoveValue, 0.0f); 
        }
    }
}
