using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellOG : MonoBehaviour
{
    public bool collapsed;
    public TileWeightBundle[] tileOptions;

    public void CreateCell(bool collapseState, TileWeightBundle[] tiles)
    {
        collapsed = collapseState;
        tileOptions = tiles;
    }

    public void RecreateCell(TileWeightBundle[] tiles)
    {
        tileOptions = tiles;
    }
}