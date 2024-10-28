using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType
{
    HEALTH,
    STAMINA,
    BUFF
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Consumable", menuName = "Item/Consumable")]
public class ConsumableSO : ItemSO
{
    [Header("ConsumableSO")]

    public GameObject dropPrefab;

    public ItemDataConsumable[] consumables;

    public override void Use()
    {
        base.Use();

        //TODO: Need to revise item usage logic.
        CharacterManager.Instance.Player.gameObject.GetComponent<Movement>().StartSpeedBoost();
    }
}
