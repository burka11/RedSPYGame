using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltinKontrol : MonoBehaviour
{
    public Sprite[] animasyonKareleri;
    SpriteRenderer spriteRenderer;
    float zaman = 0;
    int animasyonKareleriSayaci = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        zaman += Time.deltaTime;
        if (zaman > 0.03f)
        {
            spriteRenderer.sprite = animasyonKareleri[animasyonKareleriSayaci++];
            if (animasyonKareleri.Length == animasyonKareleriSayaci)
            {
                animasyonKareleriSayaci = animasyonKareleri.Length - 1;
            }
            zaman = 0;
        }

    }
}
