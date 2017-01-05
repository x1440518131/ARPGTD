using UnityEngine;
using System.Collections;
public enum InfoType
{
    Name,
    HeadPortrait,
    Level,
    Power,
    Exp,
    Diamond,
    Coin,
    Energy,
    Toughen,
    HP,
    Damage,
    Equip,
    All
}
public class PlayerInfo : MonoBehaviour {
//姓名
//头像
//等级
//战斗力
//经验数
//钻石数
//金币数
//体力数
//历练数
    public static PlayerInfo _instance;
    #region property
    private string _name;
    private string _headPortrait;
    private int _level;
    private int _power;
    private int _exp;
    private int _diamond;
    private int _coin;
    private int _energy;
    private int _toughen;
    private int _hp;
    private int _damage;
    //private int _helmID=0;
    //private int _clothID=0;
    //private int _weaponID=0;
    //private int _shoesID=0;
    //private int _necklaceID=0;
    //private int _braceletID=0;
    //private int _ringID=0;
    //private int _wingID=0;

    public InventoryItem _helmInventoryItem;
    public InventoryItem _clothInventoryItem;
    public InventoryItem _weaponInventoryItem;
    public InventoryItem _shoesInventoryItem;
    public InventoryItem _necklaceInventoryItem;
    public InventoryItem _braceletInventoryItem;
    public InventoryItem _ringInventoryItem;
    public InventoryItem _wingInventoryItem;
    #endregion

    public float energyTimer = 0;
    public float toughenTimer = 0;

    public delegate void OnPlayerInfoChangedEvent(InfoType type);
    public event OnPlayerInfoChangedEvent OnPlayerInfoChanged;

    #region get set method
    public string Name {
        get {
            return _name;
        }
        set {
            _name = value;
        }
    }

    public string HeadPortrait {
        get
        {
            return _headPortrait;
        }
        set
        {
            _headPortrait = value;
        }
    }

    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }

    public int Power
    {
        get
        {
            return _power;
        }
        set
        {
            _power = value;
        }
    }
    public int Exp
    {
        get
        {
            return _exp;
        }
        set
        {
            _exp = value;
        }
    }
    public int Diamond
    {
        get
        {
            return _diamond;
        }
        set
        {
            _diamond = value;
        }

    }
    public int Coin
    {
        get
        {
            return _coin;
        }
        set
        {
            _coin = value;
        }
    }
    public int Energy
    {
        get
        {
            return _energy;
        }
        set
        {
            _energy = value;
        }
    }
    public int Toughen
    {
        get
        {
            return _toughen;
        }
        set
        {
            _toughen = value;
        }
    }
    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    //public int HelmID
    //{
    //    get { return _helmID; }
    //    set { _helmID = value; }
    //}
    //public int ClothID
    //{
    //    get { return _clothID; }
    //    set { _clothID = value; }
    //}
    //public int WeaponID
    //{
    //    get
    //    {
    //        return _weaponID;
    //    }
    //    set
    //    {
    //        _weaponID = value;
    //    }
    //}
    //public int ShoesID
    //{
    //    get
    //    {
    //        return _shoesID;
    //    }
    //    set
    //    {
    //        _shoesID = value;
    //    }
    //}
    //public int NecklaceID
    //{
    //    get { return _necklaceID; }
    //    set { _necklaceID = value; }
    //}
    //public int BraceletID
    //{
    //    get { return _braceletID; }
    //    set { _braceletID = value; }
    //}
    //public int RingID
    //{
    //    get { return _ringID; }
    //    set { _ringID = value; }
    //}
    //public int WingID
    //{
    //    get
    //    {
    //        return _wingID;
    //    }
    //    set
    //    {
    //        _wingID = value;
    //    }
    //}
    #endregion
    #region unity event
    void Init()
    {
        this.Coin = 9870;
        this.Diamond = 1234;
        this.Energy = 78;
        this.Exp = 123;
        this.HeadPortrait = "头像底板女性";
        this.Level = 12;
        this.Name = "千颂伊";
        this.Toughen = 34;
       
        
        //this.BraceletID = 1001;
        //this.WingID = 1002;
        //this.RingID = 1003;
        //this.ClothID = 1004;
        //this.HelmID = 1005;
        //this.WeaponID = 1006;
        //this.NecklaceID = 1007;
        //this.ShoesID = 1008;

        InitHPDamagePower();
        OnPlayerInfoChanged(InfoType.All);
    }  
    #endregion
    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (this.Energy < 100)
        {
            energyTimer += Time.deltaTime;
            if (energyTimer > 60)
            {
                Energy += 1;
                energyTimer -= 60;
                OnPlayerInfoChanged(InfoType.Energy);
            }
        }
        else {
            this.energyTimer = 0;
        }

        if (this.Toughen < 50)
        {
            toughenTimer += Time.deltaTime;
            if (toughenTimer > 60)
            {
                Energy += 1;
                toughenTimer -= 60;
                OnPlayerInfoChanged(InfoType.Toughen);
            }
        }
        else
        {
            this.toughenTimer = 0;
        }
    }

    public void ChangeName(string newName)
    {
        this.Name = newName;
        OnPlayerInfoChanged(InfoType.Name);
    }


    public void DressOn(InventoryItem it)
    {
        it.IsDressed = true;
        //首先检测有没有穿上相同类型的装备
        bool isDressed = false;
        InventoryItem inventoryItemDressed = null;
        switch (it.Inventory.EquipTYPE)
        { 
            case EquipType.Bracelet:
                if (_braceletInventoryItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = _braceletInventoryItem;
                }
                _braceletInventoryItem = it;
                break;
            case EquipType.Cloth:
                if (_clothInventoryItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = _clothInventoryItem;
                }
                _clothInventoryItem = it;
                break;
            case EquipType.Helm:
                if (_helmInventoryItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = _helmInventoryItem;
                }
                _helmInventoryItem = it;
                break;
            case EquipType.Necklace:
                if (_necklaceInventoryItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = _necklaceInventoryItem;
                }
                _necklaceInventoryItem = it;
                break;
            case EquipType.Ring:
                if (_ringInventoryItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = _ringInventoryItem;
                }
                _ringInventoryItem = it;
                break;
            case EquipType.Shoes:
                if (_shoesInventoryItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = _shoesInventoryItem;
                }
                _shoesInventoryItem = it;
                break;
            case EquipType.Weapon:
                if (_weaponInventoryItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = _weaponInventoryItem;
                }
                _weaponInventoryItem = it;
                break;
            case EquipType.Wing:
                if (_wingInventoryItem != null)
                {
                    isDressed = true;
                    inventoryItemDressed = _wingInventoryItem;
                }
                _wingInventoryItem = it;
                break;
        
        }
        //有
        if (isDressed)
        {
            inventoryItemDressed.IsDressed = false;
            InventoryUI._instance.AddInventoryItem(inventoryItemDressed);
        }

        OnPlayerInfoChanged(InfoType.Equip);
        //把已经存在的脱掉 放到背包
        //没有
        //直接穿上
    }

    //需要的金币数
    public bool GetCoin(int count)
    { 
    if(_coin>count){
        _coin -= count;
        OnPlayerInfoChanged(InfoType.Coin);
        return true;
    }
       return false;
    }

    public int GetOverallPower()
    {
        float power = this.Power;
        if (_helmInventoryItem != null)
        {
            power += _helmInventoryItem.Inventory.Power * (1 + (_helmInventoryItem.Level - 1) / 10f);
        }
        if (_clothInventoryItem != null)
        {
            power += _clothInventoryItem.Inventory.Power * (1 + (_clothInventoryItem.Level - 1) / 10f);
        }
        if (_weaponInventoryItem != null)
        {
            power += _weaponInventoryItem.Inventory.Power * (1 + (_weaponInventoryItem.Level - 1) / 10f);
        }
        if (_shoesInventoryItem != null)
        {
            power += _shoesInventoryItem.Inventory.Power * (1 + (_shoesInventoryItem.Level - 1) / 10f);
        }
        if (_necklaceInventoryItem != null)
        {
            power += _necklaceInventoryItem.Inventory.Power * (1 + (_necklaceInventoryItem.Level - 1) / 10f);
        }
        if (_braceletInventoryItem != null)
        {
            power += _braceletInventoryItem.Inventory.Power * (1 + (_braceletInventoryItem.Level - 1) / 10f);
        }
        if (_ringInventoryItem != null)
        {
            power += _ringInventoryItem.Inventory.Power * (1 + (_ringInventoryItem.Level - 1) / 10f);
        }
        if (_wingInventoryItem != null)
        {
            power += _wingInventoryItem.Inventory.Power * (1 + (_wingInventoryItem.Level - 1) / 10f);
        }
        return (int)power;
    }

    void InitHPDamagePower()
    {
        this.HP = this.Level * 100;
        this.Damage = this.Level * 50;
        this.Power = this.HP + this.Damage;
    }

    void PutonEquip(int id)
    {
        if (id == 0) return;
        Inventory inventory = null;
        InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
        this.HP += inventory.HP;
        this.Damage += inventory.Damage;
        this.Power += inventory.Power;
    }

    void PutOffEquip(int id)
    {
        if (id == 0) return;
        Inventory inventory = null;
        InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
        this.HP -= inventory.HP;
        this.Damage -= inventory.Damage;
        this.Power -= inventory.Power;
    }


    public void DressOff(InventoryItem it)
    {
        switch (it.Inventory.EquipTYPE)
        {
            case EquipType.Bracelet:
                if (_braceletInventoryItem != null)
                {           
                    _braceletInventoryItem=null;
                }              
                break;
            case EquipType.Cloth:
                if (_clothInventoryItem != null)
                {
                     _clothInventoryItem=null;
                }         
                break;
            case EquipType.Helm:
                if (_helmInventoryItem != null)
                {
                     _helmInventoryItem=null;
                }             
                break;
            case EquipType.Necklace:
                if (_necklaceInventoryItem != null)
                {
                     _necklaceInventoryItem=null;
                }              
                break;
            case EquipType.Ring:
                if (_ringInventoryItem != null)
                {
                     _ringInventoryItem=null;
                }
                break;
            case EquipType.Shoes:
                if (_shoesInventoryItem != null)
                {
                     _shoesInventoryItem=null;
                }             
                break;
            case EquipType.Weapon:
                if (_weaponInventoryItem != null)
                {
                     _weaponInventoryItem=null;
                }         
                break;
            case EquipType.Wing:
                if (_wingInventoryItem != null)
                {
                     _wingInventoryItem=null;
                }              
                break;

        }
        it.IsDressed = false;
        InventoryUI._instance.AddInventoryItem(it);
        OnPlayerInfoChanged(InfoType.Equip);
    }

    public void InventoryUse(InventoryItem it,int count)
    { 
        //使用效果
        //TODO
        //处理物品使用后是否还存在
        it.Count-=count;
        if (it.Count <= 0)
        {
            InventoryManager._instance.inventoryItemList.Remove(it);
        }

    }
}
