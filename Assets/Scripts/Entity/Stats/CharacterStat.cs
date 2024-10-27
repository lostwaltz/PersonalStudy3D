
using UnityEngine;

public enum StatsChangeType
{ 
    Add,
    Multple,
    Override
}


[System.Serializable]
public class CharacterStat
{
    public StatsChangeType statsChangeType;
    [Range(1, 100f)] public int maxHealth;
    [Range(50, 100f)] public int maxStamnia;
    [Range(1, 20f)] public float speed;
}
