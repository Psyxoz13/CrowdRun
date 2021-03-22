using System.Collections.Generic;
using UnityEngine;

public class PlatformsPool : MonoBehaviour
{
    [SerializeField] private float _platformsHeight = -.1f;
    [SerializeField] private List<GameObject> _platforms;

    [Space]
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private float _effectHeight = -.2f;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 unitPosition = other.transform.position;

        PlacePlatform(unitPosition);

        if (_effect)
            PlayEffect(unitPosition);
    }

    private void PlacePlatform(Vector3 targetPosition)
    {
        var platform = GetPlatform();

        platform.transform.position = new Vector3(
            targetPosition.x,
            _platformsHeight,
            targetPosition.z);
    }

    private GameObject GetPlatform()
    {
        var platform = _platforms[0];
        _platforms.RemoveAt(0);

        return platform;
    }

    private void PlayEffect(Vector3 position)
    {
        _effect.transform.position = new Vector3(
            position.x,
            _effectHeight,
            position.z);
        _effect.Play(true);
    }
}
