using UnityEngine;
[CreateAssetMenu(menuName = "WeaponDataContainer/CreateTable", fileName = "WeaponDataTable")]
public class WeaponStatusDataContainer : ScriptableObject
{
    public int _magazineAmounts;//�}�K�W���� �� ���e�� �� �}�K�W���� �~ �}�K�W���T�C�Y
    public int _magazineSize;//�}�K�W���T�C�Y
    public int _heatLimit;//�M�ʌ��E�l
    public int _heatSpeed;//�M�ʉ��Z�l
    public int _firingRate;//���˃��[�g[��/�b]
}