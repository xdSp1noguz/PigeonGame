using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI ammo;

    public Image healthIndicator;

    public Sprite health1;
    public Sprite health2;
    public Sprite health3;
    public Sprite health4;

    public GameObject redKey;
    public GameObject blueKey;
    public GameObject greenKey;



    // 1. Меняем имя переменной на _instance (через 'c' и с подчеркиванием)
    private static CanvasManager _instance; 
    
    public static CanvasManager Instance
    {
        get { return _instance; } // 2. Исправляем опечатку здесь
    }

    private void Awake()
    {
        // 3. И исправляем опечатки во всем блоке Awake
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }



    public void UpdateHealth(int healthValue)
    {
        health.text = healthValue.ToString() + "%";
        
    }
    public void UpdateArmor(int armorValue)
    {
        armor.text = armorValue.ToString() + "%";
    }

    public void UpdateAmmo(int ammoValue)
    {
        ammo.text = ammoValue.ToString() ;
    }
    
}

