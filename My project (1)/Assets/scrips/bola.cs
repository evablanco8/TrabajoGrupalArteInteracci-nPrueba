using UnityEngine;

public class Personaje : MonoBehaviour
{
    public string nombre = "Heroe";
    public int salto = 300;
    public float velocidad = 5.0f;
    public bool estaVivo = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, salto));
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidad, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-velocidad, 0));
        }
    }
}