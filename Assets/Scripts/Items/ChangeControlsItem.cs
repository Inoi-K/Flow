using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeControlsItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Tags.player))
            other.GetComponent<PlayerController>().ChangeControls();
    }
}
