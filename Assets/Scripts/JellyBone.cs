using System.Collections;
using UnityEngine;

public class JellyBone : MonoBehaviour
{
    [SerializeField] private float _dumping = 2f;
    [SerializeField] private float _vibrationFadeSpeed = 1f;

    public float _aplitude;
    public float _vibrationSpeed;

    private Vector3 _startPosition;

    private bool _isVibrated;

    private void Start()
    {
        _startPosition = transform.localPosition;
    }

    public void Vibrate(Vector3 velocity)
    {
        if (_isVibrated == false)
        {
            _isVibrated = true;
            StartCoroutine(GetVibrationTime(velocity));
        }
        else
        {
            //_aplitude += GetAmplitude(velocity);
            _vibrationSpeed += velocity.magnitude;
        }
    }

    private void ResetPosition()
    {
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            _startPosition,
            _dumping * Time.deltaTime);
    }

    private IEnumerator GetVibrationTime(Vector3 velocity)
    {
        Vector3 axis = velocity.normalized;
        _vibrationSpeed = velocity.sqrMagnitude * 25f;


        StartCoroutine(
            GetAplitudeFade(
                 GetAmplitude(velocity)));

        while (CheckNullLessAmplitude() == false)
        {
            float vibrateAplitude = ( Mathf.Sin(Time.time * _vibrationSpeed)) * _aplitude * Time.deltaTime;

            transform.position += axis * vibrateAplitude;

            ResetPosition();

            yield return null;
        }
        _isVibrated = false;
    }
    
    private IEnumerator GetAplitudeFade(float startAmplitude)
    {
        _aplitude = startAmplitude;

        while (CheckNullLessAmplitude() == false)
        {
            _aplitude = Mathf.Lerp(
                _aplitude,
                -.1f,
                _vibrationFadeSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private bool CheckNullLessAmplitude() =>
        _aplitude <= 0f;

    private float GetAmplitude(Vector3 velocity) =>
        velocity.magnitude / _dumping;
}
