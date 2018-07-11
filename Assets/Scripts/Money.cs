using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Money : MonoBehaviour {
    public GameObject mainPanel;
    public GameObject theEndPanel;
    public GameObject upgradesPanel;
    public GameObject pongGame;
    public GameObject console;
    public GameObject timeLeft;
    public GameObject timeWasted;
    public GameObject loadingAnimation;
    public GameObject browser;
    public GameObject music;
    public GameObject help;
    public GameObject pongGameGroup;
    public GameObject consoleWindow;
    public GameObject browserWindow;
    public Sprite trollSprite;
    public Text moneyText;
    public float probabilityOfSuccess = 0.001f;
    public List<GameObject> imageList;
    private Animator anim;
    private float moneyIncrement = 0f;
    private float probability = 0f;
    [SerializeField]        // DELETE THIS!-----------------------------------------------------------------------
    private float total = 0f;
    private bool doubleTime = false;
    private bool timeWastedUpgrade = false;
    private bool browserUpgrade = false;
    private bool timeLeftUpgrade = false;
    private bool feedthebeast = false;
    private bool consoleUpgade = false;
    private bool cry4help = false;
    private float ts;
    private Text timeWastedText;
    private Text timeLeftText;
    private Text cryForHelp;
    private int seconds, minutes, hours, helpQuotesIter = 0, quotesIter = 0, iter= 0;
    private float time,time2,time3;
    private List<string> timeLeftQuotes 
        = new List<string>{ "NEVER!" ,
                                "I don't know! I don't care!",
                                "Don't you have anything better to do?",
                                "Seriously you are still here?",
                                "You seem quite persistent i will help you a bit",
                                "20 Minutes",
                                "Did I say 20? I meant more like 30",
                                "Or 50?", "Whatever I don't care im going to sleep",
                                "Just DON'T feed the beast!" };
    private List<string> helpQuotes
        = new List<string> {"Welcome to tech support",
                        "You are 10th in line please wait",
                        "Your request will now be submitted",
                        "Hello I am your helper, how can I help you?",
                        "OH sorry I forgot that you can't acutally speak to me",
                        "Oh well... I suppose I can give you a hint",
                        "OR SHOULD I?",
                        "OK fine I'll help you. -.^",
                        "42... I hope you know what this means.",
                        "And by the way I am stealing some of your money for that hint."};
    private List<string> questions
      = new List<string> { "Do midgets have night vision?",
                                "What is the square root of evil?",
                                "What is the meaning of life, the universe and everything?",
                                "How many holes in a polo?",
                                ".sdrawkcab noitseuq siht rewsna",
                                "What can you put in a bucket to make it lighter?",
                                "What is the 7th letter of the alphabet?",
                                "Do you like this game?"};
    private List<string> answers
        = new List<string> { "yes", "25.80", "42", "4", "ko", "torch", "h", "no" };
    private string input;

    // Use this for initialization
    void Start()
    {
        mainPanel.SetActive(true);
        theEndPanel.SetActive(false);     
    }
    void Update()
    {
        if (timeWastedUpgrade)
        {
            ts = Time.realtimeSinceStartup;
            hours = (int)Mathf.Floor(ts / 3600);
            float remainder = ts - hours * 3600;
            minutes = (int)Math.Floor(remainder / 60);
            remainder = remainder - minutes * 60;
            seconds = (int)(remainder);

            timeWastedText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        if (browserUpgrade)
        {
            time += Time.deltaTime;
            if (time >= 600)
            {
                Image browserimage = GameObject.Find("BrowserImage").GetComponent<Image>();
                browserimage.sprite = trollSprite;
                browserimage.GetComponent<AudioSource>().enabled = true;
            }
        }
        if (timeLeftUpgrade)
        {
            time2 += Time.deltaTime;
            
            Debug.Log(time2);
            if (time2 >= 100)
            {
                time2 = 0;
                if (quotesIter <= timeLeftQuotes.Count)
                {
                    if (quotesIter == timeLeftQuotes.Count && feedthebeast)
                    {
                        timeLeftText.text = "What did I tell you about NOT to feed the beast? THATS IT IM LEAVING!";
                    }
                    timeLeftText.text = timeLeftQuotes[quotesIter];
                }
               quotesIter++;
            }    
        }
        if (cry4help)
        {
            time3 += Time.deltaTime;

            Debug.Log(time3);
            if (time3 >= 3)
            {
                time3 = 0;
                if (helpQuotesIter <= helpQuotes.Count)
                {
                    if (helpQuotesIter == helpQuotes.Count)
                    {
                        if (total >= 1000)
                        {
                            total -= 1000;
                        }
                        else total = 0;
                    }
                    cryForHelp.text = helpQuotes[helpQuotesIter];
                }
                helpQuotesIter++;
            }
        }
        if (consoleUpgade)
        {
            Text consoleText = GameObject.Find("ConsoleText").GetComponent<Text>();
            InputField inputField = GameObject.Find("InputField").GetComponent<InputField>();
            Text resultText = GameObject.Find("ResultText").GetComponent<Text>();
            string text;

            consoleText.text = questions[iter];
            EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));
            if (Input.GetKey(KeyCode.Return) && inputField.text != "")
            {
               
                Debug.Log("yeeeyyy boiii");
                text = inputField.text;
                inputField.text = "";
                if (text.Equals(answers[iter], StringComparison.InvariantCultureIgnoreCase))
                {
                    iter++;
                    resultText.text = "CORRECT";
                    consoleText.text = questions[iter];
                    Debug.Log("we made it bois!");
                }
                else
                {
                    iter = 0;
                    resultText.text = "WRONG";
                    consoleText.text = questions[iter];
                }
               
                Debug.Log(questions[iter] + " " + answers[iter]);
            }
        }        
    }

    public void BuyUpgradesMenu()
    {
        upgradesPanel.SetActive(true);
    } //DONE
    public void CloseUpgradesWindow()
    {
        upgradesPanel.SetActive(false);
    } //DONE
    public void EarnMoney()
    {
        moneyIncrement = UnityEngine.Random.Range(0.0f, 5.0f);
        total += moneyIncrement;
        moneyText.text = "Money: $" + total.ToString("0.00");
        probability += moneyIncrement * probabilityOfSuccess;
    }  //DONE 
    public void UpdateLoadingBar()
    {
        if (doubleTime)
        { 
            if (probability * 0.02 >= 1 && imageList[0] != null)
            {
                probability = -5;
                imageList[0].SetActive(true);
                imageList.Remove(imageList[0]);
            }
        }
        else
        { 
            if (probability * 0.01 >= 1 && imageList.Count != 0)
            {
                probability = -10;
                imageList[0].SetActive(true);
                imageList.Remove(imageList[0]);
            }
        }
        if (imageList.Count == 0)
        {
            mainPanel.SetActive(false);
            theEndPanel.SetActive(true);
        }
    }  //DONE
    public void HandleDoubleTimeUpgrade(Text price)
    { 
        float cost = float.Parse(price.text.Substring(2));
        
        if (total >= cost)
        {
            total -= cost;
            moneyText.text = "Money: $" + total.ToString("0.00");
            GameObject.Find("DoubleTime").SetActive(false);
            doubleTime = true;
        }
    }  //DONE
    public void HandleLoadingUpgrade(Text price)
    {
        float cost = float.Parse(price.text.Substring(2));

        if (total >= cost)
        {
            total -= cost;
            moneyText.text = "Money: $" + total.ToString("0.00");
            GameObject.Find("LoadingScreen").SetActive(false);
            loadingAnimation.SetActive(true);
            anim = loadingAnimation.GetComponent<Animator>();
            anim.SetTrigger("Load");
        }
    }  //DONE
    public void HandlePongUpgrade(Text price)
    {
        float cost = float.Parse(price.text.Substring(2));
        if (total >= cost)
        {
            total -= cost;
            moneyText.text = "Money: $" + total.ToString("0.00");
            GameObject.Find("Pong").SetActive(false);
            pongGame.SetActive(true);
        }
    }  //DONE
    public void HandleBrowserUpgrade(Text price)
    {
        float cost = float.Parse(price.text.Substring(2));
        if (total >= cost)
        {
            total -= cost;
            moneyText.text = "Money: $" + total.ToString("0.00");
            GameObject.Find("BrowserUpgrade").SetActive(false);
            browser.SetActive(true);
            browserUpgrade = true;
        }
    } //DONE
    public void HandleMusicUpgrade(Text price)
    {
        float cost = float.Parse(price.text.Substring(2));
        if (total >= cost)
        {
            total -= cost;
            moneyText.text = "Money: $" + total.ToString("0.00");
            GameObject.Find("Music").SetActive(false);
            music.SetActive(true);
        }
    }    //DONE
    public void HandleConsoleUpgrade(Text price)
    {
        float cost = float.Parse(price.text.Substring(2));
        if (total >= cost)
        {
            total -= cost;
            moneyText.text = "Money: $" + total.ToString("0.00");
            GameObject.Find("ConsoleUpgrade").SetActive(false);
            console.SetActive(true);
        }
    }     //DONE
    public void HandleTimeLeftUpgrade(Text price)
    {
        float cost = float.Parse(price.text.Substring(2));
        if (total >= cost)
        {
            total -= cost;
            moneyText.text = "Money: $" + total.ToString("0.00");
            GameObject.Find("TimeLeft").SetActive(false);
            timeLeft.SetActive(true);
            timeLeftText = GameObject.Find("TimeLeftText").GetComponent<Text>();
            timeLeftUpgrade = true;
        }
    }  //DONE
    public void HandleTimeSpentUpgrade(Text price)
    {
        float cost = float.Parse(price.text.Substring(2));
        if (total >= cost)
        {
            total -= cost;
            timeWastedUpgrade = true;
            moneyText.text = "Money: $" + total.ToString("0.00");
            GameObject.Find("TimeWasted").SetActive(false);
            timeWasted.SetActive(true);
            timeWastedText = GameObject.Find("TimeWastedText").GetComponent<Text>();
        }
    } //DONE
    public void HandleCryForHelpUpgrade(Text price)
    {
        float cost = float.Parse(price.text.Substring(2));
        if (total >= cost)
        {
            total -= cost;
            moneyText.text = "Money: $" + total.ToString("0.00");
            GameObject.Find("CryForHelp").SetActive(false);
            help.SetActive(true);
            cryForHelp = GameObject.Find("Help").GetComponent<Text>();
            cry4help = true;
        }
    } //DONE
    public void HandleFeedTheBeast(Text price)
    {
        float cost = float.Parse(price.text.Substring(2));
        if (total >= cost)
        {
            total -= cost;
            moneyText.text = "Money: $" + total.ToString("0.00");
            probability += 0.5f;
            feedthebeast = true;
        }
    } // DONE
    public void EnablePongGame()
    {
        pongGameGroup.SetActive(true);
    } //DONE
    public void ClosePongGame()
    {
        pongGameGroup.SetActive(false);
    } //DONE
    public void EnableConsole()
    {
        consoleWindow.SetActive(true);
        consoleUpgade = true;
    } //DONE
    public void CloseConsoleWindow()
    {
        consoleWindow.SetActive(false);
        consoleUpgade = false;
    } //DONE
    public void EnableBrowser()
    {
        browserWindow.SetActive(true);
    }  //DONE
    public void CloseBrowserWindow()
    {
        browserWindow.SetActive(false);
    }  //DONE

    bool IsOver()
    {
        return (imageList.Count == 0);
    }  //DONE

}
