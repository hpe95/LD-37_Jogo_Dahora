using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothScaling : MonoBehaviour {
    private Transform objeto;

    private float _currentScale = InitScale;
    private const float TargetScale = 1.2f;
    private const float InitScale = 1f;
    private const int FramesCount = 100;
    private const float AnimationTimeSeconds = 0.1f;
    private float _deltaTime = AnimationTimeSeconds / FramesCount;
    private float _dx = (TargetScale - InitScale) / FramesCount;
    private bool _upScale = true;

    private IEnumerator Breath()
    {
        while (true)
        {
            while (_upScale)
            {
                _currentScale += _dx;
                if (_currentScale > TargetScale)
                {
                    _upScale = false;
                    _currentScale = TargetScale;
                }
                objeto.localScale = Vector2.one * _currentScale;
                yield return new WaitForSeconds(_deltaTime);
            }

            while (!_upScale)
            {
                _currentScale -= _dx;
                if (_currentScale < InitScale)
                {
                    _upScale = true;
                    _currentScale = InitScale;
                }
                objeto.localScale = Vector2.one * _currentScale;
                yield return new WaitForSeconds(_deltaTime);
            }
        }
    }

    public void StartThis()
    {
        objeto = GetComponent<Transform>();
        StartCoroutine(Breath());
    }
}
