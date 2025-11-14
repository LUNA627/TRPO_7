using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Text.Json;

namespace prack6_TRPO
{
   public class MainClass : INotifyPropertyChanged
    {
        private readonly DataWork _dataWork = new();


        public Doctor RegDoctor { get; set; } = new Doctor();
        public Doctor LogDoctor { get; set; } = new Doctor();

        private Doctor? _showInfoDoctor;
        public Doctor ShowInfoDoctor
        {
            get => _showInfoDoctor;
            set
            {
                if (_showInfoDoctor != value)
                {
                    _showInfoDoctor = value;
                    OnPropertyChanged();
                }
            }
        }


        private int? _currentDoctorId = null;
        public int? CurrentDoctorId
        {
            get => _currentDoctorId;
            set
            {
                if (_currentDoctorId != value)
                {
                    _currentDoctorId = value;
                    OnPropertyChanged();
                }
            }
        }

        public Patient NewPatient { get; set; } = new Patient();

        public DateTime? BirthdayDate
        {
            get
            {
                if (DateTime.TryParseExact(NewPatient.BirthdayPatient, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                    return date;
                return null;
            }
            set
            {
                NewPatient.BirthdayPatient = value?.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) ?? "";
                OnPropertyChanged();
                OnPropertyChanged(nameof(NewPatient.BirthdayPatient));
            }
        }

        
        public DateTime? LastAppointmentDate
        {
            get
            {
                if (DateTime.TryParseExact(NewPatient.LastAppointmentPatient, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                    return date;
                return null;
            }
            set
            {
                NewPatient.LastAppointmentPatient = value?.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) ?? "";
                OnPropertyChanged();
                OnPropertyChanged(nameof(NewPatient.LastAppointmentPatient));
            }
        }




        private int _searchPatientId;
        public int SearchPatientId
        {
            get => _searchPatientId;
            set { _searchPatientId = value; OnPropertyChanged(); }
        }


        private int _searchPatientIdForChange;
        public int SearchPatientIdForChange
        {
            get => _searchPatientIdForChange;
            set { _searchPatientIdForChange = value; OnPropertyChanged(); }
        }




        private Patient _infoPatient;
        public Patient InfoPatient
        {
            get => _infoPatient;
            set
            {
                _infoPatient = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LastDoctorFullName));
            }
        }

        public void ShowInfoPatient()
        {
            if (!_currentDoctorId.HasValue)
            {
                throw new ArgumentException("Войте в профиль доктора");
            }

            if (SearchPatientId <= 0)
           {
                throw new ArgumentException("Введите корректный id");
           }

            InfoPatient = _dataWork.LoadPatientId(SearchPatientId);
            if (InfoPatient == null)
            {
                throw new ArgumentException("Пациент не найден", "Ошибка");
            }

        }

        public int CountDoctor
        {
            get => _dataWork.CountFileDoctor();
            set
            {
               
            }
        }

        public int CountPatiet
        {
            get => _dataWork.CountFilePatient();
            set
            {
              
            }
        }

        public int CountTotal
        {
            get => CountDoctor + CountPatiet;
            set
            {
                
            }
        }


        public int SummUser;

        public string LastDoctorFullName
        {
            get
            {
                if (InfoPatient?.LastDoctorId > 0)
                {
                    Doctor? doctor = _dataWork.GetDoctorById(InfoPatient.LastDoctorId);
                    if (doctor != null)
                    {
                        return $"{doctor.LastName} {doctor.Name} {doctor.MiddleName}";
                    }
                    
                }
                return " ";
            }
            set
            {
                OnPropertyChanged();
            }
        }


        private Patient _editPatient;
        public Patient EditPatient
        {
            get => _editPatient;
            set
            {
                _editPatient = value;
                OnPropertyChanged();
            }
        }

        private Patient _copyOrgininalPatient;
       


        public void SeatchPatinetForChang()
        {

            if (!_currentDoctorId.HasValue)
            {
                throw new ArgumentException("Войте в профиль доктора");
            }

            if (_searchPatientIdForChange <= 0)
            {
                throw new ArgumentException("Введие корректный id");
            }

            Patient patient = _dataWork.LoadPatientId(_searchPatientIdForChange);

            if (patient == null)
            {
                throw new ArgumentException("Пациент не найден", "Ошибка");
            }

            _copyOrgininalPatient = JsonSerializer.Deserialize<Patient>(JsonSerializer.Serialize(patient));
            EditPatient = patient;

            BirthdayDate = DateTime.TryParseExact(
                patient.BirthdayPatient, "dd.MM.yyyy", null, DateTimeStyles.None, out DateTime b) ? b : (DateTime?)null;

            LastAppointmentDate = DateTime.TryParseExact(
                patient.LastAppointmentPatient, "dd.MM.yyyy", null, DateTimeStyles.None, out DateTime l) ? l : (DateTime?)null;

        }

        public void ResetInfoPatient()
        {
            if (!_currentDoctorId.HasValue)
            {
                throw new ArgumentException("Войте в профиль доктора");
            }
            if (_copyOrgininalPatient == null)
            {
                return;
            }

            EditPatient = JsonSerializer.Deserialize<Patient>(JsonSerializer.Serialize(_copyOrgininalPatient));
        }


        public void SaveChangeInfoPatient()
        {
            if (!_currentDoctorId.HasValue)
            {
                throw new ArgumentException("Войте в профиль доктора");
            }


            if (EditPatient == null)
            {
                throw new ArgumentException("Нет пациента для сохранения");
            }
            _dataWork.SavePatientNoId(EditPatient);
        }

        public void ClearFieldDoctor()
        {
            RegDoctor.Name = " ";
            RegDoctor.LastName = " ";
            RegDoctor.MiddleName = " ";
            RegDoctor.Specialisation = " ";
            RegDoctor.Password = " ";
            RegDoctor.RepeatPassword = " ";
        }

        public void RegistrDoctor()
        {
            if (string.IsNullOrWhiteSpace(RegDoctor.Name))
            {
                throw new ArgumentException("Введите фамилию");
            }
            if (string.IsNullOrWhiteSpace(RegDoctor.LastName))
            {
                throw new ArgumentException("Введите имя");
            }
            if (string.IsNullOrWhiteSpace(RegDoctor.MiddleName))
            {
                throw new ArgumentException("Введите отчество");
            }
            if (string.IsNullOrWhiteSpace(RegDoctor.Specialisation))
            {
                throw new ArgumentException("Введите специализацию");
            }
            if (string.IsNullOrWhiteSpace(RegDoctor.Password))
            {
                throw new ArgumentException("Введите пароль");
            }
            if (string.IsNullOrWhiteSpace(RegDoctor.RepeatPassword))
            {
                throw new ArgumentException("Введите подтвердение пароля");
            }
            if (RegDoctor.Password != RegDoctor.RepeatPassword)
            {
                throw new ArgumentException("Пароли должны совпадать");
            }
            
            _dataWork.SaveDoctor(RegDoctor);
            MessageBox.Show($"ID: {RegDoctor.Id}", "Регистрация успешна!");
            OnPropertyChanged(nameof(CountDoctor));
            ClearFieldDoctor();

        }


        public void LoginDoctor()
        {
            if (_dataWork.CheckDoctorLogin(LogDoctor.Id, LogDoctor.Password))
            {
                CurrentDoctorId = LogDoctor.Id;
                NewPatient.LastDoctorId = LogDoctor.Id;
                ShowInfoDoctor = _dataWork.GetDoctorById(LogDoctor.Id);

                LogDoctor.Id = 0;
                LogDoctor.Password = " ";
            }
            else
            {
                throw new ArgumentException("Неверный логин или пароль");
            }
        }





        public void ClearFieldPatient()
        {
            NewPatient.NamePatient = " ";
            NewPatient.LastNamePatient = " ";
            NewPatient.MiddleNamePatient = " ";
            NewPatient.DiagnosisPatient = " ";
            NewPatient.RecomendationsPatient = " ";
        }


        public void SaveNewPatinet()
        {
            if (!_currentDoctorId.HasValue)
            {
                throw new ArgumentException("Войте в профиль доктора");
            }

            if (string.IsNullOrWhiteSpace(NewPatient.NamePatient))
            {
                throw new ArgumentException("Введите фамилию");
            }
            if (string.IsNullOrWhiteSpace(NewPatient.LastNamePatient))
            {
                throw new ArgumentException("Введите имя");
            }
            if (string.IsNullOrWhiteSpace(NewPatient.MiddleNamePatient))
            {
                throw new ArgumentException("Введите отчество");
            }
            if (string.IsNullOrWhiteSpace(NewPatient.DiagnosisPatient))
            {
                throw new ArgumentException("Введите диагноз");
            }
            if (string.IsNullOrWhiteSpace(NewPatient.RecomendationsPatient))
            {
                throw new ArgumentException("Введите лечение");
            }

            

            _dataWork.SavePatient(NewPatient);
            MessageBox.Show($"ID: {NewPatient.IdPatient}", "Регистрация успешна!");
            OnPropertyChanged(nameof(CountPatiet));
            ClearFieldPatient();
        }











        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}

