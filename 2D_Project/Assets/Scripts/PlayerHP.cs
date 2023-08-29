using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 체력 관련 스크립트
//미구현 목록 : 플레이어 사망, 직업별 체력 최대치 변화

//플레이어 사망은 플레이어 애니메이션 완성되면 같이 개발 예정
//직업별 체력최대치는 시작화면 >> 직업선택화면 완성 이후 개발 예정

public class PlayerHP : MonoBehaviour
{
    GameObject      gameObject;
    SpriteRenderer  spriteRenderer;
    Rigidbody2D     rigid;

    //임시로 최대 체력 3 고정
    public int      maxHp = 3;
    int             Hp = 3;
    bool            isDie = false;

    // Start is called before the first frame update
    void Start()
    {
        Hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            if (!isDie)
                Die();
            return;
        }
    }

    void Die()
    {
        isDie = true;

        //미구현
    }


    //피격시 잠시 무적
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            onDamaged(collision.transform.position);

        }
    }
    void onDamaged(Vector2 targetPos)
    {
        gameObject = GetComponent<GameObject>();
        //(오류때문에 보류)gameObject.layer = 6;

        //얻어맞았을때 색 변경
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0.6f);

        //튕겨져나가는 방향 결정
        int dmgDirection = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(dmgDirection, 0) * 7, ForceMode2D.Impulse);

        Hp--;
    }

    //하뚜
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Space(15);

        string heart = "";
        for(int i=0; i<Hp; i++)
        {
            heart += "♥ ";
        }
        GUILayout.Label(heart);

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
