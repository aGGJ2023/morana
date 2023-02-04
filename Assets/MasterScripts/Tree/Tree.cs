using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class Tree : MonoBehaviour
{

    private const string ENEMY_TAG = "enemy";
    [SerializeField]
    private int MAX_HEALTH = 100;

    private Vector3 CenterOfMap = new Vector3(0f,0f,0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(ENEMY_TAG))
        {
            //damage root
            if (collider.GetComponent<Health>() != null)
            {
                //                              10 is just for testing
                collider.GetComponent<Health>().Damage(10);//sets taken damage
            }
        }
    }

}
