using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailsShowDefault : MonoBehaviour
{
    [SerializeField] Item item;
    public Item GetItem => item;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] Image image;

    public void setupShowDeffault(Item _item)
    {
        item = _item;
        textMeshProUGUI.text = _item.name;
        image = null;
    }
}
