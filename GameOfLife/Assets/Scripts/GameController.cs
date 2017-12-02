using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    const float positionMultiply = .46f;
    [Header("Game Group")]
    public float time;
    private int maxX;
    private int maxY;
    public Cell[,] cells;
    private int generation;
    public Cell prefab;
    private bool start = false;

    [Header("UI Group")]
    public Text generationText;
    public GameObject BarPanel;
    public GameObject StartPanel;
    public Button startButton;
    public Button autoButton;
    public Slider timeSlider;

    

    private void Start()
    {
        startButton.onClick.AddListener( () => StartGame());
        autoButton.onClick.AddListener(() => 
        {
            if (!start)
            {
                start = true;
                autoButton.GetComponentInChildren<Text>().text = "Stop";
                StartCoroutine(AutoGenerate());
            }
            else
            {
                StopAllCoroutines();
                start = false;
                autoButton.GetComponentInChildren<Text>().text = "Start";
            }
        });
        timeSlider.value = 2f;
        timeSlider.onValueChanged.AddListener((t) => {
            time = t;
        });

    }
    public void StartGame () {
        startButton.interactable = false;                           
        maxX = 50;
        maxY = 50;
        if (maxX > 0 && maxY > 0)
        {
            CreateBoard(maxX, maxY);
            generation = 0;

            BarPanel.SetActive(true);
            StartPanel.SetActive(false);
        }                
        startButton.interactable = true;        
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
        Cell[] neighbours = new Cell[8];
        neighbours[0] = cells[x, (y + 1) % maxY]; // top
        neighbours[1] = cells[(x + 1) % maxX, (y + 1) % maxY]; // top right
        neighbours[2] = cells[(x + 1) % maxX, y % maxY]; // right
        neighbours[3] = cells[(x + 1) % maxX, (maxY + y - 1) % maxY]; // bottom right
        neighbours[4] = cells[x % maxX, (maxY + y - 1) % maxY]; // bottom
        neighbours[5] = cells[(maxX + x - 1) % maxX, (maxY + y - 1) % maxY]; // bottom left
        neighbours[6] = cells[(maxX + x - 1) % maxX, y % maxY]; // left
        neighbours[7] = cells[(maxX + x - 1) % maxX, (y + 1) % maxY]; // top left
        return neighbours;
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
            generation = 0;
            generationText.text = "Generation: " + generation.ToString();

        }
    }

    IEnumerator AutoGenerate()
    {
        while (start)
        {
            yield return new WaitForSeconds(time);
            StartNextGeneration();
        }
    }
}
