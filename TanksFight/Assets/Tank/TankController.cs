using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TankController : NetworkBehaviour
{
    [SerializeField]
    private Transform _firepoint;
    [SerializeField]
    private Rigidbody _bullet;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _rotSpeed;
    private Transform _tower;


    private void  Awake()
    {
        _tower = gameObject.transform.Find("Tower");

    }
    private void Start()
    {
        if(isLocalPlayer)
        {
            foreach (var r in GetComponentsInChildren<Renderer>())
                r.material.color = Color.green;
        }
        else
        {
            foreach (var r in GetComponentsInChildren<Renderer>())
                r.material.color = Color.red;
        }
    }
  
    void Update()
    {
        if (!isLocalPlayer) return;
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * _rotSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;
        var y = Input.GetAxis("TowerHorizontal")*Time.deltaTime* _rotSpeed;
        transform.Rotate(0, x, 0);
        transform.Translate(z, 0, 0);
        _tower.Rotate(0, 0, y);
        if (Input.GetButtonDown("Fire1"))

        {
            CmdFire();
        }
    }
   
    [Command]

    private void CmdFire()
    {
        var bullet = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);
        bullet.velocity = bullet.transform.forward * 5f;

        NetworkServer.Spawn(bullet.gameObject);
    }
}
