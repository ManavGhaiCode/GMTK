using UnityEngine;
using UnityEngine.AI;

public class GhostBlue : MonoBehaviour {
    public float speed = 2f;
    public float startDelay = 30f;

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
            agent.SetDestination(new Vector2 (pacman.position.x + Random.Range(-2, 2), pacman.position.y + Random.Range(-2, 2)));
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        PACMAN _pacman = hitInfo.GetComponent<PACMAN>();

        if (_pacman != null) {
            _pacman.kill();
        }
    }
}