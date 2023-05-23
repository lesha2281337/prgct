using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string activityFile = "activity.txt";
        string protocolFile = "protocol.log";
        string outputProtocolFile = "processed_protocol.log";

        List<string> activityList = LoadActivityList(activityFile);
        ProcessProtocol(protocolFile, activityList, outputProtocolFile);

        Console.WriteLine("Процесс завершен. Результат сохранен в файле: " + outputProtocolFile);
    }

    static List<string> LoadActivityList(string activityFile)
    {
        List<string> activityList = new List<string>();

        try
        {
            using (StreamReader reader = new StreamReader(activityFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    activityList.Add(line);
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Ошибка при чтении файла activity: " + e.Message);
        }

        return activityList;
    }

    static void ProcessProtocol(string protocolFile, List<string> activityList, string outputProtocolFile)
    {
        try
        {
            using (StreamReader reader = new StreamReader(protocolFile))
            using (StreamWriter writer = new StreamWriter(outputProtocolFile))
            {
                string line;
                int lineNumber = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    bool activityFound = false;

                    foreach (string activity in activityList)
                    {
                        if (line.Contains(activity))
                        {
                            writer.WriteLine("Строка {0} из списка activity найдена в протоколе: {1}", lineNumber, line);
                            activityFound = true;
                            break;
                        }
                    }

                    if (!activityFound)
                    {
                        writer.WriteLine("Строка {0} не содержит информацию из списка activity: {1}", lineNumber, line);
                    }
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Ошибка при чтении/записи файла протокола: " + e.Message);
        }
    }
}
