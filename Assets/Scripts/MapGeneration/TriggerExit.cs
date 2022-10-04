using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    public float delay = 5f;

    public delegate void ExitAction();
    public static event ExitAction OnChunkExited;

    bool exited = false;

    private void OnTriggerExit(Collider other) {
        
        if (other.CompareTag(Tags.core)) {
            if (!exited) {
                exited = true;
                OnChunkExited();
                StartCoroutine(WaitAndDestroy());
            }
        }
    }

    IEnumerator WaitAndDestroy() {
        yield return new WaitForSeconds(delay);
        Destroy(transform.root.gameObject);
    }
}
