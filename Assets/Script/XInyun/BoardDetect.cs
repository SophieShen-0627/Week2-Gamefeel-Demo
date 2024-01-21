using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;

public class BoardDetect : MonoBehaviour
{
    public TransitionSettings transition;
    public bool SceneComplete = false;
    private int SceneNum;
    public float LoadDelay = .7f;
     private AudioSource source;
    [SerializeField] AudioClip Fail;

    private void Start()
    {
        SceneComplete = false;
        source = GetComponent<AudioSource>();
        SceneNum = FindObjectOfType<Reset>().SceneNum;
    }

    private void Update()
    {
        if (SceneComplete)
        {
            loadScene(SceneNum++);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<StonePhysics>())
        {
            collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            source.PlayOneShot(Fail);

            loadScene(SceneNum);
        }
    }

    private void loadScene(int sceneNum)
    {
        TransitionManager.Instance().Transition(sceneNum, transition, LoadDelay);
    }
}
