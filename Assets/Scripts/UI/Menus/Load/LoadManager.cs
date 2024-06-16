using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Manages the loading UI </summary>
    [RequireComponent(typeof(FractalLoader))]
    public class LoadManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI NameText;

        [SerializeField] SaveManager SaveManager;

        FractalLoader _loader;

        public IEnumerable<string> SavedFractals => !Directory.Exists(FractalSaver.SavePath) ?
            Enumerable.Empty<string>() :
            Directory.EnumerateFiles(FractalSaver.SavePath, "*" + FractalSaver.FileExtension);

        string FractalName(string path) => new FileInfo(path)
            .Name
            .Split('.')
            .First();

        int _selectedFractalIndex;

        void Start()
        {
            _loader = GetComponent<FractalLoader>();

            var savedFractals = SavedFractals.ToArray();
            
            if (savedFractals.Length == 0)
            {
                _selectedFractalIndex = -1;
            }
            else
            {
                _selectedFractalIndex = 0;
                NameText.text = FractalName(savedFractals[0]);
            }

            // if there are no fractals saved, when one is saved will be the selected one
            SaveManager.OnFractalSave += () =>
            {
                if (_selectedFractalIndex == -1)
                {
                    _selectedFractalIndex = 0;
                    ChangeFractal(0);
                }
            };
        }

        public void LoadCurrentFractal()
        {
            if (_selectedFractalIndex < 0)
            {
                return;
            }

            var file = SavedFractals.ElementAt(_selectedFractalIndex);
            _loader.Load(File.ReadAllLines(file));
        }

        public void DeleteCurrentFractal()
        {
            var savedFractals = SavedFractals.ToArray();

            if (savedFractals.Length == 0)
            {
                return;
            }

            File.Delete(savedFractals[_selectedFractalIndex]);
            
            if (savedFractals.Length == 1)
            {
                _selectedFractalIndex = -1;
                NameText.text = "...";
            }

            ChangeFractal(0);
        }

        public void ChangeFractal(int next)
        {
            if (_selectedFractalIndex < 0)
            {
                return;
            }

            var savedFractals = SavedFractals.ToArray();

            _selectedFractalIndex += next;
            _selectedFractalIndex += savedFractals.Length;
            _selectedFractalIndex %= savedFractals.Length;

            NameText.text = FractalName(savedFractals[_selectedFractalIndex]);
        }
    }
}