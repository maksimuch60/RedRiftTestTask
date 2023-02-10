using UnityEngine;

namespace Hand
{
    public struct CustomTransform
    {
        private Vector3 _position;
        private Vector3 _back;

        public Vector3 Position => _position;
        public Vector3 Back => _back;

        public CustomTransform(Vector3 position, Vector3 back)
        {
            _position = position;
            _back = back;
        }
    }
}