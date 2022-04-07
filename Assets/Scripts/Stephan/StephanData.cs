using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StephanData", order = 3)]
internal class StephanData : ScriptableObject
{
    [SerializeField] private float _fearAmountLimit = 10f;
    [SerializeField] private float _maxSize = 10f;

    public float FearAmountLimit => _fearAmountLimit;
    public float MaxSize => _maxSize;
}