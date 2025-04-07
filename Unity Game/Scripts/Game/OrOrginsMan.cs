using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Origins.AI;

public class OrOrginsMan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // References
    public GameObject controller;
    public GameObject movePlate;

    // Positions 
    private int xBoard = -1;
    private int yBoard = -1;


    // Variable to keep track of players
    public string player;

    // References for all the sprites that the gamepiece can be
    public Sprite man, woman, air, earth, water, fire;
    public Sprite ape_man, ape_woman, evolutionist_air, evolutionist_earth, evolutionist_water, evolutionist_fire;
    private AIModelManager aiManager;
    void Start()
    {
        aiManager = FindObjectOfType<AIModelManager>();
        if (aiManager == null)
        {
            Debug.LogError("AIModelManager not found in scene!");
        }
    }
    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        aiManager = FindAnyObjectByType<Origins.AI.AIModelManager>();
        // take the instantiated locations and adjust the transform
        SetCoords();

        switch (this.name)
        {
            case "woman": this.GetComponent<SpriteRenderer>().sprite = woman; player = "Creationist"; break;
            case "fire": this.GetComponent<SpriteRenderer>().sprite = fire; player = "Creationist"; break;
            case "water": this.GetComponent<SpriteRenderer>().sprite = water; player = "Creationist"; break;
            case "earth": this.GetComponent<SpriteRenderer>().sprite = earth; player = "Creationist"; break;
            case "air": this.GetComponent<SpriteRenderer>().sprite = air; player = "Creationist"; break;
            case "man": this.GetComponent<SpriteRenderer>().sprite = man; player = "Creationist"; break;


            case "ape_woman": this.GetComponent<SpriteRenderer>().sprite = ape_woman; player = "Evolutionist"; break;
            case "evolutionist_fire": this.GetComponent<SpriteRenderer>().sprite = evolutionist_fire; player = "Evolutionist"; break;
            case "evolutionist_water": this.GetComponent<SpriteRenderer>().sprite = evolutionist_water; player = "Evolutionist"; break;
            case "evolutionist_earth": this.GetComponent<SpriteRenderer>().sprite = evolutionist_earth; player = "Evolutionist"; break;
            case "evolutionist_air": this.GetComponent<SpriteRenderer>().sprite = evolutionist_air; player = "Evolutionist"; break;
            case "ape_man": this.GetComponent<SpriteRenderer>().sprite = ape_man; player = "Evolutionist"; break;

        }


    }
    public void DecideAIMove()
    {
        if (player != "Evolutionist")
        {
            Debug.Log("Not an AI piece - skipping");
            return;
        }
        Debug.Log($"AI deciding move for {this.name}");

        float[] gameState = GetCurrentGameState();
        Debug.Log($"Game state length: {gameState.Length}");

        float[] modelOutput = aiManager.Predict(gameState);
        Debug.Log($"Model output: {string.Join(", ", modelOutput)}");

        
        InitiateMovePlates();
        MovePlate[] plates = FindObjectsOfType<MovePlate>();
        Debug.Log($"Found {plates.Length} possible moves");
        if (plates.Length > 0)
        {
            // Simple selection - pick first available move
            plates[0].OnMouseUp();
            Debug.Log($"AI selected move: {plates[0].matrixX}, {plates[0].matrixY}");
        }
        else
        {
            Debug.Log("No valid moves found for this piece");
        }
    }
    private float[] GetCurrentGameState()
    {
        List<float> state = new List<float>();
        Game gameController = FindObjectOfType<Game>();

        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject piece = gameController.GetPosition(x, y);
                state.Add(piece != null ? EncodePiece(piece.name) : 0);
            }
        }
        return state.ToArray();
    }

    private float EncodePiece(string pieceName)
    {
        switch (pieceName)
        {
            case "earth": return 1.0f;
            case "water": return 2.0f;
            case "fire": return 3.0f;
            case "air": return 4.0f;
            case "man": return 5.0f;
            case "woman": return 6.0f;
            case "evolutionist_earth": return -1.0f;
            case "evolutionist_water": return -2.0f;
            case "evolutionist_fire": return -3.0f;
            case "evolutionist_air": return -4.0f;
            case "ape_man": return -5.0f;
            case "ape_woman": return -6.0f;
            default: return 0.0f;
        }
    }

    public void SetCoords()
    {
        /*float x = xBoard;
        float y = yBoard;
        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;*/

        float x = xBoard * 0.75f - 3.0f; // Adjust scale and offset
        float y = yBoard * 0.95f - 3.0f;


        this.transform.position = new Vector3(x, y, -1.0f);



    }
    public int GetXBoard()
    {
        return xBoard;
    }
    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        DestroyMovePlates();

        InitiateMovePlates();


    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);

        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "fire":
            case "water":
            case "earth":
            case "air":
            case "evolutionist_fire":
            case "evolutionist_earth":
            case "evolutionist_water":
            case "evolutionist_air":

                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;

            case "man":
            case "woman":

                LineMovePlate(1, 0);   // Right
                LineMovePlate(-1, 0);  // Left (Re-added)
                LineMovePlate(0, 1);   // Up
                LineMovePlate(1, 1);   // Up-Right
                LineMovePlate(-1, 1);  // Up-Left
                break;

            case "ape_man":
            case "ape_woman":
                LineMovePlate(1, 0);   // Right
                LineMovePlate(-1, 0);  // Left
                LineMovePlate(0, -1);  // Down
                LineMovePlate(1, -1);  // Down-Right
                LineMovePlate(-1, -1);
                break;





        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game game = controller.GetComponent<Game>();
        Game sc = game;

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnboard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;

        }
        Vector3 newPosition = new Vector3(x * 0.75f - 3.0f, y * 0.95f - 3.0f, -1.0f);
        CheckAndChangeNeutralSquare(newPosition);
        if (sc.PositionOnboard(x, y) && sc.GetPosition(x, y).GetComponent<OrOrginsMan>().player != player)
        {
            MovePlateAttackSpawn(x, y);

        }
    }
    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        /*float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;*/

        float x = matrixX * 0.75f - 3.0f;
        float y = matrixY * 0.95f - 3.0f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetRefernce(gameObject);
        mpScript.SetCoords(matrixX, matrixY);


    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        /*float x = matrixX;
        float y = matrixY;


        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;*/

        float x = matrixX * 0.75f - 3.0f;
        float y = matrixY * 0.95f - 3.0f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetRefernce(gameObject);
        mpScript.SetCoords(matrixX, matrixY);


    }
    private void CheckAndChangeNeutralSquare(Vector3 newPosition)
    {
        // Use a collider or a tag to identify neutral squares
        Collider2D[] colliders = Physics2D.OverlapPointAll(newPosition);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("NeutralSquare"))
            {
                NeutralSquare neutralSquare = collider.GetComponent<NeutralSquare>();
                if (neutralSquare != null && !neutralSquare.IsOccupied())
                {
                    if (this.name == "fire" || this.name == "evolutionist_fire")
                    {
                        neutralSquare.SetOccupied(true, neutralSquare.transform.position);
                        // Change the color of the neutral square
                        neutralSquare.ChangeColor(Color.black); // Change to your desired color
                    }
                }
            }
        }



    }
}

