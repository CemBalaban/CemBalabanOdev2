using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CemBalabanOdev2.Model;
using Newtonsoft.Json;
using System.IO;

namespace CemBalabanOdev2;

public partial class ToDoList : ContentPage
{
    private const string TodosKey = "Todos";
    public ToDoList()
    {
        InitializeComponent();
        LoadTodosFromPreferences();
        ctrlTodoList.ItemsSource = ListTodo;
    }

    public ObservableCollection<ToDo> ListTodo { get; set; } =
        new ObservableCollection<ToDo>
        {

        };

    private async void AddTodo_Click(object sender, EventArgs e)
    {
        var res = await DisplayPromptAsync("Görev Ekle", "Yapılacak:", "OK", placeholder: "Yapılacakları buraya yaz");

        if (res != "")
        {
            ToDo todo = new ToDo() { Image = "dotnet_bot.png", Text = res, IsDone = false };
            ListTodo.Add(todo);
            SaveTodosToPreferences();
        }
    }

    private async void DeleteTodo_Click(object sender, EventArgs e)
    {
        var todo = ListTodo.First(o => o.ID == ((MenuItem)sender).CommandParameter.ToString());

        var res = await DisplayAlert("Silmeyi onayla", "Silinsin mi?", "Evet", "Hayır");

        if (res == true)
        {
            ListTodo.Remove(todo);
            SaveTodosToPreferences();
        }

    }

    private async void EditTodo_Click(object sender, EventArgs e)
    {
        var todo = ListTodo.First(o => o.ID == ((MenuItem)sender).CommandParameter.ToString());

        if (todo != null)
        {
            var res = await DisplayPromptAsync("Düzenle", "Yapılacak:", initialValue: todo.Text, placeholder: "yapılacakları buraya yaz");

            if (res != "")
            {
                todo.Text = res;
                SaveTodosToPreferences();
            }
        }
    }

    private async void ImageTodo_Click(object sender, EventArgs e)
    {
        var todo = ListTodo.First(o => o.ID == ((MenuItem)sender).CommandParameter.ToString());
        var res = await DisplayActionSheet("Resim Seç", null, null, "Galeri", "Kamera");
        if (res == "Galeri")
        {

            var resim = await MediaPicker.Default.PickPhotoAsync();
            if (resim != null)
            {
                todo.Image = resim.FullPath;
                SaveTodosToPreferences();
            }

        }
        else if (res == "Kamera")
        {
            var resim = await MediaPicker.Default.CapturePhotoAsync();
            if (resim != null)
            {
                todo.Image = resim.FullPath;
                SaveTodosToPreferences();
            }
        }


    }
    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkBox = sender as CheckBox;
        var todo = checkBox?.BindingContext as ToDo;

        if (todo != null)
        {
            todo.IsDone = e.Value;
            SaveTodosToPreferences();
        }
    }
    private void SaveTodosToPreferences()
    {
        var json = JsonConvert.SerializeObject(ListTodo);
        Preferences.Set(TodosKey, json);
    }
    private void LoadTodosFromPreferences()
    {
        if (Preferences.ContainsKey(TodosKey))
        {
            var json = Preferences.Get(TodosKey, string.Empty);
            var todos = JsonConvert.DeserializeObject<ObservableCollection<ToDo>>(json);
            if (todos != null)
            {
                foreach (var todo in todos)
                {
                    ListTodo.Add(todo);
                }
            }
        }
    }
}