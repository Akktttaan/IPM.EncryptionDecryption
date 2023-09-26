using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using App;
using App.Enums;
using App.Helper;
using Microsoft.Win32;

namespace Client;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        InitializeEncryptionAlgorithms();
    }

    private void InitializeEncryptionAlgorithms()
    {
        List<string> enumDescriptions =
            (from EncryptionAlgorithmType value in Enum.GetValues(typeof(EncryptionAlgorithmType))
                select EnumHelper.GetDescription(value)).ToList();

        EncryptionAlgorithms.ItemsSource = enumDescriptions;
    }

    private void LoadTextFromFile(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() != true) return;
        try
        {
            var filePath = openFileDialog.FileName;

            var encoding = DetectFileEncoding(filePath);

            var fileContent = File.ReadAllText(filePath, encoding);

            InitialText.Text = fileContent;
            ConvertedText.Text = string.Empty;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Произошла ошибка при загрузке файла: {ex.Message}");
        }
    }

    private void SaveTextInFile(object sender, RoutedEventArgs e)
    {
        try
        {
            // Получаем текст из TextBox
            string textToSave = ConvertedText.Text;

            // Открываем диалоговое окно для выбора места сохранения файла
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                // Получаем путь к выбранному файлу
                string filePath = saveFileDialog.FileName;

                // Сохраняем текст в файл
                File.WriteAllText(filePath, textToSave);

                MessageBox.Show("Текст успешно сохранен в файл.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Произошла ошибка при сохранении файла: {ex.Message}");
        }
    }

    private void EncryptionAlgorithmChanged(object sender, SelectionChangedEventArgs e)
    {
        EncryptionKey.IsEnabled = EncryptionAlgorithms.SelectedItem != "Моно алфавитная подстановка";
        EncryptionKey.Text = string.Empty;
        ValidateEncryptButtons();
    }

    private void EncryptionKeyChanged(object sender, TextChangedEventArgs e)
    {
        ValidateEncryptButtons();
    }

    private void Encrypt(object sender, RoutedEventArgs e)
    {
        ICryptable encoder =
            EnumHelper.GetValue<EncryptionAlgorithmType>(EncryptionAlgorithms.SelectedItem.ToString()) switch
            {
                EncryptionAlgorithmType.Mono => new MonoEncoder(),
                EncryptionAlgorithmType.Caesar => new CaesarEncoder(EncryptionKey.Text),
                EncryptionAlgorithmType.Tritemius => new TritemiusEncoder(EncryptionKey.Text),
                _ => throw new ArgumentOutOfRangeException()
            };
        ConvertedText.Text = encoder.Encrypt(InitialText.Text);
    }

    private void Decrypt(object sender, RoutedEventArgs e)
    {
        ICryptable encoder =
            EnumHelper.GetValue<EncryptionAlgorithmType>(EncryptionAlgorithms.SelectedItem.ToString()) switch
            {
                EncryptionAlgorithmType.Mono => new MonoEncoder(),
                EncryptionAlgorithmType.Caesar => new CaesarEncoder(EncryptionKey.Text),
                EncryptionAlgorithmType.Tritemius => new TritemiusEncoder(EncryptionKey.Text),
                _ => throw new ArgumentOutOfRangeException()
            };
        ConvertedText.Text = encoder.Decrypt(InitialText.Text);
    }

    private void ShowFrequencyDictionary(object sender, RoutedEventArgs e)
    {
        var hui = FrequentCounter.countAppearencesOfLetter(ConvertedText.Text);
        var frequentDict = new FrequentDict(FrequentCounter.countAppearencesOfLetter(ConvertedText.Text));
        frequentDict.Show();
    }

    private void InitialTextChanged(object sender, TextChangedEventArgs e)
    {
        ValidateEncryptButtons();
    }

    private Encoding DetectFileEncoding(string filePath)
    {
        // Определите кодировку файла на основе его содержимого
        // Можно использовать различные методы, например, на основе магических байтов файла

        var buffer = new byte[4];
        using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            fileStream.Read(buffer, 0, 4);
        }

        return buffer[0] switch
        {
            0xef when buffer[1] == 0xbb && buffer[2] == 0xbf => Encoding.UTF8,
            0xff when buffer[1] == 0xfe => Encoding.Unicode,
            0xfe when buffer[1] == 0xff => Encoding.BigEndianUnicode,
            _ => Encoding.Default
        };
    }

    private void ValidateEncryptButtons()
    {
        var textLengthState = InitialText.Text.Length > 0;
        var keyState = EncryptionKey.Text.Length > 0;
        EncryptButton.IsEnabled = textLengthState && keyState;
        DecryptButton.IsEnabled = textLengthState && keyState;
    }
}