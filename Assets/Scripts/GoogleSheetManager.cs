using UnityEngine;

public class GoogleSheetManager : MonoBehaviour
{
    public static GoogleSheetManager Instance;
    [SerializeField] GoogleFormUploader googleFormUploader;
    public GoogleFormUploader GoogleFormUploader => googleFormUploader;
    [SerializeField] GoogleSheetsUpdater googleSheetsUpdater;
    public GoogleSheetsUpdater GoogleSheetsUpdater => googleSheetsUpdater;
    [SerializeField] RenderDataGoogleSheet renderDataGoogleSheet;
    public RenderDataGoogleSheet RenderDataGoogleSheet => renderDataGoogleSheet;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
