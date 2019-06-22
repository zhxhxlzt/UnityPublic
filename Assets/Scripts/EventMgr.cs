using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;

/// <summary>
/// 异步的事件处理器，通过SendEvent和HandleEvent分离了事件发送与事件处理
/// 对于调整处理事件数量来优化帧数暂时没有能力做到
/// </summary>
public class EventMgr : MonoBehaviour
{
    public enum EventRcvMode { Sync, Async }    // 同步调用，异步调用
    private Queue<ScriptEventArgs> m_eventQueue;      // 消息队列
    private Dictionary<EventType, ScriptUnityEvent> m_registers; // 事件接收者
    private float m_timeLimit = 0.01f;

    private EventMgr()
    {
        m_eventQueue = new Queue<ScriptEventArgs>();
        m_registers = new Dictionary<EventType, ScriptUnityEvent>();
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="args"></param>
    public void SendEvent( EventType type, ScriptEventArgs args, EventRcvMode mode = EventRcvMode.Sync)
    {
        if (args == null) args = ScriptEventArgs.Empty;
        args.eventType = type;
        if (mode == EventRcvMode.Async)
        {
            m_eventQueue.Enqueue(args);
        }
        else
        {
            if (m_registers.TryGetValue(type, out var registers))
            {
                registers.Invoke(args);
            }
        }
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="type"></param>
    /// <param name="func"></param>
    public void RegisterEvent( EventType type, UnityAction<ScriptEventArgs> func )
    {
        if (!m_registers.TryGetValue(type, out var listeners))
        {
            listeners = new ScriptUnityEvent();
            m_registers.Add(type, listeners);
        }
        listeners.AddListener(func);
    }

    /// <summary>
    /// 移除注册
    /// </summary>
    /// <param name="func">   </param>
    /// <param name="eventType">    </param>
    public void UnregisterEvent( EventType eventType, UnityAction<ScriptEventArgs> func )
    {
        if (m_registers.TryGetValue(eventType, out var listeners))
        {
            listeners.RemoveListener(func);
        }
    }

    private void Update()
    {
        float expires = Time.realtimeSinceStartup + m_timeLimit;
        while (m_eventQueue.Count > 0 && Time.realtimeSinceStartup < expires)
        {
            var args = m_eventQueue.Dequeue();
            if (m_registers.TryGetValue(args.eventType, out var listeners))
            {
                listeners.Invoke(args);
            }
        }
    }

    class ScriptUnityEvent : UnityEvent<ScriptEventArgs> { }
}





