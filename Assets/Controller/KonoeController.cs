using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KonoeController : MonoBehaviour
{
    [SerializeField]
    Transform target;        // 目標のGameObject
    [SerializeField]
    float duration = 50f;    // 移動にかかる時間（秒）
    [SerializeField]
    float stopDistance = 30f; // 縮小を開始する残り距離
    [SerializeField]
    float shrinkSpeed = 0.1f; // 縮小速度
    [SerializeField]
    float minimumScale = 0.1f; // 最小スケール

    private Rigidbody rb;           // Rigidbodyコンポーネント
    private Vector3 direction;      // 移動方向ベクトル
    private float speed;            // 移動速度
    private bool shrinking = false; // 縮小状態のフラグ
    private float totalDistance;    // 開始時の目的地までの距離

    private bool isBattleStart = false;

    void Start()
    {
        // Rigidbodyコンポーネントの取得
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbodyがアタッチされていません！");
            enabled = false;
            return;
        }

        // 初期設定
        Vector3 startPosition = transform.position;
        totalDistance = Vector3.Distance(startPosition, target.position);
        direction = (target.position - startPosition).normalized;
        speed = totalDistance / duration; // 時間に基づく速度を計算
    }

    void FixedUpdate()
    {
        if(!isBattleStart){ return ;}
        if (!shrinking)
        {
            // 現在位置と目的地の距離を計算
            float currentDistance = Vector3.Distance(transform.position, target.position);

            if (currentDistance > stopDistance)
            {
                // 残り距離がまだ大きい場合は移動
                rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
            }
            else
            {
                // 残り距離が小さくなったら縮小開始
                shrinking = true;
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            // 縮小処理
            if (transform.localScale.x > minimumScale)
            {
                Vector3 newScale = transform.localScale - Vector3.one * shrinkSpeed * Time.fixedDeltaTime;
                transform.localScale = Vector3.Max(newScale, Vector3.one * minimumScale);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetIsBattleStart(){
        isBattleStart = true ;
    }
}
