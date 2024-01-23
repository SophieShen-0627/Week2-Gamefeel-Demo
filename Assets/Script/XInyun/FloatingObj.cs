using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObj : MonoBehaviour
{
    [SerializeField] private Transform WaterSurface;
    [SerializeField] private float WaterBuoyant = 1;

    private Rigidbody2D rb;
    private BoardDetect board;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        board = FindObjectOfType<BoardDetect>();
    }

    // Update is called once per frame
    void Update()
    {
        Floating();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<StonePhysics>())
        {
            collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            board.RestartGame();
        }
    }

    private void Floating()
    {
        if (transform.position.y <= WaterSurface.position.y)
        {
            rb.AddForce(WaterBuoyant * (WaterSurface.position.y - transform.position.y) * Vector2.up, ForceMode2D.Force);
        }
    }
}
