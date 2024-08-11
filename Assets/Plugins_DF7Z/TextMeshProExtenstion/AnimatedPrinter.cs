using System;
using TMPro;
using UnityEditor.VersionControl;
using Task = System.Threading.Tasks.Task;

namespace Game.TextMeshProExtenstion
{

    [Serializable]
    public class AnimatedPrinterSettings
    {
        public TextMeshProUGUI TextMeshProUGUI;
        public string[] Texts;
        public float TextStartDelay = 0.3f;
        public float TextShowDelay = 2f;
        public float PrintDelay = 0.1f;
    }
    
    public class AnimatedPrinter
    {
        private AnimatedPrinterSettings _settings;
        private bool _isActive;
        private int _printDelayMS;
        private int _textShowDelayMS;

        public Action OnPrint;
        
        public AnimatedPrinter(AnimatedPrinterSettings settings)
        {
            _settings = settings;
            _isActive = false;
            _printDelayMS = (int) (_settings.PrintDelay * 1000f);
            _textShowDelayMS = (int) (_settings.TextShowDelay * 1000f);
            ClearTextUI();
        }

        public void Start()
        {
            if (_isActive) return;
            
            _isActive = true;

            StartPrint();
        }

        public void Terminate()
        {
            _isActive = false;
        }
        
        private async void StartPrint()
        {
#if UNITY_EDITOR
            if (_settings is null) throw new Exception("Settings null!");
            if (_settings.Texts is null) throw new Exception("Settings texts null!");
#endif
            ClearTextUI();
            
            await Task.Delay((int) (_settings.TextStartDelay * 1000f));
            
            for (int i = 0; i < _settings.Texts.Length; i++)
            {
                ClearTextUI();
                
                var text = _settings.Texts[i];
                
                for (int j = 0; j < _settings.Texts[i].Length; j++)
                {
                    var s = text[j];

                    _settings.TextMeshProUGUI.text += s;

                    OnPrint?.Invoke();
                    
                    await Task.Delay(_printDelayMS);
                    
                    if (!_isActive) return;
                }

                await Task.Delay(_textShowDelayMS);
                
                if (!_isActive) return;
            }
            
            End();
        }

        private void ClearTextUI() => _settings.TextMeshProUGUI.text = string.Empty;
        
        private void End()
        {
            _isActive = false;
        }
        
    }
}