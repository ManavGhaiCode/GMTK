using UnityEngine;

public class Pellet : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}