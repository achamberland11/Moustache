using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeMonte : MonoBehaviour
{
    public GameObject plateFormeMonte;  //GameObject des plateformes qui vont vers le haut

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlateformesMonte")
        {
            collision.gameObject.GetComponent<SliderJoint2D>().useMotor = true;
            GetComponent<Animator>().SetBool("animSaut", false);
            GetComponent<Animator>().SetBool("animCourse", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlateformesMonte")
        {
            collision.gameObject.GetComponent<SliderJoint2D>().useMotor = false;
        }
    }
}
