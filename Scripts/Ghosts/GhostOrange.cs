using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GhostOrange : MonoBehaviour {
    public float speed = 2f;
    public float startDelay = 20f;
    public Transform OGPos;

    private NavMeshAgent agent;
    private float TimeToStart;
    private float TimeToStop;
    private Transform pacman;

    private void Awake() {
        TimeToStart = Time.time + startDelay;
        TimeToStop = TimeToStart + 30f;
    }

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

        agent.speed = speed;

        pacman = GameObject.FindObjectOfType<PACMAN>().gameObject.transform;
    }

    private void FixedUpdate() {
        if (TimeToStop < Time.time) {
            agent.SetDestination(OGPos.position);
            TimeToStart = Time.time + startDelay;

            StartCoroutine(SetTimeToStop());
        }


        if (Time.time > TimeToStart && pacman != null) {
            agent.SetDestination(pacman.position);
        }
    }


    private void OnTriggerEnter2D(Collider2D hitInfo) {
        PACMAN _pacman = hitInfo.GetComponent<PACMAN>();

        if (_pacman != null) {
            _pacman.kill();
        }
    }

    private IEnumerator SetTimeToStop() {
        yield return new WaitForSeconds (30f);
        TimeToStop = Time.time;
    }
}