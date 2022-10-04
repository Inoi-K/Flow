using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float forwardSpeed = 500f;
    public AnimationCurve speedCurve;

    enum controls { keyboard, mouse };
    [SerializeField] controls control = controls.keyboard;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public float defaultSpeed;
    [HideInInspector] public bool isBoosting = false;

    [HideInInspector] public bool canMove = true;

    float limitRotation = 15f;
    Vector3 targetRotation;
    bool wallCollision;

    Vector3 direction;
    float prevInputX = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        defaultSpeed = forwardSpeed;
        targetRotation = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (canMove) {
            if (!isBoosting)
                forwardSpeed = speedCurve.Evaluate(Time.realtimeSinceStartup);
            direction = Vector3.forward;
            switch (control) {
                case controls.keyboard:
                    direction.x = targetRotation.y = targetRotation.z = Input.GetAxis("Horizontal");
                    break;
                case controls.mouse:
                    direction.x = targetRotation.y = targetRotation.z = Input.GetAxis("Mouse X");
                    break;
            }

            if (wallCollision && direction.x / prevInputX > 0)
                direction.x = 0;
            else
                wallCollision = false;
            rb.MovePosition(transform.position + direction.normalized * forwardSpeed * Time.deltaTime);
            print(forwardSpeed);
            //targetRotation.x += xStep * sgn;
            targetRotation.z *= -1;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation * limitRotation), 3f * Time.deltaTime);
            /*if (Mathf.Abs(targetRotation.x) >= limitRotation / 30 - xStep / 2)
                sgn *= -1;*/
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Tags.wall)) {
            wallCollision = true;
            prevInputX = direction.x;
        } 
    }

    public IEnumerator ChangeSpeed(float newSpeed, float actionTime)
    {
        isBoosting = true;
        forwardSpeed = newSpeed * speedCurve.Evaluate(Time.realtimeSinceStartup) / speedCurve.keys[speedCurve.length - 1].value;
        yield return new WaitForSeconds(actionTime);
        forwardSpeed = speedCurve.Evaluate(Time.realtimeSinceStartup);
        isBoosting = false;
    }

    public void ChangeControls() {
        if (control == controls.keyboard)
            control = controls.mouse;
        else
            control = controls.keyboard;
    }
}
