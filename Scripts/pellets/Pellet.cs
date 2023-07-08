using UnityEngine;

public class Pellet : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D hitInfo) {
        PACMAN pacman = hitInfo.GetComponent<PACMAN>();

        if (pacman != null) {
            Destroy(gameObject);
        }
    }
}