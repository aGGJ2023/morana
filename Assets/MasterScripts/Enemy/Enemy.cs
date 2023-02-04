using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const string ROOT_TAG = "Root";

    //if data isnt set this is default data
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float speed;
    [SerializeField]
    public string targetTag = ROOT_TAG;

    [SerializeField]
    private EnemyData data;

    private GameObject root;


    // Start is called before the first frame update
    void Start()
    {
        // add root tag in editor
        root = GameObject.FindGameObjectWithTag(targetTag);
        SetEnemyValues();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToRoot();
    }

    private void MoveToRoot() 
    {
        transform.position = Vector2.MoveTowards(transform.position, root.transform.position, speed * Time.deltaTime);
    }
    private void SetEnemyValues()
    {
        GetComponent<Health>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        speed = data.speed;
    }


    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.CompareTag(targetTag)) //targetTag.ToString()
        {
            //damage root
            if (collider.GetComponent<Health>() != null)
            {
                collider.GetComponent<Health>().Damage(damage);
            }
        }
    }
}
