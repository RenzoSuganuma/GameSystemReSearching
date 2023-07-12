using UnityEngine;
using UnityEngine.UI;
public class Load : MonoBehaviour
{
    [SerializeField] Text _text = default;

    public void LoadData()
    {
        // PlayerPrefs から文字列を取り出す
        string json = PlayerPrefs.GetString("SaveData");
        // デシリアライズする
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        // 画面に表示する
        string status = "Name: " + saveData.Name;
        _text.text = status;
    }
}
