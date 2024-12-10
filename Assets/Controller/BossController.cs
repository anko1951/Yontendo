using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
   // 縮小終了までの時間（秒）
    public float shrinkDuration = 5.0f;

    // 最小スケール
    public Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f);

    public GameData gameData;
    [SerializeField]
    public string NextSceneName;
    GameObject Generator;

    // 内部状態
    private Vector3 initialScale;
    private float elapsedTime = 0f;
    private bool isBattleStart = false;
    private SlimeGenerator slimeGenerator;

    void Start()
    {
        // 初期スケールを保存
        initialScale = transform.localScale;

        // SlimeGeneratorを探す
        slimeGenerator = Generator.GetComponent<SlimeGenerator>();
    }

    void Update()
    {
        // 経過時間を更新
        elapsedTime += Time.deltaTime;

        if(!isBattleStart){ elapsedTime = 0 ;}

        // 線形補間でスムーズに縮小
        transform.localScale = Vector3.Lerp(initialScale, minScale, elapsedTime / shrinkDuration);

        // 縮小が終了したらスケールを固定
        if (elapsedTime >= shrinkDuration)
        {
            gameData.isClear = true ;
            Destroy(gameObject);
            ChangeScene();
        }
    }

    public void SetIsBattleStart(){
        isBattleStart = true ;
        slimeGenerator.enabled = true;
    }

    public void ChangeScene(){
        SceneManager.LoadScene(NextSceneName);
    }
}
