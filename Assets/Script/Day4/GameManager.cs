using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SetupBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] protected Transform wheel;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        GetWheel();
    }
    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene(0);
        }
    }
    public virtual void ChangeScene(int indexScene)
    {
        if (indexScene == 0)
        {
            SceneManager.LoadScene("Home");
            wheel.gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Level_" + indexScene);
            wheel.gameObject.SetActive(false);
        }
    }
    protected virtual void GetWheel()
    {
        if (wheel != null) return;
        wheel = FindObjectOfType<WheelSpin>().transform.parent;
        Debug.Log("Reset " + nameof(wheel) + " in " + GetType().Name);
    }
}
