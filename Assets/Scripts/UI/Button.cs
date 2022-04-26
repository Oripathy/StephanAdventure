using System;
using UnityEngine;
using Player;

namespace UI
{
    internal abstract class Button : MonoBehaviour
    {
        public abstract void OnButtonPressed();
    }
}