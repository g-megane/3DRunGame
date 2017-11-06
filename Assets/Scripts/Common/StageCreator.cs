﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    /// コイン用のオブジェクト
    /// </summary>
    [SerializeField, Header("コインオブジェクト")]
    GameObject coin;

    /// <summary>
    /// ゲームデータを保有するScriptableObject
    /// </summary>
    [SerializeField, Header("GameDataのScriptableObject")]
    GameData gameData;

    /// <summary>
    /// 生成位置
    /// </summary>
    Vector3 createPos;
    
    /// <summary>
    /// 連続してスペースを開けた数
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

        //TODO: コインの配置考え直す
        //createCoin();

        createGoal();
    }

    /// <summary>
    /// 床の自動生成
    /// </summary>
    void createFloor()
    {
        float xMoveValue = stageBlock[0].GetComponent<Renderer>().bounds.size.x; // 生成位置の移動量X

        // 最初の3個の床はスペースを空けない
        for (int i = 0; i < 3; ++i) {
            var ranNum = UnityEngine.Random.Range(0, stageBlock.Count);
            var obj = Instantiate(stageBlock[0]) as GameObject;
            obj.transform.position = createPos;
            createPos.Set(createPos.x + xMoveValue, 0.0f, 0.0f);
        }

        // 980mまではランダムに生成
        for (int i = 0; createPos.x < gameData.GoalDistance - 50; ++i) {
            var ranNum = UnityEngine.Random.Range(1, stageBlock.Count + 1);
            GameObject obj;

            // スペースを開ける
            if (ranNum == stageBlock.Count) {
                // 2マス連続でスペースが空いた場合 
                if (continualSpaceCount >= 1) {
                    --i;
                    continue;
                }
                obj = Instantiate(space) as GameObject;
                ++continualSpaceCount;
            }
            // オブジェクトの生成
            else {
                continualSpaceCount = 0;
                obj = Instantiate(stageBlock[ranNum]) as GameObject;
            }

            obj.transform.position = createPos;
            createPos.Set(createPos.x + xMoveValue, 0.0f, 0.0f);
        }

        // 最後の数mは床を配置
        for (int i = 0; createPos.x < gameData.GoalDistance + 50; ++i) {
            var ranNum = UnityEngine.Random.Range(0, stageBlock.Count);
            var obj = Instantiate(stageBlock[0]) as GameObject;
            obj.transform.position = createPos;
            createPos.Set(createPos.x + xMoveValue, 0.0f, 0.0f);
        }
    }

    /// <summary>
    /// コインの自動配置
    /// </summary>
    void createCoin()
    {
        int[] coinPosX = createRandomUniqueNumbers(5, 990, 100);

        // コインの配置
        for (int i = 0; i < 100; ++i) {
            var coinObj = Instantiate(coin) as GameObject;
            var x = coinPosX[i];
            var y = UnityEngine.Random.Range(2, 6);
            coin.transform.position = new Vector3(x, y, 0.0f);
        }
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

    /// <summary>
    /// 指定範囲の重複しない乱数を配列で返す
    /// </summary>
    /// <param name="min">最低値</param>
    /// <param name="max">最大値</param>
    /// <param name="requiredNumber">必要な数量</param>
    /// <returns>重複のない乱数配列</returns>
    int[] createRandomUniqueNumbers(int min, int max, int requiredNumber)
    {
        return Enumerable.Range(min, max).OrderBy(n => Guid.NewGuid()).Take(requiredNumber).ToArray();
    }
}
