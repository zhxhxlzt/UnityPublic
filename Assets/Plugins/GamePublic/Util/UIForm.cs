using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIForm : MonoBehaviour
{
    public enum ShowMode { Additive, Solo }
    public enum UILayer { Main, Dialog, PopUp}
    public ShowMode m_ShowMode;
    public UILayer m_UILayer;
    public UnityEvent OnEsc;
}
