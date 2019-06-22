using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestObjectBehaviourNameSpace;

public class TestObjectBehaviour : MonoBehaviour
{
    public Transform box;
    private MoveComponent moveCom;
    // Start is called before the first frame update
    void Start()
    {
        moveCom = new MoveComponent(box);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) { Debug.Log("Killed"); moveCom.Dispose(); moveCom = null; }
        if (Input.GetKeyDown(KeyCode.G)) { System.GC.Collect(); }
        if (Input.GetKeyDown(KeyCode.E)) { moveCom.Enabled = false; }
        if (Input.GetKeyDown(KeyCode.Q)) { moveCom.Enabled = true; }
    }
}


namespace TestObjectBehaviourNameSpace
{
    public class MoveComponent : ObjectBehaviour
    {
        private Transform m_actor;
        public MoveComponent(Transform actor)
        {
            m_actor = actor;
        }

        protected override void FixedUpdate()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            m_actor.position += y * m_actor.forward;
            m_actor.position += x * m_actor.right;
        }
    }
}
