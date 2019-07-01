using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 为ObjectBehaviour提供“生命周期”
/// </summary>
public class ObjectBehaviourMgr : MonoBehaviour
{
    private Dictionary<ObjectBehaviour, BehaviourFunc> m_behaviourPool;
    private Stack<BehaviourFunc> m_prepareAdd;
    private Stack<ObjectBehaviour> m_prepareDel;
    
    private ObjectBehaviourMgr()
    {
        m_behaviourPool = new Dictionary<ObjectBehaviour, BehaviourFunc>();
        m_prepareAdd = new Stack<BehaviourFunc>();
        m_prepareDel = new Stack<ObjectBehaviour>();
    }
    private void PrepareTurn()
    {
        while( m_prepareAdd.Count != 0)
        {
            var p = m_prepareAdd.Pop();
            p.StartHandler.Invoke();
            m_behaviourPool.Add(p.obj, p);
        }
    }
    private void FinishTurn()
    {
        while (m_prepareDel.Count != 0)
        {
            var p = m_prepareDel.Pop();
            m_behaviourPool.Remove(p);
        }
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
    public void RegisterObjectBehaviour( ObjectBehaviour obj, BehaviourFunc behaviour )
    {
        // 添加到“待添加”字典中，防止在update过程中添加破坏迭代器
        if (!m_behaviourPool.ContainsKey(obj) && !m_prepareAdd.Contains(behaviour))
        {
            m_prepareAdd.Push(behaviour);
        }
    }
    public void UnRegisterObjectBehaviour( ObjectBehaviour obj )
    {
        if (!m_prepareDel.Contains(obj)) m_prepareDel.Push(obj);
    }
}
