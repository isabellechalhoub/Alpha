using UnityEngine;
using System.Collections;
using Prime31;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
    #region Vars
    public MovingPlatform mv;
    public GameObject gateFront;
    public GameObject gateBack;
    public ThoughtBubble thought;
    private Animator animator;
	public GameObject gameCamera;
	public GameObject healthBar;
	public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject winPanel;
	public float walkSpeed = 3;
	public float gravity = -35;
	public float jumpHeight = 2;
	public int health = 100;
	public BoxCollider2D coll;
	public GameObject healthNum;
    public GameObject shield;
    public GameObject sword;
    private bool windy;
    private bool topwind;
    private Vector3 swordStartPos;
    private Vector3 swordEndPos;
	private bool shieldin = false;
	private bool floatin = false;
	private bool playerControl = true;
	private int currHealth = 0;
	public CharacterController2D _controller;
	private AnimationController2D _anim_control;
    private bool swinging = false;
    private bool pause = false;
    private bool wind;

    private GameObject journal;
    private GameObject jar;
    private GameObject lunchbox;
    private GameObject photo;
    private GameObject plushie;
    private GameObject gameboy;
    private GameObject shell;
    private GameObject journalUI;
    private GameObject jarUI;
    private GameObject lunchboxUI;
    private GameObject photoUI;
    private GameObject plushieUI;
    private GameObject gameboyUI;
    private GameObject shellUI;

	public AudioClip windSound;
	public AudioClip umbrellaOpenSound;
	public AudioClip umbrellaSwipeSoud;
	public AudioClip umbrellaHitSound;

    public GameManager gm;

    #endregion

    void Start ()
    {
        shield = GameObject.FindGameObjectWithTag("Shield");
        sword = GameObject.FindGameObjectWithTag("Sword");
		coll = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D> ();
		_controller = gameObject.GetComponent<CharacterController2D>();
		_anim_control = gameObject.GetComponent<AnimationController2D>();
        animator = gameObject.GetComponent<Animator>();

        journal = GameObject.Find("Journal");
        jar = GameObject.Find("Jar");
        lunchbox = GameObject.Find("Lunchbox");
        photo = GameObject.Find("Photo");
        plushie = GameObject.Find("Plushie");
        gameboy = GameObject.Find("GameBoy");
        shell = GameObject.Find("Shell");

        journalUI = GameObject.Find("JournalUI");
        jarUI = GameObject.Find("JarUI");
        lunchboxUI = GameObject.Find("LunchboxUI");
        photoUI = GameObject.Find("PhotoUI");
        plushieUI = GameObject.Find("PlushieUI");
        gameboyUI = GameObject.Find("GBUI");
        shellUI = GameObject.Find("ShellUI");

		GameObject.Find("CollecUI").SetActive(true);
		GameObject.Find("HealthUI").SetActive(true);

        sword.SetActive(false);
        shield.SetActive(false);

        gameCamera.GetComponent<CameraFollow2D> ().startCameraFollow (this.gameObject);
		winPanel.SetActive(false);
		gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
		currHealth = health;
        _anim_control.setAnimation("NewFall");
        windy = false;
        topwind = false;

		setUpUI ();

        int level = PlayerPrefs.GetInt("Level");
        if (level == 1)
            gameObject.transform.position = Checkpoint.instance.spawn;
    }

	void Update ()
    {
		if (playerControl) 
		{
			Vector3 velocity = PlayerInput ();
			_controller.move (velocity * Time.deltaTime);
		}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            if (pause)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                playerControl = false;
                animator.SetBool("isIdle", true);
                //_animator.setAnimation("NewIdle");
            }
            else
            {
				UnPause();
                //Time.timeScale = 1;
                //pausePanel.SetActive(false);
                //playerControl = true;
            }
        }
	}
		
#region Movement
	private Vector3 PlayerInput()
	{
		Vector3 velocity = _controller.velocity;
		velocity.x = 0;

        #region moving platform parenting
        if (_controller.isGrounded && _controller.ground != null && (_controller.ground.tag.Equals("MovingPlatform") || _controller.ground.tag.Equals("Rotating Platform")) ) 
		{
			this.transform.parent = _controller.ground.transform;
		}
		else 
		{
            if (this.transform.parent != null)
                this.transform.parent = null;
		}
		#endregion

		#region running left/right
		// Left arrow key
		if (Input.GetAxis ("Horizontal") < 0 && !shieldin && !swinging)
		{
			velocity.x = -walkSpeed;
			if (_controller.isGrounded && !floatin) 
			{
                animator.SetBool("isWalking", true);
				//_anim_control.setAnimation ("NewWalk");
				//_animator.setFacing ("Left");
			}
            else if (!floatin){
                animator.SetBool("isFalling", true);
            }
            if (!floatin)
                _anim_control.setFacing("Left");
		}
        //else if (Input.GetKeyUp(KeyCode.LeftArrow) && !shieldin && !swinging)
        //{
        //    velocity.x = 0;
        //    if (!floatin)
        //        _animator.setAnimation("Idle");
        //}

        // Right arrow key
        else if (Input.GetAxis ("Horizontal") > 0 && !shieldin && !swinging) 
		{
			velocity.x = walkSpeed;
			if (_controller.isGrounded && !floatin) 
			{
                animator.SetBool("isWalking", true);
				//_anim_control.setAnimation ("NewWalk");
				//_animator.setFacing ("Right");
			}
            else if (!floatin){
                animator.SetBool("isFalling", true);
            }
            if (!floatin)
                _anim_control.setFacing("Right");
		}
        //else if (Input.GetKeyUp(KeyCode.RightArrow) && !shieldin && !swinging)
        //{
        //    velocity.x = 0;
        //    if (!floatin)
        //        _animator.setAnimation("Idle");
        //}
		#endregion

		#region idle
		//Idle
		else 
		{
			if (_controller.isGrounded && currHealth != 0 && !shieldin && !swinging) 
			{
                velocity.x = 0;
                animator.SetBool("isWalking", false);
                //_anim_control.setAnimation("NewIdle");
			}
		}
		#endregion

		#region Jump/Float
		// Space bar - Jump
		if (Input.GetKeyDown (KeyCode.Space) && !shieldin && _controller.isGrounded && !swinging && !floatin) 
		{
            animator.SetTrigger("isJumping");
			//_anim_control.setAnimation("NewJump");
			velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
		} 
		else if ((Input.GetKeyDown (KeyCode.Space) && !_controller.isGrounded) || floatin) 
		{
            if (!floatin) 
            {
				SoundManager.instance.PlaySingle(umbrellaOpenSound);
                animator.SetBool("isFloating", true);
                //_anim_control.setAnimation("NewDeploy");
            }
            if (topwind) { }
            else if (!windy)
			{
                velocity.y = -2;
			}
			floatin = true;
		}
		if (_controller.isGrounded || Input.GetKeyUp (KeyCode.Space))
		{
			if (!_controller.isGrounded)
			{
                animator.SetBool("isFalling", true);
                animator.SetBool("isFloating", false);
				//_anim_control.setAnimation("NewFall");
                wind = false;
			}
            else
            {
                animator.SetBool("isFloating", false);
                animator.SetBool("isFalling", false);
                //_animator.setAnimation("Land");
            }
            if (!wind)
            {
                floatin = false;
                gravity = -35;
            }
		}

        if (!_controller.isGrounded)
        {
            wind = false;
        }
        #endregion

        #region shield
        //Shield up and down
        //if (Input.GetAxis("Fire1") > 0) {
        //	shieldin = true;
        //} else
        //	shieldin = false;
        if (Input.GetKey(KeyCode.X) && !swinging) {
            shieldin = true;
            shield.SetActive(true);
        }
        else {
            shieldin = false;
            shield.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.X) && !swinging)
        {
            animator.SetBool("isBlocking", true);
			SoundManager.instance.PlaySingle(umbrellaOpenSound);
            //_anim_control.setAnimation("NewPreblock");
            shieldin = true;
            shield.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            animator.SetBool("isBlocking", false);
            //_anim_control.setAnimation("NewUnblock");
            shieldin = false;
            shield.SetActive(false);
        }
        #endregion

        #region sword swing
        // swing dat sword bb
        if (Input.GetKey(KeyCode.C) && !shieldin) {
            swinging = true;
            //sword.SetActive(true);
            sword.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.C) && !shieldin)
        {
			SoundManager.instance.PlaySingle(umbrellaSwipeSoud);
            animator.SetTrigger("isSlashing");
            //_anim_control.setAnimation("NewSlash");
            swinging = true;
            //sword.SetActive(true);
            sword.SetActive(true);

            //Transform pos = sword.GetComponent<Transform>();
            //swordStartPos = pos.localPosition;
            //Vector3 axis = new Vector3(pos.localPosition.x, pos.localPosition.y - pos.localPosition.sqrMagnitude, 0);
            //pos.RotateAround(axis, axis, 20 * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.C)) {
            sword.SetActive(false);
            swinging = false;
        }
        #endregion

        // Change velocity.
        velocity.y += gravity * Time.deltaTime;
		return velocity;
	}

    //void FixedUpdate()
    //{
    //    if(Input.GetKey(KeyCode.C) && !shieldin)
    //        swordrb.MoveRotation(swordrb.rotation + slashSpeed * Time.fixedDeltaTime);
    //}

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag.Equals("Rotating Platform"))
        {
            transform.rotation = Quaternion.identity;
        }
        if (coll.tag.Equals("Wind"))
        {
            windy = false;
            gravity = -35;
        }
        else if (coll.tag.Equals("TopWind"))
        {
            topwind = false;
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag.Equals("Wind") && floatin)
        {
			//SoundManager.instance.PlaySingle(windSound);
            gravity = 35;
            windy = true;
        }
        else if(coll.tag.Equals("TopWind"))
        {
            topwind = true;
        }
    }

#endregion

#region Damage/Death/Winning
	// When the player collides with the death collider
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "KillZ") {
			PlayerFallDeath ();
		} else if (col.tag == "Damaging")
			PlayerDamage (1);
		else if (col.tag == "YouWin")
			Winning ();
		else if (col.tag == "Enemy" && (Input.GetKey (KeyCode.X) || Input.GetKey (KeyCode.C))) {
		} else if (col.tag == "Enemy")
			PlayerDamage (1);
		else if (col.tag == "FallingPlatform") {
			StartCoroutine (fallingPlat (col));
		} else if (col.tag.Equals ("Checkpoint")) {
			Checkpoint.instance.UpdateSpawn (col.transform.position);
		} else if (col.tag.Equals ("Lvl1Bubble")) {
			thought.ChangeBubble("Level 1");
		} else if (col.tag.Equals ("PatrolBubble")) {
			thought.ChangeBubble("Patrol");
		} else if (col.tag.Equals ("BanjoBubble")) {
			thought.ChangeBubble("Banjo");
		} else if (col.tag.Equals ("GopherBubble")) {
			thought.ChangeBubble("Gopher");
		} else if (col.tag.Equals ("LunchboxBubble")) {
			thought.ChangeBubble("Lunchbox");
		}  else if (col.tag.Equals ("JarBubble")) {
			thought.ChangeBubble("Jar");
		}
        else if (col.tag.Equals("Switch"))
        {
            OpenGate();
        }

        #region Collectables
        else if (col.tag.Equals("Collectable")) {
            if (col.name.Equals("Journal")) {
                journal.SetActive(false);
                journalUI.SetActive(true);
				PlayerPrefs.SetInt("Journal", 1);
            }
            else if (col.name.Equals("Jar")) {
                jar.SetActive(false);
                jarUI.SetActive(true);
				PlayerPrefs.SetInt("Jar", 1);
            }
            else if (col.name.Equals("Lunchbox")) {
                lunchbox.SetActive(false);
                lunchboxUI.SetActive(true);
				PlayerPrefs.SetInt("Lunchbox", 1);
            }
            else if (col.name.Equals("Photo")) {
                photo.SetActive(false);
                photoUI.SetActive(true);
				PlayerPrefs.SetInt("Photo", 1);
            }
            else if (col.name.Equals("Plushie")) {
                plushie.SetActive(false);
                plushieUI.SetActive(true);
				PlayerPrefs.SetInt("Plushie", 1);
            }
            else if (col.name.Equals("GameBoy")) {
                gameboy.SetActive(false);
                gameboyUI.SetActive(true);
				PlayerPrefs.SetInt("Gameboy", 1);
            }
            else if (col.name.Equals("Shell")) {
                shell.SetActive(false);
                shellUI.SetActive(true);
				PlayerPrefs.SetInt("Shell", 1);
            }
            #endregion
        }
    }
		
	private void Winning()
	{
		playerControl = false;
        animator.SetBool("isIdle", true);
        gm.NextLevel();
	}

	// Changes player health when damage is taken. checks for death
	private void PlayerDamage(int damage)
	{
		currHealth -= damage;

        StartCoroutine(Flash());

        if (currHealth == 4)
        {
            GameObject.Find("Heart3").SetActive(false);
			PlayerPrefs.SetInt ("Heart3", 0);
        }
        if (currHealth == 2)
        {
            GameObject.Find("Heart2").SetActive(false);
			PlayerPrefs.SetInt ("Heart2", 0);
        }
        if (currHealth <= 0)
        {
            GameObject.Find("Heart1").SetActive(false);
			PlayerPrefs.SetInt ("Heart1", 0);
            PlayerDeath();
        }
	}

	// Play death animation
	private void PlayerDeath()
	{
        animator.SetBool("isIdle", true);
		playerControl = false;
		gameOverPanel.SetActive(true);
		SoundManager.instance.PlayDeathMusic();
		RemoveUI();
	}

	// Stops the camera follow and reduces health
	private void PlayerFallDeath()
	{
		currHealth = 0;
        playerControl = false;
		gameCamera.GetComponent<CameraFollow2D>().stopCameraFollow();
		gameOverPanel.SetActive(true);
		RemoveUI();
		SoundManager.instance.PlayDeathMusic();
	}

    IEnumerator Flash() 
    {
        for (int i = 0; i < 2; i++) 
            {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            yield return new WaitForSeconds(0.25f);
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator fallingPlat(Collider2D col) 
    {
        Rigidbody2D rb2d = col.gameObject.GetComponentInParent<Rigidbody2D>();
        yield return new WaitForSeconds(0.5f);
        rb2d.isKinematic = false;
        yield return 0;
    }

	public void UnPause()
	{
		Time.timeScale = 1;
		pausePanel.SetActive(false);
		playerControl = true;
	}
	void RemoveUI()
	{
		try {
			GameObject.Find("Heart3").SetActive(false);
			GameObject.Find("Heart2").SetActive(false);
			GameObject.Find("Heart1").SetActive(false);
			journalUI.SetActive(false);
			jarUI.SetActive(false);
			lunchboxUI.SetActive(false);
			photoUI.SetActive(false);
			plushieUI.SetActive(false);
			gameboyUI.SetActive(false);
			shellUI.SetActive(false);
			GameObject.Find("CollecUI").SetActive(false);
			GameObject.Find("HealthUI").SetActive(false);
		}
		catch(System.Exception e) {
		}
	}

	void setUpUI()
	{
		if (PlayerPrefs.GetInt ("Journal") == 1)
			journalUI.SetActive (true);
		else
			journalUI.SetActive (false);
		if (PlayerPrefs.GetInt ("Jar") == 1)
			jarUI.SetActive (true);
		else
			jarUI.SetActive (false);
		if (PlayerPrefs.GetInt ("Lunchbox") == 1)
			lunchboxUI.SetActive (true);
		else
			lunchboxUI.SetActive (false);
		if (PlayerPrefs.GetInt ("Photo") == 1)
			photoUI.SetActive (true);
		else
			photoUI.SetActive (false);
		if (PlayerPrefs.GetInt ("Plushie") == 1)
			plushieUI.SetActive (true);
		else
			plushieUI.SetActive (false);
		if (PlayerPrefs.GetInt ("Gameboy") == 1)
			gameboyUI.SetActive (true);
		else
			gameboyUI.SetActive (false);
		if (PlayerPrefs.GetInt ("Shell") == 1)
			shellUI.SetActive (true);
		else
			shellUI.SetActive (false);
	}

    void OpenGate()
    {
        gateFront.transform.rotation = new Quaternion(0f, 90f, 0f,gateBack.transform.rotation.w);
        gateBack.transform.rotation = new Quaternion(0f, 90f, 0f, gateBack.transform.rotation.w);
        mv.ChangeEnd();
    }
#endregion
}
