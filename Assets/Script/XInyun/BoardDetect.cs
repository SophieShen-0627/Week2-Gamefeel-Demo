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
    [SerializeField] ParticleSystem explode;

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
            loadScene(SceneNum + 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<StonePhysics>())
        {
            collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            RestartGame();
        }
    }

    public void RestartGame()
    {
        explode.Play();
        source.PlayOneShot(Fail);

        loadScene(SceneNum);
    }

    private void loadScene(int sceneNum)
    {
        TransitionManager.Instance().Transition(sceneNum, transition, LoadDelay);
        SceneComplete = false;
    }
}
