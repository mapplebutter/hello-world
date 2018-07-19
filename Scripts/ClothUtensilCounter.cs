namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class ClothUtensilCounter : MonoBehaviour
    {
        public Text counter, counter2, counter3;
        public int Value1, Value2, Value3;
        public GameObject Slot1, Slot2, Slot3;

        void Update()
        {
            Value1 = Slot1.GetComponent<SnapDropZoneOverride>().thisStack.Count;
            Value2 = Slot2.GetComponent<SnapDropZoneOverride>().thisStack.Count;
            Value3 = Slot3.GetComponent<SnapDropZoneOverride>().thisStack.Count;

            if (Value1 > 0)
            {
                //Rather than using '4' to show the maximum amount, we use 'M' or 'MAX' to indicate that the player that you can no longer put anymore utensils inside.
                if (Value1 == 4)
                {
                    counter.gameObject.SetActive(true);

                    //Rather than setting it to the current value, if the value is 4, the value will be set to 'M'.
                    counter.text = "M";
                }
                else
                {
                    counter.gameObject.SetActive(true);

                    //Else, any value than 4, will display its current number.
                    counter.text = Value1.ToString();
                }
            }
            else
            {
                counter.gameObject.SetActive(false);
            }

            if (Value2 > 0)
            {
                //Rather than using '4' to show the maximum amount, we use 'M' or 'MAX' to indicate that the player that you can no longer put anymore utensils inside.
                if (Value2 == 4)
                {
                    counter2.gameObject.SetActive(true);

                    //Rather than setting it to the current value, if the value is 4, the value will be set to 'M'.
                    counter2.text = "M";
                }
                else
                {
                    counter2.gameObject.SetActive(true);

                    //Else, any value than 4, will display its current number.
                    counter2.text = Value2.ToString();
                }
            }
            else
            {
                counter2.gameObject.SetActive(false);
            }

            if (Value3 > 0)
            {
                //Rather than using '4' to show the maximum amount, we use 'M' or 'MAX' to indicate that the player that you can no longer put anymore utensils inside.
                if (Value3 == 4)
                {
                    counter3.gameObject.SetActive(true);

                    //Rather than setting it to the current value, if the value is 4, the value will be set to 'M'.
                    counter3.text = "M";
                }
                else
                {
                    counter3.gameObject.SetActive(true);

                    //Else, any value than 4, will display its current number.
                    counter3.text = Value3.ToString();
                }
            }
            else
            {
                counter3.gameObject.SetActive(false);
            }
        }
    }
}
