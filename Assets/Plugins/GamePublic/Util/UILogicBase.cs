using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(UIForm))]
public class UILogicBase : MonoBehaviour
{
    protected virtual void OnEsc() { }
    protected Transform FindUIObject(string name )
    {
        return transform.FindDownNode(name);
    }
    protected void RegisterEvent(string eventName, UnityAction<ScriptEventArgs> func)
    {
        EventMgr.GetInstance().RegisterEvent(eventName, func);
    }
    protected void UnRegisterEvent(string eventName, UnityAction<ScriptEventArgs> func)
    {
        EventMgr.GetInstance().UnregisterEvent(eventName, func);
    }
    protected void RegisterButton(string btnName, UnityAction func)
    {
        transform.FindDownNode(btnName).GetComponent<Button>().onClick.AddListener(func);
    }
}
