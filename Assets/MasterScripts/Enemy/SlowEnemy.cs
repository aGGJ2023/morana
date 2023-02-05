using MasterScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Timeline;

public class SlowEnemy : MonoBehaviour
{
    [SerializeField]
    private string TARGET_TAG = "Tile";
    [SerializeField]
    private string PLAYER_TAG = "Player";
    [SerializeField]
    private string SLOW_ENEMY_TAG = "SlowEnemy";
    [SerializeField]
    private string SEED_TAG = "Seed";

    //if data isnt set this is default data
    [SerializeField]
    public int hp = 100;

    [SerializeField]
    private int damage = 15;
    [SerializeField]
    private float speed = 1.5f;
    [SerializeField]
    private EnemyData data;

    private bool isColliding = false;
    private GameObject target;

    [SerializeField]
    private float attackTimeDelay = 2f;
    private float lastTimeAttacked;

    // Start is called before the first frame update
    void Start()
    {
        target = FindTarget();
        SetEnemyValues();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isColliding)
            MoveToTarget();
    }

    private void MoveToTarget() 
    {
        if (target == null)
            target = FindTarget();
        else if (target.IsDestroyed())
            target = FindTarget();

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    private void SetEnemyValues()   
    {
        GetComponent<Health>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        speed = data.speed;
    }

    private GameObject FindTarget()
    {
        var marks = GameObject.FindGameObjectsWithTag(TARGET_TAG);
        if (marks.Length == 0)
        {
            return GameObject.FindGameObjectWithTag(SEED_TAG);
        }
        var nearest = marks[0];
        var distance = Vector2.Distance(transform.position, nearest.transform.position);
        foreach (var mark in marks)
        {
            var currDistance = Vector2.Distance(transform.position, mark.transform.position);
            if (currDistance < distance && EnemyManager.Instance.ContainsTarget(mark) == false)
            {
                nearest = mark;
                distance = currDistance;
            }
        }

        EnemyManager.Instance.AddTarget(nearest);

        return nearest;
    }

    public void Hurt()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(SLOW_ENEMY_TAG))
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }

        if (Time.time - lastTimeAttacked > attackTimeDelay)
        {
            if (collision.gameObject.CompareTag(SEED_TAG))
            {
              
                var script = target.GetComponent<Seed>();
                if (script != null)
                {
                    script.TakeDamage();
                }
            } else if (collision.gameObject.CompareTag(TARGET_TAG))
            {
                var handler = collision.gameObject.GetComponent<TileDestroyHandler>();
                if (handler != null)
                {
                    handler.AttackStarted();
                }
            }

        }

        isColliding = true;

    }

    // maybe unecessary
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TARGET_TAG))
            collision.gameObject.GetComponent<TileDestroyHandler>().AttackStopped();
        isColliding = false;
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.GetComponent<Health>() != null)
        {
            collider.GetComponent<Health>().Damage(damage);
        }
        if(collider.gameObject.CompareTag(SLOW_ENEMY_TAG))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collider);
            Debug.Log("Physics ignored from OnCollisionEnter2D");
        }
    }

    void OnDestroy()
    {
        EnemyManager.Instance.RemoveEnemy();
    }
}
