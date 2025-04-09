using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public class GoogleFormUploader : MonoBehaviour
{
    private const string googleFormUrl = "https://docs.google.com/forms/d/e/1FAIpQLScA8hqLmZO_E2WBAG0uxxKU9epplRfOu0P7GeSUGmR27HRRog/formResponse";
    [SerializeField] Item[] items;

    [Header("Name Entry")]
    string entry_id = "entry.752166960";
    string entry_name = "entry.34635146";
    string entry_modelName = "entry.551915683";
    string entry_type = "entry.655307224";
    string entry_level = "entry.833687470";
    string entry_speed = "entry.118791174";
    string entry_acceleration = "entry.279785031";
    string entry_durable = "entry.414656150";
    string entry_nitro = "entry.1554713041";
    string entry_state = "entry.1574322967";

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            foreach (var item in items)
            {
                StartCoroutine(sendToSite(item));
            }
        }
    }

    IEnumerator sendToSite(Item _item)
    {
        WWWForm form = new WWWForm();

        form.AddField(entry_id, _item.id);
        form.AddField(entry_name, _item.name);
        form.AddField(entry_modelName, _item.modelName);
        form.AddField(entry_type, _item.type);
        form.AddField(entry_level, _item.level);
        form.AddField(entry_speed, _item.speed.ToString());
        form.AddField(entry_acceleration, _item.acceleration.ToString());
        form.AddField(entry_durable, _item.durable.ToString());
        form.AddField(entry_nitro, _item.nitro.ToString());
        form.AddField(entry_state, _item.state.ToString());

        UnityWebRequest www = UnityWebRequest.Post(googleFormUrl, form);
        yield return www.SendWebRequest();
        Debug.Log("Done");
    }
}