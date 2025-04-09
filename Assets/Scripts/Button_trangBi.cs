using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_trangBi : MonoBehaviour
{
    Item item;
    [SerializeField] Item itemCanSee;
    public Item GetItem => item;
    [SerializeField] TextMeshProUGUI textMeshPro_name;
    [Header("Setup BackGround")]
    [SerializeField] Color32 colorNull;
    [SerializeField] Color32 colorDefault;
    [SerializeField] Image background;


    public void SetItem(Item _item)
    {
        item = _item;
        if (item != null)
            textMeshPro_name.text = item.name.ToString();
        else
            textMeshPro_name.text = "Null";
        if (itemCanSee != null)
            itemCanSee = item;
    }

    internal void setupBG()
    {
        if (item == null)
            background.color = colorNull;
        else
            background.color = colorDefault;
    }
}
