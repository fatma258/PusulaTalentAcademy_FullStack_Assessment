using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class Solution
{

    /*
     * 
     * 
     * Bir kelime listesi veriliyor. Her kelimenin ardışık sesli harflerinden oluşan en uzun alt diziyi bulun ve
      JSON formatında kelime ile birlikte alt diziyi ve uzunluğunu döndürün.
     * 
     * 
     * */
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        // Boş veya null liste kontrolü
        if (words == null || words.Count == 0)
            return JsonSerializer.Serialize(new List<object>());

        // Sesli harfleri tanımlıyoruz
        HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

        // Sonuçları tutacak liste
        var results = new List<object>();

        foreach (var word in words)
        {
            string longestSeq = ""; // Kelimedeki en uzun ardışık sesli harf alt dizisi
            string currentSeq = ""; // Şu anki ardışık sesli harf alt dizisi

            foreach (char c in word)
            {
                if (vowels.Contains(char.ToLower(c)))
                {
                    // Sesli harf → mevcut alt diziyi güncelle
                    currentSeq += c;

                    // Eğer mevcut alt dizi daha uzunsa, en uzun diziyi güncelle
                    if (currentSeq.Length > longestSeq.Length)
                        longestSeq = currentSeq;
                }
                else
                {
                    // Sessiz harf → mevcut alt diziyi sıfırla
                    currentSeq = "";
                }
            }

            // Her kelime için sonuç objesini listeye ekle
            results.Add(new
            {
                word = word,
                sequence = longestSeq,
                length = longestSeq.Length
            });
        }

        // Tüm kelimeler için JSON formatında döndür
        return JsonSerializer.Serialize(results);
    }
}

// Test ve çalıştırma için Main
//class Program
//{
//    static void Main()
//    {
//        var testCases = new List<List<string>>
//        {
//            new List<string> { "aeiou", "bcd", "aaa" },
//            new List<string> { "miscellaneous", "queue", "sky", "cooperative" },
//            new List<string> { "sequential", "beautifully", "rhythms", "encyclopaedia" },
//            new List<string> { "algorithm", "education", "idea", "strength" },
//            new List<string>() // boş liste
//        };

//        for (int i = 0; i < testCases.Count; i++)
//        {
//            string result = Solution.LongestVowelSubsequenceAsJson(testCases[i]);
//            Console.WriteLine($"Çıkış {i + 1}: {result}\n");
//        }
//    }
//}
