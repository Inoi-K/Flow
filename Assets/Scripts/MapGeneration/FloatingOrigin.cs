using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Camera))]
public class FloatingOrigin : MonoBehaviour {
    public float threshold = 2000f;
    public LevelLayoutGenerator layoutGenerator;

    void LateUpdate() {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = 0f;

        if (cameraPosition.magnitude > threshold) {
            GameManager.gm.UpdateBaseScore();

            for (int z = 0; z < SceneManager.sceneCount; z++) {
                foreach (GameObject g in SceneManager.GetSceneAt(z).GetRootGameObjects()) {
                    g.transform.position -= cameraPosition;
                }
            }

            Vector3 originDelta = Vector3.zero - cameraPosition;
            layoutGenerator.UpdateSpawnOrigin(originDelta);
        }
    }
}


