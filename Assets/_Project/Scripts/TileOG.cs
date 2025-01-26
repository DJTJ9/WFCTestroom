using UnityEngine;

public class TileOG : MonoBehaviour
{
    public int Weight;
    
    public TileWeightBundle[] upNeighbours;
    public TileWeightBundle[] downNeighbours;
    public TileWeightBundle[] leftNeighbours;
    public TileWeightBundle[] rightNeighbours;
}

[System.Serializable]
public struct TileWeightBundle
{
    public TileOG Tile;
    public int Weight;
    
    // Überschreibe == Operator
    public static bool operator ==(TileWeightBundle a, TileWeightBundle b)
    {
        return a.Tile == b.Tile;
        // return a.Tile == b.Tile && a.Weight == b.Weight;
    }
    // Überschreibe != Operator (immer zusammen mit == benötigt)
    public static bool operator !=(TileWeightBundle a, TileWeightBundle b)
    {
        return !(a == b);
    }

    // Überschreibe Equals() und GetHashCode() für eine korrekte Implementierung
    public override bool Equals(object obj)
    {
        if (obj is TileWeightBundle other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (Tile?.GetHashCode() ?? 0) ^ Weight.GetHashCode();
    }
}