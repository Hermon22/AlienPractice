using System;
using System.Collections;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 10f;
    public int baseDamage = 1;

    private bool _canMove = false;

    private void Update()
    {
        if(_canMove) 
            transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyBullet());
        _canMove = true;
    }

    private void OnDisable()
    {
        _canMove = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var enemy = other.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
