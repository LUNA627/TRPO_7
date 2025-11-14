using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace prack6_TRPO
{
   public class DataWork
    {
        private readonly Dictionary<int, Doctor> _doctors = [];

        private readonly Random rand = new();
        private HashSet<int> _patientId = new();

        public DataWork() 
        {
            LoadAllDoctors();
        }

        private void LoadAllDoctors()
        {
            _doctors.Clear();

            string[] filesDoctor = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,"D_*.json");

            foreach (string file in filesDoctor)
            {
                string jsonFileRead = File.ReadAllText(file);
                Doctor? doctor = JsonSerializer.Deserialize<Doctor>(jsonFileRead);
                
                if (doctor != null)
                {
                    _doctors[doctor.Id] = doctor;
                }
            }
        }


        public void SaveDoctor(Doctor doctor)
        {

            doctor.Id = GenDoctorId();

            string filePath = $"D_{doctor.Id}.json";
            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string jsonFileWrite = JsonSerializer.Serialize(doctor, options);
            File.WriteAllText(filePath, jsonFileWrite);
            _doctors[doctor.Id] = doctor;

            LoadAllDoctors();
        }


        public bool CheckDoctorLogin(int id, string password)
        {
            if (_doctors.TryGetValue(id, out Doctor? doctor))
            {
                if (doctor.Password == password)
                {
                    return true;
                }
            }
            return false;
        }


        public Doctor? GetDoctorById(int id)
        {
            return _doctors.GetValueOrDefault(id);
        }



        public int GenDoctorId()
        {
            int min = 10000;
            int max = 100000;

            int idDoctor;
            do
            {
                idDoctor = rand.Next(min, max);
            }
            while (_doctors.ContainsKey(idDoctor));

            return idDoctor;
        }



        public void SavePatient(Patient patient)
        {
            patient.IdPatient = GenPatientId();

            string filePath = $"P_{patient.IdPatient}.json";
            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string jsonFileWriter = JsonSerializer.Serialize(patient, options);
            File.WriteAllText(filePath, jsonFileWriter);
        }



        public void SavePatientNoId(Patient patient)
        {

            string filePath = $"P_{patient.IdPatient}.json";
            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string jsonFileWriter = JsonSerializer.Serialize(patient, options);
            File.WriteAllText(filePath, jsonFileWriter);
        }





        public Patient LoadPatientId(int idPatient)
        {
            string filePath = $"P_{idPatient}.json";

            string jsonFileRead = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Patient>(jsonFileRead);
           
        }



        public int GenPatientId()
        {
            int min = 1000000;
            int max = 10000000;

            int idPatient;
            do
            {
                idPatient = rand.Next(min, max);
            }
            while (_patientId.Contains(idPatient));

            return idPatient;
        }


        

        public int CountFileDoctor()
        {
            string[] filePath = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, $"D_*.json");
            return filePath.Length;
        }

        public int CountFilePatient()
        {
            string[] filePath = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "P_*.json");
            return filePath.Length;
        }
    }
}
