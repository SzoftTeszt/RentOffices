using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RentOffices
{
    public class Berles
    {
        public int Uid { get; set; }
        public int OfficeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DailyRate { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public int TotalPrice
        {
            get
            {
                int days = (EndDate - StartDate).Days + 1;
                return DailyRate * days;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var berlesek = new List<Berles>();
            var lines = File.ReadAllLines("office_rentals_2024.csv");
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                berlesek.Add(new Berles
                {
                    Uid = int.Parse(parts[0]),
                    OfficeId = int.Parse(parts[1]),
                    StartDate = DateTime.ParseExact(parts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    EndDate = DateTime.ParseExact(parts[3], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DailyRate = int.Parse(parts[4]),
                    Name = parts[5],
                    City = parts[6]
                });
            }

            Console.Write("Adjon meg egy hónapot (1-12): ");
            int honap = int.Parse(Console.ReadLine());

            int haviBevetel = berlesek
                .Where(b => b.StartDate.Month == honap || b.EndDate.Month == honap || (b.StartDate.Month < honap && b.EndDate.Month > honap))
                .Sum(b => b.TotalPrice);

            Console.WriteLine($"A(z) {honap}. hónap bevétele: {haviBevetel:N0} euró");

            int evesBevetel = berlesek.Sum(b => b.TotalPrice);
            Console.WriteLine($"A teljes 2024-es éves bevétel: {evesBevetel:N0} euró");

            var legdragabb = berlesek.OrderByDescending(b => b.TotalPrice).First();
            Console.WriteLine($"A legdrágább bérlés az {legdragabb.Name} irodára történt, teljes ár: {legdragabb.TotalPrice:N0} euró");

            int kulonbozoIrodakSzama = berlesek.Select(b => b.OfficeId).Distinct().Count();
            Console.WriteLine($"Összesen {kulonbozoIrodakSzama} különböző irodát béreltek ki.");

            var legtobbszorBerelt = berlesek.GroupBy(b => b.Name)
                .OrderByDescending(g => g.Count())
                .First();
            Console.WriteLine($"A legtöbbször bérelt iroda: {legtobbszorBerelt.Key} ({legtobbszorBerelt.Count()} bérlés)");

            Console.WriteLine("Bérlések száma városonként:");
            var varosok = berlesek.GroupBy(b => b.City)
                .Select(g => new { Varos = g.Key, Darab = g.Count() });
            foreach (var v in varosok)
            {
                Console.WriteLine($"{v.Varos}: {v.Darab} bérlés");
            }

            double atlagosIdotartam = berlesek.Average(b => (b.EndDate - b.StartDate).Days + 1);
            Console.WriteLine($"Átlagos bérlési időtartam: {atlagosIdotartam:F2} nap");
        }
    }
}