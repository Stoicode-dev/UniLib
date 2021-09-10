using System;
using System.Collections;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Stoicode.UniLib.AI.NeuralNetworks
{
    [Serializable]
    [MessagePackObject(true)]
    public class BackPropagationNetwork
    {
        public int[] LayerSetup { get; private set; }
        public BackPropagationLayer[] Layers { get; private set; }


        public BackPropagationNetwork(int[] layerSetup, float learnRate)
        {
            LayerSetup = new int[layerSetup.Length];
            for (var i = 0; i < layerSetup.Length; i++)
                LayerSetup[i] = layerSetup[i];

            Layers = new BackPropagationLayer[layerSetup.Length - 1];

            for (var i = 0; i < Layers.Length; i++)
                Layers[i] = new BackPropagationLayer(
                    layerSetup[i], layerSetup[i + 1], learnRate);
        }

        public float[] FeedForward(float[] inputs)
        {
            Layers[0].FeedForward(inputs);

            for (var i = 1; i < Layers.Length; i++)
                Layers[i].FeedForward(Layers[i - 1].Outputs);

            return Layers[Layers.Length - 1].Outputs;
        }

        public void BackPropagation(float[] expected)
        {
            for (var i = Layers.Length - 1; i >= 0; i--)
            {
                if (i == Layers.Length - 1)
                    Layers[i].BackPropagationOutput(expected);
                else
                    Layers[i].BackPropagationHidden(Layers[i + 1].Gamma, Layers[i + 1].Weights);
            }

            for (var i = 0; i < Layers.Length; i++)
                Layers[i].UpdateWeights();
        }
    }
}