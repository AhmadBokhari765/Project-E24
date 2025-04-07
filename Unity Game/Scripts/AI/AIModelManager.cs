using UnityEngine;
using Unity.Barracuda;

namespace Origins.AI
{
    public class AIModelManager : MonoBehaviour
    {
        public NNModel modelAsset;
        private IWorker worker;
        private Model runtimeModel;

        // In AIModelManager.cs
        void Start()
        {
            if (modelAsset == null)
            {
                Debug.LogError("Model Asset is not assigned in the Inspector!");
                return;
            }

            try
            {
                runtimeModel = ModelLoader.Load(modelAsset);
                worker = WorkerFactory.CreateWorker(runtimeModel);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to load model: {e.Message}");
            }
        }

        public float[] Predict(float[] input)
        {
            using (var inputTensor = new Tensor(1, input.Length, input))
            {
                worker.Execute(inputTensor);
                var outputTensor = worker.PeekOutput();
                float[] output = outputTensor.ToReadOnlyArray();
                Debug.Log($"Model Prediction: ({output[0]}, {output[1]})");
                return output;
            }
        }

        void OnDestroy()
        {
            worker?.Dispose();
        }
    }
}