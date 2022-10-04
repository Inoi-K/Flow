using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOperator : MonoBehaviour
{
    [Header("Boost Item")]
    public float boostUpSpeed = 40f;
    public float boostUpDuration = 5f;

    [Header("Reverse Item")]
    public float boostDownSpeed = -30f;
    public float boostDownDuration = 4f;

    PlayerController plr;
    Coroutine boostUp;
    Coroutine boostDown;

    private void Awake() {
        plr = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag) {
            case Tags.boostItem:
                if (plr.isBoosting)
                    try { StopCoroutine(boostUp); } catch { }
                boostUp = StartCoroutine(plr.ChangeSpeed(boostUpSpeed, boostUpDuration));
                break;
            case Tags.reverseItem:
                if (plr.isBoosting)
                    try { StopCoroutine(boostDown); } catch { }
                boostDown = StartCoroutine(plr.ChangeSpeed(boostDownSpeed, boostDownDuration));
                break;
        }
        /*if (other.CompareTag(Tags.boostItem))
        {
            if (plr.isBoosting)
                try { StopCoroutine(boostUp); } catch { }
            boostUp = StartCoroutine(plr.ChangeSpeed(boostUpSpeed, boostUpDuration));
        } else if (other.CompareTag(Tags.reverseItem)) {
            if (plr.isBoosting)
                try { StopCoroutine(boostDown); } catch { }
            boostDown = StartCoroutine(plr.ChangeSpeed(boostDownSpeed, boostDownDuration));
        } */
    }
}
