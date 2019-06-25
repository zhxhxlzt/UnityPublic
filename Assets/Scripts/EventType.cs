using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消息类型枚举
/// </summary>
public class EventType
{
    public const string None = "None";
    public const string Event_One = "Event_One";      // 事件种类
    public const string Event_Two = "Event_Two";
    public const string Hero_Move = "Hero_Move";
    public const string Jump = "Jump";

    public const string EventType_EventAction = "EventType_EventAction";    // 事件类_事件行为
}
