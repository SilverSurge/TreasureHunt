using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   

    // attirbutes
    private bool is_moving = false;
    [SerializeField] public float move_speed = 1f;
    private Vector2 move_input;

    public LayerMask solid_objects_layer;
    public LayerMask interactalbles_layer;


    // components
    private Animator player_animator;
    private PlayerDS universal_player_ds;


    private void Awake()
    {
        universal_player_ds = FindObjectOfType<PlayerDS>(); 
        player_animator = GetComponent<Animator>();
    }


    public void HandleUpdate()
    {
        if(!is_moving)
        {
            

            if(move_input.sqrMagnitude > 0.0001)
            {

                player_animator.SetFloat("move_x", move_input.x);
                player_animator.SetFloat("move_y", move_input.y);

                var target_pos = transform.position;
                target_pos.x += move_input.x;
                target_pos.y += move_input.y;

                if(IsWalkable(target_pos))
                    StartCoroutine(Move(target_pos));
            }
        }
        player_animator.SetBool("is_moving", is_moving);
    }

    void OnMove(InputValue value)
    {
        Vector2 temp = value.Get<Vector2>();
        // Debug.Log(temp);

        move_input = temp;
        if(temp.sqrMagnitude < 0.01)
        {
            move_input.x = 0;
            move_input.y = 0;
            //animation_state = 0;
        }
        else if(move_input.x != 0)
        {
            move_input.x = 1*Mathf.Sign(move_input.x);
            move_input.y = 0;
            //animation_state = move_input.x == 1 ? 3 : 4;
        }
        else
        {
            move_input.x = 0;
            move_input.y = 1*Mathf.Sign(move_input.y);
            //animation_state = move_input.y == 1 ? 1 : 2;
        }
        //player_animator.SetInteger("state", animation_state);
        Debug.Log(move_input);
    }

    IEnumerator Move(Vector3 target_pos)
    {
        is_moving = true;

        while((target_pos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, target_pos, move_speed * Time.deltaTime);
            yield return null;
        }

        is_moving = false;
    }

    private bool IsWalkable(Vector3 target_pos)
    {
        if(Physics2D.OverlapCircle(target_pos, 0.2f, solid_objects_layer | interactalbles_layer) != null)
                return false;
        return true;
    }

    void OnInteract(InputValue value)
    {
        Debug.Log("OnInteract");
        Interact();
    }

    void Interact()
    {
        Vector3 facing_dir = new Vector3(player_animator.GetFloat("move_x"), player_animator.GetFloat("move_y"));
        Vector3 interact_pos = facing_dir + transform.position;


        var collider = Physics2D.OverlapCircle(interact_pos, 0.2f, interactalbles_layer);

        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
            //Debug.Log("There is an NPC here");
        }
        else
            Debug.Log("NO NPC FOUND");
    }

    void OnShovel()
    {
        Debug.Log("OnShovel");
        universal_player_ds.use_shovel();
    }
    void OnCompass()
    {
        Debug.Log("OnCompass");
        universal_player_ds.use_compass();
    }

    void OnCheat()
    {
        universal_player_ds.use_cheat();
        Debug.Log("OnCheat");
    }

    
}

/*
For Animation

0 -- idle
1 -- up
2 -- down
3 -- right
4 -- left
 
*/
