using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, UIForm> m_canvas = new Dictionary<string, UIForm>();
    private Transform m_mainLayer;
    private Transform m_dialogLayer;
    private Transform m_popUpLayer;

    public void ShowUIForm( string path )
    {
        if (!m_canvas.TryGetValue(path, out var uiform))
        {
            var canvas = ObjectPoolManager.Clone(path);//SourceManager.LoadResource(path);
            uiform = canvas.GetComponent<UIForm>();
        }
        
        Transform tarLayer = null;
        switch(uiform.m_UILayer)
        {
            case UIForm.UILayer.Main:
                tarLayer = m_mainLayer;
                break;
            case UIForm.UILayer.Dialog:
                tarLayer = m_dialogLayer;
                break;
            case UIForm.UILayer.PopUp:
                tarLayer = m_popUpLayer;
                break;
        }

        if (uiform.m_ShowMode == UIForm.ShowMode.Solo)
        {
            for (int i = 0; i < tarLayer.childCount; i++)
            {
                var c = tarLayer.GetChild(i);
                c.gameObject.SetActive(false);
            }
        }
        
        uiform.transform.SetParent(tarLayer);
        uiform.transform.SetAsLastSibling();
    }

    public bool CloseUIForm( string path )
    {
        if (m_canvas.TryGetValue(path, out var uiform))
        {
            uiform.gameObject.SetActive(false);
            return true;
        }
        return false;
    }
}
