using System;
using System.Collections.Generic;
using MessagePack;

namespace Stoicode.UniLib.AI.NeuralNetworks
{
    [Serializable]
    [MessagePackObject(true)]
    public class MutationNetwork : IComparable<MutationNetwork>
    {
        public float Fitness { get; set; }

        public int[] Layers { get; set; }

        public float[][] Neurons { get; set; }
        public float[][][] Weights { get; set; }

        public string GeneticInfo { get; set; }


        public MutationNetwork() { }

        public MutationNetwork(int[] layers)
        {
            Layers = new int[layers.Length];

            for (var i = 0; i < layers.Length; i++)
                Layers[i] = layers[i];

            InitializeNeurons();
            InitializeWeights();
        }

        public MutationNetwork(MutationNetwork network, bool random = false)
        {
            Layers = new int[network.Layers.Length];

            for (var i = 0; i < Layers.Length; i++)
                Layers[i] = network.Layers[i];

            InitializeNeurons();
            InitializeWeights();
            CopyWeights(network.Weights);

            if (random)
                MutateRandom();
        }

        public MutationNetwork(MutationNetwork parent1, MutationNetwork parent2, bool random = false)
        {
            Layers = new int[parent1.Layers.Length];

            for (var i = 0; i < Layers.Length; i++)
                Layers[i] = parent1.Layers[i];

            InitializeNeurons();
            InitializeWeights();
            MutateFromParents(parent1, parent2, random);
        }
        
        /// <summary>
        /// IComparable implementation
        /// </summary>
        /// <param name="other">Other MutationNetwork</param>
        /// <returns></returns>
        public int CompareTo(MutationNetwork other)
        {
            if (other == null) return 1;

            return Fitness < other.Fitness ? -1 : 1;
        }

        /// <summary>
        /// Feed forward this neural network with a given input array
        /// </summary>
        /// <param name="inputs">Inputs to network</param>
        /// <returns></returns>
        public float[] FeedForward(float[] inputs)
        {
            // Add to neuron matrix
            for (var i = 0; i < inputs.Length; i++)
                Neurons[0][i] = inputs[i];

            // Compute values
            for (var i = 1; i < Layers.Length; i++)
            {
                for (var j = 0; j < Neurons[i].Length; j++)
                {
                    var value = 0f;

                    // Sum up weight connections
                    for (var k = 0; k < Neurons[i - 1].Length; k++)
                        value += Weights[i - 1][j][k] * Neurons[i - 1][k];

                    // Hyperbolic tangent activation
                    Neurons[i][j] = (float) System.Math.Tanh(value);
                }
            }

            return Neurons[Neurons.Length - 1];
        }

        /// <summary>
        /// Mutate neural network weights randomly
        /// </summary>
        public void MutateRandom()
        {
            for (var i = 0; i < Weights.Length; i++)
                for (var j = 0; j < Weights[i].Length; j++)
                    for (var k = 0; k < Weights[i][j].Length; k++)
                        Weights[i][j][k] = MutateWeight(Weights[i][j][k]);
        }

        /// <summary>
        /// Mutate neural network with parents
        /// </summary>
        public void MutateFromParents(MutationNetwork parent1, MutationNetwork parent2, bool random = false)
        {
            for (var i = 0; i < Neurons.Length; i++)
            {
                for (var j = 0; j < Neurons[i].Length; j++)
                {
                    var p1 = UnityEngine.Random.Range(0, 2) == 0;
                    Neurons[i][j] = p1 ? parent1.Neurons[i][j] : parent2.Neurons[i][j];
                }
            }

            for (var i = 0; i < Weights.Length; i++)
            {
                for (var j = 0; j < Weights[i].Length; j++)
                {
                    for (var k = 0; k < Weights[i][j].Length; k++)
                    {
                        Weights[i][j][k] = UnityEngine.Random.Range(0, 2) == 0
                            ? parent1.Weights[i][j][k]
                            : parent2.Weights[i][j][k];
                    }
                }
            }

            if (random)
                MutateRandom();
        }
        
        /// <summary>
        /// Create neuron matrix
        /// </summary>
        private void InitializeNeurons()
        {
            var neuronsList = new List<float[]>();

            // Add layers to neuron list
            for (var i = 0; i < Layers.Length; i++)
                neuronsList.Add(new float[Layers[i]]);

            Neurons = neuronsList.ToArray();
        }

        /// <summary>
        /// Create weights matrix.
        /// </summary>
        private void InitializeWeights()
        {
            // Weight list
            var weightsList = new List<float[][]>();

            for (var i = 1; i < Layers.Length; i++)
            {
                var layerWeightsList = new List<float[]>();

                var neuronsInPreviousLayer = Layers[i - 1];

                for (var j = 0; j < Neurons[i].Length; j++)
                {
                    var neuronWeights = new float[neuronsInPreviousLayer];

                    // Set weights randomly between 0.5f and -0.5
                    for (var k = 0; k < neuronsInPreviousLayer; k++)
                        neuronWeights[k] = UnityEngine.Random.Range(-0.5f, 0.5f);

                    // Add neuron weights of this current layer to layer weights
                    layerWeightsList.Add(neuronWeights);
                }

                // Add weights to list
                weightsList.Add(layerWeightsList.ToArray());
            }

            // Convert to 3D array
            Weights = weightsList.ToArray();
        }

        /// <summary>
        /// Copy Weights
        /// </summary>
        /// <param name="weights">Weights to copy</param>
        private void CopyWeights(float[][][] weights)
        {
            for (var i = 0; i < Weights.Length; i++)
                for (var j = 0; j < Weights[i].Length; j++)
                    for (var k = 0; k < Weights[i][j].Length; k++) 
                        Weights[i][j][k] = weights[i][j][k];
        }

        /// <summary>
        /// Mutate a weight
        /// </summary>
        /// <param name="weight">Target weight</param>
        /// <returns>Mutated weight</returns>
        private static float MutateWeight(float weight)
        {
            var random = UnityEngine.Random.Range(0, 3);

            switch (random)
            {
                // Flip sign of weight
                case 0:
                    weight *= -1f;
                    break;
                
                // Pick random weight between -0.5 and 0.5
                case 1:
                    weight = UnityEngine.Random.Range(-0.5f, 0.5f);
                    break;
                
                // Randomly change by 0% - 100%
                case 2:
                    var factor = UnityEngine.Random.Range(0f, 1f) + 1f;
                    weight *= factor;
                    break;
            }

            return weight;
        }
    }
}