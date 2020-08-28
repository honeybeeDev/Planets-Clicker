using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;  
using UnityEngine.SceneManagement;

public class Clicker : MonoBehaviour
{
    public GameObject ifNotMoney;
    public GameObject language;
    public GameObject[] learn;
    public GameObject offlineScoreText;
    private int work; // сколько заработал в оффлейне
    private string save;
    private int scene;
    private Save s; 
    public Text offlineScore; // текст заработка в оффлейн
    private Save sv = new Save();
    private string path;
    public Button planetButton;
    public Text scoreText; 
    private int clickNum;
    public GameObject clickParent, moneyPlubPrefab; 
    private int score; 
    private int bonus = 1; // изначальное зачисление за клик
    private MoneyPlus[] clickTextPool = new MoneyPlus[15];
    private int workers; // сколько рабочих
    public Button[] itemButton, itemImage;
    [Header("Shop")]
    public int[] product; // товаров сколько и цена
    public int[] workersPlus;
    public int[] bonusPlus; // бонус за клики 1 = 2 
    public Text[] bonusText; // текст после покупки
    public GameObject shop, OfflineClose , setting , restart; // кнопка магазина
    public int planetScore; // какую планету загружать
    
    public void Start() // старт сразу после игры
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            save = PlayerPrefs.GetString("Save");
            s = JsonUtility.FromJson<Save>(save);
            product = s.product;
            bonusPlus = s.bonusPlus;
            bonusText[0].text = s.text_1;
            bonusText[1].text = s.text_2;
            bonusText[2].text = s.text_3;
            bonusText[3].text = s.text_4;
            bonusText[4].text = s.text_5;
            bonusText[5].text = s.text_6;
            bonusText[6].text = s.text_7;
            bonusText[7].text = s.text_8;
            bonusText[8].text = s.text_9;
            bonusText[9].text = s.text_10;
            bonusText[10].text = s.text_11;
            bonusText[11].text = s.text_12;
            bonusText[12].text = s.text_13;
            bonusText[13].text = s.text_14;
            bonusText[14].text = s.text_15;
        }
        
        if (PlayerPrefs.HasKey("score"))
        {
           score = PlayerPrefs.GetInt("score");
        }

        if (PlayerPrefs.HasKey("workers"))
        {
            workers = PlayerPrefs.GetInt("workers");
        }

        if (PlayerPrefs.HasKey("bonus"))
        {
            bonus = PlayerPrefs.GetInt("bonus");
        }
        CheckOffline();
        score = score + work;
        if (workers >= 1)
        {
            {
                offlineScore.text = work + "R";
            }
            
        }
        else
        {
            Destroy(OfflineClose);
            Destroy(offlineScore);
            Destroy(offlineScoreText);
        }
        if (score >= 1)
        {
            for (int i = 0; i < learn.Length; i++)
            {
                Destroy(learn[i]);
            }
            Destroy(language);
        }
        StartCoroutine(Working());
        for (int i = 0; i < clickTextPool.Length; i++)
        {
            clickTextPool[i] = Instantiate(moneyPlubPrefab, clickParent.transform).GetComponent<MoneyPlus>();

        }
    }

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
    if (pause) 
    {
      PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("workers", workers);
        PlayerPrefs.SetInt("bonus", bonus);
        sv.product = product;
        sv.bonusPlus = bonusPlus;
       sv.text_1 = bonusText[0].text;
        sv.text_2 = bonusText[1].text;
        sv.text_3 = bonusText[2].text;
        sv.text_4 = bonusText[3].text;
        sv.text_5 = bonusText[4].text;
        sv.text_6 = bonusText[5].text;
        sv.text_7 = bonusText[6].text;
        sv.text_8 = bonusText[7].text;
        sv.text_9 = bonusText[8].text;
        sv.text_10 = bonusText[9].text;
        sv.text_11 = bonusText[10].text;
        sv.text_12 = bonusText[11].text;
        sv.text_13 = bonusText[12].text;
        sv.text_14 = bonusText[13].text;
        sv.text_15 = bonusText[14].text;
        save = JsonUtility.ToJson(sv);
        PlayerPrefs.SetString("Save", save);
    }
    }
#else
    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("workers", workers);
        PlayerPrefs.SetInt("bonus", bonus);
        sv.product = product;
        sv.bonusPlus = bonusPlus;
        sv.text_1 = bonusText[0].text;
        sv.text_2 = bonusText[1].text;
        sv.text_3 = bonusText[2].text;
        sv.text_4 = bonusText[3].text;
        sv.text_5 = bonusText[4].text;
        sv.text_6 = bonusText[5].text;
        sv.text_7 = bonusText[6].text;
        sv.text_8 = bonusText[7].text;
        sv.text_9 = bonusText[8].text;
        sv.text_10 = bonusText[9].text;
        sv.text_11 = bonusText[10].text;
        sv.text_12 = bonusText[11].text;
        sv.text_13 = bonusText[12].text;
        sv.text_14 = bonusText[13].text;
        sv.text_15 = bonusText[14].text;
        save = JsonUtility.ToJson(sv);
        PlayerPrefs.SetString("Save", save);
    }
#endif

    public void Language_rus()
    {
        string language = "rus";
        PlayerPrefs.SetString("language", language);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("workers", workers);
        PlayerPrefs.SetInt("bonus", bonus);
        sv.product = product;
        sv.bonusPlus = bonusPlus;
        sv.text_1 = bonusText[0].text;
        sv.text_2 = bonusText[1].text;
        sv.text_3 = bonusText[2].text;
        sv.text_4 = bonusText[3].text;
        sv.text_5 = bonusText[4].text;
        sv.text_6 = bonusText[5].text;
        sv.text_7 = bonusText[6].text;
        sv.text_8 = bonusText[7].text;
        sv.text_9 = bonusText[8].text;
        sv.text_10 = bonusText[9].text;
        sv.text_11 = bonusText[10].text;
        sv.text_12 = bonusText[11].text;
        sv.text_13 = bonusText[12].text;
        sv.text_14 = bonusText[13].text;
        sv.text_15 = bonusText[14].text;
        save = JsonUtility.ToJson(sv);
        PlayerPrefs.SetString("Save", save);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Language_eng()
    {
        string language = "eng";
        PlayerPrefs.SetString("language", language);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("workers", workers);
        PlayerPrefs.SetInt("bonus", bonus);
        sv.product = product;
        sv.bonusPlus = bonusPlus;
        sv.text_1 = bonusText[0].text;
        sv.text_2 = bonusText[1].text;
        sv.text_3 = bonusText[2].text;
        sv.text_4 = bonusText[3].text;
        sv.text_5 = bonusText[4].text;
        sv.text_6 = bonusText[5].text;
        sv.text_7 = bonusText[6].text;
        sv.text_8 = bonusText[7].text;
        sv.text_9 = bonusText[8].text;
        sv.text_10 = bonusText[9].text;
        sv.text_11 = bonusText[10].text;
        sv.text_12 = bonusText[11].text;
        sv.text_13 = bonusText[12].text;
        sv.text_14 = bonusText[13].text;
        sv.text_15 = bonusText[14].text;
        save = JsonUtility.ToJson(sv);
        PlayerPrefs.SetString("Save", save);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void Update()
    {
        scoreText.text = score + ""; // изменение каждый кадр текста денег

        for (int i = 0; i < product.Length - 1; i++)
        {
            if (score >= product[i])
            {
                itemButton[i].interactable = true;
                itemImage[i].interactable = true;
            }
            else
            {
                itemButton[i].interactable = false;
                itemImage[i].interactable = false;
            }
        }
    }

    public void DestroyLanguage()
    {
        Destroy(language);
    }

    public void DestroyLearn(int index)
    {
        Destroy(learn[index]);
    }

    public void OpenURL()
    {
        Application.OpenURL("");
    }

    public void OpenNextLearn(int index)
    {
        learn[index].SetActive(true);
    }

    public void shopOpenExit() // открыть / закрыть магазин 
    {
        shop.SetActive(!shop.activeSelf); 
    }

    public void settingOpenExit()
    {
        setting.SetActive(!setting.activeSelf);
    }

    public void restartOpenExit()
    {
        restart.SetActive(!restart.activeSelf);
    }

    public void DeleteAllSave()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void OfflineExit() 
    {
        Destroy(OfflineClose);
        Destroy(offlineScore);
    }

    public void buyBonus(int index) // купить бонус к клику 
    {
        if (score >= product[index]) 
        {
            bonus += bonusPlus[index]; 
            score -= product[index]; 
            product[index] *= 2; 
            bonusText[index].text = product[index] + "R";  
        }
    }

    public void buyNewPlanet (int index)
    {
        if (score >= product[index])
        {
            SceneManager.LoadScene(planetScore);
            PlayerPrefs.DeleteAll();
        }
        else
        {
            StartCoroutine(ForNotMoney());
        }
    }

    IEnumerator ForNotMoney()
    {
        ifNotMoney.SetActive(true);
        yield return new WaitForSeconds(2);
        ifNotMoney.SetActive(false);
    }

    public void WorkingPlus(int index) // купить улучшение
    {
        if (score >= product[index]) 
        {
            workers += workersPlus[index];
            score -= product[index]; 
            product[index] *= 2; // сколько будет стоить после покупки
            bonusText[index].text = product[index] + "R"; 
        }
    }

    IEnumerator Working()
    {
        while (true)
        {
            score += workers;
            yield return new WaitForSeconds(1);
        }
    }

    public void Click () // 
    {
        clickTextPool[clickNum].StartMotion(bonus);
        clickNum = clickNum == clickTextPool.Length - 1 ? 0 : clickNum + 1;
        score += bonus; // плюс за клик , смотря на бонус
    }
   
    private void CheckOffline()
    {
        TimeSpan ts;
        if (PlayerPrefs.HasKey("LastSession"))
        {
            ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastSession"));
            int seconds = (int)ts.TotalSeconds;
            work = (seconds * workers) / 2;
        }
        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
    }
}

class Save
{
       public int[] product;
       public int[] bonusPlus;
       public string text_1;
       public string text_2;
    public string text_3;
    public string text_4;
    public string text_5;
    public string text_6;
    public string text_7;
    public string text_8;
    public string text_9;
    public string text_10;
    public string text_11;
    public string text_12;
    public string text_13;
    public string text_14;
    public string text_15;
}
