using UnityEngine;
[CreateAssetMenu(menuName = "WeaponDataContainer/CreateTable", fileName = "WeaponDataTable")]
public class WeaponStatusDataContainer : ScriptableObject
{
    public int _bulletsCount;//‚·‚×‚Ä‚Ìc’e
    public int _magazineSize;//ƒ}ƒKƒWƒ“ƒTƒCƒY
    public int _heatLimit;//”M—ÊŒÀŠE’l
    public int _heatSpeed;//”M—Ê‰ÁZ’l
    public int _firingRate;//”­ËƒŒ[ƒg[‰ñ/•b]
}