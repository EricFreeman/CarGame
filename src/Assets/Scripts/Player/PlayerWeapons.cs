using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerWeapons : MonoBehaviour
    {
        private List<Gun> _weapons;

        void Start()
        {
            _weapons = GetComponentsInChildren<Gun>().ToList();
        }
         
        void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _weapons.ForEach(x => x.Shoot());
            }
        }
    }
}