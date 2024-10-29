using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public float ExtraSpeed = 5f;

    private void Awake()
    {
        ExtraSpeed = 3f;
    }

    public void ApplyEquip()
    {
        CharacterManager.Instance.Player.GetComponent<CharacterStatsHandler>().CurrentStat.speed += ExtraSpeed;
    }

    public void ApplyUnEquip()
    {
        CharacterManager.Instance.Player.GetComponent<CharacterStatsHandler>().CurrentStat.speed -= ExtraSpeed;
    }
}
