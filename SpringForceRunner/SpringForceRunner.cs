using SpringForce;
using System;
using System.Collections.Generic;
using System.IO;

namespace SpringForceRunner
{
    class SeaRunner
    {
        static void Main(string[] args)
        {
            var skvorecList = ReadSkvorecList("skvorecList.txt");
            var springList = ReadSpringList("springList.txt");

            // Создание экземпляра SpringWrapper
            var springWrapper = new SpringWrapper(skvorecList, springList);

            springWrapper.isDebug = true;
            while (springWrapper.Next())
            {
                
            }
        }

        private static List<(string ID, double x, double v, double m)> ReadSkvorecList(string filePath)
        {
            var skvorecList = new List<(string ID, double x, double v, double m)>();

            // Считываем все строки файла.
            foreach (var line in File.ReadLines(filePath))
            {
                // Пропускаем заголовок
                if (line.StartsWith("ID"))
                    continue;

                var parts = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 4 &&
                    double.TryParse(parts[1], out double x) &&
                    double.TryParse(parts[2], out double v) &&
                    double.TryParse(parts[3], out double m))
                {
                    skvorecList.Add((parts[0].Trim(), x, v, m));
                }
            }

            return skvorecList;
        }

        private static List<(string ID, string ID1, string ID2, double C, double Delta_C, double F)> ReadSpringList(string filePath)
        {
            var springList = new List<(string ID, string ID1, string ID2, double C, double Delta_C, double F)>();

            // Считываем все строки файла.
            foreach (var line in File.ReadLines(filePath))
            {
                // Пропускаем заголовок
                if (line.StartsWith("ID"))
                    continue;

                var parts = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 6)
                {
                    var C = double.Parse(parts[3]);
                    var delta_C = double.Parse(parts[4]);
                    var F = double.Parse(parts[5]);
                    springList.Add((parts[0].Trim(), parts[1].Trim(), parts[2].Trim(), C, delta_C, F));
                }
            }

            return springList;
        }
    }
}
