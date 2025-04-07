using NUnit.Framework;
using UnityEngine;
using Unity.Barracuda;

public class ModelLoadingTests
{
    [Test]
    public void TestModelLoading()
    {
        // Load the model from Resources/Models folder
        var modelAsset = Resources.Load<NNModel>("Assets/Models/unity_model_compatible.onnx");
        Assert.IsNotNull(modelAsset, "Failed to load ONNX model from Resources/Models");

        // Create the runtime model
        var runtimeModel = ModelLoader.Load(modelAsset);
        Assert.IsNotNull(runtimeModel, "Failed to create runtime model");

        // Verify input/output shapes
        using (var worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, runtimeModel))
        {
            // Create a dummy tensor to inspect input shape
            var inputShape = runtimeModel.inputs[0].shape;
            Debug.Log($"Model input shape: {inputShape}");
            Assert.AreEqual(100, inputShape[1], "Model should expect 100 input values (8x10 board + 20 state vars)");

            // Use inference to inspect output tensor shape
            var dummyInput = new Tensor(1, inputShape[1]); // Adjust according to your model's expected input dimensions
            worker.Execute(dummyInput);
            var output = worker.PeekOutput();
            Debug.Log($"Model output shape: {output.shape}");
            Assert.AreEqual(2, output.shape[1], "Model should output 2 values (x,y coordinates)");
            dummyInput.Dispose();
        }

        Debug.Log("✓ Model loaded successfully with correct shapes");
    }
}
