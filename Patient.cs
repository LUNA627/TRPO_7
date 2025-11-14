using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace prack6_TRPO
{
    public class Patient
    {
        public int IdPatient { get; set; }
        public string NamePatient { get; set; } = "";
        public string LastNamePatient { get; set; } = "";
        public string MiddleNamePatient { get; set; } = "";
        public string BirthdayPatient { get; set; } = "";   
        public string LastAppointmentPatient { get; set; } = ""; 
        public int LastDoctorId { get; set; }             
        public string DiagnosisPatient { get; set; } = "";
        public string RecomendationsPatient { get; set; } = "";



    }
}
