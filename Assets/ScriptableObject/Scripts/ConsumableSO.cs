using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffType
{
    public ConsumableType type;
}

public enum ConsumableType
{
    SPEEDUP,
    DOBLEJUMP,
    INVINCIBILITY
}

[CreateAssetMenu(fileName = "Consumable", menuName = "Item/Consumable")]
public class ConsumableSO : ItemSO
{
    [Header("ConsumableSO")]

    public GameObject dropPrefab;

    [SerializeField] public BuffType[] consumables;

    public override bool Use()
    {
        base.Use();

        //TODO: change Item use logic / type switch -> buff useable interface
        foreach (BuffType type in consumables)
        {
            switch (type.type)
            {
                case ConsumableType.SPEEDUP:
                    CharacterManager.Instance.Player.gameObject.GetComponent<Movement>().StartSpeedBoost();
                    break;
                case ConsumableType.DOBLEJUMP:
                    CharacterManager.Instance.Player.gameObject.GetComponent<Movement>().StartDobleJump();
                    break;
                case ConsumableType.INVINCIBILITY:
                    CharacterManager.Instance.Player.gameObject.GetComponent<VitalController>().StartInvinciblility();
                    break;
            }
        }

        return true;
    }
}
