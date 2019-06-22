using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviourMgr : MonoBehaviour
{
    private Dictionary<ObjectBehaviour, ObjectBehaviour.BehaviourFunc> m_behaviourPool;
    private Dictionary<ObjectBehaviour, ObjectBehaviour.BehaviourFunc> m_prepareAdd;
    private HashSet<ObjectBehaviour> m_prepareDel;
    
    private ObjectBehaviourMgr()
    {
        m_behaviourPool = new Dictionary<ObjectBehaviour, ObjectBehaviour.BehaviourFunc>();
        m_prepareAdd = new Dictionary<ObjectBehaviour, ObjectBehaviour.BehaviourFunc>();
        m_prepareDel = new HashSet<ObjectBehaviour>();
    }
    private void PrepareTurn()
    {
        foreach (var item in m_prepareAdd)
        {
            item.Value.StartHandler.Invoke();
            m_behaviourPool.Add(item.Key, item.Value);
        }
        m_prepareAdd.Clear();
    }
    private void FinishTurn()
    {
        foreach (var e in m_prepareDel)
        {
            m_behaviourPool.Remove(e);
        }
        m_prepareDel.Clear();
    }
    private void FixedUpdate()
    {
        PrepareTurn();  
        foreach (var item in m_behaviourPool)
        {
            item.Value.FixedUpdateHandler.Invoke();
        }
    }
    private void Update()
    {
        foreach (var item in m_behaviourPool)
        {
            item.Value.UpdateHandler.Invoke();
        }
    }
    private void LateUpdate()
    {
        foreach (var item in m_behaviourPool)
        {
            item.Value.LateUpdateHandler.Invoke();
        }
        FinishTurn();
    }
    public void RegisterObjectBehaviour( ObjectBehaviour obj, ObjectBehaviour.BehaviourFunc behaviour )
    {
        // 添加到“待添加”字典中，防止在update过程中添加破坏迭代器
        if (!m_behaviourPool.ContainsKey(obj) && !m_prepareAdd.ContainsKey(obj))
        {
            m_prepareAdd.Add(obj, behaviour);
        }
    }
    public void UnRegisterObjectBehaviour( ObjectBehaviour obj )
    {
        m_prepareDel.Add(obj);
    }
}
