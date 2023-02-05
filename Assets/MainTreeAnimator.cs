using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTreeAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Sprite[] treeStateSprites;
     SpriteRenderer spriteRenderer;
    private int index = 0;
  

    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    public void updateTree()
    {
        if (treeStateSprites.Length < index)
        {
            index++;
            spriteRenderer.sprite = treeStateSprites[index];

        }
    }
}
