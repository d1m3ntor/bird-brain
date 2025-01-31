using System;
using NeuralNet.Core;
using UnityEngine;

namespace NeuralNet
{
    public abstract class NeuralBehaviour : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        public event Action<NeuralBehaviour> onFailed;
        public BrainController brain { get; protected set; }

        public void SetBrain(BrainController brain)
        {
            this.brain = brain;
        }

        public void AddFitness(float fitnessValue)
        {
            brain.Data.fitness += fitnessValue;
        }

        public abstract void UseBrain();

        protected void OnFailed()
        {
            onFailed?.Invoke(this);
        }
    }
}