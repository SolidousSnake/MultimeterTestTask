using UnityEngine;

namespace _Project.Script
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private MultimeterView _view;

        private MultimeterController _controller;
        
        private void Start()
        {
            var model = new MultimeterModel()
            {
                Resistance = 1000,
                Power = 400
            };
            _controller = new MultimeterController(model, _view);
        }

        private void Update()
        {
            _controller.Update();
        }
    }
}