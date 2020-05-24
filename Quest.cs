using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest 
{
    public string name;
    public bool complete = false;
    public int id;
    public QuestType type;

    [Header("Para Destino")]
    public GameObject destino;

    [Header("Para Enemigos")]
    public int idEnemigo;
    public int totalAmount;
    public int currentAmount;

    [Header("Para Items")]
    public bool retieneItems;
    public List<QuestSystem.Mision.ItemsARecoger> itemsARecogers = new List<QuestSystem.Mision.ItemsARecoger>();

    public enum QuestType { 
    Recoleccion,Matar,Entrega
    }
}
