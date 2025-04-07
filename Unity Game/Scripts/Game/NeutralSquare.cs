using Unity.VisualScripting;
using UnityEngine;

public class NeutralSquare : MonoBehaviour
{
    private bool isOccupied = false;
    private bool occupied = false;
    private GameObject occupiedIndicator;
    public GameObject blockedTilePrefab;

    public void SetOccupied(bool status, Vector3 position)
    {
        occupied = status;

        if (occupied && occupiedIndicator == null)
        {
            // Instantiate the BlockedTile at the square’s position
            occupiedIndicator = Instantiate(blockedTilePrefab, position, Quaternion.identity);
        }
    }



        void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(0.0f, 0.0f, 0.0f, 1.0f); // Set color to black
        }
        else
        {
            Debug.LogError("No SpriteRenderer found on this GameObject.");
        }

    }


        // Call this method to change the color when a piece moves over this square

        public void ChangeColor(Color newColor)
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().color = newColor;
            Debug.Log($"Changed color to: {newColor}");  // Logging color change
            isOccupied = true; // Mark this square as occupied
        }
        else
        {
            Debug.LogError("No SpriteRenderer found on this GameObject.");
        }
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

}