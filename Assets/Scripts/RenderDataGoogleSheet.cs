using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Progress;

public class RenderDataGoogleSheet : MonoBehaviour
{
    const string sheetUrl = "https://docs.google.com/spreadsheets/d/1FIT4lr_ZMuSNsoHzYff1YFb_5X7y8kH5GalTZHwR3rM/gviz/tq?tqx=out:csv";
    [SerializeField] List<Item> items = new List<Item>();
    [SerializeField] List<string> columnsClearNgoac = new List<string>();

    public void LoadData()
    {
        StartCoroutine(DownloadCSV());
    }

    IEnumerator DownloadCSV()
    {
        using (WWW www = new WWW(sheetUrl))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                string csvData = www.text;

                ParseCSV(csvData);
            }
            else
            {
                Debug.LogError("Error downloading CSV: " + www.error);
            }
        }
    }

    void ParseCSV(string csvData)
    {
        string[] rows = csvData.Split('\n');
        for (int i = 0; i < rows.Length; i++)
        {
            string trimmedRow = rows[i].Trim();
            columnsClearNgoac.Clear();

            if (!string.IsNullOrEmpty(trimmedRow))
            {
                string[] columns = trimmedRow.Split(',');
                foreach (string col in columns)
                {
                    string input = col;
                    string output = input.Replace("\"", "");
                    columnsClearNgoac.Add(output);
                }

                if (i > 0)
                {
                    Item item = new Item();
                    item.id = columnsClearNgoac[0];
                    item.name = columnsClearNgoac[1];
                    item.modelName = columnsClearNgoac[2];
                    item.type = int.Parse(columnsClearNgoac[3]);
                    item.level = int.Parse(columnsClearNgoac[4]);

                    item.speed = float.Parse(columnsClearNgoac[5]);
                    item.acceleration = float.Parse(columnsClearNgoac[6]);
                    item.durable = float.Parse(columnsClearNgoac[7]);
                    item.nitro = float.Parse(columnsClearNgoac[8]);
                    item.state = int.Parse(columnsClearNgoac[9]);
                    items.Add(item);
                }
                //foreach (string column in columns)
                //{
                //    Debug.Log("Column: " + column);
                //}
            }
        }
    }


    public List<Item> GetListItems => items;
}