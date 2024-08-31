using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Skills
{
    public class SkillHolder : MonoBehaviour
    {
        [SerializeField] private List<Skill> _skills;
        [SerializeField] private GameObject _buttonPrefab;
        [SerializeField] [ReadOnly] private Skill _chargedSkill;

        private int _mana;

        [ShowInInspector] public int Mana
        {
            get => _mana;
            set { _mana = value;/* OnManaChanged.Invoke();*/ }
        }

        public Action OnManaChanged;
        

        private void UseSkill()
        {
            if (Input.GetMouseButtonDown(1)) DischargeSkill();
            if (!Input.GetMouseButtonDown(0) || _chargedSkill == null) return;
            
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            _chargedSkill.TakeEffect(mousePosition);
            Mana -= _chargedSkill.Cost;
            
            DischargeSkill();
        }

        private void Update()
        {
            UseSkill();
        }

        private void Start()
        {
            InitializeButtons();
            Mana = 100;
        }

        private void ChargeSkill(Skill skill)
        {
            _chargedSkill = skill;
            Debug.Log($"Spell {skill.name} charged");
        }

        private void InitializeButtons()
        {
            foreach (Skill skill in _skills)
            {
                GameObject buttonObject = Instantiate(_buttonPrefab, transform);
                buttonObject.GetComponentInChildren<TextMeshProUGUI>().SetText(skill.Name);
                
                buttonObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ChargeSkill(skill);
                });
            }
        }

        private void DischargeSkill()
        {
            _chargedSkill = null;
        }
    }
}