using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private int _damage=10;
	void Start ()
    {
        Destroy(gameObject, 3f);
	}

    private void OnCollisionEnter(Collision collision)
    {
        var ph = collision.collider.GetComponent<PlayerHealth>();
        if (ph) ph.GetDamage(_damage);
        Destroy(gameObject);
    }
}
