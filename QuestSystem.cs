using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "QuestSystem", order = 1)]

public class QuestSystem : ScriptableObject
{
    public Mision[] misions;
    // public float precisionDestino = 1.5f;
    [System.Serializable]
    public struct Mision
    {
        public string name;
        public string description;
        public int id;
        public QuestType tipo;

        [System.Serializable]
        public enum QuestType
        {
            Recoleccion, Matar, Entregar
        }
        //-------------------------------------------------------
        [Header("Misiones de Recoleccion")]
        public bool diferentesItems;
        public bool seQuedaConItems;
        public List<ItemsARecoger> Datos;

        [System.Serializable]
        public struct ItemsARecoger
        {
            public string nombre;
            public int cantidad;
            public int itemId;
        }

        [Header("Misiones de Matar")]
        public int cantidad;
        public int enemyId;

        [Header("Recompensas")]
        public int gold;
        public int xp;
        public bool hasSpecialR;
        public SpecialRewards[] specialR;

        [System.Serializable]
        public struct SpecialRewards
        {
            public string nombre;
            public GameObject reward;
        } 
    }
   
}
