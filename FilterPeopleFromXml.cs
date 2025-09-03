using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;

class Solution
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        if (string.IsNullOrWhiteSpace(xmlData))      
            return JsonSerializer.Serialize(new
            {
                Names = new string[0],
                TotalSalary = 0,
                AverageSalary = 0,
                MaxSalary = 0,
                Count = 0
            });

        var doc = XDocument.Parse(xmlData);

        var people = doc.Descendants("Person")   //PERSONEL BİLGİLERİNİ ALIYORUZ 
            .Select(p => new   
            {
                Name = p.Element("Name")?.Value ?? "",
                Age = int.Parse(p.Element("Age")?.Value ?? "0"),
                Department = p.Element("Department")?.Value ?? "",
                Salary = int.Parse(p.Element("Salary")?.Value ?? "0"),
                HireYear = DateTime.Parse(p.Element("HireDate")?.Value ?? "9999-01-01").Year
            })
            .Where(p => p.Age > 30 && p.Department == "IT" && p.Salary > 5000 && p.HireYear < 2019)
            .ToArray();    // 30 YAŞINDAN BÜYÜK , DEPARTMANT IT MAAŞ 5000 DEN FAZLA  İŞE GİRİŞ 2019DAN ESKİ

        int count = people.Length; //kişi sayısı 
        int totalSalary = people.Sum(p => p.Salary);  //TOPLAM MAAŞ 
        int maxSalary = count > 0 ? people.Max(p => p.Salary) : 0; //en yüksek maaş
        int averageSalary = count > 0 ? totalSalary / count : 0; // ortalama maaş
        var names = people.Select(p => p.Name).OrderBy(n => n).ToArray();
        //isimler
        return JsonSerializer.Serialize(new //json formatında döner sonuç 
        {
            Names = names,
            TotalSalary = totalSalary,
            AverageSalary = averageSalary,
            MaxSalary = maxSalary,
            Count = count
        });
    }
    //BURASI TEST KISMI 
    //
    //public static void Main()
    //{
    //    string[] xmlInputs = new string[]
    //    {
    //        "<People><Person><Name>Ali</Name><Age>35</Age><Department>IT</Department><Salary>6000</Salary><HireDate>2018-06-01</HireDate></Person><Person><Name>Ayşe</Name><Age>28</Age><Department>HR</Department><Salary>4500</Salary><HireDate>2020-04-15</HireDate></Person></People>",
    //        "<People><Person><Name>Mehmet</Name><Age>40</Age><Department>IT</Department><Salary>7500</Salary><HireDate>2017-02-01</HireDate></Person></People>",
    //        "<People><Person><Name>Zeynep</Name><Age>45</Age><Department>IT</Department><Salary>9000</Salary><HireDate>2010-01-10</HireDate></Person><Person><Name>Ahmet</Name><Age>50</Age><Department>IT</Department><Salary>8000</Salary><HireDate>2015-05-20</HireDate></Person></People>",
    //        "<People><Person><Name>Fatma</Name><Age>33</Age><Department>Finance</Department><Salary>6000</Salary><HireDate>2018-11-01</HireDate></Person></People>",
    //        "<People><Person><Name>Selim</Name><Age>32</Age><Department>IT</Department><Salary>5500</Salary><HireDate>2018-08-05</HireDate></Person></People>"
    //    };

    //    for (int i = 0; i < xmlInputs.Length; i++)
    //    {
    //        string result = FilterPeopleFromXml(xmlInputs[i]);
    //        Console.WriteLine($"Çıkış {i + 1}: {result}");
    //    }
    //}
}
