using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class MainUpdater : MonoBehaviour
{
    private Banner banner;
    public AudioSource audio;
    public AudioClip swipe;
    public AudioClip right;
    public GameObject instructions;
    public GameObject ticket;
    public GameObject zero;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;
    public GameObject seven;
    public GameObject eight;
    public GameObject nine;
    public GameObject anim;
    public GameObject timer;
    private GameObject timerGO;
    private GameObject timer1;
    private GameObject timer2;
    private GameObject ticketGO;
    private GameObject recordOne;
    private GameObject recordTwo;
    private GameObject recordThree;
    private GameObject scoreOne;
    private GameObject scoreTwo;
    private GameObject scoreThree;
    private GameObject ticket11;
    private GameObject ticket22;
    private GameObject ticket33;
    private GameObject ticket44;
    private GameObject ticket55;
    private GameObject ticket66;
    public int score = 0;
    public int record = 0;
    public float speed = 25f;
    private bool isPaused = false;
    private bool onResume = false;
    private bool isStart = true;
    private bool spawn = false;
    private bool despawn = false;
    private bool isSwipeAllowed = false;
    private bool count = false;
    private int ticks = 1;
    private int difficulty = 10;
    private bool isFirstTouch = true;
    private bool up;
    private float fp;
    private float pp;
    private float lp;
    private float dragDistance;
    private Thread spawnTickets;
    private Thread upAndDown;
    int i = 0;
    private bool updateTimer;
    private bool startTimer;
    private bool endTimer;
    private int ticket1 = 0;
    private int ticket2 = 1;
    private int ticket3 = 2;
    private int ticket4 = 3;
    private int ticket5 = 4;
    private int ticket6 = 5;
    private int sum1 = 0;
    private int sum2 = 0;

    void Start()
    {
        try
        {
            record = PlayerPrefs.GetInt("Record", 0);
        }catch
        {

        }
        updateRecord();
        updateScore();
        //audio = GetComponent<AudioSource>();
        audio.volume = 1.0f;
        upAndDown = new Thread(upDown);
        upAndDown.Start();
        dragDistance = Screen.width * 12 / 100;
        RequestBanner();
        }

    void FixedUpdate()
    {
        if (!isPaused)
        {
            if (onResume)
            {
                anim.transform.position = new Vector3(0f, 0f, 0.05f);
                upAndDown = new Thread(upDown);
                upAndDown.Start();
                spawnTickets = new Thread(new ThreadStart(spawnTicket));
                spawnTickets.Start();
                onResume = false;
            }
            if (up)
            {
                anim.transform.Translate(Vector3.up * Time.deltaTime * 0.25f);
            }
            else
            {
                anim.transform.Translate(Vector3.down * Time.deltaTime * 0.25f);
            }
            if (startTimer)
            {
                startTimer = false;
                timerGO = Instantiate(timer, new Vector3(0f, -3.5f, -0.1f), Quaternion.Euler(0, 0, 0));
            }
            if (updateTimer)
            {
                updateTimer = false;
                updateTime();
            }
            if (endTimer)
            {
                endTime();
                endTimer = false;
            }
            if (Input.touchCount > 0 && isStart)
            {
                spawnTickets = new Thread(new ThreadStart(spawnTicket));
                spawnTickets.Start();
                Destroy(instructions);
                isStart = false;
            }
            if (!isStart)
            {
                if (count)
                {
                    int isEqual = UnityEngine.Random.Range(0, 2);
                    ticket1 = UnityEngine.Random.Range(0, 10);
                    ticket2 = UnityEngine.Random.Range(0, 10);
                    ticket3 = UnityEngine.Random.Range(0, 10);
                    ticket4 = 0;
                    ticket5 = 0;
                    ticket6 = 0;
                    sum1 = ticket1 + ticket2 + ticket3;
                    if (isEqual == 0)
                    {
                        ticket4 = UnityEngine.Random.Range(0, 10);
                        ticket5 = UnityEngine.Random.Range(0, 10);
                        ticket6 = UnityEngine.Random.Range(0, 10);
                    }
                    else
                    {
                        simulateEquality(sum1);
                    }
                    sum2 = ticket4 + ticket5 + ticket6;
                    Debug.Log(sum1);
                    Debug.Log(sum2);
                    Debug.Log(ticket1);
                    Debug.Log(ticket2);
                    Debug.Log(ticket3);
                    Debug.Log(ticket4);
                    Debug.Log(ticket5);
                    Debug.Log(ticket6);
                    count = false;
                }
                if (spawn)
                {
                    createTicket();
                    spawn = false;
                    audio.PlayOneShot(swipe);
                }
                if (despawn)
                {
                    Destroy(ticketGO);
                    endTime();
                    isSwipeAllowed = false;
                    audio.PlayOneShot(swipe);
                    restart();
                }
                if (ticketGO.transform.position.y > 1)
                {
                    ticketGO.transform.Translate(Vector3.down * Time.deltaTime * speed);
                }
                else
                {
                    isSwipeAllowed = true;
                }

                if (isSwipeAllowed)
                {
                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        fp = Input.touches[0].position.x;
                        pp = Input.touches[0].position.x;
                        lp = Input.touches[0].position.x;
                    }
                    if (Input.touches[0].phase == TouchPhase.Moved)
                    {
                        pp = lp;
                        lp = Input.touches[0].position.x;
                        if (lp - pp > 0)
                        {
                            if (Mathf.Abs(lp - fp) > dragDistance) {
                                ticketGO.transform.Translate(Vector3.right * Time.deltaTime * speed * Mathf.Abs(lp - fp));
                                if (isFirstTouch)
                                {
                                    audio.PlayOneShot(swipe);
                                    isFirstTouch = false;
                                }
                            }
                        }
                        if (lp - pp < 0)
                        {
                            if (Mathf.Abs(lp - fp) > dragDistance)
                            {
                                ticketGO.transform.Translate(Vector3.left * Time.deltaTime * speed * Mathf.Abs(lp - fp));
                                if (isFirstTouch)
                                {
                                    audio.PlayOneShot(swipe);
                                    isFirstTouch = false;
                                }
                            }
                        }
                    }

                    if (ticketGO.transform.position.x < -3.0f)
                    {
                        if (sum1 != sum2)
                        {
                            score++;
                            if (score > record)
                            {
                                record = score;
                                PlayerPrefs.SetInt("Record", record);
                            }
                            ticks++;
                            Destroy(ticketGO);
                            endTime();
                            updateScore();
                            updateRecord();
                            isSwipeAllowed = false;
                            isFirstTouch = true;
                            spawnTickets.Abort();
                            spawnTickets = new Thread(new ThreadStart(spawnTicket));
                            spawnTickets.Start();
                        }
                        else
                        {
                            Destroy(ticketGO);
                            endTime();
                            restart();
                            isSwipeAllowed = false;
                            spawnTickets.Abort();
                            spawnTickets = new Thread(new ThreadStart(spawnTicket));
                            spawnTickets.Start();
                        }
                    }
                    if (ticketGO.transform.position.x > 3.0f)
                    {
                        if (sum1 == sum2)
                        {
                            score++;
                            if (score > record)
                            {
                                record = score;
                                PlayerPrefs.SetInt("Record", record);
                            }
                            ticks++;
                            Destroy(ticketGO);
                            endTime();
                            updateScore();
                            updateRecord();
                            isSwipeAllowed = false;
                            isFirstTouch = true;
                            spawnTickets.Abort();
                            spawnTickets = new Thread(new ThreadStart(spawnTicket));
                            spawnTickets.Start();
                        }
                        else
                        {
                            Destroy(ticketGO);
                            endTime();
                            restart();
                            isSwipeAllowed = false;
                            spawnTickets.Abort();
                            spawnTickets = new Thread(new ThreadStart(spawnTicket));
                            spawnTickets.Start();
                        }
                    }
                }
            }
        }
    }

    private void RequestBanner()
    {
        // Replace demo R-M-DEMO-320x50 with actual Block ID
        // Following demo Block IDs may be used for testing:
        // R-M-DEMO-320x50
        // R-M-DEMO-320x50-app_install
        // R-M-DEMO-728x90
        // R-M-DEMO-320x100-context
        // R-M-DEMO-300x250
        // R-M-DEMO-300x250-context
        // R-M-DEMO-300x300-context
        string adUnitId = "R-M-518742-1";

        if (this.banner != null)
        {
            this.banner.Destroy();
        }

        this.banner = new Banner(adUnitId, AdSize.BANNER_320x50, AdPosition.BottomCenter);

        this.banner.OnAdLoaded += this.HandleAdLoaded;
        this.banner.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.banner.OnAdOpened += this.HandleAdOpened;
        this.banner.OnAdClosed += this.HandleAdClosed;
        this.banner.OnAdLeftApplication += this.HandleAdLeftApplication;

        this.banner.LoadAd(this.CreateAdRequest());
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        this.banner.Show();
    }

    public void HandleAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        MonoBehaviour.print("HandleAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    #endregion

    private void OnApplicationPause(bool pause)
    {
        isPaused = pause;
        if (isPaused)
        {
            Destroy(ticketGO);
            endTime();
            int scoreBuffer = score;
            int ticksBuffer = ticks;
            int diffBaffer = difficulty;
            restart();
            score = scoreBuffer;
            ticks = ticksBuffer;
            difficulty = diffBaffer;
            updateScore();
            spawnTickets.Abort();
            upAndDown.Abort();
            onResume = true;
        }
    }

    private void upDown()
    {
        while (true)
        {
            up = true;
            Thread.Sleep(350);
            up = false;
            Thread.Sleep(350);
        }
    }

    private void endTime()
    {
        Destroy(timerGO);
        Destroy(timer1);
        Destroy(timer2);
    }

    private void restart()
    {
        PlayerPrefs.Save();
        try
        {
            record = PlayerPrefs.GetInt("Record", 0);
        }
        catch
        {

        }
        score = 0;
        spawn = false;
        despawn = false;
        count = false;
        isFirstTouch = true;
        isSwipeAllowed = false;
        startTimer = false;
        updateTimer = false;
        endTimer = false;
        sum1 = 0;
        sum2 = 0;
        ticks = 1;
        difficulty = 10;
        updateRecord();
        updateScore();
    }

    private void createTicket()
    {
        numUpdate(ref ticket11, ticket1, -1.4f, 10f, -0.2f);
        numUpdate(ref ticket22, ticket2, -0.84f, 10f, -0.2f);
        numUpdate(ref ticket33, ticket3, -0.28f, 10f, -0.2f);
        numUpdate(ref ticket44, ticket4, 0.28f, 10f, -0.2f);
        numUpdate(ref ticket55, ticket5, 0.84f, 10f, -0.2f);
        numUpdate(ref ticket66, ticket6, 1.4f, 10f, -0.2f);
        ticketGO = Instantiate(ticket, new Vector3(0f, 10f, -0.1f), Quaternion.Euler(0, 0, 0));
        ticket11.transform.SetParent(ticketGO.transform);
        ticket22.transform.SetParent(ticketGO.transform);
        ticket33.transform.SetParent(ticketGO.transform);
        ticket44.transform.SetParent(ticketGO.transform);
        ticket55.transform.SetParent(ticketGO.transform);
        ticket66.transform.SetParent(ticketGO.transform);
    }

    private void updateTime()
    {
        Destroy(timer1);
        Destroy(timer2);
        if (i > 9)
        {
            numUpdate(ref timer1, (i - (i % 10)) / 10, -0.24f, -3.53f, -0.2f);
            numUpdate(ref timer2, i % 10, 0.28f, -3.53f, -0.2f);
        }
        else
        {
            numUpdate(ref timer1, 0, -0.24f, -3.53f, -0.2f);
            numUpdate(ref timer2, i, 0.28f, -3.53f, -0.2f);
        }
    }

    private void updateRecord()
    {
        Destroy(recordOne);
        Destroy(recordTwo);
        Destroy(recordThree);
        if (record < 1000)
        {
            if(record < 100)
            {
                if (record < 10)
                {
                    recordOne = Instantiate(zero, new Vector3(-2.4f, 5.4f, -0.4f), Quaternion.Euler(0, 0, 0));
                    recordTwo = Instantiate(zero, new Vector3(-1.81f, 5.4f, -0.4f), Quaternion.Euler(0, 0, 0));
                    numUpdate(ref recordThree, record % 10, -1.23f, 5.4f, -0.4f);
                }
                else
                {
                    recordOne = Instantiate(zero, new Vector3(-2.4f, 5.4f, -0.4f), Quaternion.Euler(0, 0, 0));
                    numUpdate(ref recordTwo, (record - record % 10) / 10, -1.81f, 5.4f, -0.4f);
                    numUpdate(ref recordThree, record % 10, -1.23f, 5.4f, -0.4f);
                }
            }
            else
            {
                numUpdate(ref recordOne, (record - record % 100) / 100, -2.4f, 5.4f, -0.4f);
                numUpdate(ref recordTwo, (record - record % 10) / 10, -1.81f, 5.4f, -0.4f);
                numUpdate(ref recordThree, record % 10, -1.23f, 5.4f, -0.4f);
            }
        }
    }
    private void updateScore()
    {
        Destroy(scoreOne);
        Destroy(scoreTwo);
        Destroy(scoreThree);
        if (score < 1000)
        {
            if (score < 100)
            {
                if (score < 10)
                {
                    scoreOne = Instantiate(zero, new Vector3(1.27f, 5.4f, -0.4f), Quaternion.Euler(0, 0, 0));
                    scoreTwo = Instantiate(zero, new Vector3(1.86f, 5.4f, -0.4f), Quaternion.Euler(0, 0, 0));
                    numUpdate(ref scoreThree, score % 10, 2.44f, 5.4f, -0.4f);
                }
                else
                {
                    scoreOne = Instantiate(zero, new Vector3(1.27f, 5.4f, -0.4f), Quaternion.Euler(0, 0, 0));
                    numUpdate(ref scoreTwo, (score - score % 10) / 10, 1.86f, 5.4f, -0.4f);
                    numUpdate(ref scoreThree, score % 10, 2.44f, 5.4f, -0.4f);
                }
            }
            else
            {
                numUpdate(ref scoreOne, (score - score % 100) / 100, 1.27f, 5.4f, -0.4f);
                numUpdate(ref scoreTwo, (score - score % 10) / 10, 1.86f, 5.4f, -0.4f);
                numUpdate(ref scoreThree, score % 10, 2.44f, 5.4f, -0.4f);
            }
        }
    }

    private void numUpdate(ref GameObject save, int num, float x, float y, float z)
    {
        switch (num)
        {
            case 0:
                save = Instantiate(zero, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
                break;
            case 1:
                save = Instantiate(one, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
                break;
            case 2:
                save = Instantiate(two, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
                break;
            case 3:
                save = Instantiate(three, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
                break;
            case 4:
                save = Instantiate(four, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
                break;
            case 5:
                save = Instantiate(five, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
                break;
            case 6:
                save = Instantiate(six, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
                break;
            case 7:
                save = Instantiate(seven, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
                break;
            case 8:
                save = Instantiate(eight, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
                break;
            case 9:
                save = Instantiate(nine, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
                break;
        }
    }

    private void spawnTicket()
    {
        while (true)
        {
            count = true;
            Thread.Sleep(1000);
            spawn = true;
            if (ticks % 10 == 0 && difficulty > 5)
            {
                difficulty--;
            }
            startTimer = true;
            for (i = difficulty; i > 0; i--)
            {
                updateTimer = true;
                Thread.Sleep(1000);
            }
            endTimer = true;
            despawn = true;
            Thread.Sleep(500);
        }
    }

    private void simulateEquality(int sum)
    {
        int summ = sum;
        for (int i = 0; i < summ; i++)
        {
            int numToAdd = UnityEngine.Random.Range(0, 3);
            switch (numToAdd)
            {
                case 0:
                    if (ticket4 < 9)
                    {
                        ++ticket4;
                        break;
                    }
                    else
                    {
                        if (ticket5 < 9)
                        {
                            ++ticket5;
                            break;
                        }
                        else
                        {
                            if (ticket6 < 9)
                            {
                                ++ticket6;
                                break;
                            }
                        }
                    }
                    break;
                case 1:
                    if (ticket5 < 9)
                    {
                        ++ticket5;
                        break;
                    }
                    else
                    {
                        if (ticket4 < 9)
                        {
                            ++ticket4;
                            break;
                        }
                        else
                        {
                            if (ticket6 < 9)
                            {
                                ++ticket6;
                                break;
                            }
                        }
                    }
                    break;
                case 2:
                    if (ticket6 < 9)
                    {
                        ++ticket6;
                        break;
                    }
                    else
                    {
                        if (ticket5 < 9)
                        {
                            ++ticket5;
                            break;
                        }
                        else
                        {
                            if (ticket4 < 9)
                            {
                                ++ticket4;
                                break;
                            }
                        }
                    }
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.Save();
    }
}
