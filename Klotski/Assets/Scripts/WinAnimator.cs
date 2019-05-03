using UnityEngine;
using UnityEngine.UI;

public class WinAnimator : MonoBehaviour
{
    [SerializeField] private Text stepCounter;
    [SerializeField] private Text time;
    [SerializeField] private Text stageName;
    
    public void Start()
    {
        time.text = "用时 " + Store.Time;
        stepCounter.text = "总步数 " + Store.Steps;
        stageName.text = Store.NextStageConfig.GetStageName();
    }

    public void Replay()
    {
        PlayHideAnimation();
        gameObject.GetComponent<SceneLoader>().LoadPreviousSceneDelay((float) 0.5);
    }

    public void Home()
    {
        PlayHideAnimation();
        Store.NextStageConfig = null;
        gameObject.GetComponent<SceneLoader>().LoadStartSceneDelay((float) 0.5);
    }

    private static void PlayHideAnimation()
    {
        var anim = GameObject.Find("Win Panel").GetComponent<Animator>();
        anim.Play("Win Panel Hide");
        anim = GameObject.Find("Statistics").GetComponent<Animator>();
        anim.Play("Statistics Hide");
    }
    
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Home();
        }
    }
}