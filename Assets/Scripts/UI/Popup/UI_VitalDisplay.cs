using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_VitalDisplay : UI_Popup
{
    enum Images { HealthBar, StaminaBar }

    private void Start()
    {
        Bind<Image>(typeof(Images));

        Init();

        CharacterManager.Instance.Player.gameObject.GetComponent<VitalController>().OnVitalValueChange += UpdateBar;
    }

    public override void Init()
    {
        base.Init();
    }


    public void UpdateBar(float healthRatio, float staminaRatio)
    {
        Image health = Get<Image>((int)Images.HealthBar);
        Image stamina = Get<Image>((int)Images.StaminaBar);

        health.fillAmount = healthRatio;
        stamina.fillAmount = staminaRatio;
    }
}
