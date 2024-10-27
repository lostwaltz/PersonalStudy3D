using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : ValueSystem
{
    private float staminaRegenRate = 2f;
    private float timeSinceLastCahange = 2f;

    public StaminaSystem(CharacterStatsHandler _statsHandler) : base(_statsHandler)
    {
    }

    private int MaxStamina => statsHandler.CurrentStat.maxStamnia;

    public override bool ChangeValue(float amount)
    {
        timeSinceLastCahange = 0f;

        CurrentValue += amount;
        CurrentValue.Clamp(0f, MaxStamina);

        vitalController.OnUpdateValue();
        return true;
    }

    public override void Update()
    {
        timeSinceLastCahange += Time.deltaTime;
        if (timeSinceLastCahange < staminaRegenRate)
            return;


        //TODO: need retouch magic number
        CurrentValue = CurrentValue + (20f * Time.deltaTime);
        CurrentValue.Clamp(0f, MaxStamina);

        vitalController.OnUpdateValue();
    }
}
