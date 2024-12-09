using UnityEngine;
using TMPro;

namespace _Project.Script
{
    public class MultimeterView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _displayText;
        [SerializeField] private TextMeshProUGUI _voltageLabel;  
        [SerializeField] private TextMeshProUGUI _currentLabel;  
        [SerializeField] private TextMeshProUGUI _acVoltageLabel; 
        [SerializeField] private TextMeshProUGUI _resistanceLabel;
        [SerializeField] private Renderer _knobRenderer;

        [SerializeField] private Material _highlightMaterial;
        private Material _originalMaterial;

        public Transform KnobRendererTransform => _knobRenderer.transform;
        
        private void Awake() => _originalMaterial = _knobRenderer.material;

        public void RotateKnob(float angle) => _knobRenderer.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        public void UpdateDisplay(string value) => _displayText.text = value;

        public void HighlightKnob(bool highlight) => _knobRenderer.material = highlight ? _highlightMaterial : _originalMaterial;
        
        public void UpdateLabels(float voltage, float current, float acVoltage, float resistance)
        {
            _voltageLabel.text = $"V {voltage:F2}";
            _currentLabel.text = $"A {current:F2}";
            _acVoltageLabel.text = $"~ {acVoltage:F2}";
            _resistanceLabel.text = $"Ω {resistance:F2}";
        }
    }
}