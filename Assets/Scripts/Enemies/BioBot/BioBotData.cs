using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BioBotData")]
internal class BioBotData : ScriptableObject
{
    [SerializeField] private float _movementSpeed = 2f;
    [SerializeField] private float _scareSphereRadius;
    [SerializeField] private float _actionRadius = 4f;
    [SerializeField] private float _fearAmountToReceive = 2f;
    [SerializeField] private float _fearAmountToReceiveDuringAction = 20f;
    [SerializeField] private float _waitTime = 2f;

    public float MovementSpeed => _movementSpeed;
    public float ScareSphereRadius => _scareSphereRadius;
    public float ActionRadius => _actionRadius;
    public float FearAmountToReceive => _fearAmountToReceive;
    public float FearAmountToReceiveDuringAction => _fearAmountToReceiveDuringAction;
    public float WaitTime => _waitTime;
}