using UnityEngine;
using UnityEngine.AI;

public class GhostOrange : MonoBehaviour {
    public float speed = 2f;
    public float startDelay = 20f;

    private NavMeshAgent agent;
    private float TimeToStart;
    private Transform pacman;

    private void Awake() {
        TimeToStart = Time.time + startDelay;
    }

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

        agent.speed = speed;

        pacman = GameObject.FindObjectOfType<PACMAN>().gameObject.transform;
    }

    private void FixedUpdate() {
        if (Time.time > TimeToStart) {
            agent.SetDestination(pacman.position);
        }
    }
}