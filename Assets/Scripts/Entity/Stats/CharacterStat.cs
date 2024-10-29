
using UnityEngine;


[System.Serializable]
public class CharacterStat
{
    [Range(1, 100f)] public int maxHealth;
    [Range(50, 100f)] public int maxStamnia;
    [Range(1, 20f)] public float speed;
}
