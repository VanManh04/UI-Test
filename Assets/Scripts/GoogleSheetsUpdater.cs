using System.Collections;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;
using UnityEngine.Networking;
using static SRDebugger.UI.Controls.Data.NumberControl;

public class GoogleSheetsUpdater : MonoBehaviour
{
    static string[] Scopes = { SheetsService.Scope.Spreadsheets };
    static string ApplicationName = "Google Sheets API Unity";
    static string SpreadsheetId = "1FIT4lr_ZMuSNsoHzYff1YFb_5X7y8kH5GalTZHwR3rM";
    static string Range = "Sheet1!A2:J";
    private SheetsService service;

    void Start()
    {
        //string credentialsPath = Path.Combine(Application.dataPath, "Resources/testui_456202_1fd30ba954c3.json");
        //string credentialsPath = Path.Combine(Application.dataPath, "testui_456202_1fd30ba954c3.json");
        string credentialsPath = Path.Combine(Application.streamingAssetsPath, "testui_456202_1fd30ba954c3.json");
        if (!File.Exists(credentialsPath))
        {
            Debug.LogError($"null credentials tại {credentialsPath}");
            return;
        }

        GoogleCredential credential;

        using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(Scopes);
        }

        service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });
        Screen.SetResolution(720, 1280, false);
        //try
        //{
        //    StartCoroutine(LoadGoogleCredentials());
        //}
        //catch (System.Exception ex)
        //{
        //    Debug.LogError("Đã xảy ra lỗi: " + ex.Message);
        //    UI_Manager.instance.UpdateTMP_State(ex.Message);
        //}
    }

    //private IEnumerator LoadGoogleCredentials()
    //{
    //    string credentialsPath = Path.Combine(Application.streamingAssetsPath, "testui_456202_1fd30ba954c3.json");
    //        //credentialsPath = Path.Combine(Application.dataPath, "testui_456202_1fd30ba954c3.json");
    //    if (Application.platform == RuntimePlatform.Android)
    //    {
    //        UnityWebRequest www = UnityWebRequest.Get(credentialsPath);
    //        yield return www.SendWebRequest();

    //        if (www.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.LogError($"Lỗi tải tệp JSON: {www.error}");
    //            yield break;
    //        }

    //        string jsonContent = www.downloadHandler.text;
    //        Debug.Log("Tệp JSON đã tải thành công.");
    //        InitializeSheetsService(jsonContent);
    //    }
    //    else
    //    {
    //        if (File.Exists(credentialsPath))
    //        {
    //            string jsonContent = File.ReadAllText(credentialsPath);
    //            InitializeSheetsService(jsonContent);
    //        }
    //        else
    //        {
    //            Debug.LogError($"Tệp JSON không tồn tại tại {credentialsPath}");
    //        }
    //    }
    //}

    //private void InitializeSheetsService(string jsonContent)
    //{
    //    GoogleCredential credential = GoogleCredential.FromJson(jsonContent)
    //        .CreateScoped(Scopes);

    //    service = new SheetsService(new BaseClientService.Initializer()
    //    {
    //        HttpClientInitializer = credential,
    //        ApplicationName = ApplicationName,
    //    });

    //    Debug.Log("Google Sheets Service đã được khởi tạo thành công.");
    //}

    public void UpdateStatus(string targetId, int newStatus)
    {
        SpreadsheetsResource.ValuesResource.GetRequest getRequest = service.Spreadsheets.Values.Get(SpreadsheetId, Range);
        ValueRange response = getRequest.Execute();

        var values = response.Values;
        bool updated = false;
        string nameItem = "";
        if (values != null && values.Count > 0)
        {
            for (int i = 0; i < values.Count; i++)
            {
                var row = values[i];

                if (row.Count > 0 && row[0].ToString() == targetId)
                {
                    nameItem = row[1].ToString();
                    row[9] = newStatus;//.ToString()
                    ValueRange body = new ValueRange() { Values = new List<IList<object>> { row } };
                    SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = service.Spreadsheets.Values.Update(body, SpreadsheetId, $"Sheet1!A{i + 2}:J{i + 2}");
                    updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
                    updateRequest.Execute();

                    updated = true;
                    break;
                }
            }

            if (updated)
            {
                Debug.Log($"Status of ID {targetId} updated to {newStatus}");
                UI_Manager.instance.UpdateTMP_State($"Cập nhật {nameItem} State: {newStatus} thành công !");
            }
            else
            {
                Debug.LogWarning($"ID {name} not found.");
            }
        }
        else
        {
            Debug.LogWarning("No data found in the spreadsheet.");
        }
    }
}