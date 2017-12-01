using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    const float positionMultiply = .46f;

    public int maxX;
    public int maxY;
    public Cell[,] cells;
    private int generation;
    public Text generationText;

    public Cell prefab;
    void Start () {
        CreateBoard(maxX, maxY);
        generation = 0;
	}

    public void CreateBoard(int x, int y)
    {
        cells = new Cell[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Cell cell = Instantiate(prefab, new Vector3((float)i * positionMultiply, (float)j * positionMultiply, 0f), Quaternion.identity, transform) as Cell;
                cell.name = "Cell" + i + "x" + j;
                cells[i, j] = cell;
            }
        }

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                cells[i, j].Neighbours = FindNeighbours(i, j);
            }
        }
    }

    public Cell[] FindNeighbours(int x, int y)
    {
        Cell[] result = new Cell[8];
        result[0] = cells[x, (y + 1) % maxY]; // top
        result[1] = cells[(x + 1) % maxX, (y + 1) % maxY]; // top right
        result[2] = cells[(x + 1) % maxX, y % maxY]; // right
        result[3] = cells[(x + 1) % maxX, (maxY + y - 1) % maxY]; // bottom right
        result[4] = cells[x % maxX, (maxY + y - 1) % maxY]; // bottom
        result[5] = cells[(maxX + x - 1) % maxX, (maxY + y - 1) % maxY]; // bottom left
        result[6] = cells[(maxX + x - 1) % maxX, y % maxY]; // left
        result[7] = cells[(maxX + x - 1) % maxX, (y + 1) % maxY]; // top left
        return result;
    }

    public void StartNextGeneration()
    {
        foreach (Cell cell in cells)
        {
            cell.NextGeneration();
        }
        foreach (Cell cell in cells)
        {
            cell.ApplyGeneration();
        }
        generation++;
        generationText.text = "Generation: " + generation.ToString();
    }

    public void ClearCells()
    {
        foreach (Cell cell in cells)
        {
            cell.Reset();
        }
    }
}
