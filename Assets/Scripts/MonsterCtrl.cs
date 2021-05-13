using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Enumerable variable of MonsterState
public enum MonsterState { idle, trace, attack, die }

public class MonsterCtrl : MonoBehaviour
{
    //Enum Varialbe that saves monsterState
    public MonsterState monsterState = MonsterState.idle;

    //variables of monster Tr, player Tr, navMeshAgent, monsterAnimator
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    public float traceDist = 10f;   // distance of tracing
    public float attackDist; // distance of attack
    private bool isDie = false; // whether monster is die or not

    [SerializeField]
    private int hp = 100; //monster hp

    // Start is called before the first frame update
    void Start()
    {
        monsterTr = this.gameObject.GetComponent<Transform>();  //assgin monster Tr
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>(); //assgin player Tr, find with tag
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();  //assgin navMeshAgent
        nvAgent.speed = 5f;
        animator = GetComponent<Animator>(); //assign monster animator

        //this is disabled because should check distance first
        //nvAgent.destination = playerTr.position;

        StartCoroutine(this.CheckMonsterState());
        StartCoroutine(this.MonsterAction());
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.isStopped = true;
                    animator.SetBool("IsTrace", false);
                    break;

                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    this.transform.LookAt(playerTr.position);

                    nvAgent.isStopped = false;
                    animator.SetBool("IsAttack", false);
                    animator.SetBool("IsTrace", true);
                    break;

                case MonsterState.attack:
                    nvAgent.isStopped = true;
                    animator.SetBool("IsAttack", true);
                    break;
            }
            yield return null;
        }
    }

    IEnumerator CheckMonsterState()
    {


        while (!isDie)
        {
            //wait for 0.2sec
            yield return new WaitForSeconds(0.2f);
            //check distance
            float dist = Vector3.Distance(monsterTr.position, playerTr.position);

            if (dist <= attackDist)
            {
                monsterState = MonsterState.attack;
            }
            else if (dist <= traceDist)
            {
                monsterState = MonsterState.trace;
            }
            else
            {
                monsterState = MonsterState.idle;
            }
        }
    }

    public void getDamage(float damage)
    {
        hp -= (int)(damage / 2.0f);
        animator.SetTrigger("IsHit");

        if (hp <= 0)
        {
            MonsterDie();
        }
    }

    private void MonsterDie()
    {
        if (isDie)
        {
            return;
        }

        //Stop all coroutines
        StopAllCoroutines();
        isDie = true;
        monsterState = MonsterState.die;
        animator.SetTrigger("IsDie");

        //disable colliders
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;

        foreach (var coli in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coli.enabled = false;
        }

        // Score
        FindObjectOfType<GameManager>().GetScored(2);


    }


    // Update is called once per frame
    //void Update()
    //{
    //    if (Vector3.Distance(monsterTr.position, playerTr.position) < 20 && Vector3.Distance(monsterTr.position, playerTr.position) > 2)
    //    {
    //        nvAgent.destination = playerTr.position;
    //        animator.SetBool("isTrace", true);
    //    }
    //    else
    //    {
    //        //Debug.Log($"When Stoped: {monsterTr.position}, dest : {nvAgent.destination}");

    //        animator.SetBool("isTrace", false);
    //    }
    //    //nvAgent.destination = playerTr.position;
    //    //animator.SetBool("isTrace", true);
    //}
}
