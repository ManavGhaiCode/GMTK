using UnityEngine;
using UnityEngine.AI;

public class PACMAN : MonoBehaviour {
    public float speed = 2f;

    private Transform Target;
    private Rigidbody2D rb;

    private NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

        agent.speed = speed;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Pellet[] pa = GameObject.FindObjectsOfType<Pellet>();
        GameObject[] pellets = new GameObject[pa.Length];

        int i = 0;

        foreach (Pellet pellet in pa) {
            pellets.SetValue(pellet.gameObject, i);
            i++;
        }

        float lowestDist = Mathf.Infinity;
        GameObject LocalTarget = null;

        foreach (GameObject pellet in pellets) {
            float dist = Vector2.Distance(transform.position, pellet.transform.position);

            if (dist < lowestDist) {
                lowestDist = dist;
                LocalTarget = pellet;
            }
        }

        if (LocalTarget != Target) {
        Target = LocalTarget.transform;
        }
    }

    private void FixedUpdate() {
        rb.velocity = Vector2.zero;

        if (Target != null) {
            agent.SetDestination(Target.position);
        }
    }
};