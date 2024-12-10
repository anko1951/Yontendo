using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	[SerializeField]
    private float moveDefault = 5f; // 移動速度
    [SerializeField]
    private float jumpForce = 5f; // ジャンプの力
    [SerializeField]
    private float rotationSpeed = 700f; // 回転速度
    [SerializeField]
    private float minMass = 10f; // 最小重量
    

    private PlayerData playerData; // カスタムスクリプト(PlayerData)
    private Rigidbody rb;
    private Animator animator;

    /* ここから playerData 参照ステータス */
    private float addSpeed = 0f;
    private float dashSpeed = 0f;
    private float addJump = 0f;
    private float antiGravity = 0f;

    private float moveSpeed = 0f;
    private bool isDash = false;
    private bool isGround = false;

    void Awake()
    {
        GameObject playerDataObj = GameObject.FindWithTag("PlayerData"); // PlayerDataのタグを持つオブジェクトを取得
        if (playerDataObj != null)
        {
            playerData = playerDataObj.GetComponent<PlayerData>(); // PlayerDataスクリプトを取得
        }
        else
        {
            FindOutPlayerData();
        }
    }

    void Start()
    {
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
		
        if (playerData != null)
        {
            addSpeed = playerData.GetAddSpeed();
            dashSpeed = playerData.GetDashSpeed();
            addJump = playerData.GetAddJump();
            antiGravity = playerData.GetAntiGravity();

            if (antiGravity > 0)
            {
                float nowMass = rb.mass;
                float tempMass = nowMass - antiGravity;
                rb.mass = Mathf.Max(tempMass, minMass);
            }
        }
		
    }

    void Update()
    {
        // LShiftで加速
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = moveDefault + dashSpeed;
            isDash = true;
        }
        else
        {
            moveSpeed = moveDefault + addSpeed;
            isDash = false;
        }

        // 上下キーで前後に移動
        float moveInput = Input.GetAxis("Vertical"); // W/S または ↑/↓
        if (moveInput != 0)
        {
            animator.SetBool("Walk",true);
            if (isDash)
            {
                if (playerData != null)
                {
                    animator.SetBool("Run",true);
                    animator.SetBool("Walk",false);
                    playerData.useDush();
                }
            }
            else
            {
                if (playerData != null)
                {
                    animator.SetBool("Run",false);
                    playerData.useWalk();
                }
            }
        }
        else
        {
            animator.SetBool("Walk",false);
        }

        Vector3 moveDirection = transform.forward * moveInput * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + moveDirection);

        // 左右キーで方向調整
        float rotateInput = Input.GetAxis("Horizontal"); // A/D または ←/→
        float rotation = rotateInput * rotationSpeed * Time.deltaTime;

        // オブジェクトの回転
        transform.Rotate(Vector3.up * rotation);

        // 減速ロジック
        rotation *= 0.01f;

        // 回転値が小さい場合に停止
        if (Mathf.Abs(rotation) <= 0.51f){
            rotation = 0;
        }


        // スペースキーでジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            if (playerData != null)
            {
                animator.SetBool("Jump",true);
                playerData.useJump();
            }
            Jump();
        }
        else
        {
            animator.SetBool("Jump",false);
        }

        /*ここから食べる系*/
        // Zを押すと"Apple"を食べる
        if (Input.GetKeyDown(KeyCode.E)){
            Food food = playerData.EatFood("Apple");
            if (food != null){
                food.Consume();
            }
        }

    }

    void Jump()
    {
        // ジャンプするための力を加える
        rb.AddForce(Vector3.up * (jumpForce + addJump), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 衝突した相手のタグが "Enemy" か確認
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player hit an enemy!"); // デバッグログで確認
            gameObject.SetActive(false); // プレイヤーを非アクティブ化
            playerData.SendMessage("Died"); // "died" メソッドを呼び出し
        }
	}

    // 食べ物だったら見えなくしてplayerDataに入れる
    private void OnTriggerEnter(Collider other)
    {
        // 食べ物オブジェクトか確認
        Food food = other.GetComponent<Food>();
        if (food != null)
        {
            // PlayerDataに追加
            playerData.AddFood(food);

            // ゲームワールドから見えなくする
            other.gameObject.SetActive(false);

            Debug.Log($"Picked up a {food.Type}!");
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("Hashi"))
        {
            isGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("Hashi"))
        {
            isGround = false;
        }
    }

    //強制タイトル
    private void FindOutPlayerData(){
        Debug.LogWarning("PlayerDataが見つかりませんでした！Titleへ戻ります。");
        SceneManager.LoadScene("Title");
    }
}
