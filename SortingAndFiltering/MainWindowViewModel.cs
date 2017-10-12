using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace SortingAndFiltering
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string name;
        private string surname;
        private string middlename;
        private int age;
        private List<Person> persons;
        private ObservableCollection<Person> observablePersons;
        private string searchText;
        private Person selectedPerson;

        private ObservableCollection<string> attributesPerson;
        private string selectedAttribute;
        private CollectionViewSource dataPersons;

        private string sortColumn;
        private ListSortDirection sortDirection;

        public string Name { get { return name; } set { name = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
        public string Middlename { get { return middlename; } set { middlename = value; } }
        public int Age { get { return age; } set { age = value; } }
        public List<Person> Persons { get { return persons; } set { persons = value; } }
        public ObservableCollection<Person> ObservablePersons { get { return observablePersons; }
            set
            {
                if (observablePersons != value)
                {
                    observablePersons = value;
                    dataPersons = new CollectionViewSource
                    {
                        Source = observablePersons
                    };
                }
                RaisePropertyChanged();
            }
        }
        public string SearchText { get { return searchText; } set { searchText = value; RealSeach.RaiseCanExecuteChanged(); } }
        public Person SelectedPerson { get { return selectedPerson; } set { selectedPerson = value; RemovePerson.RaiseCanExecuteChanged(); } }

        public ObservableCollection<string> AttributesPerson { get { return attributesPerson; } set { attributesPerson = value; } }
        public string SelectedAttribute { get { return selectedAttribute; } set { selectedAttribute = value; } }

        public ListCollectionView DataPersons { get { return (ListCollectionView)dataPersons.View; } }

        public RelayCommand AddPerson { get; private set; }
        public RelayCommand RemovePerson { get; private set; }
        public RelayCommand RealSeach { get; private set; }
        public CustomRelayCommand SortingList { get; private set; }

        public MainWindowViewModel()
        {
            Persons = DataManager.Load<List<Person>>("Persons.xml");
            AttributesPerson = new ObservableCollection<string>() { "Имя", "Фамилия", "Отчество", "Возраст" };
            ObservablePersons = new ObservableCollection<Person>();
            UpdateCollection();
            AddPerson = new RelayCommand(AddPerson_Execute);
            RemovePerson = new RelayCommand(RemovePerson_Execute);
            RealSeach = new RelayCommand(RealSeach_Execute);
            SortingList = new CustomRelayCommand(SortingList_Execute);
        }

        private void AddPerson_Execute()
        {
            Persons.Add(new Person(Name, Surname, Middlename, Age));
            DataManager.Save(Persons, "Persons.xml");
            UpdateCollection();
        }

        private void RemovePerson_Execute()
        {
            foreach (var item in Persons)
            {
                if (item == SelectedPerson)
                {
                    Persons.Remove(item);
                    UpdateCollection();
                    break;
                }
            }
            DataManager.Save(Persons, "Persons.xml");
            UpdateCollection();
        }

        private void UpdateCollection()
        {
            ObservablePersons.Clear();
            foreach (var item in Persons)
            {
                ObservablePersons.Add(item);
            }
        }

        private void RealSeach_Execute()
        {
            if (SearchText != "" && SearchText != null && SelectedAttribute != "")
            {
                ObservablePersons.Clear();
                switch (SelectedAttribute)
                {
                    case "Имя":
                        foreach (var item in Persons.Where(x => x.Name.StartsWith(SearchText)))
                        {
                            ObservablePersons.Add(item);
                        }
                        RaisePropertyChanged("ObservablePersons");
                        break;
                    case "Фамилия":
                        foreach (var item in Persons.Where(x => x.Surname.StartsWith(SearchText)))
                        {
                            ObservablePersons.Add(item);
                        }
                        RaisePropertyChanged("ObservablePersons");
                        break;
                    case "Отчество":
                        foreach (var item in Persons.Where(x => x.Middlename.StartsWith(SearchText)))
                        {
                            ObservablePersons.Add(item);
                        }
                        RaisePropertyChanged("ObservablePersons");
                        break;
                    case "Возраст":
                        foreach (var item in Persons.Where(x => x.Age == Convert.ToInt32(SearchText)))
                        {
                            ObservablePersons.Add(item);
                        }
                        RaisePropertyChanged("ObservablePersons");
                        break;
                    default:
                        break;
                }
                
            }
            else
            {
                UpdateCollection();
            }
        }

        private void SortingList_Execute(object parameter)
        {
            string column = parameter as string;
            if (sortColumn == column)
            {
                sortDirection = sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            }
            else
            {
                sortColumn = column;
                sortDirection = ListSortDirection.Ascending;
            }
            dataPersons.SortDescriptions.Clear();
            dataPersons.SortDescriptions.Add(new SortDescription(sortColumn, sortDirection));
        }
    }
}
