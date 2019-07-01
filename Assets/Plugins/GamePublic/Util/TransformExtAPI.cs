using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtAPI
{
    /// <summary>
    /// 此对象用于暂存一个对象及其下所有子节点的transform
    /// </summary>
    private static Dictionary<Transform, Dictionary<string, Transform>> transformLib = new Dictionary<Transform, Dictionary<string, Transform>>();

    /// <summary>
    /// 使用唯一命名查找子节点的transform
    /// </summary>
    /// <param name="root"> 根节点transform    </param>
    /// <param name="name"> 目标子节点名称     </param>
    /// <returns></returns>
    public static Transform FindDownNode(this Transform root, string name)
    {
        if (!transformLib.ContainsKey(root))
        {
            Queue<Transform> trans = new Queue<Transform>();
            Dictionary<string, Transform> dict = new Dictionary<string, Transform>();
            trans.Enqueue(root);
            while (trans.Count != 0)    // 遍历节点树，将所有节点加入dict中
            {
                var curRoot = trans.Dequeue();
                if (dict.ContainsKey(curRoot.name))
                {
                    Debug.LogErrorFormat("在{0}下找到同名节点:{1},请唯一命名避免查找错误", root.name, curRoot.name);
                }
                else
                {
                    dict.Add(curRoot.name, curRoot);
                }
                for (int i = 0; i < curRoot.childCount; i++)
                {
                    trans.Enqueue(curRoot.GetChild(i));
                }
            }
            transformLib.Add(root, dict);   
            Timer.AddTimer(0, () => transformLib.Remove(root));     // 在当前帧完成查找后，在下一帧删除
        }
        // 在字典中查找节点
        if (transformLib[root].TryGetValue(name, out var target))
        {
            return target;
        }
        return null;
    }
}
