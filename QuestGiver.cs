using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{

    public QuestSystem dataB;
    public int id_Mission;
    public QuestPanel questPanel;

    [Space]
    public bool isStarted = false;
    public Quest quest;
    public bool rewarded = false;
    public bool isRewardGiver;
    //QUIEN ENTREGA  LA RECOMPENSA
    public QuestGiver questRewarder;
    //el npc que dirige hasta este npc
    public QuestGiver prevQuestGiver;
    private Jugador jugador;

    void Start()
    {
        for(int i =0; i<dataB.misions.Length;i++)
        {
            if(dataB.misions[i].id==this.id_Mission)
            {
                quest.name = dataB.misions.[i].name;
                quest.id = dataB.misions[i].id;
                quest.idEnemigo = dataB.misions[i].enemyId;
                quest.totalAmount = dataB.misions[i].cantidad;
                quest.retieneItems = dataB.misions[i].seQuedaConItems;
                quest.type = (Quest.QuestType)dataB.misions[i].tipo;

                if(dataB.misions[i].Datos !=null)
                {
                    if(dataB.misions[i].Datos.Count>0)
                    {
                        quest.itemsARecogers.Add(dataB.misions[i].Datos[0]);
                        if(quest.itemsARecogers.Count >1)//bug de duplicado ,arreglado con esta wea x
                        {
                            quest.itemsARecogers.RemoveAt(1);

                        }    
                    }
                }
                if(quest.destino !=null)
                {
                    quest.destino.GetComponent<Destino_Script>().id_Quest = quest.id;
                    quest.destino.SetActive(false);

                }
            }
        } 
        if(isRewardGiver)
        {
            questRewarder = this;
        }
        else
        {
            questRewarder.dataB = this.dataB;
            questRewarder.id_Mission = this.id_Mission;
            questRewarder.isStarted = this.isStarted;
            questRewarder.rewarded = this.rewarded;
            questRewarder.quest = this.quest;
            questRewarder.prevQuestGiver=this.prevQuestGiver´;
            questRewarder.quest.destino = this.quest.destino;
            questRewarder.questPanel = this.questPanel;

            if(quest.destino!=null)
            {
                quest.destino.GetComponent<Destino_Script>().id_Quest=quest.id;
            }    
            if(questRewarder.questRewarder==questRewarder)
            {
                questRewarder.isRewardGiver = true;
            }
            else 
            {
                questRewarder.isRewardGiver = false;
            }
        }
        void ContactoConJugador(Jugador jug)
        {
            jugador = jug;
            if (!rewarded)
            {
                if(prevQuestGiver==null)
                {
                    questPanel.accept_Button.gameObject.SetActive(true);
                    questPanel.deny_button.gameObject.SetActive(false);

                    questPanel.

                }

            }
    
    
    
    }

    // Update is called once per frame
    void Update()
    {

    }
}
