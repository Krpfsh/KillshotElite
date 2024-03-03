using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    private float _health;
    private float _lerpTimer;
    private int _speedAnimationHealOrDamage = 5;

    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _health = Mathf.Clamp(_health, 0, maxHealth);
        UpdateHealthUI();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(Random.Range(5, 10));
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            RestoreHealth(Random.Range(5, 10));
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
    }
    public void RestoreHealth(float healAmount)
    {
        _health += healAmount;
        _lerpTimer = 0f;
    }
}
