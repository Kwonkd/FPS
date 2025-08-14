using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public Weapon weapon;

    public Slider hpBar;
    public TextMeshProUGUI ammoText;
    public Image damageOverlay;

    public float hpsmothSpeed = 5.0f; // 체력바의 부드러운 움직임을 위해 선언
    private float hpTarget; // 체력을 0~1사이의 값으로 환산하기 위한 변수
    private float hpCurrent;

    public Color ammoNormalColor = Color.white;
    public Color ammoEmptyColor = Color.red;
    public float ammoBlinkInterval = 0.25f; // 탄약이 부족할 때 깜빡이는 간격
    private Coroutine ammoBlinkRoutine;

    public float hitFlashAlpha = 0.35f;
    public float hitFadeDuration = 0.25f;
    private Coroutine hitFlashRoutine;


    private void Awake()
    {
        if(playerStatus == null)
        {
            playerStatus = FindObjectOfType<PlayerStatus>();
        }

        if(weapon == null)
        {
            weapon = FindObjectOfType<Weapon>();
        }
    }

    private void OnEnable()
    {
        if(playerStatus != null)
        {
            playerStatus.OnHpChanged += HandleHpChanged;
        }

        if (weapon != null)
        {
            weapon.OnAmmoChanged += HandleAmmoChanged;
        }
    }

    private void OnDisable()
    {
        if(playerStatus != null)
        {
            playerStatus.OnHpChanged -= HandleHpChanged;
        }

        if(weapon != null)
        {
            weapon.OnAmmoChanged -= HandleAmmoChanged;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(playerStatus != null)
        {
            HandleHpChanged(playerStatus.CurrentHP, playerStatus.maxHP);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //보간함수 
        hpCurrent = Mathf.Lerp(hpCurrent, hpTarget, Time.deltaTime * hpsmothSpeed);
        hpBar.value = hpCurrent;
    }

    private void HandleHpChanged(float current, float max)
    {
        if(max <= 0)
        {
            hpTarget = 0.0f;
        }
        else
        {
            hpTarget = current / max;
        }

        hpBar.fillRect.GetComponent<Image>().color = HPColorFromRatio(hpTarget);

        if (hitFlashRoutine != null)
        {
            StopCoroutine(hitFlashRoutine);
        }
        hitFlashRoutine = StartCoroutine(HitFlash());
    }

    private void HandleAmmoChanged(int current, int reserve)
    {
        ammoText.text = current.ToString() + " / " + reserve.ToString();
        bool isEmtpy = (current == 0); //current가 0이면 true, 아니면 false

        if (isEmtpy == true)
        {
            if(ammoBlinkRoutine == null)
            {
                ammoBlinkRoutine = StartCoroutine(AmmoBlink());
            }
            else
            {
                if(ammoBlinkRoutine != null)
                {
                    StopCoroutine(ammoBlinkRoutine);
                    ammoBlinkRoutine = null;
                }

                ammoText.color = ammoNormalColor;
            }
        }
    }

    private IEnumerator AmmoBlink()
    {
        while(true)
        {
            ammoText.color = ammoEmptyColor;
            yield return new WaitForSeconds(ammoBlinkInterval);
            ammoText.color = ammoNormalColor;
            yield return new WaitForSeconds(ammoBlinkInterval);
        }
    }

    private IEnumerator HitFlash()
    {
        if(damageOverlay == null)
        {
            yield break;
        }

        damageOverlay.color = new Color(1.0f, 0.0f, 0.0f, hitFlashAlpha);

        float t = 0.0f;
        while (t < hitFadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(hitFlashAlpha, 0.0f , t/hitFadeDuration);

            damageOverlay.color = new Color(1.0f, 0.0f, 0.0f, a);
            yield return null;
        }

        damageOverlay.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
    }

    private Color HPColorFromRatio(float r)
    {
        if(r >= 0.5f)
        {
            float t = Mathf.InverseLerp(0.5f, 1.0f, r);
            return Color.Lerp(Color.yellow, Color.green, t);
        }
        else
        {
            float t = Mathf.InverseLerp(0.0f, 0.5f, r);
            return Color.Lerp(Color.red, Color.yellow, t);
        }
    }
}
