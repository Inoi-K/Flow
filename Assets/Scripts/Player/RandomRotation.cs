using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public float period = 1f;

    float timeLeft;
    Quaternion targetRotation;
    float delta = 90f;
    float t = 0f;

    private void Awake() {
        timeLeft = period;
        targetRotation = Quaternion.Euler(Random.Range(0, 2) * Random.Range(0f, delta), Random.Range(0, 2) * Random.Range(0f, delta), Random.Range(0, 2) * Random.Range(0f, delta));
    }

    private void Update() {
        if (timeLeft <= Time.deltaTime) {
            transform.rotation = targetRotation;
            timeLeft = period;
            //targetRotation = Quaternion.Euler(Random.Range(0, 2) * Random.Range(0f, delta), Random.Range(0, 2) * Random.Range(0f, delta), Random.Range(0, 2) * Random.Range(0f, delta));
            targetRotation = Quaternion.Euler(Random.value * t, Random.value * t, Random.value * t);
            t %= 360f;
        } else {
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime / timeLeft);
            ++t;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime / timeLeft);
            timeLeft -= Time.deltaTime;
        }
    }
}
