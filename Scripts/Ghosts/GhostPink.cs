using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GhostPink : MonoBehaviour {
    public float speed = 2f;
    public float startDelay = 10f;
    public Transform OGPos;

    private NavMeshAgent agent;
    private float TimeToStart;
    private float TimeToStop;
    private PACMAN pacmanAI;

    private void Awake() {
        TimeToStart = Time.time + startDelay;
        TimeToStop = TimeToStart + 30f;
    }

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

        agent.speed = speed;

        pacmanAI = GameObject.FindObjectOfType<PACMAN>();
    }

    private void FixedUpdate() {
        if (TimeToStop < Time.time) {
            agent.SetDestination(OGPos.position);
            TimeToStart = Time.time + startDelay;
        }

        if (Time.time > TimeToStart && pacmanAI.GetTarget() != null) {
            agent.SetDestination(pacmanAI.GetTarget());
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