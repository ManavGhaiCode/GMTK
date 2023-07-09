using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSwitcher : MonoBehaviour {
    void Start() {
        StartCoroutine(SwithScene());
    }

    IEnumerator SwithScene() {
        yield return new WaitForSeconds (3.2f);
        SceneManager.LoadScene("Game");
    }
}
