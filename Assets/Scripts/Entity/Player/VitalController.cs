using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: VitalSystem이 VitalController의 메서드를 알게 되며 의존성이 생겼음. 해당 문제 리팩토링 생각해보기
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
    public bool StopChangeValue = false;

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
                return healthSystem.ChangeValue(amount);
            case VitalType.STAMINA:
                return staminaSystem.ChangeValue(amount);
        }

        return false;
    }

    public void OnUpdateValue()
    {
        OnVitalValueChange?.Invoke(healthSystem.CurrentValue / statsHandler.CurrentStat.maxHealth, staminaSystem.CurrentValue / statsHandler.CurrentStat.maxStamnia);
    }

    public bool isInvinciblility = false;

    public void StartInvinciblility()
    {
        StartCoroutine(Invinciblility());
    }

    IEnumerator Invinciblility()
    {
        healthSystem.StopChangeValue = true;
        yield return new WaitForSeconds(5f);
        healthSystem.StopChangeValue = false;

    }
}