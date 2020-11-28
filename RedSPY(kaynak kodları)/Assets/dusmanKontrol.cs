using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class dusmanKontrol : MonoBehaviour
{
    public int Resim;
    GameObject[] gidilecekNoktalar;
    bool aradakiMesafeyibirKereAl = true;
    Vector3 aradakiMesafe;
    int aradakiMesafeSayacı = 0;
    bool ileriMiGeriMi = true;
    public LayerMask layerMask;
    RaycastHit2D ray;
    GameObject karakter;
    public GameObject kursun;
    float AtesZaman=0;
    int hiz = 5;

    SpriteRenderer spriteRend;

    public Sprite onTaraf;
    public Sprite ArkaTaraf;
    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];
        karakter = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        BeniGorduMu();
        if (ray.collider.tag=="Player")
        {
            hiz = 8;
            spriteRend.sprite = onTaraf;
            AtesEt();
        }
        else
        {
            hiz = 4;
            spriteRend.sprite = onTaraf;
        }

        NoktalaraGit();
    }

    public Vector2 getYon()
    {
        return (karakter.transform.position - transform.position).normalized;
    }
    void BeniGorduMu()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 1000,layerMask);
        Debug.DrawLine(transform.position,ray.point,Color.magenta);
    }
    void AtesEt()
    {
        AtesZaman += Time.deltaTime;
        if (AtesZaman>Random.Range(0.2f,1))
        {
            Instantiate(kursun, transform.position, Quaternion.identity);
            AtesZaman = 0;
        }
    }
    void NoktalaraGit()
    {
        if (aradakiMesafeyibirKereAl)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayacı].transform.position - transform.position).normalized;
            aradakiMesafeyibirKereAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayacı].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * hiz;
        if (mesafe < 0.5f)
        {
            aradakiMesafeyibirKereAl = true;
            if (aradakiMesafeSayacı == gidilecekNoktalar.Length - 1)
            {
                ileriMiGeriMi = false;
            }
            else if (aradakiMesafeSayacı == 0)
            {
                ileriMiGeriMi = true;
            }
            if (ileriMiGeriMi)
            {
                aradakiMesafeSayacı++;
            }
            else
            {
                aradakiMesafeSayacı--;
            }
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}



#if UNITY_EDITOR
[CustomEditor(typeof(dusmanKontrol))]
[System.Serializable]

class dusmanKontrolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        dusmanKontrol script = (dusmanKontrol)target;
        EditorGUILayout.Space();
        if (GUILayout.Button("ÜRET", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniObjem = new GameObject("merhaba");
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();
        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layerMask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ArkaTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
#endif