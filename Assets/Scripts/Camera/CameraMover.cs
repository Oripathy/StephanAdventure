using System;
using UnityEngine;

internal class CameraMover : MonoBehaviour
{
    [SerializeField] private LayerMask _transparencySphereMask;
    [SerializeField] private GameObject _transparencySphere;
    
    private Transform _player;
    private Vector3 _offset;

    private void Awake()
    {
        _player = FindObjectOfType<Player>().GetComponent<Transform>();
        _offset = new Vector3(0f, 15f, -6f);
        transform.Rotate(0f, 0f, 0f, Space.World);
    }

    private void Update()
    {
        if (!_player)
            return;

        transform.position = new Vector3(_player.position.x + _offset.x, _player.position.y + _offset.y,
            _player.position.z + _offset.z);
        CastRayToTransparencySphere();
    }

    private void CastRayToTransparencySphere()
    {
        if (Physics.Raycast(transform.position,
                (_transparencySphere.transform.position - transform.position).normalized, out var hit,
                Mathf.Infinity))
        {
            if (hit.transform.tag == "Player")
                _transparencySphere.transform.localScale = new Vector3(0f, 0f, 0f);
            else
                _transparencySphere.transform.localScale = new Vector3(4f, 4f, 4f);
        }
    }
}