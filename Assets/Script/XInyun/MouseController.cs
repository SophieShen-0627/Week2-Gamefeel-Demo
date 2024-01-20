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

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
                transform.right = Input.mousePosition - mouseInitialPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartRecordingMousePostion = false;
            mouseFinalPostion = Input.mousePosition;
            InitialSpeed = Vector3.Distance(mouseFinalPostion ,mouseInitialPosition) * SpeedParameter;
            InitialDirection = (mouseFinalPostion - mouseInitialPosition).normalized;
            Released = true;
        }
    }

}
