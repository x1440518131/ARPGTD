using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour {
    public static InventoryUI _instance;
    public List<InventoryItemUI> itemUIList = new List<InventoryItemUI>();
    private UIButton clearupButton;

    void Awake()
    {
        _instance = this;
        InventoryManager._instance.OnInventoryChange += this.OnInventoryChange;
        clearupButton = transform.Find("ButtonClearup").GetComponent<UIButton>();
        EventDelegate ed = new EventDelegate(this,"OnClearup");
        clearupButton.onClick.Add(ed);
    }

    void OnDestory()
    {
        InventoryManager._instance.OnInventoryChange -= this.OnInventoryChange;
    }

    void OnInventoryChange() {
        UpdateShow();    
    }

    void UpdateShow()
    {
        int temp = 0;
        for (int i = 0; i < InventoryManager._instance.inventoryItemList.Count; i++) {
            InventoryItem it = InventoryManager._instance.inventoryItemList[i];
            if (it.IsDressed == false)
            {
                itemUIList[temp].SetInventoryItem(it);
                temp++;
            }
        }
        for (int i = temp; i < itemUIList.Count; i++)
        {
            itemUIList[i].Clear();
        }
    }

    
    public void AddInventoryItem(InventoryItem it)
    {
        foreach (InventoryItemUI itUI in itemUIList)
        {
            if (itUI.it==null)
            {
                itUI.SetInventoryItem(it);
                break;
            }
        }
    }

    void OnClearup()
    {
        UpdateShow();
    }
}
