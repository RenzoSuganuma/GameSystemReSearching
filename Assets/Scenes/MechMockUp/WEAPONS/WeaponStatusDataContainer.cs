using UnityEngine;
public enum WeaponStackPosition
{
    RArm,
    LArm,
    RSHoulder,
    LShoulder,
}
[CreateAssetMenu(menuName = "WeaponDataContainer/CreateTable", fileName = "WeaponDataTable")]
public class WeaponStatusDataContainer : ScriptableObject
{
    public int _magazineAmounts;//�}�K�W���� �� ���e�� �� �}�K�W���� �~ �}�K�W���T�C�Y
    public int _magazineSize;//�}�K�W���T�C�Y
    public int _heatLimit;//�M�ʌ��E�l
    public int _heatSpeed;//�M�ʉ��Z�l
    public int _firingRate;//���˃��[�g[��/�b]
    public int _firingAmounts;//���˒e��
    public int _reloadingTime;//�����[�h����
    public int _coolingTime;//��p����
    public WeaponStackPosition _wPosition;//�ύڃ|�W�V����
}