using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LongPressToggle : Toggle
{
    public bool IsClick = false;
    [Tooltip("按下时间限制")]
    public float holdTimeLimit = 1; // 按下时间限制，超时则不响应点击，响应长按

    private Coroutine m_countTimeCoroutine;

    public override void OnPointerDown( PointerEventData eventData )
    {
        base.OnPointerDown(eventData);
        IsClick = true;
        if (m_countTimeCoroutine != null)
        {
            StopCoroutine(m_countTimeCoroutine);
        }
        m_countTimeCoroutine = StartCoroutine(BeginCountTime());
    }

    public override void OnPointerUp( PointerEventData eventData )
    {
        base.OnPointerUp(eventData);
        if (m_countTimeCoroutine != null)
        {
            StopCoroutine(m_countTimeCoroutine);
            m_countTimeCoroutine = null;
        }
    }

    public override void OnPointerClick( PointerEventData eventData )
    {
        if (IsClick)
        {
            base.OnPointerClick(eventData);
        }
    }
    
    IEnumerator BeginCountTime()
    {
        yield return new WaitForSeconds(holdTimeLimit);
        IsClick = false;
        OnLongPress();
    }

    protected virtual void OnLongPress()
    {
        Debug.Log("Long ========= Press");
    }

}
