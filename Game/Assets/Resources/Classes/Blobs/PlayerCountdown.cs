using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Resources.Classes;
using TMPro;

namespace Assets.Resources.Classes.Blobs
{
    public class PlayerCountdown : MonoBehaviour
    {
        public Player AttachedPlayer { get; set; }
        Driver.Driver GameDriver { get; set; }
        TextMeshProUGUI Text { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            this.AttachedPlayer = this.gameObject.GetComponentInParent<Player>();
            this.GameDriver = FindObjectOfType<Driver.Driver>();
            this.Text = this.gameObject.GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            int count = (int) (this.GameDriver.TimerCount + 0.25f);
            if (count >= 0)
            {
                this.Text.text = count.ToString();
            }
        }
    }
}