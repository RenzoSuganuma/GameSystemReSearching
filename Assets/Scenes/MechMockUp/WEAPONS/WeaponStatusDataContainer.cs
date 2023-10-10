using UnityEngine;
[CreateAssetMenu(menuName = "WeaponDataContainer/CreateTable", fileName = "WeaponDataTable")]
public class WeaponStatusDataContainer : ScriptableObject
{
    public int _magazineAmounts;//マガジン数 → 装弾数 ＝ マガジン数 × マガジンサイズ
    public int _magazineSize;//マガジンサイズ
    public int _heatLimit;//熱量限界値
    public int _heatSpeed;//熱量加算値
    public int _firingRate;//発射レート[回/秒]
    public int _firingAmounts;//発射弾数
    public int _reloadingTime;//リロード時間
}