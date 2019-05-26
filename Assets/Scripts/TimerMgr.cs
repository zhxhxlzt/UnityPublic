using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TimerMgr : MonoBehaviour
{
    private List<Timer> m_timerList = new List<Timer>();         // 活跃的Timer
    private List<Timer> m_pauseTimerList = new List<Timer>();    // 暂停的Timer

    /// <summary>
    /// 
    /// </summary>
    /// <param name="delay"> 首次调用延迟时间</param>
    /// <param name="func"></param>
    /// <param name="repeat"></param>
    /// <param name="interval"></param>
    /// <returns></returns>
    public Timer AddTimer(float delay, IFunctor func, int repeat=0, float interval=0) {
        Timer timer = new Timer(delay, func, repeat, interval);
        timer.expire = Time.time + delay;
        m_timerList.Add(timer);
        return timer;
    }

    public void RemoveTimer(Timer timer) {
        m_timerList.Remove(timer);
    }

    private void FixedUpdate() {
        // 检查暂停的定时器
        foreach (var timer in m_pauseTimerList.ToArray() ) {
            if( !timer.is_pause ) {
                m_timerList.Add(timer);         // 不再暂停，添加timer到等待恢复列表
            }
            else {
                timer.expire += Time.deltaTime; // 暂停过程中，到期时间延后
            }
        }

        // 检查活跃的定时器
        foreach (var timer in m_timerList.ToArray()) {
            // 定时器被废弃
            if( timer.is_discard ) {
                m_timerList.Remove(timer);
            }
            // 定时器被暂停
            else if( timer.is_pause ) {
                m_timerList.Remove(timer);
                m_pauseTimerList.Add(timer);        // 到暂停列表
            }
            // 符合调用条件
            else if( Time.time >= timer.expire ) {
                timer.Invoke();
                timer.expire = Time.time + timer.interval;  // 更新下次调用时间
            }
        }
    }
    
}


public class Timer {
    IFunctor m_func;
    public float delay { get; private set; }
    public float interval { get; private set; }
    public int repeat { get; private set; }
    public float expire { get; set; }
    public bool is_discard { get; private set; }
    public bool is_pause { get; private set; }
    
    /// <param name="delay"> 第一次调用前延迟时间</param>
    /// <param name="interval"> 每次调用时间间隔</param>
    /// <param name="repeat"> [-1: 无限循环调用] [0:不重复] [n > 0: 重复n次]</param>
    /// <param name="func"></param>
    public Timer( float delay, IFunctor func,int repeat, float interval) {
        this.delay = delay > Time.fixedDeltaTime ? delay : Time.fixedDeltaTime;
        this.m_func = func;
        this.repeat = repeat;
        this.interval = interval > Time.fixedDeltaTime ? interval : Time.fixedDeltaTime;    // 间隔最低不低于固定帧间隔
        is_discard = false;
        is_pause = false;
    }

    /// <summary>
    /// 如果repeat = -1 Invoke() 后不会被discard
    /// 如果repeat =  0 Invoke() 后会被discard
    /// 如果repeat >  0 Invoke() 后repeat-1 直到repeat=0
    /// </summary>
    public void Invoke() {
        if( repeat > -2 ) --repeat;
        if( repeat == -1 ) is_discard = true;
        m_func?.Invoke();
    }

    public void Pause() {
        is_pause = true;
    }

    public void Resume() {
        is_pause = false;
    }

    /// <summary>
    /// 废弃定时器，废弃掉后不再允许重新使用
    /// </summary>
    public void Discard() {
        is_discard = true;
    }
}


public interface IFunctor {
    void Invoke();
}


public class Functor : IFunctor {
    private Action m_func;
    public Functor(Action func ) {
        m_func = func;
    }
    public void Invoke() {
        m_func?.Invoke();
    }
}


public class Functor<T> : IFunctor {
    private Action<T> m_func;
    private T m_argv;

    public Functor(Action<T> func, T argv ) {
        m_func = func;
        m_argv = argv;
    }
    public void Invoke() {
        m_func?.Invoke(m_argv);
    }
}


public class Functor<T0, T1> : IFunctor {
    private Action<T0, T1> m_func;
    private T0 m_arvg0;
    private T1 m_argv1;

    public Functor(Action<T0, T1> func, T0 argv0, T1 argv1 ) {
        m_func = func;
        m_arvg0 = argv0;
        m_argv1 = argv1;
    }
    public void Invoke() {
        m_func?.Invoke(m_arvg0, m_argv1);
    }
}
