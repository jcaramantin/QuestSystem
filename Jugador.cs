using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class Jugador : MonoBehaviour
{
    public int experiencia;
    public int oro;

    public GameObject inventario;
    private MouseLook mouseLook;//buscar solu con mouse

    public QuestSystem database;
    public QuestTracker questTrack;
    public QuestTrackerPanel questTrackerPanel;
    public QuestPanel questPanel;

    [HideInInspector]
    public List<GameObject> invLocal = new List<GameObject>();
    private void Start()
    {
        mouseLook = gameObject.GetComponent<FirstPersonController>().m_MouseLook; //Reemplazar por el objeto de camara
        questTrack = GetComponent<QuestTracker>();
        questTrackerPanel.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("NPC_mision"))
        {
            //var npc = other.GetComponent<QuestGiver>();
            if (Input.GetKeyDown(KeyCode.E)) {
                other.GetComponent<QuestGiver>().ContactoConJugador(this);
            }
        
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Destino")) 
        {
            other.GetComponent<Destino_Script>().reached=true;
            transform.GetComponent<QuestTracker>().ActualizarQuest(other.GetComponent<Destino_Script>().id_Quest, Quest.QuestType.Entrega);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC_mision"))
        {
            questPanel.gameObject.SetActive(false);
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit2D hit;
            if (Physics2D.Raycast(transform.position,transform.forward, out hit , 200)) 
            {
                if (hit.transform.CompareTag("Enemy")) 
                {
                    var enem = hit.transform.GetComponent<Enemy>();
                    enem.gameObject.SetActive(false);
                    GetComponent<QuestTracker>().MuerteEnemiga(enem.id);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            questTrackerPanel.ActualizarBotones();
            questTrackerPanel.ActualizarDescripcionesConInfo(-1);
            questTrackerPanel.gameObject.SetActive(!questTrackerPanel.gameObject.activeSelf);
        }

        //Script proximo del inventario xd
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            inventario.SetActive(!inventario.activeInHierarchy);
            inventario.transform.parent.transform.position = inventario.GetComponent<InventarioNuevo>().originalPos;
        }
        if (inventario.activeInHierarchy|| questTrackerPanel.gameObject.activeInHierarchy||questPanel.gameObject.activeInHierarchy)
        {
            mouseLook.SetCursorLook(false);
        }
        else 
        {
            mouseLook.SetCursorLook(true);
        }

        if (!inventario.activeInHierarchy && inventario.GetComponent<InventarioNuevo>().objetoSeleccionado=null)
        {
            inventario.GetComponent<InventarioNuevo>().objetoSeleccionado.gameobject.transform.SetParent(inventario.GetComponent<InventarioNuevo>().ExParent);
            inventario.GetComponent<InventarioNuevo>().objetoSeleccionado.gameobject.transform.localPosition = Vector2.zero;
            inventario.GetComponent<InventarioNuevo>().objetoSeleccionado = null;
        }

        //findelinventario
    }

    public void Recompesar(Quest quest)
    {

        questTrackerPanel.ActualizarBotones();
        experiencia += database.misions[quest.id].xp;
        oro += database.misions[quest.id].gold;

        questPanel.accept_Button.gambeObject.SetActive(false);
        questPanel.deny_button.gameObject.SetActive(false);

        if (database.misions[quest.id].hasSpecialR)
        {
            if (database.misions[quest.id].specialR.Length>1)
            {
                string s = "Bien hecho! Completaste "+database.misions[quest.id].name+", como recompensa  has obtenido Oro(" +database.misions[quest.id].gold+ "),"+ "Experiencia (" +database.misions.[quest.id].xp+") y los siguientes items: ";
                for (int i = 0; i < database.misions.[quest.id].specialR.Length; i++)
                { 
                    s= string.Format("{0} {1}"), s, database.misions[quest.id].specialR[i].nombre);    
                }
            questPanel.ActualizarPanel(quest.name,s);
            }
            else
            {
            questPanel.ActualizarPanel(database.misions[quest.id].name,"Bien hecho! completaste"+database.misions[quest.id].name+", como recompensa" +"has obtenido el oro("
                +database.misions[quest.id].gold+")  y Experiencia ("+ database.misions[quest.id].xp+").");
            }
        if (quest.retieneItems)
        {
            List<GameObject> its = new List<GameObject>();
            int cantidadAeliminar = quest.itemsARecogers[0].cantidad;
            for(int i =0; i<invLocal.Count;i++)
            {
                if (invLocal[i].GetComponent<Itemsuelto>.ID==quest.itemsARecogers[0].itemId && cantidadAeliminar>0)
                {
                    its.Add(invLocal[i]);
                    invLocal[i].GetComponent<Itemsuelto>().EliminarItem(invLocal[i].GetComponent<Itemsuelto>().ID, cantidadAeliminar, false);
                    cantidadAeliminar--;
                }
            }
            invLocal.RemoveAll(itemB =>
            {
                return its.Find(itemA => itemA == itemB);
            });
            foreach(var item in its)
            {
                Destroy(item);
            }
            its.Clear();
        }

        if (database.misions[quest.id].hasSpecialR)
        {
            foreach (var item in database.misions[quest.id].specialR)
            {
                ItemSuelto itSuelto = inventario.GetComponent<InventarioNuevo>().itemsSueltos.Find(x => x.ID == item.reward.GetComponent<ItemSuelto>().ID);
                inventario.GetComponent<InventarioNuevo>().AgregarItem(item.reward.GetComponent.< ItemSuelto > ().ID, item.reward.GetComponent<ItemSuelto>().cantidad);
                if (itSuelto != null)
                {
                    itSuelto.cantidad += 1;

                }
                else
                {
                    var nuevoIt = Instantiate(item.reward.GetComponent<ItemSuelto>());
                    nuevoIt.Inv = inventario.GetComponent<InventarioNuevo>();
                    nuevoIt.transform.SetParent(this.transform);
                    inventario.GetComponent<InventarioNuevo>().itemsSueltos.Add(nuevoIt);
                    invLocal.Add(nuevoIt.gameObject);
                    nuevoIt.gameObject.SetActive(false);

                }
                questTrack.VerificarItem(item.reward.GetComponent<ItemSuelto>().ID);
                print("Nuevo Objeto Obtenido: "+item.nombre);

            }
        
        }









        }
    }    
    





}
