using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemSO data;

    public void OnHitRay(float hitDistance)
    {
        string str = $"{data.displayName}\n{data.description}";
        UIManager.Instance.uiContainer["UI_InfoBox"].gameObject.GetComponent<UI_InfoBox>().UpdateInfoBox(str);
    }

    public void Oninteract()
    {
        UIManager.Instance.uiContainer["UI_Inventory"].gameObject.GetComponent<UI_Inventory>().AddItem(data, 1);

        Destroy(gameObject);
    }
}
    