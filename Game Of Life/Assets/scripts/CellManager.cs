using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class CellManager : MonoBehaviour
{
    public bool state;
    public Sprite sprite_alive;
    public Sprite sprite_dead;
    public SpriteRenderer spriteRenderer;
    int count = 0;
    public Sprite next_sprite;

    List <GameObject> currentCollisions = new List <GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        StartState other_script = GameObject.Find("GridHolder").GetComponent<StartState>();
        int index = Array.IndexOf(other_script.cells, this.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentCollisions.Add(collision.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.sprite == sprite_alive)
            state = true;
        else
            state = false; 
    }


    private void OnMouseDown()
    {
        StartState other_script = GameObject.Find("GridHolder").GetComponent<StartState>();
        if (other_script.started == false)
        {
            /*for (int i = 0; i < currentCollisions.Count; i++)
            {
                if (currentCollisions[i].GetComponent<CellManager>().state == true)
                {
                    count += 1;
                }
            }
            count = 0;*/
            

            this.gameObject.GetComponent<CellManager>().state = !this.gameObject.GetComponent<CellManager>().state;

            if (spriteRenderer.sprite == sprite_alive)
            {
                spriteRenderer.sprite = sprite_dead;
            }
            else
            {
                spriteRenderer.sprite = sprite_alive;
            }
        }
        count = 0;
    }



    public void NextGeneration()
    {
        StartState other_script = GameObject.Find("GridHolder").GetComponent<StartState>();
        int index = Array.IndexOf(other_script.cells, this.gameObject);
        count = 0;
        

        for (int i = 0; i < currentCollisions.Count; i++)
        {
            if (currentCollisions[i].GetComponent<CellManager>().state == true)
            {
                count += 1;
            }
        }


        if ((state == true) && (count < 2 || count > 3))
        {
            //state = false;
            next_sprite = sprite_dead;
        }

        else if ((count == 3) && (state == false))
        {
            //state = true;
            next_sprite = sprite_alive;
        }

        else
        {
            next_sprite = spriteRenderer.sprite;
        }
    }
}
