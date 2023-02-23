using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SettingSaver : MonoBehaviour
{
    string savePath = "";
    string saveName = "Options.opt";

    [SerializeField] Apply apply;

    List<float> baseValues;

    public bool inGame = false;

    private void Start()
    {
        savePath = Application.persistentDataPath;

        string fullPath = Path.Combine(savePath, saveName);

        if (File.Exists(fullPath))
        {
            LoadOptionsFromFile();
        }
        else
        {
            baseValues = apply.GetMidRangeData();

            /*Base-Overides*/
            baseValues[0] = 0.1f;
            baseValues[1] = 0.1f;
            baseValues[2] = 0.05f;


            apply.SetSettings(baseValues);
        }

        if (inGame)
        {
            apply.ApplyOptionsToWorld();
        }
    }


    class DataC
    {
        public List<float> data;
    }
    public void SaveOptionsToFile(List<float> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            data[i] = (float)Math.Round((double)data[i], 2);
        }

        string fullPath = Path.Combine(savePath, saveName);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        DataC c = new DataC();
        c.data = data;
        string dataJ = JsonUtility.ToJson(c, true);

        Debug.LogWarning(dataJ);

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter write = new StreamWriter(stream))
            {
                write.Write(dataJ);
            }
        }

        if (data[3] == 0.1f
            && data[4] == 0.2f
            && data[5] == 0.3f
            && data[6] == 0.4f
            && data[7] == 0.5f
            )
        {
            Game.LoadScene("Test");
        }
    }

    public void LoadOptionsFromFile()
    {
        string fullPath = Path.Combine(savePath, saveName);

        DataC c = new DataC();

        string dataJ = "";
        if (File.Exists(fullPath))
        {
            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataJ = reader.ReadToEnd();
                }
            }
        }

        Debug.LogWarning("Load data: " + dataJ);
        c = JsonUtility.FromJson<DataC>(dataJ);

        List<float> data = c.data;


        apply.SetSettings(data);
    }
}
