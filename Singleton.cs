using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例管理类，如果想使用某类唯一的单例，则通过此类提供的方法获取实例
/// </summary>
public class Singleton : MonoBehaviour
{
    private static Singleton m_instance;
    private static Dictionary<string, object> m_SingletonClass = new Dictionary<string, object>();

    /// <summary>
    /// 此类唯一存在，如有多创建的，销毁新创建的
    /// </summary>
    private void Awake() {
        if (m_instance == null ) {
            m_instance = this;
        }
        else {
            Destroy(this);
        }
    }

    /// <summary>
    /// 获取继承MonoBehaviour的组件的单例
    /// </summary>
    /// <typeparam name="T"> MonoBehaviour</typeparam>
    /// <returns></returns>
    public static T GetComInstance<T>() where T : MonoBehaviour {
        var com = m_instance.GetComponent<T>();
        if (com == null ) {
            com = m_instance.gameObject.AddComponent<T>();
        }
        return com;
    }
    
    /// <summary>
    /// 获取普通类的单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetClsInstance<T>() where T : class, new() {
        string key = typeof(T).Name;
        if( m_SingletonClass.TryGetValue(key, out object ins )) {
            return ins as T;
        }
        var t = new T();
        m_SingletonClass.Add(key, t);
        return t;
    }
    
    /// <summary>
    /// 销毁单例管理类时，销毁它管理的所有单例类实例
    /// </summary>
    public void Dispose() {
        Destroy(gameObject);
    }
}
