using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public string enemyName;
    public float Speed;
    public int nextMove;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public int atkSpeed;

    private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, int _atkspeed)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkspeed;
    }

    public Player player;
    Image nowHpbar;

    public GameObject prfHpBar;
    public GameObject canvas;

    RectTransform hpBar;

    public float height = 0.8f;

    public bool isDead = false; //사망 여부
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 1);
    }

    void FixedUpdate()
     {
         //움직임
         rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

         //발판 확인
         Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);
         Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
         RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
     }

    void Think()
    {
        //Set Next Active
        nextMove = Random.Range(-1, 2);

        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);

        // Filp Sprite
        if(nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        // 문워크 방지
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 5);
    }

    void Start()
    {
        //체력바 생성
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        if (name.Equals("Enemy"))
        {
            SetEnemyStatus("Enemy", 100, 10, 1);
        }
        nowHpbar = hpBar.transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        // 체력바 월드 좌표를 UI 좌표로 바꿔주는 함수
        Vector3 _hpBarPos =
            Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        
        //스크린 좌표로 바꾼 값으로 체력바 이동
        hpBar.position = _hpBarPos;
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
    }

    private void OnTriggerEnter2D(Collider2D clo)
    {
            if (col.CompareTag("Player"))
        {
            if(player.attacked)
            {
                nowHp -= player.atkDmg;
                Debug,Log(nowHp);
                player.attacked = false;
                if(nowHp <= 0) //적 사망
                {
                    Destroy(gameObject);
                    Destroy(hpBar.gameObject);
                }
            }
        }
    }
   

}
