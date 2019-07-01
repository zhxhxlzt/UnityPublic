using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Timer : ObjectBehaviour
{
    private LinkedList<TimerNode> m_TimerNodeList;
    private LinkedListNode<TimerNode> m_Last;
    //private static Timer m_GlobalTimer;
  
    /// <summary>
    /// 添加定时器
    /// </summary>
    /// <param name="delay"> 调用延迟时间, 0为下帧调用</param>
    /// <param name="cbFunc"> 回调函数</param>
    /// <param name="repeat"> 重复次数</param>
    /// <param name="interval"> 重复调用间隔</param>
    /// <returns></returns>
    public static TimerNode AddTimer( float delay, Action cbFunc, int repeat = 0, float interval = 0 )
    {
        var timer = Singleton.GetInstance<Timer>();
        var node = new TimerNode(delay, cbFunc, repeat, interval);
        timer.m_TimerNodeList.AddLast(node);
        return node;
    }

    /// <summary>
    /// 添加TimerNode到本地创建的定时器
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="func"></param>
    /// <param name="repeat"></param>
    /// <param name="interval"></param>
    /// <returns></returns>
    public TimerNode AddLocalTimer( float delay, Action func, int repeat = 0, float interval = 0)
    {
        var node = new TimerNode(delay, func, repeat, interval);
        m_TimerNodeList.AddLast(node);
        return node;
    }

    public float TimeUsage { get; private set; }

    public Timer()
    {
        m_TimerNodeList = new LinkedList<TimerNode>();
        m_Last = new LinkedListNode<TimerNode>(null);
    }

    protected override void FixedUpdate()
    {
        var begin = Time.realtimeSinceStartup;
        m_TimerNodeList.AddLast(m_Last);    // 统一边界操作
        for (var it = m_TimerNodeList.First; it != m_TimerNodeList.Last;)
        {
            var timerNode = it.Value;

            if (timerNode.IsDiscard)
            {
                it.Value = it.Next.Value;
                m_TimerNodeList.Remove(it.Next);
                continue;   // 移除动作执行了 it = it.Next 操作
            }

            if (timerNode.IsExpired)
            {
                timerNode.Invoke();
                if (timerNode.IsRepeat)
                {
                    timerNode.Rebuild();
                }
                else
                {
                    it.Value = it.Next.Value;
                    m_TimerNodeList.Remove(it.Next);
                    continue;   // 移除动作执行了 it = it.Next 操作
                }
            }
            it = it.Next;   // 访问下一个
        }
        m_TimerNodeList.RemoveLast();   // 移除冗余节点
        TimeUsage = Time.realtimeSinceStartup - begin;
    }

    public class TimerNode
    {
        private float m_Expire;

        private Action m_Func;
        private int m_Repeat;
        private float m_Interval;

        public bool IsExpired { get { return Time.time >= m_Expire; } }
        public bool IsRepeat { get { return (m_Repeat <= -1 || m_Repeat > 0); } }
        public bool IsDiscard { get; private set; }

        public TimerNode( float delay, Action cbFunc, int repeat, float interval )
        {
            delay = Mathf.Max(delay, Time.fixedDeltaTime);
            m_Expire = Time.time + delay;
            m_Func = cbFunc;
            m_Repeat = repeat;
            m_Interval = interval;
        }
        
        public void Invoke()
        {
            m_Func?.Invoke();
            if (m_Repeat == 0)
            {
                IsDiscard = true;
            }
            else if (m_Repeat > 0)
            {
                m_Repeat -= 1;
            }
        }

        /// <summary>
        /// 重建定时器节点内部计时
        /// </summary>
        public void Rebuild()
        {
            m_Expire = Time.time + Mathf.Max(m_Interval, Time.fixedDeltaTime);
        }

        /// <summary>
        /// 直接废弃，不到时调用
        /// </summary>
        public void Discard()
        {
            IsDiscard = true;
        }
    }
}









