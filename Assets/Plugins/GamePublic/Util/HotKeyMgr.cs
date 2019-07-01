using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HotKeyMgr : ObjectBehaviour
{
    private static Dictionary<KeyCode, UnityEvent> keyDownPool = new Dictionary<KeyCode, UnityEvent>();
    private static Dictionary<KeyCode, UnityEvent> keyUpPool = new Dictionary<KeyCode, UnityEvent>();
    private static Dictionary<KeyCode, UnityEvent> KeyPool = new Dictionary<KeyCode, UnityEvent>();

    private static Dictionary<KeyCode, UnityEvent> fixedKeyDownPool = new Dictionary<KeyCode, UnityEvent>();
    private static Dictionary<KeyCode, UnityEvent> fixedKeyUpPool = new Dictionary<KeyCode, UnityEvent>();
    private static Dictionary<KeyCode, UnityEvent> fixedKeyPool = new Dictionary<KeyCode, UnityEvent>();

    private static HotKeyMgr hotKeyMgr;
    private HotKeyMgr() { }
    public enum HotKeyMode { GetKeyDown, GetKeyUp, GetKey }
    public enum UpdateMode { Update, FixedUpdate }
    
    public static void RegisterHotKey(KeyCode keyCode, UnityAction func, HotKeyMode keyMode = HotKeyMode.GetKeyDown, UpdateMode updateMode = UpdateMode.Update)
    {
        if (hotKeyMgr == null) hotKeyMgr = Singleton.GetInstance<HotKeyMgr>();
        var pool = GetTargetPool(keyMode, updateMode);
        if (!pool.TryGetValue(keyCode, out var listeners))
        {
            listeners = new UnityEvent();
            pool.Add(keyCode, listeners);
        }
        listeners.AddListener(func);
    }

    public static void UnregisterHotKey(KeyCode keyCode, UnityAction func, HotKeyMode keyMode = HotKeyMode.GetKeyDown, UpdateMode updateMode = UpdateMode.Update )
    {
        var pool = GetTargetPool(keyMode, updateMode);
        if (pool.TryGetValue(keyCode, out var listeners))
        {
            listeners.RemoveListener(func);
        }
    }

    private static Dictionary<KeyCode, UnityEvent> GetTargetPool(HotKeyMode keymode, UpdateMode updateMode)
    {
        switch (updateMode)
        {
            case UpdateMode.Update:
                switch (keymode)
                {
                    case HotKeyMode.GetKeyDown:
                        return keyDownPool;
                    case HotKeyMode.GetKeyUp:
                        return keyUpPool;
                    case HotKeyMode.GetKey:
                        return KeyPool;
                    default:
                        return new Dictionary<KeyCode, UnityEvent>();
                }
            case UpdateMode.FixedUpdate:
                switch (keymode)
                {
                    case HotKeyMode.GetKeyDown:
                        return fixedKeyDownPool;
                    case HotKeyMode.GetKeyUp:
                        return fixedKeyUpPool;
                    case HotKeyMode.GetKey:
                        return fixedKeyPool;
                    default:
                        return new Dictionary<KeyCode, UnityEvent>();
                }
            default:
                return new Dictionary<KeyCode, UnityEvent>();
        }
        
    }

    protected override void FixedUpdate()
    {
        foreach (var pair in fixedKeyDownPool)
        {
            if (Input.GetKeyDown(pair.Key)) pair.Value.Invoke();
        }
        foreach (var pair in fixedKeyUpPool)
        {
            if (Input.GetKeyUp(pair.Key)) pair.Value.Invoke();
        }
        foreach (var pair in fixedKeyPool)
        {
            if (Input.GetKey(pair.Key)) pair.Value.Invoke();
        }
    }

    protected override void Update()
    {
        foreach (var pair in keyDownPool)
        {
            if (Input.GetKeyDown(pair.Key)) pair.Value.Invoke();
        }
        foreach (var pair in keyUpPool)
        {
            if (Input.GetKeyUp(pair.Key)) pair.Value.Invoke();
        }
        foreach (var pair in KeyPool)
        {
            if (Input.GetKey(pair.Key)) pair.Value.Invoke();
        }
    }
}
