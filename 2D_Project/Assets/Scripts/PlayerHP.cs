using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾� ü�� ���� ��ũ��Ʈ
//�̱��� ��� : �÷��̾� ���, ������ ü�� �ִ�ġ ��ȭ

//�÷��̾� ����� �÷��̾� �ִϸ��̼� �ϼ��Ǹ� ���� ���� ����
//������ ü���ִ�ġ�� ����ȭ�� >> ��������ȭ�� �ϼ� ���� ���� ����

public class PlayerHP : MonoBehaviour
{
    GameObject      gameObject;
    SpriteRenderer  spriteRenderer;
    Rigidbody2D     rigid;

    //�ӽ÷� �ִ� ü�� 3 ����
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

        //�̱���
    }


    //�ǰݽ� ��� ����
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
        //(���������� ����)gameObject.layer = 6;

        //���¾����� �� ����
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0.6f);

        //ƨ���������� ���� ����
        int dmgDirection = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(dmgDirection, 0) * 7, ForceMode2D.Impulse);

        Hp--;
    }

    //�϶�
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
            heart += "�� ";
        }
        GUILayout.Label(heart);

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
