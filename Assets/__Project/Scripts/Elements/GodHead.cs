using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GodHead : MonoBehaviour, IOnTickHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Strings

    public static List<string> Names = new List<string>() {
        "Macuilcozcacuauhtli", "Macuilcuetzpalin", "Macuilmalinalli", "Macuiltochtli", "Macuilxochitl", "Cuahuitlicac", "Patecatl", "Ixtlilton", "Ometochtli", "Tezcatzoncatl", "Tlilhua",
        "Toltecatl", "Tepoztecatl", "Texcatzonatl", "Colhuatzincatl", "Macuiltochtli", "Iztacuhca", "Tlatlauhca", "Cozauhca", "Yayauhca", "Cipactonal", "Huehuecoyotl", "Huehueteotl",
        "Mictlanpachecatl", "Cihuatecayotl", "Tlalocayotl", "Huitztlampaehecatl", "Quetzalcoatl", "Xiuhtecuhtli", "Mictlantecuhtli", "Acolmiztli", "Techlotl", "Nextepeua", "Iixpuzteque", "Tzontemoc",
        "Xolotl", "Cuaxolotl", "Tloque-Nahuaque", "Ometeotl", "Ometecuhtli", "Tonacatecuhtli", "Piltzintecuhtli", "Citlalatonac", "Tonatiuh", "Nanauatzin", "Tecciztecatl", "Tlahuizcalpantecuhtli",
        "Xolotl", "Xocotl", "Tezcatlipoca", "Quetzalcoatl", "Xipe-Totec", "Huitzilopochtli", "Painal", "Tlacahuepan", "Tepeyollotl", "Itzcaque",
        "Chalchiutotolin", "Ixquitecatl", "Itztlacoliuhqui", "Macuiltotec", "Itztli", "Amapan", "Uappatzin", "Itzpapalotltotec", "Miquiztlitecuhtli", "Tlaloc", "Tlaloque", "Chalchiuhtlatonal",
        "Atlaua", "Opochtli", "Teoyaomiqui", "Tlaltecayoa", "Cipactli", "Itztapaltotec", "Cinteotl", "Ppillimtec", "Omacatl", "Chicomexochtli",
        "Chiconahuiehecatl", "Coyotlinahual", "Xoaltecuhtli", "Xippilli", "Xochipilli"
    };

    public static List<string> Domains = new List<string>() {
        "stars", "medicine", "fertility", "underworld", "ballgame", "sacrifice", "earth", "art", "excess", "pleasure", "gluttony", "music", "gambling",
        "healing", "peyote", "maize", "astrology", "old-age", "deception", "wisdom", "winds", "light", "fire"
    };
    
    #endregion

    public ResourceCount Demands;
    public float TotalTicks;

    public float BonusTicks;
    public float MalusTicks;

    public SpriteRenderer Head;

    [HideInInspector] public string Name;
    [HideInInspector] public string Domain;

    int _currentCount;
    float _pastTicks;

    Rigidbody2D _rb;
    bool _shaken = false;

    private void Start()
    {
        GameManager.Instance.Register(this);
    }

    public void Initialize(Sprite headSprite, Color col)
    {
        Head.sprite = headSprite;
        Head.color = col;

        Name = GodHead.Names.PickRandom();
        Domain = Random.value < .5f ? "Goddess of " : "God of ";
        Domain += GodHead.Domains.PickRandom();
    }

    public void OnTick()
    {
        _pastTicks++;

        if (_pastTicks >= TotalTicks) DestroyHead(-MalusTicks);

        Head.material.SetFloat("_Timer", Mathf.Clamp01(1f - (_pastTicks / TotalTicks)));
    }

    private void DestroyHead(float ticksToAdd)
    {
        // TODO: animate
        GameManager.Instance.AddTicksToDoom(ticksToAdd);
        AltarManager.Instance.AskForNewHead();
        
        GameManager.Instance.Unregister(this);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_shaken)
        {
            _shaken = true;
            ShakeManager.Instance.Shake();
        }
    }

    public void OnPointerClick(PointerEventData e)
    {
        if (e.button != PointerEventData.InputButton.Left) return;

        int available = ResourceManager.Instance.GetQuantity(Demands.Resource);
        if (available == 0) return;

        if (Demands.Count <= available)
        {
            ResourceManager.Instance.Add(Demands.Resource, -Demands.Count);
            Tooltip.Instance.Hide();

            JobsManager.Instance.UnlockResearch();

            DestroyHead(BonusTicks);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.Instance.ShowGod(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Instance.Hide();
    }
}
