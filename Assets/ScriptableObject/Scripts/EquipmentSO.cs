using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Item/Equipment")]
public class EquipmentSO : ItemSO
{

    [Header("EquipmentSO")]
    public GameObject dropPrefab;

    public float jumpPower;

    public override void Use()
    {
        base.Use();


    }
}
