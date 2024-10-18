using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController2 : MonoBehaviour
{

    public float speed;
    public float sec = 1f;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            StartCoroutine (SetCountText());
        }
        else if (other.gameObject.CompareTag("Trigger"))
        {
            Application.LoadLevel("Stage 2");
        }
    }

    IEnumerator SetCountText()
    {
        countText.text = "Count: " + count.ToString() + "/39";
        if (count >= 39)
        {
            winText.text = "You Win! You're now returning to the Level Screen.";
            yield return new WaitForSeconds(sec);
            Application.LoadLevel("_StartScreen");
        }
    }
}