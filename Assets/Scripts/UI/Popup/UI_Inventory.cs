using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_Inventory : UI_Popup
{
    public List<ItemSO> itemDataList = new List<ItemSO>();
    public List<int> quantityList = new List<int>();
    public UI_Slot[] slots;

    private int curEquipSlot;

    enum GameObjects { SlotList }
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject go = Get<GameObject>((int)GameObjects.SlotList);

        slots = go.GetComponentsInChildren<UI_Slot>();

        int index = 0;
        foreach (UI_Slot slot in slots)
        {
            slot.uI_Inventory = this;
            slot.slotIndex = index++;
        }

        CharacterManager.Instance.Player.controller.OnOpenInventoryEvent += Togle;
    }

    private void Start()
    {
        Init();

        gameObject.SetActive(false);
    }

    private void Togle()
    {
        Cursor.lockState = gameObject.activeInHierarchy ? CursorLockMode.Locked : CursorLockMode.None;

        gameObject.SetActive(!gameObject.activeInHierarchy);
        CharacterManager.Instance.Player.OpenUI = gameObject.activeInHierarchy;

    }

    public void Update()
    {
    }

    public void AddItem(ItemSO itemSO, int addQuantity)
    {
        if (itemSO.canStack)
            AddStackableItem(itemSO, addQuantity);
        else
            AddNonStackableItem(itemSO, addQuantity);

        UpdateInventory();
    }
    private void AddStackableItem(ItemSO itemSO, int addQuantity)
    {
        for (int i = 0; i < itemDataList.Count; i++)
        {
            if (itemDataList[i] == itemSO && quantityList[i] < itemSO.maxStackAmount)
            {
                int spaceLeft = itemSO.maxStackAmount - quantityList[i];
                int quantityToAdd = Mathf.Min(spaceLeft, addQuantity);
                quantityList[i] += quantityToAdd;
                addQuantity -= quantityToAdd;

                if (addQuantity <= 0)
                    return;
            }
        }

        if (addQuantity > 0 && itemDataList.Count < slots.Length)
        {
            int quantityToAdd = Mathf.Min(itemSO.maxStackAmount, addQuantity);
            itemDataList.Add(itemSO);
            quantityList.Add(quantityToAdd);
            addQuantity -= quantityToAdd;

            if (addQuantity > 0)
                AddStackableItem(itemSO, addQuantity);
        }
    }

    private void AddNonStackableItem(ItemSO itemSO, int addQuantity)
    {
        if (itemDataList.Count + addQuantity > slots.Length)
        {
            int availableSlots = slots.Length - itemDataList.Count;
            addQuantity = availableSlots;
        }

        for (int i = 0; i < addQuantity; i++)
        {
            itemDataList.Add(itemSO);
            quantityList.Add(1);
        }
    }

    public void UpdateInventory()
    {
        int index = 0;

        foreach (UI_Slot slot in slots)
        {
            if (index < itemDataList.Count)
            {
                slot.UpdateSlot(itemDataList[index], quantityList[index]);
                index++;
            }
            else
            {
                slot.UpdateSlot(null, 0);

            }
        }
    }

    public void RemoveItem(int index, int removeQuantity)
    {
        if (itemDataList[index].canStack)
        {
            if (0 < quantityList[index])
            {
                quantityList[index] -= removeQuantity;
                if (quantityList[index] <= 0)
                {
                    quantityList.RemoveAt(index);
                    itemDataList.RemoveAt(index);
                }
            }
        }
        else
        {
            for (int i = 0; i < removeQuantity; i++)
            {
                quantityList.RemoveAt(index);
                itemDataList.RemoveAt(index);
            }
        }

        UpdateInventory();
    }
}
