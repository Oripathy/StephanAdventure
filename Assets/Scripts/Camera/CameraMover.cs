using Player;
using UnityEngine;

namespace Camera
{
    internal class CameraMover : MonoBehaviour
    {
        [SerializeField] private LayerMask _includedLayers;
        [SerializeField] private GameObject _transparencySphere;
    
        private Transform _player;
        private Vector3 _offset;
        private string _transparencySphereTag = "TransparencySphere";

        private void Awake()
        {
            _player = FindObjectOfType<PlayerEntity>().GetComponent<Transform>();
            _offset = new Vector3(0f, 10f, -6f);
            transform.Rotate(60f, 0f, 0f, Space.World);
        }   

        private void Update()
        {
            if (!_player)
                return;

            FollowPlayer();
            CastRayToTransparencySphere();
        }

        private void CastRayToTransparencySphere()
        {
            if (Physics.Raycast(transform.position,
                    (_transparencySphere.transform.position - transform.position).normalized, out var hit,
                    Mathf.Infinity, _includedLayers))
            {
                if (hit.collider.CompareTag("Player"))
                    _transparencySphere.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                else
                    _transparencySphere.transform.localScale = new Vector3(4f, 4f, 4f);
            }
        }

        private void FollowPlayer()
        {
            if (_player.position.z > 1.7 && _player.position.z < 44)
                transform.position = new Vector3(_player.position.x + _offset.x, _offset.y, _player.position.z + _offset.z);
            else
                transform.position = new Vector3(_player.position.x + _offset.x, _offset.y, transform.position.z);

        }
    }
}