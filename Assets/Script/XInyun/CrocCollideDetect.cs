using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocCollideDetect : MonoBehaviour
{
    [SerializeField] AudioClip Chew;
    [SerializeField] AudioClip GetPoint;
    [SerializeField] AudioSource source1;
    [SerializeField] AudioSource source2;
    [SerializeField] ParticleSystem GetPoints;

    private BoardDetect board;
    private Animator Corc;

    private void Start()
    {
        Corc = GetComponentInChildren<Animator>();
        board = FindObjectOfType<BoardDetect>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<StonePhysics>())
        {
            GetPoints.Play();

            GetComponent<Collider2D>().enabled = false;
            Destroy(collision.gameObject);
            Corc.SetTrigger("CloseMouse");
            source1.PlayOneShot(Chew);
            source2.PlayOneShot(GetPoint);

            board.SceneComplete = true;
        }
    }
}
