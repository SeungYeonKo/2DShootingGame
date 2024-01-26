using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private float CurrentTime = 0f;

    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //Debug.Log(enemies.Length);
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            enemy.Death();
            enemy.MakeItem();
        }
    }
    private void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime >= 3f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Enemy enemy = otherCollider.gameObject.GetComponent<Enemy>();
        if ( enemy)
        {
            enemy.Death();
            enemy.MakeItem();
        }
    }
}
