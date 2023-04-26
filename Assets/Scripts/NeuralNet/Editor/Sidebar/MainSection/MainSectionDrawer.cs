using System;
using System.Collections.Generic;
using System.IO;
using NeuralNet.Core;
using NeuralNet.Core.Layers;
using NeuralNet.Core.Neurons.Abstract;
using NeuralNet.Core.Neurons.Output;
using NeuralNet.Editor.Abstract;
using NeuralNet.Editor.NeuralNetwork;
using NeuralNet.Editor.Workspace;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Sidebar.MainSection
{
    public class MainSectionDrawer : StylizedDrawer<MainSectionModel>
    {
        public event Action OnSelectedNetworkDataChanged;
        
        private Rect rect;
        private WindowState state => NeuralEditorWindow.Instance.state;
        private WorkspaceModel workspaceModel;

        protected override void ApplyStyles()
        {
            rect = new Rect(new Vector2(10, 20), new Vector2(220, 200));
        }

        public override void Draw(MainSectionModel args)
        {
            GUILayout.BeginArea(rect);
            var previousNetworkData = state.SelectedNetworkData;
            state.SelectedNetworkData = EditorGUILayout.ObjectField("", state.SelectedNetworkData, typeof(NeuralNetworkData), false) as NeuralNetworkData;
            if (previousNetworkData != state.SelectedNetworkData)
            {
                Debug.Log(true);
                OnSelectedNetworkDataChanged?.Invoke();
            }
            
            if (GUILayout.Button("Import network"))
            {
                var path = EditorUtility.OpenFilePanel("Choose neural network asset", "Assets", "json");
                state.SelectedNetworkData = JsonConvert.DeserializeObject<NeuralNetworkData>(path);
                workspaceModel.GraphModel = new GraphModel(state.SelectedNetworkData);
            }
            
            if (GUILayout.Button("Save network"))
            {
                var s = EditorUtility.SaveFilePanel("Save network asset", "Assets", "brain", "json");
                var inputLayer = new Layer<BaseNeuron>(new List<BaseNeuron>(), 0);
                var weightedLayers = new List<Layer<WeightedNeuron>>();
                var outputLayer = new Layer<WeightedNeuron>(new List<WeightedNeuron>(), 0);
                var neuralNetworkModel = ScriptableObject.CreateInstance<NeuralNetworkData>();
                if (s.Length > 0) File.WriteAllText(s, JsonConvert.SerializeObject(neuralNetworkModel));
            }
            
            GUILayout.EndArea();
        }
    }
}