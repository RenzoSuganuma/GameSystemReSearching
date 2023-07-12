using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    [SerializeField] InputField _nameInput = default;

   
    public void SaveDat()
    {
        string name = this._nameInput.text;

        SaveData svDat = new SaveData(name);

        //TO JSON
        string json = JsonUtility.ToJson(svDat);

        PlayerPrefs.SetString("SaveData", json);
    }
}
