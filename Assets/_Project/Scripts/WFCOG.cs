using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class WaveFunction : MonoBehaviour
{
    public int dimensions;
    public TileWeightBundle[] tileObjects;
    public List<CellOG> gridComponents;
    public CellOG cellObj;

    int iterations = 0;

    void Awake() {
        gridComponents = new List<CellOG>();
        InitializeGrid();
    }

    void InitializeGrid() {
        for (int y = 0; y < dimensions; y++) {
            for (int x = 0; x < dimensions; x++) {
                CellOG newCell = Instantiate(cellObj, new Vector2(x, y), Quaternion.identity);
                newCell.CreateCell(false, tileObjects);
                gridComponents.Add(newCell);
            }
        }

        StartCoroutine(CheckEntropy());
    }


    IEnumerator CheckEntropy() {
        List<CellOG> tempGrid = new List<CellOG>(gridComponents);

        tempGrid.RemoveAll(c => c.collapsed);

        tempGrid.Sort((a, b) => { return a.tileOptions.Length - b.tileOptions.Length; });

        int arrLength = tempGrid[0].tileOptions.Length;
        int stopIndex = default;

        for (int i = 1; i < tempGrid.Count; i++) {
            if (tempGrid[i].tileOptions.Length > arrLength) {
                stopIndex = i;
                break;
            }
        }

        if (stopIndex > 0) {
            tempGrid.RemoveRange(stopIndex, tempGrid.Count - stopIndex);
        }

        yield return new WaitForSeconds(0.01f);

        CollapseCell(tempGrid);
    }

    void CollapseCell(List<CellOG> tempGrid) {
        int randIndex = UnityEngine.Random.Range(0, tempGrid.Count);

        CellOG cellToCollapse = tempGrid[randIndex];

        cellToCollapse.collapsed = true;
        TileWeightBundle selectedTile = SelectTileBasedOnWeight(cellToCollapse.tileOptions);
        cellToCollapse.tileOptions = new TileWeightBundle[] { selectedTile };

        TileOG foundTile = cellToCollapse.tileOptions[0].Tile;
        Instantiate(foundTile, cellToCollapse.transform.position, Quaternion.identity);

        UpdateGeneration();
    }

    TileWeightBundle SelectTileBasedOnWeight(TileWeightBundle[] options) {
        int totalWeight = 0;
        Debug.LogWarning($"{options.Length} tile options selected");
        // Berechne das Gesamtgewicht
        foreach (var option in options) {
            totalWeight += option.Weight;
        }

        // Zufällige Gewichtsauswahl
        int randomWeight = UnityEngine.Random.Range(0, totalWeight);
        int currentWeight = 0;

        foreach (var option in options) {
            currentWeight += option.Weight;
            if (randomWeight <= currentWeight) {
                return option; // Passendes Tile gefunden
            }
        }

        if (totalWeight == 0)
            throw new Exception("Total weight is 0. Check weight distribution.");

        throw new Exception("Failed to select a tile. Check weight distribution.");
    }

    void UpdateGeneration() {
        List<CellOG> newGenerationCell = new List<CellOG>(gridComponents);

        for (int y = 0; y < dimensions; y++) {
            for (int x = 0; x < dimensions; x++) {
                var index = x + y * dimensions;
                if (gridComponents[index].collapsed) {
                    Debug.Log("called");
                    newGenerationCell[index] = gridComponents[index];
                }
                else {
                    List<TileWeightBundle> options = new List<TileWeightBundle>();
                    foreach (TileWeightBundle t in tileObjects) {
                        options.Add(t);
                    }

                    //update above
                    if (y > 0) {
                        CellOG up = gridComponents[x + (y - 1) * dimensions];
                        List<TileWeightBundle> validOptions = new List<TileWeightBundle>();

                        foreach (TileWeightBundle possibleOptions in up.tileOptions) {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].Tile.upNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    //update right
                    if (x < dimensions - 1) {
                        CellOG right = gridComponents[x + 1 + y * dimensions];
                        List<TileWeightBundle> validOptions = new List<TileWeightBundle>();

                        foreach (TileWeightBundle possibleOptions in right.tileOptions) {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].Tile.leftNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    //look down
                    if (y < dimensions - 1) {
                        CellOG down = gridComponents[x + (y + 1) * dimensions];
                        List<TileWeightBundle> validOptions = new List<TileWeightBundle>();

                        foreach (TileWeightBundle possibleOptions in down.tileOptions) {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].Tile.downNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    //look left
                    if (x > 0) {
                        CellOG left = gridComponents[x - 1 + y * dimensions];
                        List<TileWeightBundle> validOptions = new List<TileWeightBundle>();

                        foreach (TileWeightBundle possibleOptions in left.tileOptions) {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].Tile.rightNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    TileWeightBundle[] newTileList = new TileWeightBundle[options.Count];

                    for (int i = 0; i < options.Count; i++) {
                        newTileList[i] = options[i];
                    }

                    newGenerationCell[index].RecreateCell(newTileList);
                }
            }
        }

        gridComponents = newGenerationCell;
        iterations++;

        if (iterations < dimensions * dimensions) {
            StartCoroutine(CheckEntropy());
        }
    }

    void CheckValidity(List<TileWeightBundle> optionList, List<TileWeightBundle> validOption) {
        // for (int x = optionList.Count - 1; x >= 0; x--)
        // {
        //     var element = optionList[x];
        //     if (!validOption.Contains(element))
        //     {
        //         optionList.RemoveAt(x);
        //     }
        // }

        for (int x = optionList.Count - 1; x >= 0; x--) {
            var optionTile = optionList[x].Tile; // Hol dir das Tile aus der aktuellen Option
        
            // Suche in validOption nach einem Bundle mit dem gleichen Tile
            var match = validOption.Find(bundle => bundle.Tile == optionTile);
        
            if (match.Tile != null) {
                // Aktualisiere das Gewicht des Tiles in der Option
                optionList[x] = new TileWeightBundle {
                    Tile = optionTile,
                    Weight = match.Weight // Update das Gewicht
                };
            }
            else {
                // Entferne das Tile aus den Optionen, da es nicht in validOption enthalten ist
                optionList.RemoveAt(x);
            }
        }
    }
}