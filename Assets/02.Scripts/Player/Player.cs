using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int Health = 5;
    public AudioSource TouchSource;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //플레이어와의 충돌 체크
        if (collision.collider.tag == "Enemy")
        {
            TouchSource.Play();
        }
    }

    public int GetHealth()
    {
        return Health;
    }
    public void SetHealth(int health)
    {
        Health = health;
    }
    public void AddHealth(int health)
    {
        Health += health;
        Debug.Log($"체력 : {Health}");
    }
    public void MinusHealth(int health)
    { 
        Health -= health;

        if(Health <= 0)
        {
            gameObject.SetActive(false);
        }

        Debug.Log($"현재 체력 : {Health}");
        }
}
