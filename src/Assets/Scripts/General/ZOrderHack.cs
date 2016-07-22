using UnityEngine;

namespace Assets.Scripts.General
{
    public class ZOrderHack : MonoBehaviour
    {
        void Start()
        {
            var nextLayer = StaticBullshitHack.GetNextCraterSortingOrder();
            Debug.Log(nextLayer);
            GetComponentInChildren<SpriteRenderer>().sortingOrder = nextLayer;
        }
    }

    public static class StaticBullshitHack
    {
        private static int _bullshit;

        public static int GetNextCraterSortingOrder()
        {
            return _bullshit++;
        }
    }
}