using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Equipment
{
    public int id;
    public string name;
    public Sprite icon;
}

[Serializable]
public class EquipmentContext
{
    [SerializeField] private Equipment[] _equipments;
    [SerializeField] private List<int> _repositoryEquipments;
    [SerializeField] private List<int> _equippedEquipments;

    public Equipment[] equipments { get => _equipments; }
    public List<int> repositoryEquipments { get => _repositoryEquipments; }
    public List<int> equippedEquipments { get => _equippedEquipments; }

    public int[] GetNewGainEquipments()
    {
        var repoCount = _repositoryEquipments.Count;
        var equippedCount = _equippedEquipments.Count;
        var diffCount = equippedCount - repoCount;
        if (diffCount == 0) return new int[0];
        repositoryEquipments.Sort();
        equippedEquipments.Sort();
        var rt = new int[diffCount];
        { 
            var i = 0;
            var j = 0;
            var k = 0;
            while (k < diffCount)
            {
                if (equippedEquipments[i] != repositoryEquipments[j])
                {
                    rt[k++] = equippedEquipments[i];
                }
                else
                {
                    j++;
                }
                i++;
            }
        }
        return rt;
    }
}

[Serializable]
public class ApplicationContext
{
    [SerializeField] EquipmentContext _equipment;

    public EquipmentContext equipment { get => _equipment; set => _equipment = value; }
}
namespace CompanyName.Domain
{
    [Serializable]
    public struct ApplicationConfiguration
    {
        [Serializable]
        public struct Scenes
        {
            [SerializeField] private string _mainMenu;
            [SerializeField] private string _game;

            public string mainMenu { get => _mainMenu; }
            public string game { get => _game; }
        }

        [Serializable]
        public struct Keymap
        {
            [SerializeField] private KeyCode _cancel;

            public KeyCode cancel { get => _cancel; }
        }
        [SerializeField] private Scenes _scenes;
        [SerializeField] private Keymap _keymap;

        public Scenes scenes { get => _scenes; }
        public Keymap keymap { get => _keymap; }
    }
}