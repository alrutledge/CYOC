using UnityEngine;

namespace Assets.Scripts.ChoiceEngine
{
    public class AnalyticsManager : MonoBehaviour
    {

        private void Awake()
        {
            AndroidGoogleAnalytics.instance.StartTracking();
        }

    }
}
