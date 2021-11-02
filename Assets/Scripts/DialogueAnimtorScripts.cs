using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueAnimtorScripts : MonoBehaviour
{
    Animator animator;
    [YarnCommand("PlayAnimation")]
    public void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
