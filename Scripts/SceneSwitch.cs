using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {
    public void SwitchScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
