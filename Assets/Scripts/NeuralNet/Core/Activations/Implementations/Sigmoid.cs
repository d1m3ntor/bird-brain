using System;

namespace NeuralNet.Core.Activations
{
    public class Sigmoid : BaseActivationController
    {
        public Sigmoid(ActivationModel model) : base(model)
        {
        }
        
        public override float Apply(float weightedSum)
        {
            if (weightedSum > 38.53f) return 1.0f;
            if (weightedSum < -38.53f) return 0.0f;
            
            var k = (float)Math.Exp(weightedSum);
            return k / (1.0f + k);
        }
    }
}