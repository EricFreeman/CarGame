using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.UI;
using UnityEventAggregator;

namespace Assets.Scripts.Player
{
    public class PlayerWeapons : MonoBehaviour
    {
        public float MaxHeat;
        public float CurrentHeat;
        public float CoolingSpeed;
        public float OverheatCooldown;
        public Image HeatImage;

        private List<Gun> _weapons;
        private bool _isOverheating;

        void Start()
        {
            _weapons = GetComponentsInChildren<Gun>().ToList();
        }
         
        void Update()
        {
            UpdateHeatSprite();

            if (Input.GetKey(KeyCode.Space) && !_isOverheating)
            {
                _weapons
                    .Where(x => x.CanShoot())
                    .Each(x =>
                    {
                        CurrentHeat = Mathf.Min(CurrentHeat + x.Heat, MaxHeat);
                        x.Shoot();
                    });

                if (CurrentHeat >= MaxHeat)
                {
                    _isOverheating = true;
                }
            }
            else
            {
                CurrentHeat = Mathf.Max(CurrentHeat - CoolingSpeed * Time.deltaTime, 0);

                if (Math.Abs(CurrentHeat) < .01f)
                {
                    _isOverheating = false;
                }
            }
        }

        private void UpdateHeatSprite()
        {
            var percent = CurrentHeat / MaxHeat;
            HeatImage.fillAmount = percent;

            if (_isOverheating)
            {
                HeatImage.color = new Color(.5f, .5f, .5f);
            }
            else
            {
                HeatImage.color = Color.Lerp(Color.yellow, Color.red, percent);
            }
        }
    }
}