using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePhysics : MonoBehaviour
{
    public float i = 10;

    private MouseController m_controller;
    private Rigidbody2D m_rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_controller = GetComponent<MouseController>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_rigidbody.isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (m_controller.Released)
        {
            m_controller.Released = false;
            m_rigidbody.isKinematic = false;
            m_rigidbody.AddForce(m_controller.InitialSpeed * m_controller.InitialDirection, ForceMode2D.Impulse);
        }

    }
}
