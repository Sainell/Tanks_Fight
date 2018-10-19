using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField]
    private RectTransform _canvas;
    [SerializeField]
    private Image _fillImage;

    public int MaxHealth = 100;
    [SyncVar (hook ="OnHealthChanged")]
    public int CurrentHealth = 100;

    private void LateUpdate()
    {
        _canvas.LookAt(Camera.main.transform, Vector3.up);
    }
    public void GetDamage(int damage)
    {
        if (!isServer) return;
        if (CurrentHealth <= 0) return;

       OnHealthChanged(CurrentHealth - damage);
        if (CurrentHealth <= 0) Respawn();
    }

    private void OnHealthChanged(int health)
    {
        CurrentHealth = health;
        _fillImage.fillAmount = (float)CurrentHealth / MaxHealth;   
    }
    private void Respawn()
    {
        transform.position = Vector3.up * 2f;
        CurrentHealth = MaxHealth;
    }
}
