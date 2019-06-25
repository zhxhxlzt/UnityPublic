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
public class EventMgr : ObjectBehaviour
{
    public enum EventRcvMode { Sync, Async }    // 同步调用，异步调用
    private Queue<ScriptEventArgs> m_eventQueue;      // 消息队列
    private Dictionary<string, ScriptUnityEvent> m_registers; // 事件接收者
    private float m_timeLimit = 0.005f;

    private EventMgr()
    {
        m_eventQueue = new Queue<ScriptEventArgs>();
        m_registers = new Dictionary<string, ScriptUnityEvent>();
    }

    public static EventMgr GetInstance() => Singleton.GetInstance<EventMgr>();

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="args"></param>
    public void SendEvent( string eventName, ScriptEventArgs args, EventRcvMode mode = EventRcvMode.Sync)
    {
        if (args == null) args = ScriptEventArgs.Empty;
        args.EventName = eventName;
        if (mode == EventRcvMode.Async)
        {
            m_eventQueue.Enqueue(args);
        }
        else
        {
            if (m_registers.TryGetValue(eventName, out var registers))
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
    public void RegisterEvent( string eventName, UnityAction<ScriptEventArgs> func )
    {
        if (!m_registers.TryGetValue(eventName, out var listeners))
        {
            listeners = new ScriptUnityEvent();
            m_registers.Add(eventName, listeners);
        }
        listeners.AddListener(func);
    }

    /// <summary>
    /// 移除注册
    /// </summary>
    /// <param name="func">   </param>
    /// <param name="eventType">    </param>
    public void UnregisterEvent( string eventName, UnityAction<ScriptEventArgs> func )
    {
        if (m_registers.TryGetValue(eventName, out var listeners))
        {
            listeners.RemoveListener(func);
        }
    }

    protected override void Update()
    {
        float expires = Time.realtimeSinceStartup + m_timeLimit;
        while (m_eventQueue.Count > 0 && Time.realtimeSinceStartup < expires)
        {
            var args = m_eventQueue.Dequeue();
            if (m_registers.TryGetValue(args.EventName, out var listeners))
            {
                listeners.Invoke(args);
            }
        }
    }

    class ScriptUnityEvent : UnityEvent<ScriptEventArgs> { }
}





