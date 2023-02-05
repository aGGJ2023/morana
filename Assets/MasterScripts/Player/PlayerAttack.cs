using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Place this script inside attackEnemy position in Player prefab that needs a circle collider 
    [SerializeField] Transform checkPosition;
    [SerializeField] float checkRadious=1;
    [SerializeField] string[] EnemyTags;
    [SerializeField] Animator animator;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            detectAttack();
            animator.SetTrigger("Attack");
        }
    }

     void detectAttack()
    {
        Collider2D[] circleColliders = Physics2D.OverlapCircleAll(checkPosition.position, checkRadious);
        foreach (var hitObject in circleColliders)
        {
            foreach (var tag in EnemyTags)
            {
                if (hitObject.gameObject.CompareTag(tag))
                {
                    //BORNA CHANGE THIS TO YOUR CLASS
                    if(hitObject.GetComponent<Enemy>() != null)
                    {
                        Debug.Log("we hit enemy with class");
                        //hitObject.GetComponent<Enemy>.Hurt()
                    }
                }
              

            }
            Debug.Log("We hit" + hitObject.gameObject.name);

        
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(checkPosition.position, checkRadious);
    }




}
