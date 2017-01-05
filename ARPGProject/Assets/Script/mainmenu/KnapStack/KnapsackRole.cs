using UnityEngine;
using System.Collections;

public class KnapsackRole : MonoBehaviour {
    private KnapsackRoleEquip helmEquip;
    private KnapsackRoleEquip clothEquip;
    private KnapsackRoleEquip weaponEquip;
    private KnapsackRoleEquip shoesEquip;
    private KnapsackRoleEquip necklackEquip;
    private KnapsackRoleEquip braceletEquip;
    private KnapsackRoleEquip ringEquip;
    private KnapsackRoleEquip wingEquip;

    private UILabel hpLabel;
    private UILabel damageLabel;
    private UILabel expLabel;
    private UISlider expSlider;
    void Awake()
    {
        helmEquip = transform.Find("Role/HelmSprite").GetComponent<KnapsackRoleEquip>();
        clothEquip = transform.Find("Role/ClothSprite").GetComponent<KnapsackRoleEquip>();
        weaponEquip = transform.Find("Role/WeaponSprite").GetComponent<KnapsackRoleEquip>();
        shoesEquip = transform.Find("Role/ShoesSprite").GetComponent<KnapsackRoleEquip>();
        necklackEquip = transform.Find("Role/NecklaceSprite").GetComponent<KnapsackRoleEquip>();
        braceletEquip = transform.Find("Role/BraceletSprite").GetComponent<KnapsackRoleEquip>();
        ringEquip = transform.Find("Role/RingSprite").GetComponent<KnapsackRoleEquip>();
        wingEquip = transform.Find("Role/WingSprite").GetComponent<KnapsackRoleEquip>();

        hpLabel = transform.Find("Role/HpBg/Label").GetComponent<UILabel>();
        damageLabel = transform.Find("Role/DamageBg/Label").GetComponent<UILabel>();
        expLabel = transform.Find("Role/ExpSlider/Label").GetComponent<UILabel>();
        expSlider = transform.Find("Role/ExpSlider").GetComponent<UISlider>();

        PlayerInfo._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
    }

    void OnDestroy()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;
    }

    void OnPlayerInfoChanged(InfoType type)
    {
        if (type == InfoType.All || type == InfoType.HP || type == InfoType.Damage || type == InfoType.Exp||type ==InfoType.Equip)
        {
            UpdateShow();
        }
    }

    void UpdateShow()
    {
        PlayerInfo info = PlayerInfo._instance;

        //helmEquip.SetId(info.HelmID);
        //clothEquip.SetId(info.ClothID);
        //weaponEquip.SetId(info.WeaponID);
        //shoesEquip.SetId(info.ShoesID);
        //necklackEquip.SetId(info.NecklaceID);
        //braceletEquip.SetId(info.BraceletID);
        //ringEquip.SetId(info.RingID);
        //wingEquip.SetId(info.WingID);

        helmEquip.SetInventoryItem(info._helmInventoryItem);
        clothEquip.SetInventoryItem(info._clothInventoryItem);
        weaponEquip.SetInventoryItem(info._weaponInventoryItem);
        shoesEquip.SetInventoryItem(info._shoesInventoryItem);
        necklackEquip.SetInventoryItem(info._necklaceInventoryItem);
        braceletEquip.SetInventoryItem(info._braceletInventoryItem);
        ringEquip.SetInventoryItem(info._ringInventoryItem);
        wingEquip.SetInventoryItem(info._wingInventoryItem);

        hpLabel.text = info.HP.ToString();
        damageLabel.text= info.Damage.ToString();
        expSlider.value = (float)info.Exp / GameController.GetRequireExpBylevel(info.Level + 1);
        expLabel.text = info.Exp + "/" + GameController.GetRequireExpBylevel(info.Level + 1);
    }
}
