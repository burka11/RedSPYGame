using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class KarakterKontrol : MonoBehaviour
{
    public Sprite[] beklemeAnim;
    public Sprite[] ziplamaAnim;
    public Sprite[] yurumeAnim;

    public Text canText;
    public Text AltinText;
    int can = 100;
    public Image SiyahArkaPlan;
    float SiyaharkaplanSayaci = 0;

    int beklemeAnimSayac = 0;
    int yurumeAnimSayac = 0;

    SpriteRenderer spriteRender;
    Rigidbody2D fizik;

    Vector3 vec;
    Vector3 kameraSonPos;
    Vector3 kameraIlkPos;

    GameObject kamera;

    bool birKereZıpla = true;

    float anaMenuyeDonZaman = 0;

    int altinSayaci = 0;
    float horizontal = 0;
    float YurumeAnimZaman = 0;
    float beklemeAnimZaman = 0;
    void Start()
    {
        Time.timeScale = 1;
        spriteRender = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        SiyahArkaPlan.gameObject.SetActive(false);
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (SceneManager.GetActiveScene().buildIndex>PlayerPrefs.GetInt("kacincilevel"))
        {
            PlayerPrefs.SetInt("kacincilevel", SceneManager.GetActiveScene().buildIndex);
        }

        kameraIlkPos = kamera.transform.position - transform.position;
        canText.text = "CAN  " + can;
        AltinText.text = "30 /" + altinSayaci;

        
    }
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (birKereZıpla)
            {
                fizik.AddForce(new Vector2(0, 500));
                birKereZıpla = false;
            }

        }

    }
    void FixedUpdate()
    {
        KarakterHareket();
        Animasyon();
        if (can<=0)
        {
            Time.timeScale = 0.4f;
            canText.enabled = false;  
            SiyaharkaplanSayaci += 0.04f;
            SiyahArkaPlan.gameObject.SetActive(true);
            SiyahArkaPlan.color = new Color(0,0,0,SiyaharkaplanSayaci);
            anaMenuyeDonZaman += Time.deltaTime;
            if (anaMenuyeDonZaman>1)
            {
                SceneManager.LoadScene("AnaMenu");
            }

        }
    }
    void KarakterHareket()
    {
        horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal * 10, fizik.velocity.y, 0);
        fizik.velocity = vec;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        birKereZıpla = true;
    }
     void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="kursun")
        {
            can--;
            canText.text = "CAN " + can;
        }
        if (col.gameObject.tag == "dusman")
        {
            can-=10;
            canText.text = "CAN " + can;
        }
        if (col.gameObject.tag == "testere")
        {
            can -= 10;
            canText.text = "CAN " + can;
        }
        if (col.gameObject.tag=="levelbitsin")
        {
            //BeklemeSuresi();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        //if (col.gameObject.tag == "canver" || col.gameObject.tag == "altin")
        //{
        //    if (can == 100)
        //    {
        //        can =100;
        //        canText.text = "CAN " + can;
        //        col.GetComponent<BoxCollider2D>().enabled = false;
        //        col.GetComponent<canver>().enabled = true;
        //        Destroy(col.gameObject, 3);
        //    }
        //}
        if (col.gameObject.tag == "canver")
        {
            can += 10;
            canText.text = "CAN " + can;
            col.GetComponent<BoxCollider2D>().enabled = false;
            col.GetComponent<canver>().enabled = true;
            Destroy(col.gameObject,3);

        }
        if (col.gameObject.tag == "altin")
        {
            altinSayaci++;
            AltinText.text = "30 / " + altinSayaci;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "su")
        {
            can = 0;
        }
        if (col.gameObject.tag=="sınır")
        {
            can = 0;
           
        }

    }
    //IEnumerator BeklemeSuresi()
    //{
    //    yield return new WaitForSeconds(5);
    //}
    void Animasyon()
    {
        if (true)
        {
            if (horizontal == 0)
            {
                beklemeAnimZaman += Time.deltaTime;
                if (beklemeAnimZaman > 0.05f)
                {
                    spriteRender.sprite = beklemeAnim[beklemeAnimSayac++];
                    if (beklemeAnimSayac == beklemeAnim.Length)
                    {
                        beklemeAnimSayac = 0;
                    }
                    beklemeAnimZaman = 0;
                }


            }
            else if (horizontal > 0)
            {
                YurumeAnimZaman += Time.deltaTime;
                if (YurumeAnimZaman > 0.01f)
                {
                    spriteRender.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    YurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(1, 1, 1);

            }
            else if (horizontal < 0)
            {
                YurumeAnimZaman += Time.deltaTime;
                if (YurumeAnimZaman > 0.01f)
                {
                    spriteRender.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    YurumeAnimZaman = 0;
                }
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (fizik.velocity.y>0)
            {
                spriteRender.sprite = ziplamaAnim[0];
            }
            else
            {
                spriteRender.sprite = ziplamaAnim[1];
            }
            if (horizontal>0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal<0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
       
    }

    void LateUpdate()
    {
        KameraKontrol();
    }
    void KameraKontrol()
    {
        kameraSonPos = kameraIlkPos + transform.position;
        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPos, 0.08f);
    }
}
