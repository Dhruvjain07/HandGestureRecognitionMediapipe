using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureToAnimation : MonoBehaviour
{
    private float time;
    private Animator animator;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        text.text = "None";
        Debug.Log("start");
        StartCoroutine(first());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator first(){
        yield return new WaitForSeconds(4f);
        Debug.Log("yo");
        animator.SetInteger("Gesture",1);
        yield return new WaitForSeconds(3f);
        text.text ="One";
        yield return new WaitForSeconds(5f);
        animator.SetInteger("Gesture",2);
        text.text ="Two";
        yield return new WaitForSeconds(4f);
        animator.SetInteger("Gesture",3);
        text.text ="Three";
        yield return new WaitForSeconds(4f);
        animator.SetInteger("Gesture",4);
        text.text ="Four";
        yield return new WaitForSeconds(4f);
        animator.SetInteger("Gesture",5);
        text.text ="Five";
        yield return new WaitForSeconds(3f);
        text.text ="None";
    }
}
