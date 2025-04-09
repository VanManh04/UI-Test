
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetailsShowAll : MonoBehaviour
{
    [SerializeField] Item item;
    public Item GetItem => item;
    [SerializeField] TextMeshProUGUI[] textMeshProDetails;

    public void setupData(Item _item)
    {
        textMeshProDetails[0].text = "ID: " + _item.id;
        textMeshProDetails[1].text = "Name: " + _item.name;
        textMeshProDetails[2].text = "ModelName: " + _item.modelName;
        textMeshProDetails[3].text = "Type: " + _item.type.ToString();
        textMeshProDetails[4].text = "Level: " + _item.level.ToString();
        textMeshProDetails[5].text = "Speed: " + _item.speed.ToString();
        textMeshProDetails[6].text = "Acceleration: " + _item.acceleration.ToString();
        textMeshProDetails[7].text = "Durable: " + _item.durable.ToString();
        textMeshProDetails[8].text = "Nitro: " + _item.nitro.ToString();
    }
}
