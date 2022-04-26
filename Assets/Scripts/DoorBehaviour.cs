using System.Collections;
using Player;
using UnityEngine;

internal class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 _openPosition;
    [SerializeField] private Vector3 _closePosition;
    [SerializeField] private GameObject _door;

    private float _step = 0.2f;
    private Coroutine _currentCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerEntity>(out var player))
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(OpenDoor());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerEntity>(out var player))
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(CloseDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        Debug.Log("Opening");
        while (Vector3.MoveTowards(_door.transform.position, _openPosition, _step) != _openPosition)
        {
            _door.transform.position = Vector3.MoveTowards(_door.transform.position, _openPosition, _step);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator CloseDoor()
    {
        Debug.Log("Closing");
        while (Vector3.MoveTowards(_door.transform.position, _closePosition, _step) != _closePosition)
        {
            _door.transform.position = Vector3.MoveTowards(_door.transform.position, _closePosition, _step);
            yield return new WaitForFixedUpdate();
        }
    }
}