using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトをプールするためのクラス
/// </summary>
public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// 唯一のインスタンス
    /// </summary>
    static ObjectPool instance;
    public static ObjectPool Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<ObjectPool>();
                if (instance == null) {
                    instance = new GameObject("ObjectPool").AddComponent<ObjectPool>();
                }
            }
            
            return instance;
        }
    }
    
    /// <summary>
    /// ゲームオブジェクトのDictionary
    /// </summary>
    Dictionary<int, List<GameObject>> pooledGameObjects = new Dictionary<int, List<GameObject>>();

    /// <summary>
    /// ゲームオブジェクトをpooledGameObjectsから取得する。(必要であれば新たに生成)
    /// </summary>
    public GameObject getGameObject(GameObject prefab, Vector2 position, Quaternion rotation)
    {
        // プレハブのインスタンスIDをkeyとする
        int key = prefab.GetInstanceID();

        // Dictionaryにkeyが存在しなければ作成
        if (pooledGameObjects.ContainsKey(key) == false) {
            pooledGameObjects.Add(key, new List<GameObject>());
        }

        List<GameObject> gameObjects = pooledGameObjects[key];

        GameObject go = null;

        for (int i = 0; i < gameObjects.Count; ++i) {
            go = gameObjects[i];
            
            // 非アクティブであれば
            if (go.activeInHierarchy == false) {
                go.transform.position = position; // 位置を設定
                go.transform.rotation = rotation; // 角度を設定
                go.SetActive(true);               // アクティブに

                return go;
            }
        }

        // 使用できるものがないので新たに生成
        go = Instantiate(prefab, position, rotation) as GameObject;
        // ObjectPoolゲームオブジェクトの子要素に
        go.transform.parent = transform;
        // リストに追加
        gameObjects.Add(go);

        return go;
    }

    /// <summary>
    /// ゲームオブジェクトを非アクティブにする。(再利用可能状態になる)
    /// </summary>
    /// <param name="go">破棄するオブジェクト</param>
    public void releaseGameObject(GameObject go)
    {
        // 非アクティブにする
        go.SetActive(false);
    }
}
