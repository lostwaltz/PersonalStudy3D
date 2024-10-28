using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Dictionary<string, UI_Base> uiContainer = new Dictionary<string, UI_Base>();

    private GameObject rootUI;

    public GameObject RootUI
    {
        get
        {
            if(null == rootUI)
            {
                rootUI = GameObject.Find("ROOT_UI");

                if (null == rootUI)
                    rootUI = new GameObject("ROOT_UI");
            }
            return rootUI;
        }
    }

    int _order = 10;

    public int Order { get { return _order; } }


    SortedList<int, UI_Popup> _popupList = new SortedList<int, UI_Popup>();
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        uiContainer.Add("UI_InfoBox", Instance.ShowPopupUI<UI_InfoBox>());
        uiContainer["UI_InfoBox"].gameObject.SetActive(false);

        uiContainer.Add("UI_Inventory", Instance.ShowPopupUI<UI_Inventory>());
    }


    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = ResourceManager.Instance.Instantiate($"UI/Scene/{name}");

        T sceneUI = go.GetOrAddComponent<T>();
        _sceneUI = sceneUI;

        go.transform.SetParent(RootUI.transform);

        return sceneUI;
    }
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        
        GameObject go = ResourceManager.Instance.Instantiate($"UI/Popup/{name}");

        T popup = go.GetOrAddComponent<T>();
        _popupStack.Push(popup);

        go.transform.SetParent(RootUI.transform);

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if(_popupStack.Count == 0) return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
        {
            _order = 10;
            return;
        }

        UI_Popup popup = _popupStack.Pop();
        ResourceManager.Instance.Destroy(popup.gameObject);
        popup = null;

        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }

}
