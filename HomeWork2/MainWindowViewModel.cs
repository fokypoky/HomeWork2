using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Command;

namespace HomeWork2;

public class MainWindowViewModel : ViewModel
{
    private string _text = "";
    private string _handledText = "";
    private string _startIndex = "0";
    private string _endIndex = "0";

    public string HandledText
    {
        get => _handledText;
        set => SetField(ref _handledText, value);
    }
    public string Text
    {
        get => _text;
        set => SetField(ref _text, value);
    }

    public string StartIndex
    {
        get => _startIndex;
        set => SetField(ref _startIndex, value);
    }

    public string EndIndex
    {
        get => _endIndex;
        set => SetField(ref _endIndex, value);
    }

    public RelayCommand HandleTextCommand
    {
        get => new RelayCommand(OnHandleTextExecuted);
    }

    private void OnHandleTextExecuted()
    {
        HandledText = "";
        int startIndex;
        int endIndex;

        List<string> words = new List<string>();
        foreach (var word in Text.Split(' '))
        {
            if (!String.IsNullOrEmpty(word))
            {
                words.Add(word);
            }
        }

        #region Проверка ввода
        
        try
        {
            startIndex = Convert.ToInt32(_startIndex);
        }
        catch
        {
            MessageBox.Show("В начальном индексе могут быть только цифры. Без пробелов и иных знаков");
            return;
        }
        try
        {
            endIndex = Convert.ToInt32(_endIndex);
        }
        catch
        {
            MessageBox.Show("В конечном индексе могут быть только цифры. Без пробелов и иных знаков");
            return;
        }

        if (startIndex > words.Count || startIndex < 1 || startIndex == words.Count)
        {
            if (startIndex > words.Count || startIndex < 1)
            {
                MessageBox.Show("Начальный индекс вне диапазона слов");
            }

            if (startIndex == words.Count)
            {
                MessageBox.Show("Начальный индекс не может быть последним словом");
            }
            return;
        }

        if (endIndex > words.Count || endIndex < 1 || endIndex < startIndex)
        {
            if (endIndex > words.Count || endIndex < 1)
            {
                MessageBox.Show("Конечный индекс вне диапазона слов");
            }

            if (endIndex < startIndex)
            {
                MessageBox.Show("Конечный индекс не может быть меньше начального");
            }
            return;
        }

        if (words.Count == 0)
        {
            MessageBox.Show("Введенный текст не содержит слов");
            return;
        }
        #endregion

        List<int> intervalBetweenIndexes = new List<int>();
        for (int i = startIndex - 1; i <= endIndex - 1; i++)
        {
            intervalBetweenIndexes.Add(i);
        }

        var startWords = new List<string>();
        var endWords = new List<string>();

        for (int i = 0; i < words.Count; i++)
        {
            bool canAdd = true;
            foreach (var index in intervalBetweenIndexes)
            {
                if (i == index)
                {
                    canAdd = false;
                }
            }

            if (canAdd)
            {
                startWords.Add(words.ElementAt(i));
            }
        }
        
        foreach (var index in intervalBetweenIndexes)
        {
            endWords.Add(words.ElementAt(index));
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Вывод
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        foreach (var word in startWords)
        {
            HandledText += word + " ";
        }
        foreach (var word in endWords)
        {
            HandledText += word + " ";
        }
        
    }
}