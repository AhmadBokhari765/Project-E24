using UnityEngine;
using System.Collections.Generic;
using UnityEngine;


public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;
    // Board positions, not world positions
    public int matrixX;
    public int matrixY;

    // false: movement, true: attacking 
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            // Change to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 1.0f, 1.0f);

        }
    }
    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        // Check if the move is valid (not occupied by a blocked tile)
        if (!CanMoveTo(matrixX, matrixY)) return;

        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            Destroy(cp);
        }
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<OrOrginsMan>().GetXBoard(),
            reference.GetComponent<OrOrginsMan>().GetYBoard());
        reference.GetComponent<OrOrginsMan>().SetXBoard(matrixX);
        reference.GetComponent<OrOrginsMan>().SetYBoard(matrixY);
        reference.GetComponent<OrOrginsMan>().SetCoords();

        controller.GetComponent<Game>().SetPosition(reference);

        reference.GetComponent<OrOrginsMan>().DestroyMovePlates();




    }
    private bool CanMoveTo(int x, int y)
    {
        Vector3 targetPosition = new Vector3(x, y, -1); // Convert board coordinates to world position
        Collider2D[] colliders = Physics2D.OverlapPointAll(targetPosition);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("NeutralSquare"))
            {
                NeutralSquare neutralSquare = collider.GetComponent<NeutralSquare>();
                if (neutralSquare != null && neutralSquare.IsOccupied() &&
                    reference.name != "fire" && reference.name != "evolutionist_fire")
                {
                    return false; // Block movement if occupied and not a fire piece
                }
            }
        }
        return true; // Move is allowed
    }
    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }
    public void SetRefernce(GameObject obj)
    {
        reference = obj;
    }
    public GameObject GetReference()
    {
        return reference;
    }
}