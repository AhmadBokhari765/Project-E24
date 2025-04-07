using UnityEngine;

public class ElementRules : MonoBehaviour
{
    public bool CanMoveTo(OrOrginsMan piece, int targetX, int targetY, GameObject[,] positions)
    {
        // Check if the target position is within bounds
        if (targetX < 0 || targetX >= 10 || targetY < 0 || targetY >= 8)
            return false;

        // Get the target piece if it exists
        OrOrginsMan targetPiece = positions[targetX, targetY]?.GetComponent<OrOrginsMan>();

        // Check if the target square is occupied
        if (targetPiece != null)
        {
            // Restrict movement based on element rules
            if (piece.name.Contains("fire") && targetPiece.name.Contains("water"))
                return false; // Fire cannot move to water
            if (piece.name.Contains("water") && targetPiece.name.Contains("fire"))
                return false; // Water cannot move to fire
            // Add more rules as needed
        }

        return true; // Allow the move if all checks pass
    }
}