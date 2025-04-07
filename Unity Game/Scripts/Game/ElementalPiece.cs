using UnityEngine;

public class ElementalPiece : MonoBehaviour
{
    public Color pieceColor; // Set this in the Inspector for each elemental piece

    void Start()
    {
        // Set the color based on the piece type
        switch (gameObject.name)
        {
            case "EarthPiece":
                pieceColor = Color.green;
                break;
            case "WaterPiece":
                pieceColor = Color.blue;
                break;
            case "FirePiece":
                pieceColor = Color.red;
                break;
            case "AirPiece":
                pieceColor = Color.yellow;
                break;
        }
    }

    // Method to check for neutral squares and change their color
    public void CheckForNeutralSquare(Vector3 newPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(newPosition);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("NeutralSquare"))
            {
                NeutralSquare neutralSquare = collider.GetComponent<NeutralSquare>();
                if (neutralSquare != null)
                {
                    neutralSquare.ChangeColor(pieceColor);
                }
            }
        }
    }
}