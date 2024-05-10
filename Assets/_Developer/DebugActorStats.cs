using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using UnityEngine;

namespace RoboStruct.Debugging
{
    /// <summary>
    /// WARNING: Debugging scripts are not made to be efficient or optimised. They are used for Unity Editor debugging purposes and are not included in the release build.
    /// </summary>
    public class DebugActorStats : MonoBehaviour
    {
        public GameObject DisplayedText;
        [SerializeField] private List<TextMeshProUGUI> _enemyText = new List<TextMeshProUGUI>();
        [SerializeField] private List<TextMeshProUGUI> _playerText = new List<TextMeshProUGUI>();

        private void Start()
        {
            if (!UnityEngine.Debug.isDebugBuild) Destroy(this);
        }

        private void Update()
        {
            if (UnityEngine.Debug.isDebugBuild)
            {
                UpdateStats<Enemy>(_enemyText, actor => Mathf.RoundToInt(actor.HitPoints).ToString());
                UpdateStats<Player>(_playerText, actor => Mathf.RoundToInt(actor.HitPoints).ToString());
            }
        }
        
        /// <summary>
        /// WARNING: The values displayed are NOT precise or accurate; for testing purposes only!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textList"></param>
        /// <param name="valueGetter"></param>
        private void UpdateStats<T>(List<TextMeshProUGUI> textList, Func<T, string> valueGetter) where T : MonoBehaviour
        {
            T[] actors = FindObjectsOfType<T>(true);

            foreach (T actor in actors)
            {
                if (actor == null) continue;

                Rigidbody2D rb = actor.GetComponentInChildren<Rigidbody2D>();
                if (rb != null && rb.transform.Find(DisplayedText.name) != null) continue;

                GameObject actorCanvas = Instantiate(DisplayedText, rb != null ? rb.transform : actor.transform);
                actorCanvas.name = DisplayedText.name;
                actorCanvas.SetActive(true);

                TextMeshProUGUI actorText = actorCanvas.GetComponentInChildren<TextMeshProUGUI>();
                textList.Add(actorText);
            }

            if (textList.Count > 0)
            {
                for (int i = 0; i < textList.Count; i++)
                {
                    if (textList[i] == null) textList.Remove(textList[i]);
                    else textList[i].text = valueGetter(actors[i]);
                }
            }
        }
    }
}
