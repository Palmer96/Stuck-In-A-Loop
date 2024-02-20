using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    CharacterController controller;
    CharacterMovement_Basic player;
    Animator anim;
    [SerializeField]
    private float m_currentSpeed = 5;
    [SerializeField]
    private float m_maxDistanceToPlayer = 20;
    [SerializeField]
    private float m_attackDistance = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameplayManager.Instance.player;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        SegmentManager.Instance.segmentGenerated += Respawn;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameplayManager.Instance.gameRunning)
        {
            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance > m_maxDistanceToPlayer)
                {
                    Respawn();
                }
                else
                {
                    MoveToPlayer();
                }

                CorrectHeight();

                if (distance < m_attackDistance)
                {
                    anim.SetTrigger("Attack");
                }
            }
        }
        else
        {
            anim.SetTrigger("Stop");
        }
    }

    void MoveToPlayer()
    {
        transform.LookAt(player.transform);
        controller.Move(transform.forward * Time.deltaTime * m_currentSpeed);
    }

    void CorrectHeight()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = 0;
        transform.position = newPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GameplayManager.Instance.GameComplete(false);
        }
    }

    public void Respawn()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 100)
        {
            controller.enabled = false;
            transform.position = SegmentManager.Instance.m_currentSegment.GetRandomAdjacentSegment();
            controller.enabled = true;
        }
    }
}

