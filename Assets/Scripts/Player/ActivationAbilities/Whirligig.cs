using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirligigActivation : MonoBehaviour
{
    Whirligig Whirligig;
    Player _player;
    float recoveryTime = 0.6f;

    void Start()
    {
        Whirligig = new Whirligig();
        _player = GameObject.Find("Player").GetComponent<Player>();
        transform.localScale = new Vector3(Whirligig.Radius, 0.01f, Whirligig.Radius);

        StartCoroutine(DestroyCoroutine());
    }

    private void Update()
    {
        transform.position = _player.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(Whirligig.DamageCount);
                enemy.ApplyImpulseAndRecover(Whirligig.PushForce, recoveryTime, transform.position);
            }
        }
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(Whirligig.Duration);
        Destroy(gameObject);
    }
}

