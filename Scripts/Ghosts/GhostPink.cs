using UnityEngine;
using UnityEngine.AI;

public class GhostPink : MonoBehaviour {
    public float speed = 2f;
    public float startDelay = 10f;

    private NavMeshAgent agent;
    private float TimeToStart;
    private PACMAN pacmanAI;

    private void Awake() {
        TimeToStart = Time.time + startDelay;
    }

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

        agent.speed = speed;

        pacmanAI = GameObject.FindObjectOfType<PACMAN>();
    }

    private void FixedUpdate() {
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
}