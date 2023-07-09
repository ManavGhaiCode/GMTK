using UnityEngine;
using UnityEngine.AI;

public class PACMAN : MonoBehaviour {
    public float speed = 2f;

    private Transform Target;
    private Rigidbody2D rb;
    private NavMeshAgent agent;

    private float GhostDist = 0f;

    [SerializeField] private Transform[] Ghosts;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

        agent.speed = speed;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        GhostDist = Vector2.Distance(transform.position, Ghosts[0].position);

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
            float ghostDist = Vector2.Distance(Ghosts[0].position, pellet.transform.position);

            if (dist < lowestDist) {
                if (!(dist < .18f)) {
                    if (ghostDist >= GhostDist - .2f) {
                        lowestDist = dist;
                        LocalTarget = pellet;
                    }
                }
            }
        }

        if (LocalTarget != Target) {
            Target = LocalTarget.transform;
        }
    }

    private void FixedUpdate() {
        if (Target != null) {
            Vector2 LookDir = (Vector2)Target.position - rb.position;

            float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;

            rb.velocity = Vector2.zero;

            agent.SetDestination(Target.position);
        }
    }

    public Vector2 GetTarget() {
        return Target.position;
    }

    public void kill() {
        Destroy(gameObject);
    }
};