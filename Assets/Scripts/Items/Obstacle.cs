using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum DirectionsToMove
    {
        left,
        right,
        forward,
        backward,
        up,
        statical
    }
    public DirectionsToMove direction = DirectionsToMove.statical;
    public float moveSpeed = 10f;
    public float duration = 5f;
    public ParticleSystem explosion;

    float timePassed = 0f;
    int sgn = 1;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (direction != DirectionsToMove.statical)
        {
            Vector3 dir = Vector3.zero;
            switch (direction)
            {
                case DirectionsToMove.left:
                    dir = Vector3.left;
                    break;
                case DirectionsToMove.right:
                    dir = Vector3.right;
                    break;
                case DirectionsToMove.forward:
                    dir = Vector3.forward;
                    break;
                case DirectionsToMove.backward:
                    dir = Vector3.back;
                    break;
                case DirectionsToMove.up:
                    dir = Vector3.up;
                    break;
            }
            if (timePassed >= duration) {
                sgn *= -1;
                timePassed = 0f;
            }
            timePassed += Time.deltaTime;
            rb.MovePosition(transform.position + sgn * dir * moveSpeed * Time.deltaTime);
        }
    }

    public void Explosion() {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
