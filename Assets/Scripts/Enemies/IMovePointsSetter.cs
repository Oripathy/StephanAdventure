using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    internal interface IMovePointsSetter
    {
        public void SetMovePoints(List<Transform> movePoints);
    }
}