using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class HealthSystem : ValueSystem
{
    private float healthChangeDelay = 0.5f;

    private float timeSinceLastChange = float.MaxValue;

    private CharacterEventContainer eventContainer;

    private int MaxHealth => statsHandler.CurrentStat.maxHealth;

    public bool IsAttacked { get; private set; }


    public HealthSystem(CharacterStatsHandler _statsHandler, CharacterEventContainer _eventContainer) : base(_statsHandler)
    {
        eventContainer = _eventContainer;
        CurrentValue = statsHandler.CurrentStat.maxHealth;
    }
    public override void Update()
    {
        if (true == IsAttacked && timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;

            if (timeSinceLastChange >= healthChangeDelay)
            {
                eventContainer.OninvinciblilityEnd?.Invoke();
                IsAttacked = false;
            }
        }
    }
    public override bool ChangeValue(float change)
    {
        if (change > 0)
        {
            CurrentValue += change;
            CurrentValue = Mathf.Clamp(CurrentValue, 0, MaxHealth);
            eventContainer.OnHeal?.Invoke(CurrentValue);

            vitalController.OnUpdateValue();
            return true;
        }

        if (true == StopChangeValue)
            return false;

        if (false == CheackHealthChangeDelayEnd())
            return false;

        CurrentValue += change;
        CurrentValue = Mathf.Clamp(CurrentValue, 0, MaxHealth);

        eventContainer.OnDamage?.Invoke(CurrentValue);
        IsAttacked = true;

        timeSinceLastChange = 0f;

        if (CurrentValue == 0)
            eventContainer.OnDeath?.Invoke();


        vitalController.OnUpdateValue();
        return true;
    }

    private bool CheackHealthChangeDelayEnd()
    {
        return timeSinceLastChange >= healthChangeDelay;
    }
}
