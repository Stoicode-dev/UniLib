using System;
using Random = System.Random;

namespace Stoicode.UniLib.AI.NeuralNetworks
{
    public class BackPropagationLayer
    {
        public readonly float[] Outputs;
        public readonly float[,] Weights;
        public readonly float[] Gamma;
        
        private readonly float[,] weightsDelta;
        private readonly float[] error;

        private readonly Random rnd = new Random();
        
        private readonly int inputCount;
        private readonly int outputCount;

        private float learnRate;
        
        private float[] inputs;


        public BackPropagationLayer(int inputCount, int outputCount, float learnRate)
        {
            this.inputCount = inputCount;
            this.outputCount = outputCount;

            inputs = new float[inputCount];
            Outputs = new float[outputCount];
            Weights = new float[outputCount, inputCount];
            weightsDelta = new float[outputCount, inputCount];
            Gamma = new float[outputCount];
            error = new float[outputCount];

            SetLearnRate(learnRate);
            InitializeWeights();
        }

        public void SetLearnRate(float value)
        {
            learnRate = value;
        }

        public void UpdateWeights()
        {
            for (var i = 0; i < outputCount; i++)
                for (var j = 0; j < inputCount; j++)
                    Weights[i, j] -= weightsDelta[i, j] * learnRate;
        }

        public float[] FeedForward(float[] inputs)
        {
            this.inputs = inputs;

            for (var i = 0; i < outputCount; i++)
            {
                Outputs[i] = 0;
                for (var k = 0; k < inputCount; k++)
                    Outputs[i] += inputs[k] * Weights[i, k];

                Outputs[i] = (float) System.Math.Tanh(Outputs[i]);
            }

            return Outputs;
        }

        public void BackPropagationOutput(float[] expected)
        {
            for (var i = 0; i < outputCount; i++)
                error[i] = Outputs[i] - expected[i];

            for (var i = 0; i < outputCount; i++)
                Gamma[i] = error[i] * TanHDerivative(Outputs[i]);

            for (var i = 0; i < outputCount; i++)
                for (var j = 0; j < inputCount; j++)
                    weightsDelta[i, j] = Gamma[i] * inputs[j];
        }

        public void BackPropagationHidden(float[] gammaForward, float[,] weightsForward)
        {
            for (var i = 0; i < outputCount; i++)
            {
                Gamma[i] = 0;

                for (var j = 0; j < gammaForward.Length; j++)
                    Gamma[i] += gammaForward[j] * weightsForward[j, i];

                Gamma[i] *= TanHDerivative(Outputs[i]);
            }

            for (var i = 0; i < outputCount; i++)
                for (var j = 0; j < inputCount; j++)
                    weightsDelta[i, j] = Gamma[i] * inputs[j];
        }
        
        private void InitializeWeights()
        {
            for (var i = 0; i < outputCount; i++)
            for (var j = 0; j < inputCount; j++)
                Weights[i, j] = (float) rnd.NextDouble() - 0.5f;
        }

        private static float TanHDerivative(float value) => 1 - value * value;
    }
}