using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
/*
 * Açıklama = Bir tamsayı listesi veriliyor. Bu listede ardışık olarak artış gösteren alt dizilerden toplamı en yüksek
 *olan alt diziyi bulun. Bulduğunuz alt diziyi JSON formatında döndürün
 *  
 * 
 * 
 * 
*/
public class Solution
{
    public static string MaxIncreasingSubArrayAsJson(List<int> numbers)   //metod tanımlanıyor 
    {
        if (numbers == null || numbers.Count == 0)  //liste boşşa [] döndürür
            return JsonSerializer.Serialize(new List<int>());

        List<int> bestSubarray = new List<int>();  //şu ana kadar bulunan en yüksek puanlı artan alt dizi
        int bestSum = int.MinValue; //alt dizinin toplamını tutar .

        List<int> currentSubarray = new List<int> { numbers[0] }; // alt dizi başlatılıyor.

        for (int i = 1; i < numbers.Count; i++) //döngü birden başlar.
        {
            if (numbers[i] > numbers[i - 1]) // eğer bi önceki elemandan büyükse artış var demektir.
            {
                currentSubarray.Add(numbers[i]); //ve bu mevcut diziye eklenir.  
            }


            //bu kısıma kadar artış gösteren sayıyı yani bu ardışık olduğunu gösterir bunu alt diziye ekledik.
            else //artış yoksa  (burası da ardışık olarak ilerlemediği durum .)
            {
                int currentSum = currentSubarray.Sum(); //Şu anki alt dizinin toplamı(currentSum) hesaplanır.
                if (currentSum > bestSum) //Eğer bu toplam bestSum’dan büyükse → bestSubarray ve bestSum güncellenir.
                {
                    bestSum = currentSum;
                    bestSubarray = new List<int>(currentSubarray);
                }
                currentSubarray = new List<int> { numbers[i] }; //Yeni alt dizi başlatılır, mevcut elemanla (numbers[i]).
            }
        }

        // Döngü bittikten sonra son alt dizi kontrol edilir . 
        // Döngü içinde sadece artış bozulduğunda alt diziyi değerlendiriyoruz.
        // Eğer listenin sonundaki alt dizi hâlâ artış içindeyse, 
        // bu kontrol ile en iyi alt diziyi güncelleriz.
        int lastSum = currentSubarray.Sum();
        if (lastSum > bestSum)
        {
            bestSum = lastSum;
            bestSubarray = new List<int>(currentSubarray);
        }

        return JsonSerializer.Serialize(bestSubarray); // json formatında dönmesi için bu metodu kullandım .
    }
}
