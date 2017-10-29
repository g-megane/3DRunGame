using System.IO;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エクセルのインポート用クラス(シングルトン)
/// </summary>
public class ExcelImporter 
{
    private static ExcelImporter instance;
    
    public static ExcelImporter Instance {
        get {
            if (instance == null) {
                instance = new ExcelImporter();
            }

            return instance;
        }
    }

    /// <summary>
    /// csvファイルを読み込みデータを返す
    /// </summary>
    /// <param name="fileName">ファイル名</param>
    /// <returns>csvデータ</returns>
    public List<string[]> importCSV(string fileName)
    {
        TextAsset csvFile      = Resources.Load("MapData/" + fileName) as TextAsset;
        if (csvFile == null) {
            Debug.logger.Log(LogType.Error, "エラー", fileName + " is not found");
            return null;
        }

        StringReader reader    = new StringReader(csvFile.text);

        List<string[]> csvData = new List<string[]>();

        // １行づつcsvを読み込み「,」で分割
        while (reader.Peek() > -1) {
            string line = reader.ReadLine();
            csvData.Add(line.Split(','));
        }

        return csvData;
    }
}
