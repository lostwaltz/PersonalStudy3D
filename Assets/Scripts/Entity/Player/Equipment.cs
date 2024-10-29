using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Equipment : MonoBehaviour
{
    public Equip curEquip;
    public Transform equipParent;

    void Start()
    {
    }

    // Update is called once per frame
    public void EquipNew(EquipmentSO data)
    {
        UnEquip();
        curEquip = Instantiate(data.equipPrefab, equipParent).GetComponent<Equip>();
        curEquip.ApplyEquip();
    }

    public void UnEquip()
    {
        if (curEquip != null)
        {
            curEquip.ApplyUnEquip();
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}
