using UnityEngine;
using UnityEngine.AI;

public class PACMAN : MonoBehaviour {
    public float speed = 2f;

    private Transform Target;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private NavMeshAgent agent;

    private bool isResponing = false;
    private float GhostDist = 0f;
    private Vector2 avgGhostPos;


    [SerializeField] private Transform[] Ghosts;
    [SerializeField] private Transform ResponPoint;
    [SerializeField] private int lifes = 4;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

        agent.speed = speed;

        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (!isResponing) {
            avgGhostPos = (Ghosts[0].position + Ghosts[1].position + Ghosts[2].position + Ghosts[3].position) / new Vector2 (4, 4);
            GhostDist = Vector2.Distance(transform.position, avgGhostPos);

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
        } else {
            Target = ResponPoint;
            agent.SetDestination(ResponPoint.position);

            if (Vector2.Distance(transform.position, ResponPoint.position) < .05f) {
                Invoke("SetIsResponingf", .2f);
            }
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
    void SetIsResponingf() {
        isResponing = false;

        sprite.color = new Color (255, 239, 85);
    }

    public Vector2 GetTarget() {
        return Target.position;
    }


    public void kill() {
        if (!isResponing) {
            if (lifes > 0) {
                lifes -= 1;
                agent.SetDestination(new Vector2 (0, 3.75f));

                speed += .1f;
                isResponing = true;
                agent.speed = speed;

                sprite.color = new Color (0, 0, 0);
                return;
            }

            Destroy(gameObject);
        }
    }
};