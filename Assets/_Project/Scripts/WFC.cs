using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
public class WFC : MonoBehaviour
{
    public int dimensions;
    public TileWeightBundle[] tileObjects;
    public List<Cell> gridComponents;
    public Cell cellObj;

    int iterations = 0;

    void Awake()
    {
        gridComponents = new List<Cell>();
        InitializeGrid();
    }

    void InitializeGrid()
    {
        for (int y = 0; y < dimensions; y++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                Cell newCell = Instantiate(cellObj, new Vector2(x, y), Quaternion.identity);
                newCell.CreateCell(false, tileObjects);
                gridComponents.Add(newCell);
            }
        }

        StartCoroutine(CheckEntropy());
    }


    IEnumerator CheckEntropy()
    {
        List<Cell> tempGrid = new List<Cell>(gridComponents);

        tempGrid.RemoveAll(c => c.collapsed);

        tempGrid.Sort((a, b) => { return a.tileOptions.Length - b.tileOptions.Length; });

        int arrLength = tempGrid[0].tileOptions.Length;
        int stopIndex = default;

        for (int i = 1; i < tempGrid.Count; i++)
        {
            if (tempGrid[i].tileOptions.Length > arrLength)
            {
                stopIndex = i;
                break;
            }
        }

        if (stopIndex > 0)
        {
            tempGrid.RemoveRange(stopIndex, tempGrid.Count - stopIndex);
        }

        yield return new WaitForSeconds(0.01f);

        CollapseCell(tempGrid);
    }

    void CollapseCell(List<Cell> tempGrid)
    {
        int randIndex = UnityEngine.Random.Range(0, tempGrid.Count);

        Cell cellToCollapse = tempGrid[randIndex];

        cellToCollapse.collapsed = true;
        
        Debug.Log($"Tile Options: {cellToCollapse.tileOptions.Length}");
        TileWeightBundle selectedTile = SelectTileBasedOnWeight(cellToCollapse.tileOptions);
        cellToCollapse.tileOptions = new TileWeightBundle[] { selectedTile };

        Tile foundTile = cellToCollapse.tileOptions[0].Tile;
        Instantiate(foundTile, cellToCollapse.transform.position, Quaternion.identity);

        UpdateGeneration();
    }
    
    TileWeightBundle SelectTileBasedOnWeight(TileWeightBundle[] options)
    {
        int totalWeight = 0;

        // Berechne das Gesamtgewicht
        foreach (var option in options)
        {
            totalWeight += option.Weight;
        }

        // Zufällige Gewichtsauswahl
        int randomWeight = UnityEngine.Random.Range(0, totalWeight);
        int currentWeight = 0;

        foreach (var option in options)
        {
            currentWeight += option.Weight;
            if (randomWeight <= currentWeight)
            {
                return option; // Passendes Tile gefunden
            }
        }
        
        if (totalWeight == 0)
            throw new Exception("Total weight is 0. Check weight distribution.");

        throw new Exception("Failed to select a tile. Check weight distribution.");
    }

    void UpdateGeneration()
    {
        List<Cell> newGenerationCell = new List<Cell>(gridComponents);

        for (int y = 0; y < dimensions; y++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                var index = x + y * dimensions;
                if (gridComponents[index].collapsed)
                {
                    Debug.Log("called");
                    newGenerationCell[index] = gridComponents[index];
                }
                else
                {
                    List<TileWeightBundle> options = new List<TileWeightBundle>();
                    foreach (TileWeightBundle t in tileObjects)
                    {
                        options.Add(t);
                    }

                    //update above
                    if (y > 0)
                    {
                        Cell up = gridComponents[x + (y - 1) * dimensions];
                        List<TileWeightBundle> validOptions = new List<TileWeightBundle>();

                        foreach (TileWeightBundle possibleOptions in up.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj.Equals(possibleOptions));
                            var valid = tileObjects[valOption].Tile.upNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    //update right
                    if (x < dimensions - 1)
                    {
                        Cell right = gridComponents[x + 1 + y * dimensions];
                        List<TileWeightBundle> validOptions = new List<TileWeightBundle>();

                        foreach (TileWeightBundle possibleOptions in right.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj.Equals(possibleOptions));
                            var valid = tileObjects[valOption].Tile.leftNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    //look down
                    if (y < dimensions - 1)
                    {
                        Cell down = gridComponents[x + (y + 1) * dimensions];
                        List<TileWeightBundle> validOptions = new List<TileWeightBundle>();

                        foreach (TileWeightBundle possibleOptions in down.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj.Equals(possibleOptions));
                            var valid = tileObjects[valOption].Tile.downNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    //look left
                    if (x > 0)
                    {
                        Cell left = gridComponents[x - 1 + y * dimensions];
                        List<TileWeightBundle> validOptions = new List<TileWeightBundle>();

                        foreach (TileWeightBundle possibleOptions in left.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj.Equals(possibleOptions));
                            var valid = tileObjects[valOption].Tile.leftNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    TileWeightBundle[] newTileList = new TileWeightBundle[options.Count];

                    for (int i = 0; i < options.Count; i++)
                    {
                        newTileList[i] = options[i];
                    }

                    newGenerationCell[index].RecreateCell(newTileList);
                }
            }
        }

        gridComponents = newGenerationCell;
        iterations++;

        if(iterations < dimensions * dimensions)
        {
            StartCoroutine(CheckEntropy());
        }

    }

    void CheckValidity(List<TileWeightBundle> optionList, List<TileWeightBundle> validOption)
    {
        for (int x = optionList.Count - 1; x >= 0; x--)
        {
            var element = optionList[x];
            if (!validOption.Contains(element))
            {
                optionList.RemoveAt(x);
            }
        }
    }
}
