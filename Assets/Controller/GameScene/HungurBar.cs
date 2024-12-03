using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public PlayerData playerData;
    public Image fillImage;
    public float smoothTime = 0.2f; // 補間にかかる時間

    private float targetFillAmount;
    private float currentVelocity; // 内部的に使われる速度

    void Update()
    {
        if (playerData != null && fillImage != null)
        {
            targetFillAmount = Mathf.Clamp01(playerData.GetHungry())/100;

            // SmoothDampでFillAmountを滑らかに変化
            fillImage.fillAmount = Mathf.SmoothDamp(
                fillImage.fillAmount, targetFillAmount, ref currentVelocity, smoothTime
            );
        }
    }
}
