using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    [SerializeField] RenderDataGoogleSheet renderDataGoogleSheet;
    [SerializeField] List<Item> fullItems = new List<Item>();
    [SerializeField] TextMeshProUGUI textMeshProUGUI_State;

    public void UpdateTMP_State(string _str) => textMeshProUGUI_State.text = _str;

    [Header("Trang bi")]
    [SerializeField] Button_trangBi[] trangbis;
    //[SerializeField] Button_trangBi trangbi_1;
    //[SerializeField] Button_trangBi trangbi_2;
    //[SerializeField] Button_trangBi trangbi_3;
    //[SerializeField] Button_trangBi trangbi_4;

    [Header("Tui do")]
    [SerializeField] List<Button_trangBi> trangBiTuiDos = new List<Button_trangBi>();
    [SerializeField] Button_trangBi button_TrangBi;
    [SerializeField] Transform parentButton_tuido;

    [Header("Canvas - DetailsItem")]
    [SerializeField] DetailsShowDefault showDefault;
    [SerializeField] DetailsShowAll showAll;

    [Header("Canvas - DetailDefault")]
    [SerializeField] GameObject btn_trangbi;
    [SerializeField] GameObject btn_thao;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        renderDataGoogleSheet.LoadData();
    }

    void Start()
    {
        fullItems = renderDataGoogleSheet.GetListItems;

        Invoke(nameof(Load_Button_Tuido), .2f);
    }

    private void Load_Button_Tuido()
    {
        if (fullItems.Count < 0)
        {
            Load_Button_Tuido();
            return;
        }
        foreach (Item item in fullItems)
        {
            if (item.state == 0)
            {
                Button_trangBi newButton_tuido = Instantiate(button_TrangBi, parentButton_tuido);
                newButton_tuido.SetItem(item);
                newButton_tuido.gameObject.SetActive(true);

                trangBiTuiDos.Add(newButton_tuido);
            }
            else if (item.state == 1)
            {
                if (item.type == 1)
                    trangbis[0].SetItem(item);
                else if (item.type == 2)
                    trangbis[1].SetItem(item);
                else if (item.type == 3)
                    trangbis[2].SetItem(item);
                else if (item.type == 4)
                    trangbis[3].SetItem(item);
                //if (item.type == 1)
                //    trangbi_1.SetItem(item);
                //else if (item.type == 2)
                //    trangbi_2.SetItem(item);
                //else if (item.type == 3)
                //    trangbi_3.SetItem(item);
                //else if (item.type == 4)
                //    trangbi_4.SetItem(item);
            }
            else
            {
                print(item.name + "  " + item.state);
                Debug.LogError("Item Error State");
            }
        }

        //trangbi_1.setupBG();
        //trangbi_2.setupBG();
        //trangbi_3.setupBG();
        //trangbi_4.setupBG();

        foreach (Button_trangBi trangbi in trangbis)
        {
            trangbi.setupBG();
        }
    }

    public void GetTrangBiLoai(int _loai)
    {
        foreach (Button_trangBi trangbi in trangBiTuiDos)
        {
            if (trangbi.GetItem.type == _loai)
                trangbi.gameObject.SetActive(true);
            else
                trangbi.gameObject.SetActive(false);
        }
    }

    #region Show All Details
    public void OpenOrClose_DetailsShowAll(bool _bool)
    {
        showAll.gameObject.SetActive(_bool);
    }

    #endregion

    #region Show Default Details
    public void OpenOrClose_DetailsShowDefault(bool _bool)
    {
        if (showDefault.GetItem.state == 0)
        {
            btn_thao.SetActive(false);
            btn_trangbi.SetActive(true);
        }
        else
        {
            btn_thao.SetActive(true);
            btn_trangbi.SetActive(false);
        }

        showDefault.gameObject.SetActive(_bool);
    }

    public void Button_ChiTiet()
    {
        //print("chi tiet " + showDefault.GetItem.name);
        showAll.setupData(showDefault.GetItem);
        OpenOrClose_DetailsShowAll(true);
    }

    public void Button_TrangBi()
    {
        //print("equip "+showDefault.GetItem.name);
        Item itemNew = showDefault.GetItem; //lay tui do trang bi vao
        int type = itemNew.type;


        Item itemOld = trangbis[type - 1].GetItem;//lay trang bi bo vao tui do

        if (itemOld != null)
        {
            Button_trangBi newButton_tuido = Instantiate(button_TrangBi, parentButton_tuido);
            newButton_tuido.SetItem(itemOld);
            newButton_tuido.gameObject.SetActive(true);

            trangBiTuiDos.Add(newButton_tuido);
        }
        foreach (Button_trangBi buttonTuiDo in trangBiTuiDos)
        {
            if (buttonTuiDo.gameObject.activeSelf)
            {
                print(buttonTuiDo.GetItem.name);
                if (buttonTuiDo.GetItem.id == itemNew.id)
                {
                    Destroy(buttonTuiDo.gameObject);
                    trangBiTuiDos.Remove(buttonTuiDo);
                    break;
                }
            }
        }

        trangbis[type - 1].SetItem(itemNew);
        trangbis[type - 1].setupBG();

        OpenOrClose_DetailsShowDefault(false);
        if (itemNew != null)
        {
            StartCoroutine(Countine(itemNew, 1));
            itemNew.state = 1;
        }
        if (itemOld != null)
        {

            StartCoroutine(Countine(itemOld, 0));
            itemOld.state = 0;
        }
    }

    public void Button_thao()
    {
        //print("thap " + showDefault.GetItem.name);
        Item itemThao = showDefault.GetItem;
        if (itemThao == null)
            return;

        Button_trangBi newButton_tuido = Instantiate(button_TrangBi, parentButton_tuido);
        newButton_tuido.SetItem(itemThao);
        newButton_tuido.gameObject.SetActive(true);

        trangBiTuiDos.Add(newButton_tuido);

        trangbis[itemThao.type - 1].SetItem(null);
        trangbis[itemThao.type - 1].setupBG();

        OpenOrClose_DetailsShowDefault(false);
        itemThao.state = 0;
        StartCoroutine(Countine(itemThao, 0));
    }

    private System.Collections.IEnumerator Countine(Item itemThao, int _state)
    {

        yield return new WaitForSeconds(.5f);
        GoogleSheetManager.Instance.GoogleSheetsUpdater.UpdateStatus(itemThao.id, _state);
    }

    #endregion

    public void SetDataItem_ShowDefault(Button_trangBi _buttonTrangBi)
    {
        showDefault.setupShowDeffault(_buttonTrangBi.GetItem);
    }

}
