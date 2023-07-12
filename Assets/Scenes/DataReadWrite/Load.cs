using UnityEngine;
using UnityEngine.UI;
public class Load : MonoBehaviour
{
    [SerializeField] Text _text = default;

    public void LoadData()
    {
        // PlayerPrefs ���當��������o��
        string json = PlayerPrefs.GetString("SaveData");
        // �f�V���A���C�Y����
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        // ��ʂɕ\������
        string status = "Name: " + saveData.Name;
        _text.text = status;
    }
}
