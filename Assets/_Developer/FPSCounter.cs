using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoboStruct.Debugging
{
    /// <summary>
    /// WARNING: Debugging scripts are not made to be efficient or optimised. They are used for Unity Editor debugging purposes and are not included in the release build.
    /// </summary>
    public class FPSCounter : MonoBehaviourSingletonPersistent<FPSCounter>
    {
        [SerializeField] private Text Text;

        private Dictionary<int, string> CachedNumberStrings = new();
        private int[] _frameRateSamples;
        private int _cacheNumbersAmount = 300;
        private int _averageFromAmount = 30;
        private int _averageCounter = 0;
        private int _currentAveraged;

        void Start()
        {
            {
                for (int i = 0; i < _cacheNumbersAmount; i++)
                {
                    CachedNumberStrings[i] = i.ToString();
                }
                _frameRateSamples = new int[_averageFromAmount];
            }
        }
        void Update()
        {
            {
                var currentFrame = (int)Math.Round(1f / Time.smoothDeltaTime);
                _frameRateSamples[_averageCounter] = currentFrame;
            }

            {
                var average = 0f;

                foreach (var frameRate in _frameRateSamples)
                {
                    average += frameRate;
                }

                _currentAveraged = (int)Math.Round(average / _averageFromAmount);
                _averageCounter = (_averageCounter + 1) % _averageFromAmount;
            }

            {
                Text.text = _currentAveraged switch
                {
                    var x when x >= 0 && x < _cacheNumbersAmount => CachedNumberStrings[x],
                    var x when x >= _cacheNumbersAmount => $"> {_cacheNumbersAmount}",
                    var x when x < 0 => "< 0",
                    _ => "?"
                };
            }
        }
    }

}
