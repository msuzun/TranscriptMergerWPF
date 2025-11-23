using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using UdemyTranscriptMergerWPF.Helpers;
using UdemyTranscriptMergerWPF.Services;

namespace UdemyTranscriptMergerWPF
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<LogMessage> LogItems { get; set; }
            = new ObservableCollection<LogMessage>();

        public MainWindow()
        {
            InitializeComponent();

            // Start in Light mode
            ThemeManager.ApplyTheme(false);

            // Bind Logs
            logList.ItemsSource = LogItems;

            AddLog("SYSTEM", "Uygulama başlatıldı.");
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }

        // ---------------------------------
        // LOG SYSTEM
        // ---------------------------------
        private void AddLog(string type, string text)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                LogItems.Add(new LogMessage
                {
                    Type = type,
                    Text = text,
                    Time = DateTime.Now
                });

                // Auto scroll to bottom
                logList.Dispatcher.Invoke(() =>
                {
                    var sv = logList.Parent as ScrollViewer;
                    sv?.ScrollToEnd();
                });
            });
        }


        // ---------------------------------
        // FILE OPERATIONS
        // ---------------------------------
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "TXT Files|*.txt";
            dialog.Multiselect = true;

            AddLog("USER", "Dosya ekleme penceresi açıldı.");

            if (dialog.ShowDialog() == true)
            {
                foreach (var f in dialog.FileNames)
                {
                    lstFiles.Items.Add(f);
                    AddLog("INFO", $"Dosya eklendi: {System.IO.Path.GetFileName(f)}");
                }
            }
        }

        private void lstFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFiles.SelectedItem == null) return;

            string path = lstFiles.SelectedItem.ToString();
            txtPreview.Text = File.ReadAllText(path);

            AddLog("SYSTEM", $"Önizleme güncellendi → {System.IO.Path.GetFileName(path)}");
        }

        private void lstFiles_Drop(object sender, DragEventArgs e)
        {
            AddLog("USER", "Dosya sürükle-bırak işlemi yapıldı.");

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (var f in files.Where(x => x.EndsWith(".txt")))
                {
                    lstFiles.Items.Add(f);
                    AddLog("INFO", $"Dosya eklendi: {System.IO.Path.GetFileName(f)}");
                }
            }
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idx = lstFiles.SelectedIndex;
                if (idx <= 0) return;

                var item = lstFiles.Items[idx];
                lstFiles.Items.RemoveAt(idx);
                lstFiles.Items.Insert(idx - 1, item);
                lstFiles.SelectedIndex = idx - 1;

                AddLog("USER", "Dosya listesinde yukarı taşıma işlemi.");
            }
            catch (Exception ex)
            {
                AddLog("ERROR", ex.Message);
            }
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idx = lstFiles.SelectedIndex;
                if (idx < 0 || idx == lstFiles.Items.Count - 1) return;

                var item = lstFiles.Items[idx];
                lstFiles.Items.RemoveAt(idx);
                lstFiles.Items.Insert(idx + 1, item);
                lstFiles.SelectedIndex = idx + 1;

                AddLog("USER", "Dosya listesinde aşağı taşıma işlemi.");
            }
            catch (Exception ex)
            {
                AddLog("ERROR", ex.Message);
            }
        }


        // ---------------------------------
        // EXPORT FUNCTIONS
        // ---------------------------------
        private void btnExportTxt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new Microsoft.Win32.SaveFileDialog();
                dialog.Filter = "TXT File|*.txt";
                dialog.FileName = "merged.txt";

                AddLog("USER", "TXT export işlemi başlatıldı.");

                if (dialog.ShowDialog() == true)
                {
                    var sections = FileMergeService.BuildSections(lstFiles.Items.Cast<string>().ToList());
                    File.WriteAllText(dialog.FileName,
                        string.Join("\n---\n", sections.Select(x => x.Title + "\n\n" + x.Content)));

                    AddLog("EXPORT", $"TXT başarıyla oluşturuldu: {dialog.FileName}");
                }
            }
            catch (Exception ex)
            {
                AddLog("ERROR", ex.Message);
            }
        }

        private void btnExportPdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new Microsoft.Win32.SaveFileDialog();
                dialog.Filter = "PDF File|*.pdf";
                dialog.FileName = "merged.pdf";

                AddLog("USER", "PDF export işlemi başlatıldı.");

                if (dialog.ShowDialog() == true)
                {
                    var sections = FileMergeService.BuildSections(lstFiles.Items.Cast<string>().ToList());
                    PdfExporter.Export(dialog.FileName, sections);

                    AddLog("EXPORT", $"PDF başarıyla oluşturuldu: {dialog.FileName}");
                }
            }
            catch (Exception ex)
            {
                AddLog("ERROR", ex.Message);
            }
        }

        private void btnExportWord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new Microsoft.Win32.SaveFileDialog();
                dialog.Filter = "Word File|*.docx";
                dialog.FileName = "merged.docx";

                AddLog("USER", "Word export işlemi başlatıldı.");

                if (dialog.ShowDialog() == true)
                {
                    var sections = FileMergeService.BuildSections(lstFiles.Items.Cast<string>().ToList());
                    WordExporter.Export(dialog.FileName, sections);

                    AddLog("EXPORT", $"Word başarıyla oluşturuldu: {dialog.FileName}");
                }
            }
            catch (Exception ex)
            {
                AddLog("ERROR", ex.Message);
            }
        }

        // ---------------------------------
        // THEME
        // ---------------------------------
        private void btnToggleTheme_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ToggleTheme();
            AddLog("SYSTEM", "Tema değiştirildi.");
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }


    // -----------------------------------------
    // LOG MESSAGE CLASS
    // -----------------------------------------
    public class LogMessage
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
