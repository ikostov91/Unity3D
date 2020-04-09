using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    // Parameters
    [SerializeField] private float _mainEngineThrust = 1200f;
    [SerializeField] private float _rotationThrust = 180f;
    [SerializeField] private float _sceneLoadDelay = 1f;
    [SerializeField] private float _sceneRestartDelay = 2f;

    // Audio clips
    [SerializeField] private AudioClip _mainEngine;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _winLevelSound;

    // Particle Effects
    [SerializeField] private ParticleSystem _engineParticles;
    [SerializeField] private ParticleSystem _successParticles;
    [SerializeField] private ParticleSystem _deathParticles;

    // Components
    private Rigidbody _myRigidBody;
    private AudioSource _myAudioSource;

    // State
    private PlayerState _playerState = PlayerState.Alive;
    private bool _collisionsEnabled = true;
    
    // Start is called before the first frame update
    void Start()
    {
        this._myRigidBody = GetComponent<Rigidbody>();
        this._myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this._playerState == PlayerState.Alive)
        {
            this.RespondToThrustInput();
            this.RespondToRotateInput();
        }

        if (Debug.isDebugBuild)
        {
            this.RespondToDebugKeys();
        }
    }

    private void RespondToThrustInput()
    {
        // GetKey - continious press
        // GetKeyDown - one time press
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            this.ApplyThrust();
        }
        else
        {
            this.StopApplyingThrust();
        }
    }

    private void ApplyThrust()
    {
        Vector3 thrust = Vector3.up * this._mainEngineThrust * Time.deltaTime;
        this._myRigidBody.AddRelativeForce(thrust);

        if (!this._myAudioSource.isPlaying)
        {
            this._myAudioSource.PlayOneShot(this._mainEngine);
        }

        if (!this._engineParticles.isPlaying)
        {
            this._engineParticles.Play();
        }
    }

    private void StopApplyingThrust()
    {
        this._myAudioSource.Stop();
        this._engineParticles.Stop();
    }

    private void RespondToRotateInput()
    {
        this._myRigidBody.angularVelocity = Vector3.zero; // remove rotation due to physics

        Vector3 rotation = Vector3.forward * this._rotationThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.Rotate(rotation);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.Rotate(-1 * rotation);
        }
    }

    private void OnCollisionEnter(Collision otherCollider)
    {
        if (this._playerState != PlayerState.Alive || !this._collisionsEnabled) // ignore collisions
        {
            return;
        }

        switch (otherCollider.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                this.StartSuccessSequence();
                break;
            default:
                this.StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        this._playerState = PlayerState.Transcending;
        this._myAudioSource.Stop();
        this._myAudioSource.PlayOneShot(this._winLevelSound);
        this._successParticles.Play();
        this.Invoke("FinishLevel", this._sceneLoadDelay);
    }

    private void StartDeathSequence()
    {
        this._playerState = PlayerState.Dying;
        this._myAudioSource.Stop();
        this._myAudioSource.PlayOneShot(this._deathSound);
        this._deathParticles.Play();
        this.Invoke("RestartLevel", this._sceneRestartDelay);
    }

    private void FinishLevel()
    {
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    private void RestartLevel()
    {
        FindObjectOfType<LevelLoader>().RestartScene();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            int sceneIndex = this.GetNextSceneIndex();
            SceneManager.LoadScene(sceneIndex);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            this._collisionsEnabled = !this._collisionsEnabled;
            Debug.Log("Collisions enabled: " + this._collisionsEnabled);
        }
    }

    private int GetNextSceneIndex()
    {
        int allScenesCount = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1 < allScenesCount) ? (currentSceneIndex + 1) : 0;

        return nextSceneIndex;
    }
}
