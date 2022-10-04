using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 3;
    public ParticleSystem deathEffect;

    [System.Serializable]
    public class Arrow {
        public GameObject Detail;
        public bool IsAlive = true;
        public string AnimName;
    }

    public Arrow[] detailsToDestroy;

    Animator anim;
    GameObject prevObstacle;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.obstacle) && other.gameObject != prevObstacle) {
            prevObstacle = other.gameObject;
            other.GetComponent<Obstacle>().Explosion();
            Destroy(other.gameObject);
            if ((other.transform.position.x <= transform.position.x || !detailsToDestroy[1].IsAlive) && detailsToDestroy[0].IsAlive)
                StartCoroutine(DestoryObject(detailsToDestroy[0], anim.GetCurrentAnimatorStateInfo(0).length));
            else if (detailsToDestroy[1].IsAlive)
                StartCoroutine(DestoryObject(detailsToDestroy[1], anim.GetCurrentAnimatorStateInfo(1).length));
            else if (!detailsToDestroy[0].IsAlive && !detailsToDestroy[1].IsAlive)
                LoseHP();
        }
    }

    IEnumerator DestoryObject(Arrow obj, float waitTime) {
        obj.IsAlive = false;
        anim.SetTrigger(obj.AnimName);
        LoseHP();
        yield return new WaitForSeconds(waitTime);
        Destroy(obj.Detail);
    }

    void LoseHP()
    {
        --health;
        if (health == 0)
            StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        gameObject.GetComponent<PlayerController>().canMove = false;
        Transform core = transform.GetChild(0).transform;
        Instantiate(deathEffect, core.position, core.rotation);
        Destroy(core.gameObject);
        yield return new WaitForSeconds(1f);
        GameManager.gm.GameOver();
    }
}
