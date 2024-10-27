using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ValueSystem
{
    public ValueSystem(CharacterStatsHandler _statsHandler)
    {
        statsHandler = _statsHandler;
        vitalController = CharacterManager.Instance.Player.GetComponent<VitalController>();
    }

    protected CharacterStatsHandler statsHandler;
    protected VitalController vitalController;
    public float CurrentValue;

    public abstract void Update();
    public abstract bool ChangeValue(float amount);
}

public enum VitalType
{
    HEALTH, STAMINA
}

public class VitalController : MonoBehaviour
{
    private Action<float, float> onVitalValueChange;
    public Action<float, float> OnVitalValueChange { get { return onVitalValueChange; } set { onVitalValueChange += value ;}  }

    CharacterEventContainer eventContainer;
    CharacterStatsHandler statsHandler;

    private ValueSystem healthSystem;
    private ValueSystem staminaSystem;

    public float CurStamina { get { return staminaSystem.CurrentValue; } }

    private void Awake()
    {
        eventContainer = GetComponent<CharacterEventContainer>();
        statsHandler = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        healthSystem = new HealthSystem(statsHandler, eventContainer);
        staminaSystem = new StaminaSystem(statsHandler);
    }

    private void Update()
    {
        healthSystem.Update();
        staminaSystem.Update();
    }

    public bool ChangeValue(VitalType vitalType, float amount)
    {
        switch (vitalType)
        {
            case VitalType.HEALTH:
                return staminaSystem.ChangeValue(amount);
            case VitalType.STAMINA:
                return staminaSystem.ChangeValue(amount);
        }

        return false;
    }

    public void OnUpdateValue()
    {
        OnVitalValueChange?.Invoke(healthSystem.CurrentValue / statsHandler.CurrentStat.maxHealth, staminaSystem.CurrentValue / statsHandler.CurrentStat.maxStamnia);
    }

}