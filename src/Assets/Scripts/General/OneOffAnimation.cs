using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.General
{
    public class OneOffAnimation : MonoBehaviour
    {
        public List<Sprite> Animation;
         
        void Start ()
        {
            var ac = GetComponent<AnimationController>();
            ac.PlayAnimation(Animation, AnimationType.DestroyWhenComplete);
        }
    }
}
