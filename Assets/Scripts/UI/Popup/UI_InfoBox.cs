using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_InfoBox : UI_Popup
{
    enum Texts { Text }

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<TMP_Text>(typeof(Texts));
    }

    public void UpdateInfoBox(string _text)
    {
        GetText((int)Texts.Text).text = _text;
    }
}
