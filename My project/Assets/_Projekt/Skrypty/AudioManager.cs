using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("èrÛd≥a düwiÍku")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Muzyka")]
    public AudioClip backgroundMusic;

    [Header("Efekty düwiÍkowe")]
    public AudioClip enemyDeathSound;
    public AudioClip towerPlaceSound;
    public AudioClip upgradeSuccessSound;
    public AudioClip notEnoughGoldSound;
    public AudioClip loseSound;

    [Header("G≥oúnoúÊ ustawiana w Inspectorze")]
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;

    [Range(0f, 1f)]
    public float sfxVolume = 0.8f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SetupMusic();
        SetupSfx();
    }

    private void SetupMusic()
    {
        if (musicSource == null)
        {
            Debug.LogWarning("Brak MusicSource w AudioManager.");
            return;
        }

        musicSource.volume = musicVolume;
        musicSource.loop = true;
        musicSource.playOnAwake = false;

        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    private void SetupSfx()
    {
        if (sfxSource == null)
        {
            Debug.LogWarning("Brak SfxSource w AudioManager.");
            return;
        }

        sfxSource.volume = sfxVolume;
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
    }

    public void PlaySfx(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        if (sfxSource == null)
        {
            return;
        }

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayEnemyDeath()
    {
        PlaySfx(enemyDeathSound);
    }

    public void PlayTowerPlace()
    {
        PlaySfx(towerPlaceSound);
    }

    public void PlayUpgradeSuccess()
    {
        PlaySfx(upgradeSuccessSound);
    }

    public void PlayNotEnoughGold()
    {
        PlaySfx(notEnoughGoldSound);
    }

    public void PlayLose()
    {
        PlaySfx(loseSound);
    }
}