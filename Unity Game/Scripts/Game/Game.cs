using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Barracuda;

public class Game : MonoBehaviour
{
    public GameObject gamepiece;
    public GameObject neutralSquarePrefab;
    public Sprite earthSprite;
    public Sprite waterSprite;
    public Sprite fireSprite;
    public Sprite airSprite;
    private bool isAITurn = false; // Add this with your other private fields

    // Positions and team for each gamepiece

    private GameObject[,] positions = new GameObject[10, 8];
    private GameObject[] playerCreationist = new GameObject[10];
    private GameObject[] playerEvolutionist = new GameObject[10];

    private string currentPlayer = "Creationist";

    private bool gameOver = false;

    public void Start()
    { // Functions (parameters1,parameter2)
        playerCreationist = new GameObject[] { Create("earth",0, 0), Create("water", 1, 0),
            Create("fire", 2, 0), Create("air", 3, 0),
            Create("woman", 4, 0), Create("man", 5, 0),Create("air",6, 0),Create("fire",7, 0),Create("water",8, 0),Create("earth",9, 0)};
        playerEvolutionist = new GameObject[] {Create("evolutionist_earth",0, 7), Create("evolutionist_water", 1, 7),
            Create("evolutionist_fire", 2, 7), Create("evolutionist_air", 3, 7),
            Create("ape_woman", 4, 7), Create("ape_man", 5,7),Create("evolutionist_air",6, 7),Create("evolutionist_fire",7, 7),Create("evolutionist_water",8, 7),Create("evolutionist_earth",9, 7)};
        for (int i = 0; i < playerCreationist.Length; i++)
        {
            SetPosition(playerCreationist[i]);
            SetPosition(playerEvolutionist[i]);

        }
        InstantiateNeutralSquares();

    }
    public void Update()
    {
        if (currentPlayer == "Evolutionist" && !gameOver && !isAITurn)
        {
            StartCoroutine(AITurn());
        }
    }
    private IEnumerator AITurn()
    {
        isAITurn = true;
        Debug.Log("AI Turn Started");
        yield return new WaitForSeconds(1f);

        foreach (GameObject piece in playerEvolutionist)
        {
            if (piece != null)
            {
                Debug.Log($"Processing AI piece: {piece.name}");
                piece.GetComponent<OrOrginsMan>().DecideAIMove();
                yield return new WaitForSeconds(0.5f);
            }
        }

        currentPlayer = "Creationist";
        isAITurn = false;
        Debug.Log("AI Turn Ended");
    }
    public float[] GetCurrentGameState()
    {
        float[] state = new float[100]; // 8x10 board + 20 game state vars

        // Encode board state (example implementation)
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject piece = positions[x, y];
                state[y * 10 + x] = piece != null ?
                    (piece.GetComponent<OrOrginsMan>().player == "Creationist" ? 1f : -1f) : 0f;
            }
        }

        // Add game state flags (modify as needed)
        for (int i = 80; i < 100; i++)
        {
            state[i] = 0f; // Initialize additional state vars
        }

        return state;
    }

    public GameObject Create(string name, int x, int y)
    {
        if (gamepiece == null)
        {
            Debug.LogError("Gamepiece prefab is not assigned in Game.cs!");
            return null;
        }
        GameObject obj = Instantiate(gamepiece, new Vector3(0, 0, -1), Quaternion.identity);
        obj.name = name;
        OrOrginsMan cm = obj.GetComponent<OrOrginsMan>();
        if (cm != null) {
            cm.SetXBoard(x);
            cm.SetYBoard(y);
            cm.Activate();
            // Set the sprite or color based on the name
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                switch (name)
                {
                    case "earthsprite":
                        spriteRenderer.sprite = earthSprite; // Assign the earth sprite
                        break;
                    case "watersprite":
                        spriteRenderer.sprite = waterSprite; // Assign the water sprite
                        break;
                    case "firesprite":
                        spriteRenderer.sprite = fireSprite; // Assign the fire sprite
                        break;
                    case "airsprite":
                        spriteRenderer.sprite = airSprite; // Assign the air sprite
                        break;
                        // Add more cases if needed
                }
            }




        }
        
        return obj;
    }
    // Set all piece positions on the position board

    public void SetPosition(GameObject obj)
    {
        OrOrginsMan cm = obj.GetComponent<OrOrginsMan>();
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }
    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;

    }
    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];

    }
    public bool PositionOnboard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }
    private void InstantiateNeutralSquares()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                // Skip positions where player pieces are already placed
                if (positions[x, y] == null)
                {
                    // Calculate the position for the neutral square
                    Vector3 position = new Vector3(x * 0.75f - 3.0f, y * 0.95f - 3.0f, -1);
                    GameObject neutralSquare = Instantiate(neutralSquarePrefab, position, Quaternion.identity);
                    neutralSquare.tag = "NeutralSquare"; // Set the tag for identification
                    
                }
            }
        }
    }
    
}


