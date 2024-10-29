using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Item/Equipment")]
public class EquipmentSO : ItemSO
{

    [Header("EquipmentSO")]
    public GameObject equipPrefab;

    public float jumpPower;

    public override bool Use()
    {
        base.Use();
        CharacterManager.Instance.Player.GetComponent<Equipment>().EquipNew(this);

        return false;
    }
}
