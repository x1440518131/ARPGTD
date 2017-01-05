using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager _instance;
    public TextAsset listinfo;
    public Dictionary<int, Inventory> inventoryDict = new Dictionary<int, Inventory>();
    public List<InventoryItem> inventoryItemList = new List<InventoryItem>();

    public delegate void OnInventoryChangeEvent();
    public event OnInventoryChangeEvent OnInventoryChange;

    void Awake()
    {
        _instance = this;
        ReadInventoryInfo();
    }

    void Start()
    {
        ReadInventoryItemInfo();
    }

    void ReadInventoryInfo()
    {
        string str = listinfo.ToString();
        string[] itemStrArray = str.Split('\n');
        foreach (string itemStr in itemStrArray)
        {
            //ID 名称 图标 类型（Equip，Drug） 装备类型(Helm,Cloth,Weapon,Shoes,Necklace,Bracelet,Ring,Wing) 售价 星级 品质 伤害 生命 战斗力 作用类型 作用值 描述
            string[] proArray = itemStr.Split('|');
            Inventory inventory = new Inventory();
            inventory.ID = int.Parse(proArray[0]);
            inventory.Name = proArray[1];
            inventory.ICON = proArray[2];
            switch (proArray[3])
            {
                case "Equip":
                    inventory.InventoryTYPE = InventoryType.Equip;
                    break;
                case "Drug":
                    inventory.InventoryTYPE = InventoryType.Drug;
                    break;
                case "Box":
                    inventory.InventoryTYPE = InventoryType.Box;
                    break;
            }
            if (inventory.InventoryTYPE == InventoryType.Equip)
            {
                switch (proArray[4])
                {
                    case "Helm":
                        inventory.EquipTYPE = EquipType.Helm;
                        break;
                    case "Cloth":
                        inventory.EquipTYPE = EquipType.Cloth;
                        break;
                    case "Weapon":
                        inventory.EquipTYPE = EquipType.Weapon;
                        break;
                    case "Shoes":
                        inventory.EquipTYPE = EquipType.Shoes;
                        break;
                    case "Necklace":
                        inventory.EquipTYPE = EquipType.Necklace;
                        break;
                    case "Bracelet":
                        inventory.EquipTYPE = EquipType.Bracelet;
                        break;
                    case "Ring":
                        inventory.EquipTYPE = EquipType.Ring;
                        break;
                    case "Wing":
                        inventory.EquipTYPE = EquipType.Wing;
                        break;
                }

            }
            //print(itemStr);
            //售价 星级 品质 伤害 生命 战斗力 作用类型 作用值 描述
            inventory.Price = int.Parse(proArray[5]);
            if (inventory.InventoryTYPE == InventoryType.Equip)
            {
                inventory.StarLevel = int.Parse(proArray[6]);
                inventory.Quality = int.Parse(proArray[7]);
                inventory.Damage = int.Parse(proArray[8]);
                inventory.HP = int.Parse(proArray[9]);
                inventory.Power = int.Parse(proArray[10]);
            }
            if (inventory.InventoryTYPE == InventoryType.Drug)
            {
                inventory.ApplyValue = int.Parse(proArray[12]);
            }
            inventory.Des = proArray[13];
            inventoryDict.Add(inventory.ID, inventory);
        }
    }

    //完成角色背包信息的初始化，获得需要的物品
    void ReadInventoryItemInfo()
    {
        //TODO 需要链接服务器，获得当前角色的物品信息
        //随机生成主角需要的物品
        for (int i = 0; i < 20; i++)
        {
            int id = Random.Range(1001, 1020);
            Inventory j = null;
            inventoryDict.TryGetValue(id, out j);
            if (j.InventoryTYPE == InventoryType.Equip)
            {
                InventoryItem it = new InventoryItem();
                it.Inventory = j;
                it.Level = Random.Range(0, 10);
                it.Count = 1;
                inventoryItemList.Add(it);
            }
            else { 
                //先判断背包里是否存在
                InventoryItem it = null;
                bool isExit = false;
                foreach (InventoryItem temp in inventoryItemList)
                {
                    if (temp.Inventory.ID == id)
                    {
                        isExit = true;
                        it = temp;
                        break;
                    }
                }
                if (isExit)
                {
                    it.Count++;
                }
                else
                {
                    it = new InventoryItem();
                    it.Inventory = j;
                    it.Count = 1;
                    inventoryItemList.Add(it);
                }

            }
        }
        OnInventoryChange();
    }
}
