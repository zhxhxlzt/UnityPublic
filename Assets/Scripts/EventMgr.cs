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
    private Queue<EventData> m_eventQueue;      // 消息队列
    private Dictionary<EventType, List<Action<EventArgs>>> m_registers; // 事件接收者
    private float m_timeLimit = 0.01f;

    private EventMgr()
    {
        m_eventQueue = new Queue<EventData>();
        m_registers = new Dictionary<EventType, List<Action<EventArgs>>>();
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="args"></param>
    public void SendEvent( EventType type, EventArgs args, EventRcvMode mode = EventRcvMode.Sync)
    {
        if (args == null) args = EventArgs.Empty;
        if (mode == EventRcvMode.Async)
        {
            m_eventQueue.Enqueue(new EventData(type, args));
        }
        else
        {
            if (m_registers.TryGetValue(type, out var registers))
            {
                foreach (var reg in registers)
                {
                    reg.Invoke(args);
                }
            }
        }
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="type"></param>
    /// <param name="func"></param>
    public void RegisterEvent( EventType type, Action<EventArgs> func )
    {
        if (m_registers.TryGetValue(type, out var registers))
        {
            registers.Add(func);
        }
        else
        {
            m_registers.Add(type, new List<Action<EventArgs>>() { func });
        }
    }

    /// <summary>
    /// 移除注册
    /// </summary>
    /// <param name="func">   </param>
    /// <param name="eventType">    </param>
    public void UnregisterEvent( Action<EventArgs> func, EventType eventType )
    {
        if (m_registers.TryGetValue(eventType, out var registers))
        {
            registers.Remove(func);
        }
    }

    private void Update()
    {
        float expires = Time.realtimeSinceStartup + m_timeLimit;
        while (m_eventQueue.Count > 0 && Time.realtimeSinceStartup < expires)
        {
            var unityEvent = m_eventQueue.Dequeue();
            if (m_registers.TryGetValue(unityEvent.type, out var registers))
            {
                foreach (var reg in registers)
                {
                    reg?.Invoke(unityEvent.args);
                }
            }
        }
    }

    /// <summary>
    /// 消息单元，具体消息通过继承EventArgs的类来传递
    /// </summary>
    class EventData
    {
        public EventType type { get; private set; }
        public EventArgs args { get; private set; }
        public EventData( EventType type, EventArgs args )
        {
            this.type = type;
            this.args = args;
        }
    }
}





