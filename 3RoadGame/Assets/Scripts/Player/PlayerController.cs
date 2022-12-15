using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller;
    private Vector3 kierunek;
    public float speedPrzod;
    private int AktualnaLinia = 1; //0=lewo, 1=środek, 2=prawo | zaczyna od 1
    public float dystansLinii = 4; // dystans pomiedzy dwoma liniami
    public float skok;
    public float grawitacja = -10;
    private bool slide = false;

    public Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerManager.start) //jezeli nie tapnelismy w ekran
        {
            return; // to nie mozemy sie ruszac
        }

        speedPrzod = speedPrzod + 0.5f * Time.deltaTime;

        animator.SetBool("started", true);

        kierunek.z = speedPrzod;
         // skacz z grawitacja

        if(controller.isGrounded) // na ziemi
        {
            
            // kierunek.y = -1;
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                
                kierunek.y = skok; // skacz w kierunku ^
                FindObjectOfType<MusicManager>().PlayMusic("jump");
                animator.SetBool("naZiemi", false); 
                
            }
            
            
        } else // nie na ziemi - wlacz grawitacje
        {
           kierunek.y = kierunek.y + grawitacja * Time.deltaTime; // jak nie jestesmy na platformie to wlacz grawitacje zeby moc zmieniac kierunki w powietrzu.
           animator.SetBool("naZiemi", true);
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(!slide)
            {
                StartCoroutine(Slide());
            }
        }


        //W której linii powiniśmy być na podstawie wciskanych strzałek
        // controller.Move(kierunek*Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.RightArrow)) // w prawo
        {
            AktualnaLinia = AktualnaLinia + 1; // 1 + 1
            if(AktualnaLinia == 3) // jezeli jestesmy z prawej stronie i nacisniemy strzalke w prawo to zostajemy na prawej stronie.
                AktualnaLinia = 2;
        }

                if(Input.GetKeyDown(KeyCode.LeftArrow)) // w lewo
        {
            AktualnaLinia = AktualnaLinia - 1;
            if(AktualnaLinia == -1)// jezeli jestesmy z lewej strony i nacisniemy strzalke w lewo to zostajemy z lewej strony.
                AktualnaLinia = 0;
        }

        //Gdzie powinien byc gracz w przyszlosci...
        //kiedy klikniemy prawa strzalke musimy zmienic aktualniaLinie i tak samo w lewo
        Vector3 PozycjaGracza = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(AktualnaLinia == 0) // jestesmy z lewej strony
        {
            PozycjaGracza = PozycjaGracza + Vector3.left * dystansLinii;
        } 
        else if (AktualnaLinia == 2) // jestesmy z prawej strony
        {
            PozycjaGracza = PozycjaGracza + Vector3.right * dystansLinii;
        }

        if(!PlayerManager.start)
        {
            return;

        }
        controller.Move(kierunek * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, PozycjaGracza, 10 * Time.fixedDeltaTime); // i bedzie na dobrej drodze
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)//hit zbiera informacje w co uderza gracz
    {
        if(hit.transform.tag == "Przeszkoda") // czy uderza w przeszkode z tagiem "Przeszkoda"
        {
            PlayerManager.koniecGry = true;
            FindObjectOfType<MusicManager>().PlayMusic("over");
        }
    }

    private IEnumerator Slide()
    {

        slide = true;
        animator.SetBool("slide", true);
        FindObjectOfType<MusicManager>().PlayMusic("slide");
        controller.center = new Vector3(0, -1, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.3f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;

        animator.SetBool("slide", false);
        slide=false;
    }
}
