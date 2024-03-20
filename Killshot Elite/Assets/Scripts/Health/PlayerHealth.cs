using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    private float _health;
    private float _lerpTimer;
    private int _speedAnimationHealOrDamage = 5;

    [Header("Damage Overlay")]
    public Image Overlay;
    public float Duration;
    public float FadeSpeed;

    private float _durationTimer;

    void Start()
    {
        _health = maxHealth;
        Overlay.color = new Color(Overlay.color.r, Overlay.color.g, Overlay.color.b, 0);
    }

    void Update()
    {
        _health = Mathf.Clamp(_health, 0, maxHealth);
        UpdateHealthUI();

        if (Overlay.color.a > 0)
        {
            if(_health < 30)
            {
                return;
            }
            _durationTimer += Time.deltaTime;
            if (_durationTimer > Duration)
            {
                //fade ImageBlood
                float tempAlpha = Overlay.color.a;
                tempAlpha -= Time.deltaTime * FadeSpeed;
                Overlay.color = new Color(Overlay.color.r, Overlay.color.g, Overlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        Debug.Log(_health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = _health / maxHealth;
        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            _lerpTimer = Time.deltaTime;
            float percentComplete = _lerpTimer / chipSpeed;
            percentComplete *= _speedAnimationHealOrDamage;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            _lerpTimer = Time.deltaTime;
            float percentComplete = _lerpTimer / chipSpeed;
            percentComplete *= _speedAnimationHealOrDamage;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }
    public void TakeDamage(float damage)
    {
        _health -= damage;
        _lerpTimer = 0f;
        _durationTimer = 0f;
        Overlay.color = new Color(Overlay.color.r, Overlay.color.g, Overlay.color.b, 0.4f);
    }
    public void RestoreHealth(float healAmount)
    {
        _health += healAmount;
        _lerpTimer = 0f;
    }
}
