using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    int randInt, prizelength;
    public string prizeWon;
    float time;
    bool started, rolling, startable;
    Animator animator;
    Collider2D player;
    Vector3 aim;
    GameObject icon;

    [SerializeField] List<string> prizes;
    [SerializeField] List<float> chance;
    [SerializeField] GameObject animatorSlotOne;
    [SerializeField] GameObject animatorSlotTwo;
    [SerializeField] GameObject animatorSlotThree;

    [SerializeField] GameObject shield, dash, ram, d_jump, f_run, sloMo, magnet, jump_worse, enemy_magnet, s_run;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        time = 0;
        startable = false;
        started = false;
        rolling = false;
        prizeWon = "";
        player = null;
    }

    void getPrize()
    {
        prizelength = prizes.ToArray().Length;

        // only whole number chances
        List<string> combined = new List<string>();
        for (int i = 0; i < prizelength; i++)
        {
            for (int j = 0; j < chance[i]; j++)
            {
                combined.Add(prizes[i]);
            }
        }

        randInt = Random.Range(0, combined.ToArray().Length - 1);
        prizeWon = combined[randInt];

        if (prizeWon != "" && player != null)
        {
            var playerMovement = player.GetComponent<Movement>();
            switch (prizeWon)
            {
                case "Schild":
                    GameObject shield1 = Instantiate(shield);
                    icon = shield1;
                    playerMovement.shieldEnabled = true;
                    break;
                case "Dash":
                    GameObject dash1 = Instantiate(dash);
                    icon = dash1;
                    playerMovement.dashEnabled = true;
                    break;
                case "Ram":
                    GameObject ram1 = Instantiate(ram);
                    icon = ram1;
                    //playerMovement.ramEnabdled = true
                    break;
                case "DoubleJump":
                    GameObject d_jump1 =Instantiate(d_jump);
                    icon = d_jump1;
                    playerMovement.doubleJumpEnabled = true;
                    break;
                case "Gr��ererM�nzRadius":
                    GameObject magnet1 = Instantiate(magnet);
                    icon = magnet1;
                    //enable coin magnet
                    break;
                case "Spr�ngeSchlechter":
                    GameObject jump_worse1 = Instantiate(jump_worse);
                    icon = jump_worse1;
                    playerMovement.jumpCooldown = 2f;
                    break;
                case "GegnerZuEinem":
                    GameObject enemy_magnet1 = Instantiate(enemy_magnet);
                    icon = enemy_magnet1;
                    //enable enemy magnet
                    break;
            }
            icon.transform.position = player.transform.position;
            StartCoroutine(PrizeAnimation());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            startable = true;

            player = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            startable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            aim = player.transform.position;
            aim.y += 3.5f; // distance in units
        }

        if (Input.GetKeyDown(KeyCode.E) && startable)
        {
            animator.SetBool("animationRunning", true);
            started = true;
        }
    }

    IEnumerator PrizeAnimation()
    {
        while (Vector3.Distance(icon.transform.position, aim) > .05f)
        {
            icon.transform.position = Vector3.Lerp(icon.transform.position, aim, 2.5f * Time.deltaTime);
            yield return null;
        }

        Destroy(icon);
    }

    private void FixedUpdate()
    {
        if (started)
        {
            startable = false;
            time += Time.fixedDeltaTime;
            if (time > .4f && !rolling)
            {
                animatorSlotOne.GetComponent<Animator>().SetBool("animationRolling", true);
                animatorSlotTwo.GetComponent<Animator>().SetBool("animationRolling", true);
                animatorSlotThree.GetComponent<Animator>().SetBool("animationRolling", true);
                animator.SetBool("animationRunning", false);
                time = 0;
                rolling = true;
            }
            if (time > 3 && rolling)
            {
                animatorSlotOne.GetComponent<Animator>().SetBool("animationRolling", false);
                animatorSlotTwo.GetComponent<Animator>().SetBool("animationRolling", false);
                animatorSlotThree.GetComponent<Animator>().SetBool("animationRolling", false);
                time = 0;
                rolling = false;
                started = false;
                getPrize();
            }
        }
    }
}
