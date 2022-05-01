using UnityEngine;

namespace Enemies
{
    internal interface IProvocable
    {
        public void Provoke(Vector3 decoyPosition);
    }
}