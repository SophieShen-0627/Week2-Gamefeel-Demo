using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLine : MonoBehaviour
{
    [SerializeField] float LineLengthScale = 0.1f;
    [SerializeField] float LineOffset = 0.2f;

    private MouseController m_controller;
    private LineRenderer lr;
    void Start()
    {
        m_controller = GetComponent<MouseController>();

        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_controller.StartRecordingMousePostion)
        {
            lr.enabled = true;
            renderLine();
        }
        else
            lr.enabled = false;
    }

    private void renderLine()
    {
        Vector2 inputdirection = m_controller.InitialDirection;
        float LineLength = m_controller.InitialSpeed * LineLengthScale;

        Vector2 lineStart = (Vector2)transform.position + inputdirection.normalized * LineOffset;
        Vector2 lineEnds = lineStart + inputdirection.normalized * LineLength;

        lr.SetPosition(0, lineStart);
        lr.SetPosition(1, lineEnds);

        float normalizedSpeed = m_controller.InitialSpeed / m_controller.MaxSpeed;
        Color linecolor = new Color(normalizedSpeed, 0, 1 - normalizedSpeed, 1);
        Color Endcolor = new Color(normalizedSpeed, 0, 1 - normalizedSpeed, .6f);

        lr.startColor = linecolor;
        lr.endColor = Endcolor;
    }
}
