using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectBehaviour : IDisposable
{
    private BehaviourFunc m_behaviour;
    private static ObjectBehaviourMgr m_mgr;
    private bool m_enable;
    protected ObjectBehaviour()
    {
        m_enable = true;
        OnEnable();
        Awake();
        m_behaviour = new BehaviourFunc()
        {
            obj = this,
            StartHandler = Start,
            FixedUpdateHandler = FixedUpdate,
            UpdateHandler = Update,
            LateUpdateHandler = LateUpdate
        };
        if (m_mgr == null) m_mgr = Singleton.GetInstance<ObjectBehaviourMgr>();
        m_mgr.RegisterObjectBehaviour(this, m_behaviour);
    }
    protected virtual void Awake() { }
    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }
    protected virtual void Start() { }
    protected virtual void FixedUpdate() { }
    protected virtual void Update() { }
    protected virtual void LateUpdate() { }
    protected virtual void OnDestroy() { }
    public void Dispose()
    {
        OnDisable();
        OnDestroy();
        m_mgr.UnRegisterObjectBehaviour(this);
    }
    public bool Enabled
    {
        get { return m_enable; }
        set
        {
            if (m_enable != value)
            {
                m_enable = value;
                if (m_enable)
                {
                    OnEnable();
                    m_behaviour.FixedUpdateHandler = FixedUpdate;
                    m_behaviour.UpdateHandler = Update;
                    m_behaviour.LateUpdateHandler = LateUpdate;
                }
                else
                {
                    OnDisable();
                    m_behaviour.FixedUpdateHandler = () => { };     // 替换Update委托更新达到Disable效果
                    m_behaviour.UpdateHandler = () => { };
                    m_behaviour.LateUpdateHandler = () => { };
                }
            }
        }
    }
}

public class BehaviourFunc
{
    public ObjectBehaviour obj;
    public Action StartHandler;
    public Action FixedUpdateHandler;
    public Action UpdateHandler;
    public Action LateUpdateHandler;
}
