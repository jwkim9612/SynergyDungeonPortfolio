using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeReward : MonoBehaviour
{
    [SerializeField] private Text remainingTimeText = null;
    [SerializeField] private Button receiptButton = null;
    private float remainingTime;

    private void Start()
    {
        QuestManager.Instance.OnTimeRewardCheck += UpdateTimeReward;
        QuestManager.Instance.TimeRewardCheck();

        SetReceiptButton();
    }

    private void SetReceiptButton()
    {
        receiptButton.onClick.AddListener(() =>
        {
            QuestManager.Instance.TimeRewardReceipt();
        });
    }

    private void UpdateTimeReward()
    {
        remainingTime = QuestManager.Instance.remainingTimeOfTimeReward;
        if (remainingTime > 0)
        {
            receiptButton.interactable = false;
            StartCoroutine(Co_PlayRemainingTimeText());
        }
        else
        {
            remainingTimeText.text = "00 : 00 : 00";
            receiptButton.interactable = true;
        }
    }

    private IEnumerator Co_PlayRemainingTimeText()
    {
        int hour = (int)remainingTime / 60 / 60;
        int minute = (int)remainingTime / 60 % 60;
        int second = (int)remainingTime % 60;

        remainingTimeText.text = $"{hour.ToString("D2")} : {minute.ToString("D2")} : {second.ToString("D2")}";

        while (hour != 0 || minute != 0 || second != 0)
        {
            yield return new WaitForSeconds(1.0f);

            if (second == 0)
            {
                if (minute == 0)
                {
                    if (hour == 0)
                    {
                        break;
                    }

                    --hour;
                    minute = 60;
                }

                --minute;
                second = 59;

            }
            else
                --second;

            remainingTimeText.text = $"{hour.ToString("D2")} : {minute.ToString("D2")} : {second.ToString("D2")}";
        }

        QuestManager.Instance.TimeRewardCheck();
    }

    private void OnDestroy()
    {
        if(TimeManager.IsAlive)
        {
            QuestManager.Instance.OnTimeRewardCheck -= UpdateTimeReward;
        }
    }
}
