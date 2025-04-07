using UnityEngine;
using Unity.Barracuda;
using NUnit.Framework;

namespace Origins.AI
{
    public class AIModelManager : MonoBehaviour
    {
        public NNModel modelAsset;
        private IWorker worker;
        private Model runtimeModel;

        void Start()
        {
            runtimeModel = ModelLoader.Load(modelAsset);
            worker = WorkerFactory.CreateWorker(runtimeModel);
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