using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.AI;
#endif

public class ProceduralTerrainGenerator : MonoBehaviour
{
    #region Variable Declarations

    [TextArea (1, 3)]
    public string note = "Width and Height must be power of two.";

#pragma warning disable 0649
    [SerializeField] private bool generateTerrain;
    [SerializeField] private int width = 1024; // x
    [SerializeField] private int depth = 15; // y
    [SerializeField] private int height = 1024; // z
    [SerializeField] private float scale = 20f;
#pragma warning restore 0649

    #endregion

    private void Awake ()
    {
        if (generateTerrain)
        {
            Terrain terrain = GetComponent<Terrain> ();
            terrain.terrainData = GenerateTerrain (terrain.terrainData);
#if UNITY_EDITOR
            NavMeshBuilder.BuildNavMesh ();
#endif
        }

    }

    TerrainData GenerateTerrain (TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3 (width, depth, height);
        terrainData.SetHeights (0, 0, GenerateHeights ());

        return terrainData;
    }

    private float[, ] GenerateHeights ()
    {
        float[, ] heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight (x, y);
            }
        }

        return heights;
    }

    private float CalculateHeight (int x, int y)
    {
        var xCoord = (float) x / width * scale;
        var yCoord = (float) y / height * scale;

        return Mathf.PerlinNoise (xCoord, yCoord);

    }
}