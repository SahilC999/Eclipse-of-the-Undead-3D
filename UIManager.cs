using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Manages all UI elements: HUD, notifications, crosshair, and hit indicators.
/// Attach this to a Canvas GameObject.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("HUD Elements")]
    public Text healthText;
    public Slider healthBar;
    public Text ammoText;
    public Text scoreText;
    public Text waveText;

    [Header("Notifications")]
    public Text notificationText;
    public Animator notificationAnimator;

    [Header("Damage Indicator")]
    public Image damageIndicator;
    public float damageFadeSpeed = 2f;

    [Header("Crosshair & Hitmarker")]
    public Image crosshair;
    public Image hitMarker;
    public float hitMarkerDuration = 0.2f;

    private Color damageColor;
    private bool showDamage = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (damageIndicator) damageColor = damageIndicator.color;
        if (damageIndicator) damageIndicator.color = new Color(damageColor.r, damageColor.g, damageColor.b, 0);
        if (hitMarker) hitMarker.enabled = false;
        UpdateScore(0);
        UpdateWave(1);
    }

    void Update()
    {
        if (showDamage)
        {
            float newAlpha = Mathf.Lerp(damageIndicator.color.a, 0, Time.deltaTime * damageFadeSpeed);
            damageIndicator.color = new Color(damageColor.r, damageColor.g, damageColor.b, newAlpha);
            if (newAlpha <= 0.05f) showDamage = false;
        }
    }

    // 🔹 HEALTH UI UPDATE
    public void UpdateHealth(int current, int max)
    {
        if (healthText) healthText.text = current + " / " + max;
        if (healthBar) healthBar.value = (float)current / max;
    }

    // 🔹 AMMO UI UPDATE
    public void UpdateAmmo(int current, int max)
    {
        if (ammoText) ammoText.text = "Ammo: " + current + " / " + max;
    }

    // 🔹 SCORE UI UPDATE
    public void UpdateScore(int score)
    {
        if (scoreText) scoreText.text = "Score: " + score;
    }

    // 🔹 WAVE UI UPDATE
    public void UpdateWave(int wave)
    {
        if (waveText) waveText.text = "Wave: " + wave;
        ShowNotification("Wave " + wave + " Started!");
    }

    // 🔹 NOTIFICATIONS
    public void ShowNotification(string message)
    {
        if (notificationText) notificationText.text = message;
        if (notificationAnimator) notificationAnimator.SetTrigger("Show");
    }

    // 🔹 DAMAGE INDICATOR
    public void ShowDamageEffect()
    {
        if (damageIndicator)
        {
            damageIndicator.color = new Color(damageColor.r, damageColor.g, damageColor.b, 0.5f);
            showDamage = true;
        }
    }

    // 🔹 HITMARKER
    public void ShowHitMarker()
    {
        if (!hitMarker) return;
        StartCoroutine(HitMarkerEffect());
    }

    IEnumerator HitMarkerEffect()
    {
        hitMarker.enabled = true;
        yield return new WaitForSeconds(hitMarkerDuration);
        hitMarker.enabled = false;
    }

    // 🔹 LOW AMMO ALERT
    public void LowAmmoWarning()
    {
        ShowNotification("⚠️ Low Ammo! Find a crate!");
    }

    // 🔹 BOSS INCOMING ALERT
    public void BossIncoming()
    {
        ShowNotification("🔥 BOSS ZOMBIE INCOMING!");
    }
}
