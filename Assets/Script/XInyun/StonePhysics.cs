using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePhysics : MonoBehaviour
{
    [SerializeField] float BounceScale = 500;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Water>())
        {
            Vector2 velocity = m_rigidbody.velocity;
            Vector2 velocityDir = new Vector2(-m_controller.InitialDirection.y, m_controller.InitialDirection.x).normalized;

            m_rigidbody.AddForce(velocityDir * velocity * velocity * BounceScale, ForceMode2D.Impulse);
            Debug.Log(velocityDir * velocity * velocity * BounceScale + "velocity is :  " + velocity);
        }
    }
}
