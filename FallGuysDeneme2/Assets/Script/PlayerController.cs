using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//bool ile sürekli koşu ve sağ sol işlerini kontolr ettirmeliyim

//telefon dikeyde y yatayda x 
//oyuncunun yon islerini transform.LookAt ilede yapabilirdm

public class PlayerController : MonoBehaviour
{

    public CharacterController characterController;

    //// Swipe Controller/////
    //Firs touch position
    float  swipe1y;
    // Moved to uch position
    float  swipe2y;
    //Horizontal and Vertical value
    public float horizontal = 0, vertical = 0;
    //Karektere hiz vermek icin
    public float speed = 6f;
    //Donus yumusatma
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    //SurekliHareket
    
    //Animator
    Animator animator;
    
    //AktifYonDegisimi
    float yatayDegisim = 0;
    Vector3 direction,direction1;
    //Yercekimi
    float useGravity = 9.81f;
    //Ebeveyn Icin 
    public Transform playerParentTransform;
    public Transform silindirParentTransform1, silindirParentTransform2, silindirParentTransform3;
    //Boya Prefabı
    public GameObject boya;
    public float boyagenisligi = 1f;
    public bool DuvarBoyamaDurum = false;
    public GameObject wallAktif;
    public GameObject bitisDuvari;
    public GameObject Boyama;
    public GameObject WallPainter;
    //Hareketi durdurma Kamera Hareketi Ve Paint Baslatma
    bool AktifYonDurumu = true;
    //GirlsKonum
    float girlsKonum;
   
    




    void Start()
    {
        direction1 = new Vector3(0, -useGravity, 0).normalized;
        animator = GetComponent<Animator>();
       //boya.transform.rotation = Quaternion.Euler(90f, 180f,0f);

    }

    private void Update()
    {

        characterController.Move(direction1);

        ControlHorizontal();
        ControlVertical();

        //if (surekliHareket == true)
        //{
        //    ControlHorizontal();
        //    ControlVertical();
        //}


        DeadCheck();
        AktifYon();
        SwipeController();
        ControlHorizontal();
        ControlVertical();
    }



    void AktifYon()
    {
        if (AktifYonDurumu)
        {
            //Dokunmadan ayri surekli calismasi icim
            direction = new Vector3(horizontal, 0, vertical).normalized;
        }
    }







    void ControlVertical()
    {//saga sola

        
        
            vertical = (yatayDegisim / 2f);
        



        //if (swipe1x > swipe2x+400f)
        //{
            
        //    vertical = -1;
        //}
        //if (swipe1x > swipe2x + 200f)
        //{

        //    vertical = -0.5f;
        //}
        //else if (swipe1x+400f < swipe2x)
        //{
            
        //  vertical = 1f;
        //}
        //else if (swipe1x + 200f < swipe2x)
        //{

        //    vertical = 0.5f;
        //}

        //else
        //{
        //    vertical = 0;
        //}

    }
    void ControlHorizontal()
    { //yukari asagi
        if (swipe1y > swipe2y)
        {
            
            horizontal = 1;
        }
        else if (swipe1y < swipe2y)
        {
            
            horizontal = -1;
        }
        else
        {
            //horizontal = 0;
        }
    }


    void SwipeController()
    {
        

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            if (DuvarBoyamaDurum)
            {
                //Parmak Pozisyonu aldim
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit wall; //Dokundugum nesne
                if (Physics.Raycast(ray, out wall)) //bir neseneye dokunursa wall icine at
                {
                    //boya olusturup boyama icine attım direk de yapabilirdim
                    //yazılımcı boya genisligini ayarlayabilir
                    if (wall.transform.tag == "DuvarBoya")
                    {
                        WallPainter.transform.position = wall.point;
                        //.idenity doğrultuyu değiştirir hep aynı doğrultu olur
                        Boyama = Instantiate(boya, wall.point, Quaternion.Euler(0f, -90f, 0f));
                    }
                         
                        //Boyama.transform.localScale = Vector3.one * boyagenisligi;
                    
                }
            }
           
            //AktifYonlendirme 
            yatayDegisim = (touch.deltaPosition.x)/2f;
            //Debug.Log(touch.deltaPosition);
            
            if (touch.phase == TouchPhase.Ended)
            {

                vertical = 0;
                horizontal = 0;
                
                animator.SetBool("Run", false);
                animator.SetBool("Wait", true);


            }

            if (touch.phase == TouchPhase.Began)
            {
             

                
                swipe1y = touch.position.y;
               
            }

            //if (touch.phase == TouchPhase.Stationary)
            //{
            //    swipe1x = touch.position.x;
            //    swipe1y = touch.position.y;
            //}
            if (touch.phase == TouchPhase.Moved)
            {
                //Kosu anim basla ve durmayi kontrol amacli degis
                animator.SetBool("Wait", false);
                animator.SetBool("Run", true);
                // x ve y ' nin positionşarı
               
                swipe2y = touch.position.y;

               

                
                //ControlHorizontal();
                //ControlVertical();


                //Normalized iki tuşa aynı anda başıldığında total da max 1 olarak ayarlanır 
                //dik ucgen mantıgı ile olusmaz. her dogrultuya 1 birim olur
               

                

            }
            

            //vektorun uzunlugu kontrol ediyorum magnitude ile 
            if (direction.magnitude > 0.1f)
            {
                //directiondaki nokta hedefimiz oluyor vektor ile konumu 
                //mathf.red2deg capildi sayıyı radyandan dereceye çeviri
                //atan2 x ve z nin yönlerini bölüyor ve rad2deg ile açıya çeviriyorum

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                //Acisal belli surede yavas donme, eularangels o anki rotas
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);//donme isini cozdum

                if (AktifYonDurumu)
                {
                    characterController.Move(direction * speed * Time.deltaTime);
                }
            }



        }





    }




    void DeadCheck()
    { //Olum kontrolu yapiliyor y ekseninde -0.5e esit yada kucuk olursa restart level yapilir
        if (transform.position.y <= -0.4f)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().RestartLevel();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        

        if(collision.gameObject.tag == "Duvar1")
        {
            //degdigi objenin uyesi olur beraberinde doner 
            transform.parent = silindirParentTransform1;
           
        }
        if (collision.gameObject.tag == "Duvar2")
        {
            //degdigi objenin uyesi olur beraberinde doner 
            transform.parent = silindirParentTransform2;
        
        }
        if (collision.gameObject.tag == "Duvar3")
        {
            //degdigi objenin uyesi olur beraberinde doner 
            transform.parent = silindirParentTransform3;
            
        }

        if (collision.gameObject.tag == "Duvar4")
        {
           
            //kendi objesine yollanir
            transform.parent = playerParentTransform;
        }
        if (collision.gameObject.tag == "HorizontalObstacle")
        {
           // GameObject.Find("GameManager").GetComponent<GameManager>().RestartLevel();
            //transform.position = new Vector3(-2.191f, 0.077f, -0.009f);
        }
        if (collision.gameObject.tag == "KameraAktif")
        {
            AktifYonDurumu = false;
            wallAktif.SetActive(true);
            DuvarBoyamaDurum = true;
            bitisDuvari.SetActive(false);
            WallPainter.SetActive(true);
            animator.SetBool("Bitis", true);
        }
       
    }


    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Girls")
    //    {
    //      float girlsKonum =  other.gameObject.transform.position.x;
    //        if (girlsKonum > transform.position.x)
    //        {

    //        }
    //       // else if(transform.position.x>)
    //    }
    //}





}
