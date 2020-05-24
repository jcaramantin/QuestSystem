using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestTracker : MonoBehaviour
{
    public QuestSystem db;
    //Quest comenzandas
    public List<Quest> activeQuests = new List<Quest>();
    //Quest Terminadas
    public List<Quest> finishedQuests = new List<Quest>();
    //Quest  ya terminadas, se cobro rewards
    public List<Quest> completeQuests = new List<Quest>();
    //Lista de los NPCS que nos dieron las misiones
    [HideInInspector] public List<QuestGiver> rewarders = new List<QuestGiver>();

    public void MuerteEnemiga(int enemy_ID) {
        if (activeQuests.Count > 0) {
            for (int i = 0; i < activeQuests.Count; i++) {
                if (activeQuests[i].idEnemiga == enemy_ID)
                {
                    activeQuests[i].currentAmount++;
                    if (activeQuests[i].currentAmount < activeQuests[i].totalAmount) {
                        print("Cantidad restante de enemigos: " + (activeQuests[i].totalAmount - activeQuests[i].currentAmount));
                    }
                    ActualizarQuest(activeQuests[i].id, activeQuests[i].type);
                    break;
                }
            }
        }
    }


    public void ActualizarQuest(int quest_ID, Quest.QuestType type, int? cantItems = null)
    {
        var val = activeQuests.Find(x => x.id == quest_ID);
        if (type == Quest.QuestType.Matar) {
            if (val.currentAmount <= val.totalAmount)
            {
                Debug.LogWarning("Quest: " + db.misions[val.id].name + " completadas!");
                val.complete = true;
            }
            else {
                print("Aun no has completado la quest: " + db.misions[val.id].name);
            }
        }

        if (type == Quest.QuestType.Entrega)
        {
            if (val.destino.GetComponent<Destino_Script>().reached)
            {
                Debug.LogWarning("Quest: " + db.misions[val.id].name + " completada!");
                val.complete = true;
            }
            else
            {
                print("Aun te falta para completar tu objetivo Adalid!");
            }
        }
        

        if (type == Quest.QuestType.Recoleccion)
        {
            foreach (var item in val.itemsARecogers)//mejor ,pa que ejecute pa cada uno .
            {
                if (cantItems!=null) 
                {
                    if (cantItems == item.cantidad)
                    {
                        Debug.LogWarning("Quest: " + db.misions[val.id].name + "completada!");
                        val.complete = true;
                    }
                    else 
                    {
                        print("Que fue loco ? Tovia te falta recolectar item ps papi .Te faltan: "+(item.cantidad-cantItems));
                    }
                }
            }

        }
    }

    public void VerificarItem(int item_ID)
    {
        Quest q = null;
        if (activeQuests.Count > 0)
        {
            if (activeQuests.Count >0) 
            {
                if (activeQuests.Exists(x->x.ItemsARecogers.Exist(a => a.ItemsID == item_ID)))
                {
                    q = activeQuests.Find(x->x.ItemsARecogers.Exists(a => a.itemID == item_ID));
                }
                else
                {
                    q = null;
                    return;
                }
                for (int i=0; i<activeQuests.Count;i++) 
                {
                    if (q.itemsARecogers[0].itemId == item_ID && activeQuests[i].id == q.id) 
                    {
                        //ESTO SOLO FUNCIONA  SI LAS QUEST  VAN EN ORDEN Y DE FORMA ASCENDENTEA  0
                        int cantidad = DiscrimacionDeItems(db.misions[activeQuests[i].id.Datos[0].itemID]);
                        ActualizarQuest(activeQuests[i].id, activeQuests[i].type, cantidad);
                        q = null;
                        break;
                    }
                }

            }
        }

    }

    public int DiscriminacionDeItems(int id) 
    {
        int itemsMatched = 0;
        foreach (var item in GetComponent<Jugador>().invLocal) 
        {
            if (item.GetComponent<ItemSuelto>().ID == id)
            {
                itemsMatched++;
            }
        }
        return itemsMatched;
    }
  
    

}

