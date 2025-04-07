using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Origins.AI;
using Unity.Barracuda;
using System.Collections;

public class IntegrationTests
{
    [UnityTest]
    public IEnumerator TestGameIntegration()
    {
        // Setup game
        var game = new GameObject().AddComponent<Game>();
        yield return null; // Wait for initialization

        // Setup AI
        var aiManager = new GameObject().AddComponent<AIModelManager>();
        aiManager.modelAsset = Resources.Load<NNModel>("Assets/Resources/Models/unity_model_compatible.onnx");

        // Get current state
        float[] gameState = game.GetCurrentGameState();

        // Get prediction
        float[] move = aiManager.Predict(gameState);
        Debug.Log($"AI suggested move: ({move[0]}, {move[1]})");

        // Verify
        Assert.IsTrue(game.PositionOnboard((int)move[0], (int)move[1]),
            "AI suggested invalid board position");

        Debug.Log("✓ Integration test passed");
    }
}