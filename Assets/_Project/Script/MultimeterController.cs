using UnityEngine;

namespace _Project.Script
{
    public class MultimeterController
    {
        private readonly MultimeterView _view;
        private readonly MultimeterModel _model;

        private MultimeterModel.MultimeterMode _currentMode = MultimeterModel.MultimeterMode.Neutral;
        private readonly int _modeCount = System.Enum.GetValues(typeof(MultimeterModel.MultimeterMode)).Length;

        private float _currentAngle = 0f;
        private const float _stepAngle = 360f / 5f;

        public MultimeterController(MultimeterModel model, MultimeterView view)
        {
            _view = view;
            _model = model;
        }

        public void Update()
        {
            if (IsKnobHighlighted())
            {
                _view.HighlightKnob(true);

                float scroll = Input.GetAxis("Mouse ScrollWheel");
                switch (scroll)
                {
                    case > 0f:
                        ChangeMode(1);
                        break;
                    case < 0f:
                        ChangeMode(-1);
                        break;
                }
            }
            else
                _view.HighlightKnob(false);

            UpdateLabels();
        }

        private bool IsKnobHighlighted()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                return hit.transform == _view.KnobRendererTransform;

            return false;
        }

        private void ChangeMode(int direction)
        {
            int nextMode = ((int)_currentMode + direction + _modeCount) % _modeCount;
            _currentMode = (MultimeterModel.MultimeterMode)nextMode;

            _currentAngle = nextMode * _stepAngle;
            _view.RotateKnob(_currentAngle);

            string value = _model.GetDisplayValue(_currentMode);
            _view.UpdateDisplay(value);

            UpdateLabels();
        }

        private void UpdateLabels()
        {
            float voltage = 0f;
            float current = 0f;
            float acVoltage = 0f;
            float resistance = _model.Resistance;

            switch (_currentMode)
            {
                case MultimeterModel.MultimeterMode.VoltageDC:
                    voltage = Mathf.Sqrt(_model.Power * _model.Resistance);
                    break;
                case MultimeterModel.MultimeterMode.Current:
                    current = Mathf.Sqrt(_model.Power / _model.Resistance);
                    break;
                case MultimeterModel.MultimeterMode.VoltageAC:
                    acVoltage = 0.01f;
                    break;
            }

            _view.UpdateLabels(voltage, current, acVoltage, resistance);
        }
    }
}