using System.IO;
using UnityEngine;

public class DataLoader
{
    public int[] LoadExpCSV(string dataName){
        string[] textData = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, dataName)).Split("\n");

        int[] expData = new int[textData.Length - 1];
        
        for(int i = 1; i < textData.Length; i++)
        {
            string line = textData[i].Trim(); // ← \r 제거 + 공백 제거
            if (string.IsNullOrWhiteSpace(line)) continue; // 빈 줄 건너뛰기


            expData[i-1] = int.Parse(line.Split(",")[1]);
        }

        return expData;
    }
}
