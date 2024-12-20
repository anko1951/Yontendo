using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HungerBar : MonoBehaviour
{
    public PlayerData playerData;
    public Image fillImage;
    public float smoothTime = 0.2f; // 補間にかかる時間

    private float targetFillAmount;
    private float currentVelocity; // 内部的に使われる速度

    private void Awake() {
        GameObject playerDataObj = GameObject.FindWithTag("PlayerData"); // PlayerDataのタグを持つオブジェクトを取得
        if (playerDataObj != null)
        {
            playerData = playerDataObj.GetComponent<PlayerData>(); // PlayerDataスクリプトを取得
        }
        else{
            FindOutPlayerData();
        }
    }

    void Update()
    {
        if (playerData != null && fillImage != null)
        {
            targetFillAmount = Mathf.Clamp01(playerData.GetHungry()/playerData.GetHungryMax());

            // SmoothDampでFillAmountを滑らかに変化
            fillImage.fillAmount = Mathf.SmoothDamp(
                fillImage.fillAmount, targetFillAmount, ref currentVelocity, smoothTime
            );
        }
    }

    //強制タイトル
    private void FindOutPlayerData(){
        Debug.LogWarning("PlayerDataが見つかりませんでした！Titleへ戻ります。");
        SceneManager.LoadScene("Title");
    }
}
