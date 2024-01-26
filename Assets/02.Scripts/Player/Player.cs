using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 5;
    public AudioSource TouchSource;
    


    private void OnCollisionEnter2D(Collision2D collision)
    {

        //플레이어와의 충돌 체크
        if (collision.collider.tag == "Enemy")
        {
            TouchSource.Play();
        }
    }
}
