using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class StartState : MonoBehaviour
{
    public static int cols = 41;
    public static int rows = 20;
    public float start_x = -9.95f; 
    public float start_y = 4.74f;

    float randCell;

    public GameObject cell;
    public Sprite sprite_alive;
    public Sprite sprite_dead;

    public int alive_percentage;
    public float delay;

    public GameObject[] cells = new GameObject[cols * rows];
    int array_pos = 0;
    GameObject new_cell;


    public bool started = false;
    bool simulate = false;
    float nextGen;
    public bool done;



    // Start is called before the first frame update
    void Start()
    { 

        for (int col = 0; col < cols; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                randCell = UnityEngine.Random.Range(0, 100);
                if (randCell < alive_percentage)
                {
                    cell.GetComponent<SpriteRenderer>().sprite = sprite_alive;
                    cell.GetComponent<CellManager>().state = true;
                    new_cell = Instantiate(cell, new Vector2((start_x + (float)col / 2), (start_y - (float)row / 2)), Quaternion.identity);
                    new_cell.name += array_pos;
                }
                else
                {
                    cell.GetComponent<SpriteRenderer>().sprite = sprite_dead;
                    cell.GetComponent<CellManager>().state = false;
                    new_cell = Instantiate(cell, new Vector2((start_x + (float)col / 2), (start_y - (float)row / 2)), Quaternion.identity);
                    new_cell.name += array_pos;
                }

                cells[array_pos] = new_cell;
                array_pos += 1;
            }
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("return"))
        {
            Debug.Log("STARTED");
            started = true;
        }



        if (started == true)
        {
            if (Input.GetKeyDown("space"))
            {
                simulate = !simulate;
                Debug.Log("simulate: " + simulate);
            }


            if (simulate && Time.time > nextGen)
            {
                NextGeneration();
                
                nextGen = Time.time + delay;
            }

            else if (Input.GetMouseButtonDown(0))
            {
                NextGeneration();
            }
        }
    }


    void NextGeneration()
    {
        for (int i = 0; i < cells.Count(); i++)
        {
            cells[i].GetComponent<CellManager>().NextGeneration();
        }

        for (int i = 0; i < cells.Count(); i++)
        {
            cells[i].GetComponent<CellManager>().spriteRenderer.sprite = cells[i].GetComponent<CellManager>().next_sprite;
        }
        
    }


}
