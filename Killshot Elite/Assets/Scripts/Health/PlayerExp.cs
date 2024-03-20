using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    [Header("Health Bar")]
    public float maxExperience = 100f;
    public float chipSpeed = 2f;
    public Image frontExpBar;
    public Image backExpBar;

    private float _exp;
    private float _lerpTimer;
    private int _speedAnimationHealOrDamage = 5;

    [Header("Damage Overlay")]
    public float Duration;
    public float FadeSpeed;
    [SerializeField] TextMeshProUGUI levelNumberText;
    private int  levelNumber = 1;
    void Start()
    {
        _exp = 0;
    }

    void Update()
    {
        _exp = Mathf.Clamp(_exp, 0, maxExperience);
        UpdateHealthUI();
        UpdateLevelNumber();
    }

    private void UpdateLevelNumber()
    {
        if(_exp == maxExperience)
        {
            _exp = 0;
            levelNumber++;
        }
        levelNumberText.text = levelNumber.ToString();
    }

    public void UpdateHealthUI()
    {
        Debug.Log(_exp);
        float fillF = frontExpBar.fillAmount;
        float fillB = backExpBar.fillAmount;
        float hFraction = _exp / maxExperience;
        if(fillB > hFraction)
        {
            frontExpBar.fillAmount = hFraction;
            backExpBar.color = Color.red;
            _lerpTimer = Time.deltaTime;
            float percentComplete = _lerpTimer / chipSpeed;
            percentComplete *= _speedAnimationHealOrDamage;
            backExpBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(fillF < hFraction)
        {
            backExpBar.fillAmount = hFraction;
            backExpBar.color = Color.green;
            _lerpTimer = Time.deltaTime;
            float percentComplete = _lerpTimer / chipSpeed;
            percentComplete *= _speedAnimationHealOrDamage;
            frontExpBar.fillAmount = Mathf.Lerp(fillF, backExpBar.fillAmount, percentComplete);
        }
    }
    //Если понадобитсья вычитать опыт
    //public void TakeDamage(float damage)
    //{
    //    _exp -= damage;
    //    _lerpTimer = 0f;
    //    _durationTimer = 0f;
    //    Overlay.color = new Color(Overlay.color.r, Overlay.color.g, Overlay.color.b, 1);
    //}
    public void AddExperience(float expAmount)
    {
        _exp += expAmount;
        _lerpTimer = 0f;
    }
}
