using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        int musicPlayersCount = FindObjectsOfType<MusicPlayer>().Length;

        if (musicPlayersCount > 1) // more than 1 music player in scene
        {
            Destroy(this.gameObject);
        } 
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
