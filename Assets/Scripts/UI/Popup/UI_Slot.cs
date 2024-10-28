using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_Slot : UI_Base
{
    public UI_Inventory uI_Inventory;
    public int slotIndex;

    ItemSO itemSO;

    enum Images { Icon }

    enum Texts { Text }

    enum Buttons { Close }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GetImage((int)Images.Icon).sprite = null;
        UpdateSlot(null, 0);

        GetButton((int)Buttons.Close).gameObject.BindEvent(RemoveItem);

        gameObject.BindEvent(UseItem);
    }

    private void Start()
    {
        Init();
    }

    public void UpdateSlot(ItemSO _itemSO, int itemQuantity)
    {
        itemSO = _itemSO;

        GetImage((int)Images.Icon).enabled = !(itemSO == null);
        GetText((int)Texts.Text).enabled = !(itemSO == null);
        GetButton((int)Buttons.Close).enabled = !(itemSO == null);

        GetImage((int)Images.Icon).sprite = itemSO?.icon;
        GetText((int)Texts.Text).text = itemQuantity.ToString();
    }

    public void RemoveItem(PointerEventData data)
    {
        uI_Inventory.RemoveItem(slotIndex, 1);
    }

    public void UseItem(PointerEventData data)
    {
        if (null == itemSO)
            return;

        itemSO.Use();
        uI_Inventory.RemoveItem(slotIndex, 1);
    }
}
