using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePhysics : MonoBehaviour
{
    [SerializeField] float BounceScale = 500;
    [SerializeField] float MassScale = 1;
    [SerializeField] ParticleSystem Water;
    [SerializeField] float WaterSpeedScale = 2;

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
            CalculateVerticalVelocity();
            SpawnPartical(collision.ClosestPoint(transform.position));
        }
    }

    private void SpawnPartical(Vector2 spawnPostion)
    {
        ParticleSystem water = Instantiate(Water, spawnPostion, Quaternion.identity);
        water.transform.forward = m_controller.InitialDirection.normalized;
        var main = water.main;
        main.startSpeed = m_rigidbody.velocity.magnitude * WaterSpeedScale;
        water.Play();
    }
    private void CalculateVerticalVelocity()
    {
        //the width of the stone
        float ContactArea = 0;
        float Cd = 0;
        if (transform.right.y >= 0)
        {
            ContactArea = transform.localScale.x * m_controller.InitialDirection.normalized.x;
            Cd = 2;
        }
        else
        {
            ContactArea = transform.localScale.y;
            Cd = 0.1f;
        }

        //F= 0.5 * Cd * rou * A * V^2

        //Vector2 force = BounceScale * Cd * ContactArea * m_rigidbody.velocity.magnitude * m_rigidbody.velocity.magnitude * - m_rigidbody.velocity.normalized;
        Vector2 velocityDir = new Vector2(-m_controller.InitialDirection.y, m_controller.InitialDirection.x).normalized;
        Vector2 force = BounceScale * Cd * ContactArea * m_rigidbody.velocity.magnitude * m_rigidbody.velocity.magnitude * velocityDir.normalized;

        if (force.y >= MassScale * 9.8)
        {
            float ContactTime = 2 * m_rigidbody.velocity.y / (float)(Mathf.Abs(force.y) * MassScale - 9.8);

            float xVelocity = m_rigidbody.velocity.x - MassScale * ContactTime * Mathf.Abs(force.x);
            float yVelocity = -m_rigidbody.velocity.y;

            m_rigidbody.velocity = new Vector2(xVelocity, yVelocity);
        }
        else
        {
            m_rigidbody.AddForce(force * MassScale);
        }

    }
}
