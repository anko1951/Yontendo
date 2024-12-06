using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
   // 縮小終了までの時間（秒）
    public float shrinkDuration = 5.0f;

    // 最小スケール
    public Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f);

    // 内部状態
    private Vector3 initialScale;
    private float elapsedTime = 0f;

    void Start()
    {
        // 初期スケールを保存
        initialScale = transform.localScale;
    }

    void Update()
    {
        // 経過時間を更新
        elapsedTime += Time.deltaTime;

        // 線形補間でスムーズに縮小
        transform.localScale = Vector3.Lerp(initialScale, minScale, elapsedTime / shrinkDuration);

        // 縮小が終了したらスケールを固定
        if (elapsedTime >= shrinkDuration)
        {
            transform.localScale = minScale;
        }
    }
}
