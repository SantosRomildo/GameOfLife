using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour {

    [SerializeField]
    private Color deadCellColor;
    [SerializeField]
    private  Color aliveCellColor;


    private  Cell[] neighbours;
    
    private bool isCellAlive = false;
    private bool isCellAliveInNextGeneration = false;

    private SpriteRenderer spriteRenderer;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = deadCellColor;
	}
	
    void OnMouseDown()
    {
        isCellAlive = !isCellAlive;
        ChangeSpriteColor();
    }

    public Cell[] Neighbours { get { return neighbours; } set { neighbours = value; } }

    public void NextGeneration()
    {
        int qtdAliveNeighbours = GetAliveNeigbours();
        if (isCellAlive)
        {
            if (qtdAliveNeighbours != 2 && qtdAliveNeighbours != 3)
            {
                isCellAliveInNextGeneration = false;
            }
            else
            {
                isCellAliveInNextGeneration = true;
            }
        }
        else
        {
            if(qtdAliveNeighbours == 3)
            {
                isCellAliveInNextGeneration = true;
            }
            else
            {
                isCellAliveInNextGeneration = false;
            }
        }
    }

    public void ApplyGeneration()
    {
        isCellAlive = isCellAliveInNextGeneration;
        ChangeSpriteColor();
    }
    private int GetAliveNeigbours()
    {
        int count = 0;
        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i] != null && neighbours[i].isCellAlive == true)
                count++;
        }
        return count;
    }

    private void ChangeSpriteColor()
    {
        if (isCellAlive)
        {
            spriteRenderer.color = aliveCellColor;

        }
        else
        {
            spriteRenderer.color = deadCellColor;

        }
    }

    public void Reset()
    {
        isCellAlive = false;
        ChangeSpriteColor();
    }
}
