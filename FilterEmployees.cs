using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class Solution
{
    /*
     *AÇIKLAMA:
     *Statik bir tuple üzerinde şu filtreleme ve hesaplamaları yapın:
 - Yaşı 25 ile 40 arasında (dahil)
 - Departmanı IT veya Finance
 - Maaşı 5000 ile 9000 arasında (dahil)
 - İşe giriş tarihi 2017’den sonra
 Filtre sonrası:
 - İsimleri uzunluklarına göre azalan, ardından alfabetik olarak sıralayın
 - Toplam maaş, ortalama maaş, en düşük maaş, en yüksek maaş, toplam çalışan sayısı
 hesaplanır
 Sonuç JSON olarak döndürülmeli     *
     *
     *
     *
     *
    */ 
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        if (employees == null)
        {
            return JsonSerializer.Serialize(new   //json formatında döndürmek için
            {
                Names = new string[0],
                TotalSalary = 0m,
                AverageSalary = 0m,
                MinSalary = 0m,
                MaxSalary = 0m,
                Count = 0
            });
        }
        // Filtreleme   burda istenilen özelliklere göre çekme yapıyoruz.
        var filtered = employees
            .Where(e => e.Age >= 25 && e.Age <= 40)
            .Where(e => e.Department == "IT" || e.Department == "Finance")
            .Where(e => e.Salary >= 5000m && e.Salary <= 9000m)
            .Where(e => e.HireDate >= new DateTime(2017, 12, 31))
            .ToList();


        int count = filtered.Count;
        decimal totalSalary = filtered.Sum(e => e.Salary);
        decimal averageSalary = count > 0 ? Math.Round(totalSalary / count, 2) : 0m;
        decimal minSalary = count > 0 ? filtered.Min(e => e.Salary) : 0m;
        decimal maxSalary = count > 0 ? filtered.Max(e => e.Salary) : 0m;

        var names = filtered  //isme göre filtreleme.
            .OrderByDescending(e => e.Name.Length)
            .ThenBy(e => e.Name)
            .Select(e => e.Name)
            .ToArray();

        return JsonSerializer.Serialize(new  //json formatında döndürmek için 
        {
            Names = names,
            TotalSalary = totalSalary,
            AverageSalary = averageSalary,
            MinSalary = minSalary,
            MaxSalary = maxSalary,
            Count = count
        });
    }

    // Main ile örnek test
    //public static void Main()
    //{
    //    var examples = new List<List<(string, int, string, decimal, DateTime)>>()
    //    {
    //        new List<(string, int, string, decimal, DateTime)>
    //        {
    //            ("Ali", 30, "IT", 6000m, new DateTime(2018, 5, 1)),
    //            ("Ayse", 35, "Finance", 8500m, new DateTime(2019, 3, 15)),
    //            ("Veli", 28, "IT", 7000m, new DateTime(2020, 1, 1))
    //        },
    //        new List<(string, int, string, decimal, DateTime)>
    //        {
    //            ("Mehmet", 26, "Finance", 5000m, new DateTime(2021, 7, 1)),
    //            ("Zeynep", 39, "IT", 9000m, new DateTime(2018, 11, 20))
    //        },
    //        new List<(string, int, string, decimal, DateTime)>
    //        {
    //            ("Burak",41,"IT",6000m,new DateTime(2018,6,1))
    //        },
    //        new List<(string, int, string, decimal, DateTime)>
    //        {
    //            ("Canan", 29, "Finance", 8000m, new DateTime(2019, 9, 1)),
    //            ("Okan", 35, "IT", 7500m, new DateTime(2020, 5, 10))
    //        },
    //        new List<(string, int, string, decimal, DateTime)>
    //        {
    //            ("Elif",27,"Finance",6500m,new DateTime(2017,12,31))
    //        }
    //    };

    //    for (int i = 0; i < examples.Count; i++)
    //    {
    //        Console.WriteLine($"Çıkış {i + 1}: {FilterEmployees(examples[i])}");
    //    }
    //}
}
