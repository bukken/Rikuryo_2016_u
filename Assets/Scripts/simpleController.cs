using UnityEngine;
using System.Collections;

public class simpleController : MonoBehaviour {

	public float hook_power = 0.1f;
    public float rl_speed = 0.03f;
    public float fb_speed = 0.05f;
    public float accel = 0.001f;

	Vector3 speed = new Vector3(0.03f, 0,0.05f);
    Vector3 n_speed = Vector3.zero;
	float height = 0.1f;
	float hook_off = 1.25f;
    float rl_accel, fb_accel;
	Rigidbody rb;
	LineRenderer lr;
    Animator animator;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		lr = GetComponent<LineRenderer> ();
        animator = GetComponent<Animator> ();
		lr.SetWidth (0.1f, 0.1f);
        lr.SetVertexCount (2);
	}

	void Update () {
		Move ();
	}

	void Move(){
		Vector3 m_pos = transform.position;

        if ((!Input.GetKey("right") && !Input.GetKey("left")) ||
             (Input.GetKey("right") && Input.GetKey("left")) ||
              Input.GetKeyUp("right") || Input.GetKeyUp("left")){
            rl_accel = 0;
            n_speed.x = 0;
        }
        if (Input.GetKeyDown("right") || (Input.GetKeyUp("left") && Input.GetKey("right"))) rl_accel = accel;
        if (Input.GetKeyDown("left") || (Input.GetKeyUp("right") && Input.GetKey("left")))  rl_accel = -1 * accel;

        if ((!Input.GetKey("up") && !Input.GetKey("down")) ||
            (Input.GetKey("up") && Input.GetKey("down")) ||
             Input.GetKeyUp("up") || Input.GetKeyUp("down")){
            fb_accel = 0;
            n_speed.z = 0;
        }
        if (Input.GetKeyDown("up") || (Input.GetKeyUp("down") && Input.GetKey("up")))   fb_accel = accel;
        if (Input.GetKeyDown("down") || (Input.GetKeyUp("up") && Input.GetKey("down"))) fb_accel = -1 * accel;

        animator.SetFloat("Accel_x", rl_accel);
        animator.SetFloat("Accel_z", fb_accel);
        if (n_speed.x < rl_speed && n_speed.x > -1 * rl_speed)
            n_speed.x += rl_accel;
        if (n_speed.z < fb_speed && n_speed.z > -1 * fb_speed / 3)
            n_speed.z += fb_accel;

        if (is_Landing() && Input.GetButtonDown("Jump")) {
			rb.AddForce(Vector3.up*5, ForceMode.VelocityChange);
            animator.SetTrigger("Jumping");
		}

        if (rb.velocity.magnitude > 50) rb.velocity = rb.velocity.normalized * 50;

        m_pos += n_speed;
		transform.position = m_pos;
        animator.SetBool("Landing", is_Landing());
	}

	void HookShot(Vector3 end_pos){
		Vector3 player_pos = transform.localPosition + new Vector3 (0, hook_off, 0);
		Vector3 target = end_pos - player_pos;
        bool is_r = true;
        float t_gravity = 1.0f;
        if (target.x < 0) is_r = false;
        if (target.x < 1 && target.z < 1) t_gravity = 0.5f;
        if (Physics.CheckSphere(end_pos, 1.0f, (1 << 2))) t_gravity = 0.3f;
        rb.AddForce(hook_power * target.normalized * t_gravity, ForceMode.VelocityChange);
        lr.enabled = true;
        lr.SetPosition(0, player_pos + new Vector3(0.2f * (is_r ? 1 : -1), 0.45f, 0.3f));
        lr.SetPosition(1, end_pos);
        if (Physics.CheckSphere(end_pos, 3.0f, (1 << 2)) &&
            rb.velocity.magnitude > 10)
            rb.velocity = rb.velocity.normalized * 10;
        animator.SetBool("Hooking", true);
        animator.SetFloat("target_x", target.x);
	}

	void HookUp(){
		lr.enabled = false;
        animator.SetBool("Hooking", false);
	}
    
    bool is_Landing(){
        if (Physics.CheckSphere(transform.position, 0.1f, ~(1 << 2))) return true;
        return false;
    }
}
