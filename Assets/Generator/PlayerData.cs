using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
   
    public static PlayerData Instance { get; private set; } // シングルトンインスタンスのプロパティ

    [SerializeField] private float addSpeed = 3.0f;
    [SerializeField] private float dashSpeed = 10.0f;
    [SerializeField] private float addJump = 50.0f;
    [SerializeField] private float antiGravity = 1.0f;
    [SerializeField] private float hungryDefault = 100.0f;
    [SerializeField] private float hungryMax = 200.0f;
    //[SerializeField] private float covert = 10.0f

    [SerializeField] private int speedLv = 1;
    [SerializeField] private int jumpLv = 1;
    [SerializeField] private int dashLv = 1;
    [SerializeField] private int antiGravityLv = 1;
    [SerializeField] private int hungryDefaultLv = 1;
    [SerializeField] private int hungerMaxLv = 1;
    //[SerializeField] private int covertLv = 1;

    public GameData gameData;

    private float hungry;
    private bool isDead;
    private Dictionary<string, Stack<Food>> foodInventory = new Dictionary<string, Stack<Food>>();

    private void Awake()
    {
        if (Instance == null)
        {
            // Transform の親を解除してルートに移動
            transform.SetParent(null);
            Instance = this;
            DontDestroyOnLoad(gameObject); // オブジェクトをシーン間で維持
        }
        else
        {
            Destroy(gameObject); // 既に存在するインスタンスがある場合、重複するオブジェクトを破棄
        }
        gameData.lifeMax = 100 ;
        gameData.life = 100 ;
        gameData.isClear = false;
    }

    private void Start(){
        this.hungry = hungryDefault * hungryDefaultLv ;
    }

    // Getterメソッド（float: 値 × レベル）
    public float GetAddSpeed() => addSpeed * speedLv;
    public float GetDashSpeed() => dashSpeed * dashLv;
    public float GetAddJump() => addJump * jumpLv;
    public float GetAntiGravity() => antiGravity * antiGravityLv;
    public float GetHungryDefault() => hungryDefault * hungryDefaultLv;
    public float GetHungryMax() => hungryMax * hungerMaxLv;

    // Getterメソッド
    public int GetSpeedLv() => speedLv;
    public int GetJumpLv() => jumpLv;
    public int GetDashLv() => dashLv;
    public int GetAntiGravityLv() => antiGravityLv;
    public int GetHungryDefaultLv() => hungryDefaultLv;
    public int GetGravityLv() => antiGravityLv;
    public int GetHungerLv() => hungerMaxLv;
    public float GetHungry() => hungry;
    public bool GetIsDead() => isDead;

    // Setterメソッド
    public void SetIsDead(bool boo) => this.isDead = boo ;

    // 各LvをインクリメントするLvUpメソッド
    public void LvUpSpeed() => speedLv++;
    public void LvUpJump() => jumpLv++;
    public void LvUpDash() => dashLv++;
    public void LvUpAntiGravity() => antiGravityLv++;
    public void LvUpHungryDefault() => hungryDefaultLv++;
    public void LvUpGravity() => antiGravityLv++;
    public void LvUpHunger() => hungerMaxLv++;

    // Use系
    public void useDush(){ this.hungry -= 0.1f ;}
    public void useJump(){ this.hungry -= 10.0f; }
    public void useWalk(){ this.hungry -= 0.01f; }
    public void UseHungry(){ this.hungry -= 0.05f; }


    // 食べ物を所持するためのメソッド
    public void AddFood(Food food)
    {
        string foodType = food.Type;

        if (!foodInventory.ContainsKey(foodType))
        {
            foodInventory[foodType] = new Stack<Food>();
        }

        foodInventory[foodType].Push(food);
    }

    // 特定の種類の食べ物を食べる（取り出す）メソッド
    public Food EatFood(string foodType)
    {
        if (foodInventory.ContainsKey(foodType) && foodInventory[foodType].Count > 0)
        {
            return foodInventory[foodType].Pop();
        }

        Debug.Log($"No food of type {foodType} available!");
        return null;
    }

    //食べ物を一括スコア化するメソッド
    public void ChangeScoreAllFoods(){
        foreach (string foodType in foodInventory.Keys){
            Debug.Log($"Eating all food of type: {foodType}");
            while (foodInventory[foodType].Count > 0){
                Food food = foodInventory[foodType].Pop();
                gameData.resultScore += food.changeScore();
            }
        }
        Debug.Log(gameData.resultScore);
    }

    //空腹度回復系メソッド
    public void FullCharge(){
        this.hungry = GetHungryDefault();
    }

    public void EatCharge(float foodHeal){
        this.hungry += foodHeal;
    }


    //死んだときの動作
    public void Died(){
        GameObject gameDirector = GameObject.FindGameObjectWithTag("GameDirector"); // GameDirector を探す
        if (gameDirector != null && !isDead){
            ChangeScoreAllFoods();
            isDead = true;
            gameDirector.SendMessage("PlayerDiedScene");
        }else{
            Debug.LogError("GameDirector が見つかりません");
        }
    }

    //一番最後に実行するアップデート
    private void LateUpdate(){
        if(this.hungry <= 0 && !isDead){
            this.Died();
        }
    }

    // デバッグ用に現在のステータスを表示するメソッド
    public void DebugStats()
    {
        Debug.Log($"Add Speed: {GetAddSpeed()}, Dash Speed: {GetDashSpeed()}, Add Jump: {GetAddJump()}, Anti Gravity: {GetAntiGravity()}");
        Debug.Log($"Hungry Default: {GetHungryDefault()}, Hungry Max: {GetHungryMax()}");
        Debug.Log($"Speed Lv: {speedLv}, Jump Lv: {jumpLv}, Dash Lv: {dashLv}, Anti Gravity Lv: {antiGravityLv}");
        Debug.Log($"Hungry Default Lv: {hungryDefaultLv}, Gravity Lv: {antiGravityLv}, Hunger Lv: {hungerMaxLv}");
    }

}
