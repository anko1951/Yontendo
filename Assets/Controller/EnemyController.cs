using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  [SerializeField]
    GameObject Player; // ターゲットとなるプレイヤーオブジェクト
    [SerializeField]
    float farm = 5.0f; // 距離の閾値
    [SerializeField]
    float Speed = 2.0f; // 移動速度
    [SerializeField]
    float jumpForce = 30.0f;
    //[SerializeField]
    //float returnSpeed = 1.5f; // 元の位置に戻る速度
    [SerializeField]
    float rotationTolerance = 1.0f; // 角度の許容範囲

    private Rigidbody rb; // Rigidbodyを使用するための変数
    private bool isTracking = false; // 追跡モードのオンオフフラグ
    private Vector3 startPosition; // オブジェクトの初期位置
    //private bool isReturning = false; // 元の位置に戻るフラグ
    private PlayerData playerDataScript; // PlayerDataスクリプトの参照

    void Awake()
    {
        // PlayerDataタグを持つオブジェクトを探す
        GameObject playerDataObject = GameObject.FindWithTag("PlayerData");
        if (playerDataObject != null)
        {
            // PlayerDataスクリプトを取得
            playerDataScript = playerDataObject.GetComponent<PlayerData>();
        }
        else
        {
            Debug.LogError("PlayerDataタグを持つオブジェクトが見つかりません");
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 初期位置を保存
        startPosition = transform.position;

        // コルーチンを開始
        StartCoroutine(ToggleTracking());
    }

    void FixedUpdate()
    {
        if (Player == null) return;

        if (isTracking)
        {
            // 追跡モードの動作
            float distance = Vector3.Distance(transform.position, Player.transform.position);

            if (distance <= farm)
            {
                Vector3 direction = (Player.transform.position - transform.position).normalized;
                rb.AddForce(Vector3.up * (jumpForce) * Time.deltaTime, ForceMode.Impulse);
                rb.MovePosition(transform.position + direction * Speed * Time.fixedDeltaTime);
                Vector3 angle = new Vector3(0,30,0);
                // 回転を進行方向に合わせる
                RotateTowards(direction);
                // PlayerDataのUseHungryメソッドを呼び出す
                if (playerDataScript != null)
                {
                    playerDataScript.UseHungry();
                }
            }
        }
        else
        {
            /*
            // 元の位置に戻る動作
            if (isReturning)
            {
                Vector3 direction = (startPosition - transform.position).normalized;
                rb.MovePosition(transform.position + direction * returnSpeed * Time.fixedDeltaTime);

                // 元の位置に到達したらフラグをオフ
                if (Vector3.Distance(transform.position, startPosition) < 0.1f)
                {
                    isReturning = false;
                }
            }
            */
        }
    }

    private void RotateTowards(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);

        // 角度差が許容範囲を超えている場合のみ回転を更新
        if (angleDifference > rotationTolerance)
        {
            rb.MoveRotation(targetRotation);
        }
    }

    // 追跡モードを0.2秒ごとに切り替えるコルーチン
    private IEnumerator ToggleTracking()
    {
        while (true)
        {
            bool previousTracking = isTracking;
            isTracking = !isTracking; // フラグを切り替え

            // 追跡モードがオフに切り替わった場合
            if (previousTracking && !isTracking)
            {
                //isReturning = true; // 元の位置に戻るフラグをオン
            }

            yield return new WaitForSeconds(0.2f); // 0.2秒待機
        }
    }

}
