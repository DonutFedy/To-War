using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDataBase
{
    [Serializable]
    public class BasicStatus
    {
        public float Hp;
        public float Mp;
        public float AttackSpeed;
        public float AttackRange;
        public float AttackPower;
        public float CriticalChance;
        public float Deffence;
        public float DeffencePentraction;
        public float Evasive;
        public float MoveSpeed;

        public static BasicStatus operator *(BasicStatus _soruceS, float _status)
        {
            return new BasicStatus(_soruceS.Hp * _status, _soruceS.Mp * _status, _soruceS.AttackSpeed * _status, _soruceS.AttackRange * _status, _soruceS.AttackPower * _status, _soruceS.CriticalChance * _status, _soruceS.Deffence * _status, _soruceS.DeffencePentraction * _status, _soruceS.Evasive * _status, _soruceS.MoveSpeed * _status);
        }

        public static BasicStatus operator + (BasicStatus _source, BasicStatus _status)
        {
            return new BasicStatus(_source.Hp + _status.Hp, _source.Mp + _status.Mp, _source.AttackSpeed + _status.AttackSpeed, _source.AttackRange + _status.AttackRange, _source.AttackPower + _status.AttackPower, _source.CriticalChance + _status.CriticalChance, _source.Deffence + _status.Deffence, _source.DeffencePentraction + _status.DeffencePentraction, _source.Evasive + _status.Evasive, _source.MoveSpeed + _status.MoveSpeed);
        }
        public static BasicStatus operator -(BasicStatus _source, BasicStatus _status)
        {
            return new BasicStatus(_source.Hp - _status.Hp, _source.Mp - _status.Mp, _source.AttackSpeed - _status.AttackSpeed, _source.AttackRange - _status.AttackRange, _source.AttackPower - _status.AttackPower, _source.CriticalChance - _status.CriticalChance, _source.Deffence - _status.Deffence, _source.DeffencePentraction - _status.DeffencePentraction, _source.Evasive - _status.Evasive, _source.MoveSpeed - _status.MoveSpeed);
        }

        public BasicStatus(float hp, float mp, float attackSpeed, float attackRange, float attackPower, float criticalChance, float diffence, float diffencePentraction, float evasive, float moveSpeed)
        {
            SetData(hp, mp, attackSpeed, attackRange, attackPower, criticalChance, diffence, diffencePentraction, evasive, moveSpeed);
        }
        public BasicStatus()
        {
            SetData(0,0,0,0,0,0,0,0,0,0);
        }

        private void SetData(float hp, float mp, float attackSpeed, float attackRange, float attackPower, float criticalChance, float diffence, float diffencePentraction, float evasive, float moveSpeed)
        {
            Hp = hp;
            Mp = mp;
            AttackSpeed = attackSpeed;
            AttackRange = attackRange;
            AttackPower = attackPower;
            CriticalChance = criticalChance;
            Deffence = diffence;
            DeffencePentraction = diffencePentraction;
            Evasive = evasive;
            MoveSpeed = moveSpeed;
        }

        public void AddStatus(BasicStatus status)
        {
            Hp += status.Hp;
            Mp += status.Mp;
            AttackSpeed += status.AttackSpeed;
            AttackRange += status.AttackRange;
            AttackPower += status.AttackPower;
            CriticalChance += status.CriticalChance;
            Deffence += status.Deffence;
            DeffencePentraction += status.DeffencePentraction;
            Evasive += status.Evasive;
            MoveSpeed += status.MoveSpeed;
        }
    }
}