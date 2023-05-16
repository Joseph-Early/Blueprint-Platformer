using UnityEngine;

/*
*   This code is ported from a 3D grid system so may contain unnecessary code for a 2D game
*/

namespace Misc
{
    using UnityEngine;

    public class Grid : MonoBehaviour
    {
        [SerializeField] private int gridSize = 10; // Number of grid units along each axis
        [SerializeField] private float gridSpacing = 1.0f; // Distance between each grid line
        [SerializeField] private Color gridColor = Color.red; // Color of the grid lines
        [SerializeField] private float xOffset = 0.0f; // Offset of the grid along the X axis
        [SerializeField] private float yOffset = 0.0f; // Offset of the grid along the Y axis

        // Cached reference to the grid object
        private GameObject _grid;

        // Cached offset of the grid along the X axis
        private float cachedXOffset;

        // Cached offset of the grid along the Y axis
        private float cachedYOffset;

        void Start()
        {
            _grid = DrawGrid();
        }

        void Update()
        {
            // If the grid offset has changed, redraw the grid
            if (xOffset != cachedXOffset || yOffset != cachedYOffset)
            {
                cachedXOffset = xOffset;
                cachedYOffset = yOffset;
                _grid = DrawGrid();
            }
        }

        GameObject DrawGrid()
        {
            // If gridObject already exists, destroy it
            if (_grid != null)
            {
                Destroy(_grid);
                _grid = null;
            }

            // Create new empty game object to hold the grid lines
            GameObject gridObject = new GameObject("Grid");

            // Set the position of the grid object to the center of the grid
            float gridOffsetX = (gridSize * gridSpacing) / 2.0f + xOffset;
            float gridOffsetY = (gridSize * gridSpacing) / 2.0f + yOffset;
            gridObject.transform.position = new Vector3(-gridOffsetX, 0.0f, -gridOffsetY);

            // Create the material for the grid lines
            Material lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
            lineMaterial.color = gridColor;

            // Draw the lines along the X axis
            for (int i = 0; i <= gridSize; i++)
            {
                float xPos = i * gridSpacing;
                Vector3 start = new Vector3(xPos, 0.0f, 0.0f) + new Vector3(xOffset, yOffset, 0f);
                Vector3 end = new Vector3(xPos, gridSize * gridSpacing, 0.0f) + new Vector3(xOffset, yOffset, 0f);
                DrawLine(gridObject.transform, start, end, lineMaterial);
            }

            // Draw the lines along the Y axis
            for (int i = 0; i <= gridSize; i++)
            {
                float yPos = i * gridSpacing;
                Vector3 start = new Vector3(0.0f, yPos, 0.0f) + new Vector3(xOffset, yOffset, 0f);
                Vector3 end = new Vector3(gridSize * gridSpacing,  yPos, 0f) + new Vector3(xOffset, yOffset, 0.0f);
                DrawLine(gridObject.transform, start, end, lineMaterial);
            }

            // Return the grid object
            return gridObject;
        }


        void DrawLine(Transform parent, Vector3 start, Vector3 end, Material material)
        {
            // Create new game object to hold the line renderer
            GameObject lineObject = new GameObject("Line");

            // Attach the line renderer component to the new game object
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
            lineRenderer.material = material;
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;

            // Set the start and end points of the line renderer
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            // Set the parent of the line object to the grid object
            lineObject.transform.SetParent(parent);
        }
    }
}