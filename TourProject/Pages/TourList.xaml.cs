﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourProject.Classes;

namespace TourProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для TourList.xaml
    /// </summary>
    public partial class TourList : Page
    {
        List<Tour> TourFilter = new List<Tour>();
        public TourList()
        {
            InitializeComponent();
            BaseClass.tBE = new Entities1();
            TourFilter = BaseClass.tBE.Tour.ToList();

            listTour.ItemsSource = BaseClass.tBE.Tour.ToList();
            List<Type> Tp = BaseClass.tBE.Type.ToList();

            cmbTourType.Items.Add("Все типы");
            for (int i = 0; i < Tp.Count; i++)
            {
                cmbTourType.Items.Add(Tp[i].Name_Type);
            }

            cmbTourType.SelectedIndex = 0;
            //cbActualTour.IsChecked; //индекс для галочки актуальных туров

            //тут можно добавить счетчик для количества билетов

        }

        void Filter()  // метод для одновременной фильтрации, поиска и сортировки
        {
            List<Tour> typeList = new List<Tour>();  // пустой список, который далее будет заполнять элементами, удавлетворяющими условиям фильтрации, поиска и сортировки

            string tour = cmbTourType.SelectedValue.ToString();  // выбранное пользователем название тура
            int index = cmbTourType.SelectedIndex;

            //требуется починить эту часть для хоть какой-то работы фильтра
            // поиск значений, удовлетворяющих условия фильтра
            if (index != 0)
            {
                //typeList = BaseClass.tBE.Type.Where(x => x.Type.Name_Type == tour).ToList();
            }
            //if (index == 0) //пока будет заглушкой
            //{ 

            //}
            else  // если выбран пункт "Все типы", то сбрасываем фильтрацию:
            {
                typeList = BaseClass.tBE.Tour.ToList();
            }


            // поиск совпадений по названиям туров
            if (!string.IsNullOrWhiteSpace(tbSearch.Text))  // если строка не пустая и если она не состоит из пробелов
            {
                typeList = typeList.Where(x => x.Name_Tour.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }


            // выбор только актуальных туров
            if (cbActualTour.IsChecked == true)
            {
                typeList = typeList.Where(x => x.IsActual = true).ToList();
            }

            listTour.ItemsSource = typeList;
            if (typeList.Count == 0)
            {
                MessageBox.Show("нет записей");
            }
            //listTour.Text = "Количество записей " + typeList.Count;
        }

        
        private void cmbTourType_SelectionChanged(object sender, SelectionChangedEventArgs e) //Фильтрация по типу тура
        {
            Filter();
        }
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }
        private void cbActualTour_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }
    }
}
