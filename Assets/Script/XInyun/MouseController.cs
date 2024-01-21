using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float InitialSpeed = 0;
    public Vector2 InitialDirection = new Vector2(0,0);
    public bool Released = false;

    [SerializeField] private float SpeedParameter = 1;

    [SerializeField] private bool StartRecordingMousePostion = false;
    [SerializeField] private Vector3 mouseInitialPosition;
    [SerializeField] private Vector3 mouseFinalPostion;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseInitialPosition = Input.mousePosition;
            StartRecordingMousePostion = true;
        }

        if (StartRecordingMousePostion)
        {
            if (Input.mousePosition != mouseInitialPosition)
            {
                Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 Temp = mousePosInWorld - transform.position;
                transform.right = new Vector3(Temp.x, Temp.y, 0).normalized;
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            StartRecordingMousePostion = false;
            mouseFinalPostion = Input.mousePosition;
            InitialSpeed = Vector3.Distance(mouseFinalPostion ,mouseInitialPosition) * SpeedParameter;
            InitialDirection = transform.right.normalized;
            Released = true;
        }
    }

}
