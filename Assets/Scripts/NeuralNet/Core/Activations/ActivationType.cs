using System;

namespace NeuralNet.Core.Activations
{
    [Serializable]
    public enum ActivationType
    {
        Sigmoid,
        Relu,
        Leakyrelu,
        Tanh
    }
}