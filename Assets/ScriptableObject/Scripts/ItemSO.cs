using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UsableItem
{
    public void Use();
}

public class ItemSO : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;

    public Sprite icon;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    public virtual bool Use()
    {
        return false;
    }
}
