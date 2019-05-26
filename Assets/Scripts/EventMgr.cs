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
public class EventMgr : MonoBehaviour {
    // 消息队列
    private Queue<UnityEvent> m_eventQueue = new Queue<UnityEvent>();
    // 事件接收者
    private Dictionary<EventType, List<Action<EventArgs>>> m_registers = new Dictionary<EventType, List<Action<EventArgs>>>();

    private void Start() {
        Singleton.GetComInstance<TimerMgr>().AddTimer(0, new Functor(HandleEvent), -1, 0);
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="args"></param>
    public void SendEvent( EventType type, EventArgs args ) {
        m_eventQueue.Enqueue(new UnityEvent(type, args));
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="type"></param>
    /// <param name="func"></param>
    public void RegisterEvent( EventType type, Action<EventArgs> func ) {
        if( m_registers.TryGetValue(type, out var registers) ) {
            registers.Add(func);
        }
        else {
            m_registers.Add(type, new List<Action<EventArgs>>() { func });
        }
    }

    /// <summary>
    /// 移除注册
    /// </summary>
    /// <param name="func"></param>
    /// <param name="eventType"></param>
    public void UnregisterEvent( Action<EventArgs> func, EventType? eventType = null ) {
        // 从所有注册的委托中移除func
        if( eventType == null ) {
            foreach( var registers in m_registers.Values ) {
                registers.Remove(func);
            }
        }
        else if( m_registers.TryGetValue(eventType.Value, out var registers) ) {
            registers.Remove(func);
        }
    }

    private void HandleEvent() {
        while( m_eventQueue.Count > 0 ) {
            var unityEvent = m_eventQueue.Dequeue();
            if( m_registers.TryGetValue(unityEvent.type, out var registers) ) {
                foreach( var reg in registers ) {
                    reg?.Invoke(unityEvent.args);
                }
            }
        }
    }
}

/// <summary>
/// 消息单元，具体消息通过继承EventArgs的类来传递
/// </summary>
public class UnityEvent {
    public EventType type { get; private set; }
    public EventArgs args { get; private set; }
    public UnityEvent( EventType type, EventArgs args ) {
        this.type = type;
        this.args = args;
    }
}


/// <summary>
/// 消息类型枚举
/// </summary>
public enum EventType {
    None,
    Event_One,      // 事件种类
    Event_Two,
}
