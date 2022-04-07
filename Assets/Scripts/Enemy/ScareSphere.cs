using UnityEngine;

internal abstract class ScareSphere : MonoBehaviour
{
    [SerializeField] private protected Collider _collider;
    
    public float FearAmountToReceive { get; private protected set; }
}