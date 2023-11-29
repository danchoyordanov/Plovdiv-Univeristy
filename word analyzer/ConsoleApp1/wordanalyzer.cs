using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

class wordanalyzer
{
    static void Main()
    {
        string filePath = @"C:\Users\dyord\OneDrive\Desktop\Miron_Ivanov_-_Zhivej_kato_drugite_i_bydi_blagosloven_-_11509-b.txt";
        
        string content = File.ReadAllText(filePath, Encoding.UTF8);

        
        string[] words = GetWords(content);
        
        Console.WriteLine($"Number of words: {GetWordCount(words)}");
        Console.WriteLine($"Shortest word: {GetShortestWord(words)}");
        Console.WriteLine($"Longest word: {GetLongestWord(words)}");
        Console.WriteLine($"Average word length: {GetAverageWordLength(words):F2}");

        var mostCommonWords = GetMostCommonWords(words, 5);
        Console.WriteLine("Five most common words:");
        PrintWordFrequency(mostCommonWords);

        var leastCommonWords = GetLeastCommonWords(words, 5);
        Console.WriteLine("Five least common words:");
        PrintWordFrequency(leastCommonWords);
    }

    static string[] GetWords(string content)
    {
        string[] words = Regex.Split(content, @"\W+");
        
        words = Array.FindAll(words, w => w.Length >= 3);

        return words;
    }

    static int GetWordCount(string[] words)
    {
        return words.Length;
    }

    static string GetShortestWord(string[] words)
    {
        return GetExtremeWord(words, (min, current) => current.Length < min.Length);
    }

    static string GetLongestWord(string[] words)
    {
        return GetExtremeWord(words, (max, current) => current.Length > max.Length);
    }

    static double GetAverageWordLength(string[] words)
    {
        int totalLength = 0;
        foreach (var word in words)
        {
            totalLength += word.Length;
        }

        return (double)totalLength / words.Length;
    }

    static Dictionary<string, int> GetWordFrequency(string[] words)
    {
        Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

        foreach (var word in words)
        {
            if (wordFrequency.ContainsKey(word))
            {
                wordFrequency[word]++;
            }
            else
            {
                wordFrequency[word] = 1;
            }
        }

        return wordFrequency;
    }

    static List<string> GetMostCommonWords(string[] words, int count)
    {
        var wordFrequency = GetWordFrequency(words);

        return wordFrequency.Keys
            .OrderByDescending(word => wordFrequency[word])
            .Take(count)
            .ToList();
    }

    static List<string> GetLeastCommonWords(string[] words, int count)
    {
        var wordFrequency = GetWordFrequency(words);

        
        return wordFrequency.Keys
            .OrderBy(word => wordFrequency[word])
            .Take(count)
            .ToList();
    }

    static string GetExtremeWord(string[] words, Func<string, string, bool> comparison)
    {
        string extremeWord = words[0];

        foreach (var word in words)
        {
            if (comparison(extremeWord, word))
            {
                extremeWord = word;
            }
        }

        return extremeWord;
    }

    static void PrintWordCount(string[] words)
    {
        Console.WriteLine($"Number of words: {GetWordCount(words)}");
    }

    static void PrintShortestWord(string[] words)
    {
        Console.WriteLine($"Shortest word: {GetShortestWord(words)}");
    }

    static void PrintLongestWord(string[] words)
    {
        Console.WriteLine($"Longest word: {GetLongestWord(words)}");
    }

    static void PrintAverageWordLength(string[] words)
    {
        Console.WriteLine($"Average word length: {GetAverageWordLength(words):F2}");
    }

    static void PrintWordFrequency(List<string> words)
    {
        foreach (var word in words)
        {
            Console.WriteLine($"{word}");
        }
    }
}
