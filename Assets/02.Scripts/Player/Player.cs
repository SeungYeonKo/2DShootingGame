using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 5;
    public AudioSource TouchSource;
    


    private void OnCollisionEnter2D(Collision2D collision)
    {

        //�÷��̾���� �浹 üũ
        if (collision.collider.tag == "Enemy")
        {
            TouchSource.Play();
        }
    }
}
