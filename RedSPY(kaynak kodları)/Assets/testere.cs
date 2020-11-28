using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class testere : MonoBehaviour
{
    public int Resim;
    GameObject[] gidilecekNoktalar;
    bool aradakiMesafeyibirKereAl = true;
    Vector3 aradakiMesafe;
    int aradakiMesafeSayacı=0;
    bool ileriMiGeriMi = true;
    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];
        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, 5);
        NoktalaraGit();
    }

    void NoktalaraGit()
    {
        if (aradakiMesafeyibirKereAl)
        {
            aradakiMesafe = (gidilecekNoktalar[aradakiMesafeSayacı].transform.position - transform.position).normalized;
            aradakiMesafeyibirKereAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[aradakiMesafeSayacı].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * 10;
        if (mesafe<0.5f)
        {
            aradakiMesafeyibirKereAl = true;
            if (aradakiMesafeSayacı==gidilecekNoktalar.Length-1)
            {
                ileriMiGeriMi = false;
            }
            else if (aradakiMesafeSayacı==0)
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
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i+1).transform.position);
        }
    }
#endif
}



#if UNITY_EDITOR
[CustomEditor(typeof(testere))]
[System.Serializable]

class TestereEditor:Editor
{
    public override void OnInspectorGUI()
    {
        testere script = (testere)target;
        if (GUILayout.Button("ÜRET",GUILayout.MinWidth(100),GUILayout.Width(100)))
        {
            GameObject yeniObjem = new GameObject("merhaba");
            yeniObjem.transform.parent = script.transform;
            yeniObjem.transform.position = script.transform.position;
            yeniObjem.name = script.transform.childCount.ToString();
        }
    }
}
#endif
